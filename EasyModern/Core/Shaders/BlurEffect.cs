using SharpDX;
using SharpDX.Direct3D9;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Rectangle = System.Drawing.Rectangle;

namespace EasyModern.Core.Effects
{
    public class BlurEffect : IDisposable
    {
        private readonly Device _device;

        // Pixel Shaders para blur
        private PixelShader _psBlurH;
        private PixelShader _psBlurV;

        // Para dibujar quads full-screen
        private VertexDeclaration _quadDecl;

        public BlurEffect(Device device)
        {
            _device = device;
        }

        /// <summary>
        /// Llamar una sola vez para compilar shaders y crear la VertexDeclaration.
        /// </summary>
        public void Initialize()
        {
            CreateShaders();

            // Crear VertexDeclaration para un quad con (pos, uv)
            _quadDecl?.Dispose();
            _quadDecl = new VertexDeclaration(_device, new[]
            {
                new VertexElement(0, 0, DeclarationType.Float4, DeclarationMethod.Default, DeclarationUsage.Position, 0),
                new VertexElement(0, 16, DeclarationType.Float2, DeclarationMethod.Default, DeclarationUsage.TextureCoordinate, 0),
                VertexElement.VertexDeclarationEnd
            });
        }

        private void CreateShaders()
        {
            // Sencillo blur gaussiano 2-pass (horizontal+vertical)
            string blurHLSL = @"
sampler s0 : register(s0);
float2 texelSize; // (1/width, 1/height)

// Pesos gauss aproximados para 5 taps
static const float weight[5] = {
    0.227027027, 0.194594595, 0.121621622, 0.054054054, 0.016216216
};

// Blur horizontal
float4 PS_BlurHorizontal(float2 texCoords : TEXCOORD0) : COLOR
{
    float4 color = tex2D(s0, texCoords) * weight[0];
    for(int i = 1; i < 5; i++)
    {
        color += tex2D(s0, texCoords + float2(texelSize.x*i, 0)) * weight[i];
        color += tex2D(s0, texCoords - float2(texelSize.x*i, 0)) * weight[i];
    }
    return color;
}

// Blur vertical
float4 PS_BlurVertical(float2 texCoords : TEXCOORD0) : COLOR
{
    float4 color = tex2D(s0, texCoords) * weight[0];
    for(int i = 1; i < 5; i++)
    {
        color += tex2D(s0, texCoords + float2(0, texelSize.y*i)) * weight[i];
        color += tex2D(s0, texCoords - float2(0, texelSize.y*i)) * weight[i];
    }
    return color;
}";
            // Compilar shader horizontal
            using (var psHByteCode = ShaderBytecode.Compile(blurHLSL, "PS_BlurHorizontal", "ps_2_0", ShaderFlags.None))
            {
                _psBlurH?.Dispose();
                _psBlurH = new PixelShader(_device, psHByteCode);
            }
            // Compilar shader vertical
            using (var psVByteCode = ShaderBytecode.Compile(blurHLSL, "PS_BlurVertical", "ps_2_0", ShaderFlags.None))
            {
                _psBlurV?.Dispose();
                _psBlurV = new PixelShader(_device, psVByteCode);
            }
        }

