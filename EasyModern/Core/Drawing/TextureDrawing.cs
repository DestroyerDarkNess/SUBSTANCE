using SharpDX;
using SharpDX.Direct3D9;
using System;
using System.Runtime.InteropServices;
using Rectangle = System.Drawing.Rectangle;

namespace EasyModern.Core.Drawing
{
    /// <summary>
    /// Clase encargada de dibujar una textura en la pantalla
    /// (o en tu render target actual) usando Direct3D9.
    /// </summary>
    public class TextureDrawing : IDisposable
    {
        private readonly Device _device;
        private VertexDeclaration _quadDecl;

        public TextureDrawing(Device device)
        {
            _device = device ?? throw new ArgumentNullException(nameof(device));
            CreateVertexDeclaration();
        }

        /// <summary>
        /// Inicializa la VertexDeclaration para dibujar quads con (x,y,z,rhw,color,uv).
        /// </summary>
        private void CreateVertexDeclaration()
        {
            _quadDecl?.Dispose();

            _quadDecl = new VertexDeclaration(_device, new[]
            {
                new VertexElement(0, 0,   DeclarationType.Float4, DeclarationMethod.Default,
                                  DeclarationUsage.Position, 0),
                new VertexElement(0, 16,  DeclarationType.Float2, DeclarationMethod.Default,
                                  DeclarationUsage.TextureCoordinate, 0),
                VertexElement.VertexDeclarationEnd
            });
        }

        /// <summary>
        /// Dibuja la textura dada en la región <paramref name="destRect"/> (coordenadas de pantalla),
        /// con opacidad <paramref name="alpha"/>.
        /// </summary>
        /// <param name="texture">Textura de Direct3D9 que ya tengas creada/cargada.</param>
        /// <param name="destRect">Rectángulo en píxeles (coordenadas de pantalla) donde dibujar.</param>
        /// <param name="alpha">Opacidad (1.0f = opaco, 0.0f = invisible).</param>
        public void DrawTexture(SharpDX.Direct3D9.Texture texture, Rectangle destRect, float alpha = 1.0f)
        {
            try
            {

                if (texture == null)
                    return;

                // Habilitar blending si queremos aplicar opacidad
                _device.SetRenderState(RenderState.AlphaBlendEnable, true);
                _device.SetRenderState(RenderState.SourceBlend, Blend.SourceAlpha);
                _device.SetRenderState(RenderState.DestinationBlend, Blend.InverseSourceAlpha);

                // Configurar el pipeline para dibujar quads con PositionRhw + Diffuse + TexCoord
                _device.VertexDeclaration = null; // Para usar FVF
                _device.VertexFormat = VertexFormat.PositionRhw | VertexFormat.Diffuse | VertexFormat.Texture1;

                _device.SetTexture(0, texture);

                // Convertir alpha float -> byte
                byte alphaByte = (byte)(alpha * 255);

                // Esquinas del rectángulo
                float x1 = destRect.Left;
                float y1 = destRect.Top;
                float x2 = destRect.Right;
                float y2 = destRect.Bottom;

                // Creamos el quad en coordenadas de pixel (PositionRhw)
                var quad = new[]
                {
                new VertexRHW {
                    Pos = new Vector4(x1, y2, 0, 1),
                    Color = new ColorBGRA(255, 255, 255, alphaByte).ToBgra(),
                    UV = new Vector2(0, 1)
                },
                new VertexRHW {
                    Pos = new Vector4(x1, y1, 0, 1),
                    Color = new ColorBGRA(255, 255, 255, alphaByte).ToBgra(),
                    UV = new Vector2(0, 0)
                },
                new VertexRHW {
                    Pos = new Vector4(x2, y2, 0, 1),
                    Color = new ColorBGRA(255, 255, 255, alphaByte).ToBgra(),
                    UV = new Vector2(1, 1)
                },
                new VertexRHW {
                    Pos = new Vector4(x2, y1, 0, 1),
                    Color = new ColorBGRA(255, 255, 255, alphaByte).ToBgra(),
                    UV = new Vector2(1, 0)
                },
            };

                // Dibujamos el quad con TriangleStrip
                _device.DrawUserPrimitives(PrimitiveType.TriangleStrip, 2, quad);

                _device.SetRenderState(RenderState.AlphaBlendEnable, true);
                _device.SetRenderState(RenderState.SourceBlend, Blend.SourceAlpha);
                _device.SetRenderState(RenderState.DestinationBlend, Blend.InverseSourceAlpha);


            }
            catch { }

        }

        public void Dispose()
        {
            _quadDecl?.Dispose();
        }

        /// <summary>
        /// Estructura interna para vértices con PositionRhw + Diffuse + UV.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct VertexRHW
        {
            public Vector4 Pos;   // x,y,z,rhw
            public int Color;     // Diffuse (ARGB)
            public Vector2 UV;    // U,V
        }
    }
}
