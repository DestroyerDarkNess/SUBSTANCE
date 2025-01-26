using SharpDX.Direct3D9;
using System.Drawing;
using System.Drawing.Imaging;

namespace EasyModern.Core.Utils
{
    public static class TextureHelper
    {
        /// <summary>
        /// Crea una textura Direct3D9 en Pool.Default a partir de un Bitmap.
        /// Esto te permite usar Core.Instances.TextureDrawer.DrawTexture(...)
        /// sin crear texturas cada segundo.
        /// 
        /// Uso:
        ///   Texture tex = TextureHelper.CreateTextureFromBitmap(device, myBitmap);
        ///   Core.Instances.TextureDrawer.DrawTexture(tex, new Rectangle(50,50,100,100));
        /// 
        /// Recuerda hacer tex.Dispose() cuando ya no la necesites.
        /// </summary>
        public static SharpDX.Direct3D9.Texture CreateTextureFromBitmap(Device device, Bitmap bmp)
        {
            if (device == null) throw new System.ArgumentNullException(nameof(device));
            if (bmp == null) return null;

            int width = bmp.Width;
            int height = bmp.Height;

            // 1) Crear una textura lockeable en SystemMemory
            var sysMemTex = new SharpDX.Direct3D9.Texture(
                device,
                width,
                height,
                1,
                0, // Usage.None
                Format.A8R8G8B8,
                Pool.SystemMemory
            );

            // 2) Copiar los píxeles del Bitmap => sysMemTex
            var rect = sysMemTex.LockRectangle(0, LockFlags.None);
            int pitch = rect.Pitch;

            var bmpData = bmp.LockBits(
                new System.Drawing.Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* destPtr = (byte*)rect.DataPointer;
                byte* srcPtr = (byte*)bmpData.Scan0;

                for (int y = 0; y < height; y++)
                {
                    byte* destLine = destPtr + (y * pitch);
                    byte* srcLine = srcPtr + (y * bmpData.Stride);

                    for (int x = 0; x < width; x++)
                    {
                        // Formato en bmp => BGR (24 bpp)
                        byte B = srcLine[x * 3 + 0];
                        byte G = srcLine[x * 3 + 1];
                        byte R = srcLine[x * 3 + 2];

                        // En A8R8G8B8 => BGRA con A=255
                        destLine[x * 4 + 0] = B;
                        destLine[x * 4 + 1] = G;
                        destLine[x * 4 + 2] = R;
                        destLine[x * 4 + 3] = 255;
                    }
                }
            }

            bmp.UnlockBits(bmpData);
            sysMemTex.UnlockRectangle(0);

            // 3) Crear la textura final en Pool.Default
            var finalTex = new SharpDX.Direct3D9.Texture(
                device,
                width,
                height,
                1,
                0, // Usage
                Format.A8R8G8B8,
                Pool.Default
            );

            // 4) Transferir sysMemTex => finalTex en la GPU
            device.UpdateTexture(sysMemTex, finalTex);

            // 5) Ya no necesitamos la sysMemTex
            sysMemTex.Dispose();

            return finalTex;
        }
    }
}