        /// <summary>
        /// 1) Toma un Bitmap (capturado, por ejemplo, con GameCapture),
        /// 2) Lo sube a una textura en SystemMem,
        /// 3) Copia a una textura RenderTarget en Pool.Default,
        /// 4) Aplica 2 pasadas de blur,
        /// 5) Dibuja la textura final en <paramref name="destRect"/> (coordenadas de pantalla)
        ///    con transparencia <paramref name="alpha"/>.
        /// 
        /// Useful para “poner el fondo blur” en tu overlay.
        /// </summary>
        public void BlurBitmapAndDraw(Bitmap bmp, Rectangle destRect, float alpha = 1.0f)
        {
            if (bmp == null) return;

            // Tamaño del bitmap
            int w = bmp.Width;
            int h = bmp.Height;
            if (w <= 0 || h <= 0) return;

            // 1) Crear texturas:
            //   a) sysMemTex -> lockeable (SystemMem)
            //   b) rtScene   -> RenderTarget en Pool.Default
            //   c) rtTemp    -> RenderTarget en Pool.Default (aux blur pass)
            using (var sysMemTex = new SharpDX.Direct3D9.Texture(_device, w, h, 1, 0, Format.A8R8G8B8, Pool.SystemMemory))
            using (var rtScene = new SharpDX.Direct3D9.Texture(_device, w, h, 1, Usage.RenderTarget, Format.A8R8G8B8, Pool.Default))
            using (var rtTemp = new SharpDX.Direct3D9.Texture(_device, w, h, 1, Usage.RenderTarget, Format.A8R8G8B8, Pool.Default))
            {
                // 2) Subir el bitmap a sysMemTex
                UploadBitmapToSysMemTexture(bmp, sysMemTex);

                // 3) Copiar sysMemTex => rtScene
                _device.UpdateTexture(sysMemTex, rtScene);

                // 4) 2 pasadas de blur
                BlurPass(rtScene, rtTemp, w, h, _psBlurH);
                BlurPass(rtTemp, rtScene, w, h, _psBlurV);

                // 5) Dibujar la textura final en destRect
                _device.BeginScene();
                {
                    DrawTextureAtRect(rtScene, destRect, alpha);
                }
                _device.EndScene();
            }
        }

        #region Upload Bitmap => Texture (SystemMem)
        private void UploadBitmapToSysMemTexture(Bitmap bmp, SharpDX.Direct3D9.Texture sysMemTex)
        {
            var rect = sysMemTex.LockRectangle(0, LockFlags.None);
            int pitch = rect.Pitch;

            var bmpData = bmp.LockBits(
                new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* destPtr = (byte*)rect.DataPointer;
                byte* srcPtr = (byte*)bmpData.Scan0;

                for (int y = 0; y < bmp.Height; y++)
                {
                    byte* destLine = destPtr + y * pitch;
                    byte* srcLine = srcPtr + y * bmpData.Stride;

                    for (int x = 0; x < bmp.Width; x++)
                    {
                        // B,G,R
                        byte B = srcLine[x * 3 + 0];
                        byte G = srcLine[x * 3 + 1];
                        byte R = srcLine[x * 3 + 2];

                        // Convertimos a A8R8G8B8 => BGRA
                        destLine[x * 4 + 0] = B;
                        destLine[x * 4 + 1] = G;
                        destLine[x * 4 + 2] = R;
                        destLine[x * 4 + 3] = 255;
                    }
                }
            }

            bmp.UnlockBits(bmpData);
            sysMemTex.UnlockRectangle(0);
        }
        #endregion

        #region Blur Pass
        private void BlurPass(SharpDX.Direct3D9.Texture src, SharpDX.Direct3D9.Texture dst, int w, int h, PixelShader ps)
        {
            var oldRT = _device.GetRenderTarget(0);
            var dstSurf = dst.GetSurfaceLevel(0);

            // Asignamos RT = dst
            _device.SetRenderTarget(0, dstSurf);
            _device.Clear(ClearFlags.Target, new ColorBGRA(0, 0, 0, 255), 1.0f, 0);

            _device.VertexShader = null;
            _device.PixelShader = ps;
            _device.SetTexture(0, src);

            // Seteamos la constante (1/width, 1/height)
            float invW = 1.0f / w;
            float invH = 1.0f / h;
            var texelSize = new float[] { invW, invH, 0, 0 };
            _device.SetPixelShaderConstant(0, texelSize);

            _device.BeginScene();
            {
                DrawFullscreenQuad(w, h);
            }
            _device.EndScene();

            // Restaurar
            _device.PixelShader = null;
            _device.SetTexture(0, null);

            _device.SetRenderTarget(0, oldRT);
            dstSurf.Dispose();
        }
        #endregion

