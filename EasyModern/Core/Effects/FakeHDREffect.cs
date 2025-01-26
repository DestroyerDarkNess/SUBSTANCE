using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace EasyModern.Core.Effects
{
    /// <summary>
    /// Aplica un efecto Fake HDR ultra rápido con paralelización y sin llamadas externas.
    /// </summary>
    public class UltraFastParallelFakeHDREffect
    {
        private float intensity; // Intensidad del efecto HDR
        public bool DebugMode { get; set; } // Modo de depuración

        /// <summary>
        /// Constructor del efecto Fake HDR.
        /// </summary>
        /// <param name="intensity">Intensidad del efecto (0.0f a 2.0f).</param>
        public UltraFastParallelFakeHDREffect(float intensity = 1.0f)
        {
            // Limitar intensidad manualmente sin Math.Clamp
            this.intensity = intensity < 0.0f ? 0.0f : (intensity > 2.0f ? 2.0f : intensity);
            this.DebugMode = false; // Depuración desactivada por defecto
        }

        /// <summary>
        /// Aplica el efecto Fake HDR sobre un Bitmap en su lugar.
        /// </summary>
        /// <param name="bmp">Bitmap al que se aplicará el efecto.</param>
        public unsafe void ApplyEffect(Bitmap bmp)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = bmp.Width;
                int height = bmp.Height;
                int stride = data.Stride;
                byte* ptrBase = (byte*)data.Scan0;

                // Paralelización por filas para aprovechar múltiples núcleos
                Parallel.For(0, height, y =>
                {
                    byte* row = ptrBase + y * stride;
                    bool applyEffect = !DebugMode || y < height / 2; // Depuración: aplicar solo a la mitad superior

                    for (int x = 0; x < width; x++)
                    {
                        // Calcular puntero al píxel actual
                        byte* pixel = row + x * 3;

                        // Leer los componentes RGB
                        byte b = pixel[0];
                        byte g = pixel[1];
                        byte r = pixel[2];

                        // Si no se aplica el efecto en modo depuración, continuar
                        if (!applyEffect) continue;

                        // Calcular el brillo promedio
                        float brightness = (r + g + b) / 3.0f / 255.0f;

                        // Aplicar la fórmula HDR
                        float hdrFactor = 1.0f + intensity * (brightness * brightness);

                        // Calcular nuevos valores RGB directamente en línea sin Math.Clamp
                        float newB = b * hdrFactor;
                        pixel[0] = newB < 0 ? (byte)0 : (newB > 255 ? (byte)255 : (byte)newB);

                        float newG = g * hdrFactor;
                        pixel[1] = newG < 0 ? (byte)0 : (newG > 255 ? (byte)255 : (byte)newG);

                        float newR = r * hdrFactor;
                        pixel[2] = newR < 0 ? (byte)0 : (newR > 255 ? (byte)255 : (byte)newR);
                    }
                });
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }
    }

    /*
     * Ejemplo de uso:
     * 
     * using System.Drawing;
     * 
     * class Program
     * {
     *     static void Main()
     *     {
     *         // Cargar una imagen
     *         Bitmap bmp = (Bitmap)Image.FromFile("image.png");
     * 
     *         // Crear el efecto Fake HDR optimizado
     *         UltraFastParallelFakeHDREffect hdrEffect = new UltraFastParallelFakeHDREffect(intensity: 1.2f)
     *         {
     *             DebugMode = true // Activar el modo de depuración para comparación
     *         };
     * 
     *         // Aplicar el efecto
     *         hdrEffect.ApplyEffect(bmp);
     * 
     *         // Guardar la imagen con el efecto aplicado
     *         bmp.Save("image_with_fake_hdr_parallel.png");
     *     }
     * }
     */
}