        #region Dibujar Textura en pantalla
        /// <summary>
        /// Dibuja la textura (de tamaño w,h) en la región <paramref name="destRect"/> 
        /// (coordenadas de pantalla), con opacidad <paramref name="alpha"/>.
        /// </summary>
        private void DrawTextureAtRect(SharpDX.Direct3D9.Texture texture, Rectangle destRect, float alpha)
        {
            // Habilitar blending
            _device.SetRenderState(RenderState.AlphaBlendEnable, true);
            _device.SetRenderState(RenderState.SourceBlend, Blend.SourceAlpha);
            _device.SetRenderState(RenderState.DestinationBlend, Blend.InverseSourceAlpha);

            // FVF => PositionRhw | Diffuse | Tex1
            _device.VertexDeclaration = null;
            _device.VertexFormat = VertexFormat.PositionRhw | VertexFormat.Diffuse | VertexFormat.Texture1;

            _device.SetTexture(0, texture);

            byte alphaByte = (byte)(alpha * 255);

            float x1 = destRect.Left;
            float y1 = destRect.Top;
            float x2 = destRect.Right;
            float y2 = destRect.Bottom;

            var quad = new[]
            {
                new VertexRHW {
                    Pos=new Vector4(x1, y2, 0, 1),
                    Color=new ColorBGRA(255,255,255,alphaByte).ToBgra(),
                    UV=new Vector2(0,1)},
                new VertexRHW {
                    Pos=new Vector4(x1, y1, 0, 1),
                    Color=new ColorBGRA(255,255,255,alphaByte).ToBgra(),
                    UV=new Vector2(0,0)},
                new VertexRHW {
                    Pos=new Vector4(x2, y2, 0, 1),
                    Color=new ColorBGRA(255,255,255,alphaByte).ToBgra(),
                    UV=new Vector2(1,1)},
                new VertexRHW {
                    Pos=new Vector4(x2, y1, 0, 1),
                    Color=new ColorBGRA(255,255,255,alphaByte).ToBgra(),
                    UV=new Vector2(1,0)},
            };

            _device.DrawUserPrimitives(PrimitiveType.TriangleStrip, 2, quad);

            // Apagar blending
            _device.SetRenderState(RenderState.AlphaBlendEnable, false);
        }

        /// <summary>
        /// Llenar el RT actual con un quad fullscreen en [-1..1].
        /// asumiendo que el RT es del tamaño w x h.
        /// </summary>
        private void DrawFullscreenQuad(int w, int h)
        {
            _device.VertexDeclaration = _quadDecl;
            _device.SetRenderState(RenderState.ZEnable, false);
            _device.SetRenderState(RenderState.AlphaBlendEnable, false);

            var quad = new[]
            {
                new QuadVertex { Pos=new Vector4(-1,-1, 0,1), UV=new Vector2(0,1) },
                new QuadVertex { Pos=new Vector4(-1, 1, 0,1), UV=new Vector2(0,0) },
                new QuadVertex { Pos=new Vector4( 1,-1, 0,1), UV=new Vector2(1,1) },
                new QuadVertex { Pos=new Vector4( 1, 1, 0,1), UV=new Vector2(1,0) },
            };

            _device.DrawUserPrimitives(PrimitiveType.TriangleStrip, 2, quad);
        }
        #endregion

        #region Structs
        [StructLayout(LayoutKind.Sequential)]
        private struct QuadVertex
        {
            public Vector4 Pos;
            public Vector2 UV;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct VertexRHW
        {
            public Vector4 Pos;   // x,y,z,rhw
            public int Color;     // Diffuse
            public Vector2 UV;
        }
        #endregion

        public void Dispose()
        {
            _psBlurH?.Dispose();
            _psBlurV?.Dispose();
            _quadDecl?.Dispose();
        }
    }
}
