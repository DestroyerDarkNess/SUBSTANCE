using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;

namespace EasyModern.Core.Effects
{
    public static class ImageEffect
    {

        /// <summary>
        /// Aplica un efecto de desenfoque al Bitmap en su lugar.
        /// </summary>
        /// <param name="bmp">Bitmap a procesar.</param>
        /// <param name="radius">Radio del desenfoque.</param>
        public static unsafe void BlurEffect(Bitmap bmp, int radius = 1)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));
            if (radius <= 0) return;

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                byte* ptrBase = (byte*)data.Scan0;

                for (int y = radius; y < height - radius; y++)
                {
                    for (int x = radius; x < width - radius; x++)
                    {
                        int sumR = 0, sumG = 0, sumB = 0, count = 0;

                        for (int ky = -radius; ky <= radius; ky++)
                        {
                            for (int kx = -radius; kx <= radius; kx++)
                            {
                                byte* pNeighbor = ptrBase + (y + ky) * stride + (x + kx) * 3;
                                sumB += pNeighbor[0];
                                sumG += pNeighbor[1];
                                sumR += pNeighbor[2];
                                count++;
                            }
                        }

                        byte* pPixel = ptrBase + y * stride + x * 3;
                        pPixel[0] = (byte)(sumB / count);
                        pPixel[1] = (byte)(sumG / count);
                        pPixel[2] = (byte)(sumR / count);
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }

        /// <summary>
        /// Aplica un efecto de “Glass” (vidrio esmerilado) a un Bitmap en su lugar,
        /// sin asignaciones de memoria. Recorre de abajo hacia arriba y de derecha a izquierda,
        /// utilizando un desplazamiento aleatorio hacia adelante (hacia abajo/derecha).
        /// </summary>
        /// <param name="bmp">Bitmap a procesar.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void GlassEffect(Bitmap bmp)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                byte* ptrBase = (byte*)data.Scan0;

                // Semilla para el generador de números “pseudoaleatorios”
                int seed = 1234567;

                // Recorre de abajo-arriba y derecha-izquierda
                for (int y = height - 1; y >= 0; y--)
                {
                    for (int x = width - 1; x >= 0; x--)
                    {
                        // Generamos dos números pseudoaleatorios (offsetX, offsetY)
                        seed = (seed * 214013 + 2531011);
                        int randVal = (seed >> 16) & 0x7FFF;
                        int offsetX = randVal % 3;  // 0..2

                        seed = (seed * 214013 + 2531011);
                        randVal = (seed >> 16) & 0x7FFF;
                        int offsetY = randVal % 3;  // 0..2

                        // Calculamos la posición destino desde donde tomaremos el color
                        int nx = x + offsetX;
                        if (nx >= width) nx = width - 1;

                        int ny = y + offsetY;
                        if (ny >= height) ny = height - 1;

                        // Leemos el color original (que aún no fue sobreescrito)
                        byte* pSrc = ptrBase + ny * stride + nx * 3;
                        byte b = pSrc[0];
                        byte g = pSrc[1];
                        byte r = pSrc[2];

                        // Escribimos en la posición actual
                        byte* pDest = ptrBase + y * stride + x * 3;
                        pDest[0] = b;
                        pDest[1] = g;
                        pDest[2] = r;
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }


        public static unsafe void FloydSteinbergDither(Bitmap bmp)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));

            // Asumimos que el bitmap está en formato de 24bpp.
            // Si no, habría que convertirlo o adaptarlo.
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                byte* ptrBase = (byte*)data.Scan0;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        // Apuntador al píxel actual (B, G, R)
                        byte* pPixel = ptrBase + y * stride + x * 3;

                        // Convertimos a un valor de gris (0..255).  
                        // pPixel[0] = B, pPixel[1] = G, pPixel[2] = R
                        int oldBlue = pPixel[0];
                        int oldGreen = pPixel[1];
                        int oldRed = pPixel[2];

                        // Promedio simple para gris (o podrías usar 0.299R+0.587G+0.114B).
                        int oldGray = (oldBlue + oldGreen + oldRed) / 3;

                        // Nuevo valor (0 o 255) según el dither
                        int newGray = (oldGray < 128) ? 0 : 255;

                        // Escribimos ese valor en los 3 canales (B, G, R)
                        pPixel[0] = (byte)newGray;
                        pPixel[1] = (byte)newGray;
                        pPixel[2] = (byte)newGray;

                        // Calculamos el error
                        int error = oldGray - newGray;

                        // Difundimos el error a los vecinos:
                        // Floyd-Steinberg (en una sola pasada de izquierda a derecha)
                        //   p(x+1, y)   += 7/16 * error
                        //   p(x-1, y+1) += 3/16 * error
                        //   p(x, y+1)   += 5/16 * error
                        //   p(x+1, y+1) += 1/16 * error

                        // 1) p(x+1, y)
                        if (x + 1 < width)
                        {
                            byte* pN = ptrBase + y * stride + (x + 1) * 3;
                            int nVal = (pN[0] + pN[1] + pN[2]) / 3;  // Gris actual de vecino
                            nVal += (7 * error) / 16;               // Diffusión
                            if (nVal < 0) nVal = 0; if (nVal > 255) nVal = 255;
                            // Escribimos en B, G, R
                            pN[0] = (byte)nVal;
                            pN[1] = (byte)nVal;
                            pN[2] = (byte)nVal;
                        }

                        // 2) p(x-1, y+1)
                        if (y + 1 < height && x - 1 >= 0)
                        {
                            byte* pN = ptrBase + (y + 1) * stride + (x - 1) * 3;
                            int nVal = (pN[0] + pN[1] + pN[2]) / 3;
                            nVal += (3 * error) / 16;
                            if (nVal < 0) nVal = 0; if (nVal > 255) nVal = 255;
                            pN[0] = (byte)nVal;
                            pN[1] = (byte)nVal;
                            pN[2] = (byte)nVal;
                        }

                        // 3) p(x, y+1)
                        if (y + 1 < height)
                        {
                            byte* pN = ptrBase + (y + 1) * stride + x * 3;
                            int nVal = (pN[0] + pN[1] + pN[2]) / 3;
                            nVal += (5 * error) / 16;
                            if (nVal < 0) nVal = 0; if (nVal > 255) nVal = 255;
                            pN[0] = (byte)nVal;
                            pN[1] = (byte)nVal;
                            pN[2] = (byte)nVal;
                        }

                        // 4) p(x+1, y+1)
                        if (y + 1 < height && x + 1 < width)
                        {
                            byte* pN = ptrBase + (y + 1) * stride + (x + 1) * 3;
                            int nVal = (pN[0] + pN[1] + pN[2]) / 3;
                            nVal += (1 * error) / 16;
                            if (nVal < 0) nVal = 0; if (nVal > 255) nVal = 255;
                            pN[0] = (byte)nVal;
                            pN[1] = (byte)nVal;
                            pN[2] = (byte)nVal;
                        }
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }

        public static unsafe void BloomFastEffectNoAlpha(
      Bitmap bmp,
      byte brightThreshold = 200,  // Umbral para determinar si un píxel es muy brillante
      byte darkThreshold = 10,     // Umbral para determinar si un píxel es muy oscuro
      byte brightenAmount = 10,    // Cuánto aclaramos los vecinos si es brillante
      byte darkenAmount = 2       // Cuánto oscurecemos los vecinos si es oscuro
  )
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));

            // Bloqueamos bits para acceso directo
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                byte* ptrBase = (byte*)data.Scan0;

                // Recorremos de izquierda a derecha y de arriba a abajo
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        // Tomamos el píxel actual
                        byte* pCurrent = ptrBase + y * stride + x * 3;

                        // Guardamos el color original (B, G, R)
                        byte b = pCurrent[0];
                        byte g = pCurrent[1];
                        byte r = pCurrent[2];

                        // Brillo = promedio simple (para no usar float)
                        int brightness = (r + g + b) / 3;

                        // Si es muy brillante, "empujamos" brillo a los vecinos
                        if (brightness >= brightThreshold)
                        {
                            // p(x+1, y)
                            if (x + 1 < width)
                            {
                                byte* pN = ptrBase + y * stride + (x + 1) * 3;
                                for (int c = 0; c < 3; c++)
                                {
                                    int val = pN[c] + brightenAmount;
                                    if (val > 255) val = 255;
                                    pN[c] = (byte)val;
                                }
                            }

                            // p(x, y+1)
                            if (y + 1 < height)
                            {
                                byte* pN = ptrBase + (y + 1) * stride + x * 3;
                                for (int c = 0; c < 3; c++)
                                {
                                    int val = pN[c] + brightenAmount;
                                    if (val > 255) val = 255;
                                    pN[c] = (byte)val;
                                }
                            }

                            // p(x+1, y+1)
                            if (x + 1 < width && y + 1 < height)
                            {
                                byte* pN = ptrBase + (y + 1) * stride + (x + 1) * 3;
                                for (int c = 0; c < 3; c++)
                                {
                                    int val = pN[c] + brightenAmount;
                                    if (val > 255) val = 255;
                                    pN[c] = (byte)val;
                                }
                            }
                        }
                        // Si es muy oscuro, "empujamos" la sombra
                        else if (brightness <= darkThreshold)
                        {
                            // p(x+1, y)
                            if (x + 1 < width)
                            {
                                byte* pN = ptrBase + y * stride + (x + 1) * 3;
                                for (int c = 0; c < 3; c++)
                                {
                                    int val = pN[c] - darkenAmount;
                                    if (val < 0) val = 0;
                                    pN[c] = (byte)val;
                                }
                            }

                            // p(x, y+1)
                            if (y + 1 < height)
                            {
                                byte* pN = ptrBase + (y + 1) * stride + x * 3;
                                for (int c = 0; c < 3; c++)
                                {
                                    int val = pN[c] - darkenAmount;
                                    if (val < 0) val = 0;
                                    pN[c] = (byte)val;
                                }
                            }

                            // p(x+1, y+1)
                            if (x + 1 < width && y + 1 < height)
                            {
                                byte* pN = ptrBase + (y + 1) * stride + (x + 1) * 3;
                                for (int c = 0; c < 3; c++)
                                {
                                    int val = pN[c] - darkenAmount;
                                    if (val < 0) val = 0;
                                    pN[c] = (byte)val;
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }

        public static unsafe void InvertEffect(Bitmap bmp)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));

            // Bloquea el bitmap para acceso directo a su memoria
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                byte* ptrBase = (byte*)data.Scan0;

                // Recorremos la imagen fila por fila
                for (int y = 0; y < height; y++)
                {
                    // Apuntador al inicio de la fila
                    byte* rowPtr = ptrBase + y * stride;

                    for (int x = 0; x < width; x++)
                    {
                        // Cada píxel (B, G, R) ocupa 3 bytes consecutivos
                        byte* pPixel = rowPtr + x * 3;

                        // Invertimos cada canal: nuevo valor = 255 - valor
                        pPixel[0] = (byte)(255 - pPixel[0]); // B
                        pPixel[1] = (byte)(255 - pPixel[1]); // G
                        pPixel[2] = (byte)(255 - pPixel[2]); // R
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }

        public static unsafe void PosterizeEffect(Bitmap bmp, int levels = 4)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));

            // Aseguramos que haya al menos 2 niveles
            if (levels < 2) levels = 2;
            if (levels > 256) levels = 256;

            // Bloqueamos para acceso directo
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                byte* ptrBase = (byte*)data.Scan0;

                // Tamaño de cada rango de color
                // (por ejemplo, con levels=4 => step=64)
                int step = 256 / levels;

                for (int y = 0; y < height; y++)
                {
                    byte* rowPtr = ptrBase + y * stride;

                    for (int x = 0; x < width; x++)
                    {
                        // Puntero al píxel actual (B, G, R)
                        byte* pPixel = rowPtr + x * 3;

                        // Para cada canal (B, G, R)
                        for (int c = 0; c < 3; c++)
                        {
                            int val = pPixel[c];

                            // Determinamos a qué "bucket" pertenece
                            int bucket = val / step;

                            // Mapeamos al inicio del rango
                            int newVal = bucket * step;

                            // Aseguramos que no exceda 255
                            if (newVal > 255) newVal = 255;

                            // Aplicamos
                            pPixel[c] = (byte)newVal;
                        }
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }

        public static unsafe void ContrastEffect(Bitmap bmp, int factor = 120)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));

            // factor = 100 => sin cambio
            // factor > 100 => más contraste
            // factor < 100 => menos contraste
            // Bloqueamos el bitmap para acceso directo
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                byte* ptrBase = (byte*)data.Scan0;

                for (int y = 0; y < height; y++)
                {
                    byte* rowPtr = ptrBase + y * stride;

                    for (int x = 0; x < width; x++)
                    {
                        // Puntero al píxel actual (B, G, R)
                        byte* pPixel = rowPtr + x * 3;

                        for (int c = 0; c < 3; c++)
                        {
                            int val = pPixel[c];

                            // Transformación lineal centrada en 128
                            // newVal = ((val - 128) * factor / 100) + 128
                            int tmp = (val - 128) * factor / 100 + 128;

                            // Aseguramos que quede en [0..255]
                            if (tmp < 0) tmp = 0;
                            if (tmp > 255) tmp = 255;

                            pPixel[c] = (byte)tmp;
                        }
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }

        public static unsafe void QuickSharpenEffect(Bitmap bmp, int strength = 1)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));

            // Bloqueamos el Bitmap para acceso directo
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                byte* ptrBase = (byte*)data.Scan0;

                // Recorremos cada fila
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width - 1; x++)
                    {
                        // Píxel actual
                        byte* pCur = ptrBase + y * stride + x * 3;
                        // Píxel vecino a la derecha (x+1)
                        byte* pRight = pCur + 3; // 3 bytes más adelante en la misma fila

                        // Para cada canal (B, G, R)
                        for (int c = 0; c < 3; c++)
                        {
                            // Diferencia entre píxel actual y su vecino derecho
                            int diff = pCur[c] - pRight[c];

                            // “Sharpen” sumando la diferencia (es decir, realzando contornos)
                            int newVal = pCur[c] + diff * strength;

                            // Limitamos a [0..255]
                            if (newVal < 0) newVal = 0;
                            if (newVal > 255) newVal = 255;

                            pCur[c] = (byte)newVal;
                        }
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }


        public static unsafe void EmbossEffect(Bitmap bmp)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));

            // Bloqueamos el Bitmap para acceso directo
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                byte* ptrBase = (byte*)data.Scan0;

                // Recorremos hasta width-1 y height-1 para no salir de rango al leer el píxel diagonal
                for (int y = 0; y < height - 1; y++)
                {
                    for (int x = 0; x < width - 1; x++)
                    {
                        // Píxel actual
                        byte* pCur = ptrBase + y * stride + x * 3;
                        // Píxel en la diagonal (x+1, y+1)
                        byte* pDiag = ptrBase + (y + 1) * stride + (x + 1) * 3;

                        // Para cada canal (B, G, R)
                        for (int c = 0; c < 3; c++)
                        {
                            // Calculamos la diferencia
                            int diff = pCur[c] - pDiag[c];

                            // Centramos en 128 para darle ese toque de “relieve”
                            diff += 128;

                            // Aseguramos el valor en [0..255]
                            if (diff < 0) diff = 0;
                            if (diff > 255) diff = 255;

                            // Asignamos al píxel actual
                            pCur[c] = (byte)diff;
                        }
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }

        /// <summary>
        /// Aplica un desenfoque radial a las esquinas de la imagen, dejando el centro nítido.
        /// La zona nítida está definida por 'radius' (distancia al centro), 
        /// y el desenfoque aumenta con la distancia al centro si 'smoothTransition' es true.
        /// Además, 'blurSize' controla el tamaño del kernel (2*blurSize+1).
        /// </summary>
        /// <param name="bmp">El bitmap a procesar.</param>
        /// <param name="focusRadius">
        /// Radio (en píxeles) alrededor del centro en el que NO se aplica (o apenas se aplica) el blur.
        /// Fuera de este radio, se desenfoca.
        /// </param>
        /// <param name="smoothTransition">
        /// Si true, el desenfoque crece gradualmente desde 'focusRadius' hasta los bordes.
        /// Si false, el desenfoque empieza bruscamente justo a partir de 'focusRadius'.
        /// </param>
        /// <param name="blurSize">
        /// Controla el tamaño del kernel de desenfoque. Si blurSize=1, es un 3x3.
        /// blurSize=2 => 5x5, blurSize=3 => 7x7, etc.
        /// </param>
        public static unsafe void RadialBlurEdgesEffect(
            Bitmap bmp,
            int focusRadius = 50,
            bool smoothTransition = true,
            int blurSize = 1)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));

            if (focusRadius < 1) focusRadius = 1;
            if (blurSize < 1) blurSize = 1;

            // Bloquear bits
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                byte* ptrBase = (byte*)data.Scan0;

                // Centro de la imagen
                float cx = width / 2f;
                float cy = height / 2f;

                // El radio de la zona enfocada al cuadrado (para comparar dist^2)
                float focusRadiusSq = focusRadius * focusRadius;

                // Rango de índices para el kernel:
                // p.ej. si blurSize=1 => ky, kx en [-1..1] => 3x3
                int kMin = -blurSize;
                int kMax = blurSize;
                int kernelSide = (blurSize * 2) + 1;  // p.ej. 3, 5, 7...

                // Recorremos de abajo a arriba y derecha a izquierda
                // para minimizar la contaminación de píxeles que ya fueron desenfocados
                for (int y = height - 1; y >= 0; y--)
                {
                    for (int x = width - 1; x >= 0; x--)
                    {
                        // Distancia al centro (cuadrada)
                        float dx = x - cx;
                        float dy = y - cy;
                        float dist2 = dx * dx + dy * dy;

                        // Si está fuera del foco, aplicamos blur
                        if (dist2 > focusRadiusSq)
                        {
                            // Calcula factor de transición suave (0..1)
                            float factor = 1f;
                            if (smoothTransition)
                            {
                                // Podrías definir un rango mayor si quieres que
                                // la transición sea más suave entre focusRadius y, por ejemplo, 
                                // la distancia máxima de la imagen.
                                // Aquí hacemos un ejemplo en el que 
                                // "0.0 => justo en focusRadius" y 
                                // "1.0 => dist2 es el doble de focusRadiusSq".
                                float maxDist2 = 2f * focusRadiusSq;
                                if (dist2 < maxDist2)
                                {
                                    factor = (dist2 - focusRadiusSq) / (maxDist2 - focusRadiusSq);
                                    if (factor < 0f) factor = 0f;
                                    if (factor > 1f) factor = 1f;
                                }
                            }

                            // Sumamos píxeles del kernel
                            int sumB = 0, sumG = 0, sumR = 0, count = 0;

                            for (int ky = kMin; ky <= kMax; ky++)
                            {
                                int ny = y + ky;
                                if (ny < 0 || ny >= height)
                                    continue;

                                for (int kx = kMin; kx <= kMax; kx++)
                                {
                                    int nx = x + kx;
                                    if (nx < 0 || nx >= width)
                                        continue;

                                    // Vecino
                                    byte* pN = ptrBase + ny * stride + nx * 3;
                                    sumB += pN[0];
                                    sumG += pN[1];
                                    sumR += pN[2];
                                    count++;
                                }
                            }

                            // Promedios
                            int avgB = sumB / count;
                            int avgG = sumG / count;
                            int avgR = sumR / count;

                            // Mezclamos con el color actual (antes de escribirlo)
                            byte* pCur = ptrBase + y * stride + x * 3;
                            int origB = pCur[0];
                            int origG = pCur[1];
                            int origR = pCur[2];

                            // newVal = orig * (1 - factor) + avg * factor
                            pCur[0] = (byte)(origB + (avgB - origB) * factor); // B
                            pCur[1] = (byte)(origG + (avgG - origG) * factor); // G
                            pCur[2] = (byte)(origR + (avgR - origR) * factor); // R
                        }
                        // De lo contrario, dist2 <= focusRadiusSq => se deja intacto
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }

        /// <summary>
        /// Pseudo-antialias in-place, muy simplificado.
        /// Recorre la imagen de abajo a arriba y derecha a izquierda,
        /// detecta bordes y los suaviza con un promedio local.
        /// </summary>
        /// <param name="bmp">Bitmap a procesar (24bpp)</param>
        /// <param name="threshold">
        /// Diferencia de color para considerar un borde (0..255).
        /// Un valor ~30..60 puede funcionar. Muy bajo = borra detalles, muy alto = poco suavizado.
        /// </param>
        /// <param name="strength">
        /// Qué fracción se promedia en los bordes (0..1).
        /// 0 => no suaviza, 1 => promedio al 100%.
        /// </param>
        public static unsafe void PseudoMorphologicalAA(Bitmap bmp, int threshold = 32, float strength = 0.5f)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));
            if (threshold < 0) threshold = 0;
            if (threshold > 255) threshold = 255;
            if (strength < 0f) strength = 0f;
            if (strength > 1f) strength = 1f;

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;

                byte* ptrBase = (byte*)data.Scan0;

                // Recorremos de bottom-right a top-left
                for (int y = height - 1; y >= 0; y--)
                {
                    for (int x = width - 1; x >= 0; x--)
                    {
                        // Puntero al píxel actual: B, G, R
                        byte* pCur = ptrBase + y * stride + x * 3;

                        // Color actual
                        int cb = pCur[0];
                        int cg = pCur[1];
                        int cr = pCur[2];

                        // Compararemos con dos vecinos (ejemplos):
                        // - Vecino Izquierdo (x-1, y)
                        // - Vecino Superior (x, y-1)
                        // [Podrías agregar diagonal si quieres más suavizado]

                        // 1) Vecino Izquierdo
                        if (x - 1 >= 0)
                        {
                            byte* pLeft = pCur - 3; // 3 bytes menos en la misma fila
                            int lb = pLeft[0];
                            int lg = pLeft[1];
                            int lr = pLeft[2];

                            int diff = Math.Abs(lb - cb) +
                                       Math.Abs(lg - cg) +
                                       Math.Abs(lr - cr);

                            if (diff > threshold)
                            {
                                // Suavizamos entre pCur y pLeft
                                // newCur = cur*(1-str) + left*str
                                // newLeft = left*(1-str) + cur*str
                                // Aquí, para no “machacar” demasiado, 
                                // podemos solo actualizar pCur o solo pLeft o ambos.

                                // Ejemplo: solo actualizamos pCur
                                // (o podrías actualizar mitad y mitad)
                                int ncb = (int)(cb * (1f - strength) + lb * strength);
                                int ncg = (int)(cg * (1f - strength) + lg * strength);
                                int ncr = (int)(cr * (1f - strength) + lr * strength);

                                pCur[0] = (byte)ncb;
                                pCur[1] = (byte)ncg;
                                pCur[2] = (byte)ncr;

                                // Actualizamos cb, cg, cr en local
                                cb = ncb; cg = ncg; cr = ncr;
                            }
                        }

                        // 2) Vecino Superior
                        if (y - 1 >= 0)
                        {
                            byte* pUp = pCur - stride; // fila anterior, misma columna
                            int ub = pUp[0];
                            int ug = pUp[1];
                            int ur = pUp[2];

                            int diff = Math.Abs(ub - cb) +
                                       Math.Abs(ug - cg) +
                                       Math.Abs(ur - cr);

                            if (diff > threshold)
                            {
                                // Suavizamos pCur con pUp, igual que antes
                                int ncb = (int)(cb * (1f - strength) + ub * strength);
                                int ncg = (int)(cg * (1f - strength) + ug * strength);
                                int ncr = (int)(cr * (1f - strength) + ur * strength);

                                pCur[0] = (byte)ncb;
                                pCur[1] = (byte)ncg;
                                pCur[2] = (byte)ncr;

                                cb = ncb; cg = ncg; cr = ncr;
                            }
                        }
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }

        /// <summary>
        /// Realiza un anti-aliasing morfológico naive con múltiples vecinos y 2 pasadas.
        /// Evita asignar memoria extra (sin buffers), pero puede difuminar detalles.
        /// </summary>
        /// <param name="bmp">Imagen 24bpp a procesar.</param>
        /// <param name="passes">Cuántas pasadas de suavizado (2 suele bastar).</param>
        /// <param name="threshold">
        /// Diferencia de color para considerar "borde". 
        /// Cuanto más bajo, más "agresivo" el suavizado (más difuminado).
        /// </param>
        /// <param name="strength">
        /// Fracción de mezcla al promediar con el vecino. 
        /// 0 = sin cambio, 1 = reemplazo total.
        /// </param>
        /// <param name="useDiagonals">
        /// Si true, compara también diagonales (8 vecinos); si false, solo 4 vecinos (arriba, abajo, izq., der.).
        /// </param>
        public static unsafe void MorphologicalAA2Pass(
            Bitmap bmp,
            int passes = 2,
            int threshold = 32,
            float strength = 0.6f,
            bool useDiagonals = true)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));

            if (threshold < 0) threshold = 0;
            if (threshold > 255) threshold = 255;
            if (strength < 0f) strength = 0f;
            if (strength > 1f) strength = 1f;
            if (passes < 1) passes = 1;

            // Bloqueamos el bitmap
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                byte* ptrBase = (byte*)data.Scan0;

                // Define offsets de vecinos (8 direcciones + 4 direcciones)
                // Cada offset = (deltaY, deltaX)
                // CUIDADO con el orden en cada pasada.
                // Para la pasada 1 (top-left -> bottom-right),
                // quizá conviene vecinos a la derecha/abajo. 
                // Para la pasada 2 (bottom-right -> top-left),
                // conviene vecinos a la izquierda/arriba.
                // Pero aquí haremos un set general y usaremos ambos pasadas para “peinar” todos lados.

                // 4 vecinos principales
                var neighbors4 = new (int dy, int dx)[]
                {
                (-1, 0), // arriba
                (1, 0),  // abajo
                (0, -1), // izquierda
                (0, 1)   // derecha
                };

                // diagonales
                var diagonals = new (int dy, int dx)[]
                {
                (-1, -1), // arriba izq
                (-1,  1), // arriba der
                ( 1, -1), // abajo izq
                ( 1,  1)  // abajo der
                };

                // Creamos un arreglo con vecinos totales
                // si useDiagonals = true => 8 vecinos, si false => 4
                (int dy, int dx)[] neighborOffsets;
                if (useDiagonals)
                {
                    neighborOffsets = new (int dy, int dx)[neighbors4.Length + diagonals.Length];
                    neighbors4.CopyTo(neighborOffsets, 0);
                    diagonals.CopyTo(neighborOffsets, neighbors4.Length);
                }
                else
                {
                    neighborOffsets = neighbors4;
                }

                for (int pass = 0; pass < passes; pass++)
                {
                    // Elegimos el orden de recorrido según sea par o impar
                    bool forward = (pass % 2 == 0);

                    if (forward)
                    {
                        // PASADA de top-left -> bottom-right
                        for (int y = 0; y < height; y++)
                        {
                            for (int x = 0; x < width; x++)
                            {
                                ApplyMorphAA(ptrBase, stride, width, height, x, y,
                                             neighborOffsets, threshold, strength);
                            }
                        }
                    }
                    else
                    {
                        // PASADA de bottom-right -> top-left
                        for (int y = height - 1; y >= 0; y--)
                        {
                            for (int x = width - 1; x >= 0; x--)
                            {
                                ApplyMorphAA(ptrBase, stride, width, height, x, y,
                                             neighborOffsets, threshold, strength);
                            }
                        }
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }

        /// <summary>
        /// Función auxiliar que detecta bordes y suaviza al píxel (x,y) con sus vecinos.
        /// </summary>
        private static unsafe void ApplyMorphAA(
            byte* ptrBase,
            int stride,
            int width,
            int height,
            int x,
            int y,
            (int dy, int dx)[] offsets,
            int threshold,
            float strength)
        {
            // Puntero al píxel actual
            byte* pCur = ptrBase + y * stride + x * 3;

            // Color actual
            int cb = pCur[0];
            int cg = pCur[1];
            int cr = pCur[2];

            // Revisamos cada vecino
            // Si la diferencia con el vecino es > threshold, 
            // promediamos en parte (controlado por strength).
            for (int i = 0; i < offsets.Length; i++)
            {
                int ny = y + offsets[i].dy;
                int nx = x + offsets[i].dx;
                // Chequeo de límites
                if (nx < 0 || nx >= width || ny < 0 || ny >= height)
                    continue;

                // Puntero a vecino
                byte* pN = ptrBase + ny * stride + nx * 3;
                int nb = pN[0];
                int ng = pN[1];
                int nr = pN[2];

                int diff = Math.Abs(nb - cb) + Math.Abs(ng - cg) + Math.Abs(nr - cr);

                if (diff > threshold)
                {
                    // Suavizar “pCur” hacia el vecino.
                    // newCur = cur*(1-strength) + neighbor*strength
                    int ncb = (int)(cb * (1f - strength) + nb * strength);
                    int ncg = (int)(cg * (1f - strength) + ng * strength);
                    int ncr = (int)(cr * (1f - strength) + nr * strength);

                    // Asignar
                    pCur[0] = (byte)ncb;
                    pCur[1] = (byte)ncg;
                    pCur[2] = (byte)ncr;

                    // Actualizar cb, cg, cr para siguientes vecinos
                    cb = ncb; cg = ncg; cr = ncr;
                }
            }
        }


        //        float[] myMatrix = new float[]
        //{
        //                    1.2f, 0f,   0f,
        //                    0f,   1f,   0f,
        //                    0f,   0f,   0.9f
        //};

        //        // Offset (R, G, B)
        //        float[] myOffset = new float[] { 10f, 0f, 0f };

        /// <summary>
        /// Aplica una corrección de color usando una matriz 3x3 y un vector offset (R, G, B).
        /// Cada canal se reescala linealmente y luego se clamp a [0..255].
        /// </summary>
        /// <param name="bmp">El Bitmap 24bpp a procesar.</param>
        /// <param name="matrix">
        /// Matriz de 9 elementos (3x3): [m00, m01, m02, m10, m11, m12, m20, m21, m22].
        /// Por defecto, la identidad.
        /// </param>
        /// <param name="offset">
        /// Offset de 3 elementos para (R, G, B). Por defecto, [0, 0, 0].
        /// </param>
        public static unsafe void ColorCorrection(Bitmap bmp, float[] matrix, float[] offset)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));

            // Validamos la matriz y el offset
            if (matrix == null || matrix.Length < 9)
            {
                // Usar la matriz identidad por defecto
                matrix = new float[]
                {
                1f, 0f, 0f,
                0f, 1f, 0f,
                0f, 0f, 1f
                };
            }
            if (offset == null || offset.Length < 3)
            {
                offset = new float[] { 0f, 0f, 0f };
            }

            // Bloqueamos el bitmap para acceso directo (asumiendo 24bppRgb)
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                byte* ptrBase = (byte*)data.Scan0;

                // Extraemos los elementos de la matriz para escribir más cómodo
                float m00 = matrix[0], m01 = matrix[1], m02 = matrix[2];
                float m10 = matrix[3], m11 = matrix[4], m12 = matrix[5];
                float m20 = matrix[6], m21 = matrix[7], m22 = matrix[8];

                // Offsets
                float offR = offset[0];
                float offG = offset[1];
                float offB = offset[2];

                // Recorremos cada píxel
                for (int y = 0; y < height; y++)
                {
                    // Puntero inicial de la fila
                    byte* rowPtr = ptrBase + y * stride;

                    for (int x = 0; x < width; x++)
                    {
                        // Puntero al píxel actual (orden B, G, R)
                        byte* pPixel = rowPtr + x * 3;

                        // Tomamos los valores originales en float
                        float b = pPixel[0];
                        float g = pPixel[1];
                        float r = pPixel[2];

                        // Aplicamos la matriz
                        // R' = m00*r + m01*g + m02*b + offR
                        // G' = m10*r + m11*g + m12*b + offG
                        // B' = m20*r + m21*g + m22*b + offB
                        float nr = (m00 * r) + (m01 * g) + (m02 * b) + offR;
                        float ng = (m10 * r) + (m11 * g) + (m12 * b) + offG;
                        float nb = (m20 * r) + (m21 * g) + (m22 * b) + offB;

                        // Clampeamos a [0..255]
                        if (nr < 0f) nr = 0f; if (nr > 255f) nr = 255f;
                        if (ng < 0f) ng = 0f; if (ng > 255f) ng = 255f;
                        if (nb < 0f) nb = 0f; if (nb > 255f) nb = 255f;

                        // Guardamos en el píxel
                        pPixel[2] = (byte)nr; // R
                        pPixel[1] = (byte)ng; // G
                        pPixel[0] = (byte)nb; // B
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }

        /// <summary>
        /// Aplica un "Fake HDR" in-place:
        ///  - Levanta sombras (brillos por debajo de mid).
        ///  - Comprime altas luces (brillos por encima de mid).
        /// Una sola pasada, sin buffers, usando una función de potencia.
        /// </summary>
        /// <param name="bmp">El Bitmap a procesar (24 bpp).</param>
        /// <param name="mid">
        /// Punto medio de brillo (0..255). Típicamente 128.
        /// </param>
        /// <param name="shadowPow">
        /// Exponente para sombras (<1 => se levantan más).
        /// Ej: 0.8f
        /// </param>
        /// <param name="highlightPow">
        /// Exponente para altas luces (>1 => se comprimen).
        /// Ej: 1.2f
        /// </param>
        public static unsafe void ApplyFakeHDR(
            Bitmap bmp,
            float mid = 128f,
            float shadowPow = 0.8f,
            float highlightPow = 1.2f)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));

            // Asegurar rangos razonables
            if (mid < 1f) mid = 1f;
            if (mid > 254f) mid = 254f;
            if (shadowPow < 0.01f) shadowPow = 0.01f;
            if (highlightPow < 0.01f) highlightPow = 0.01f;

            // Bloqueamos bits para acceso directo
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                byte* ptrBase = (byte*)data.Scan0;

                // Recorremos la imagen fila a fila
                for (int y = 0; y < height; y++)
                {
                    byte* rowPtr = ptrBase + y * stride;
                    for (int x = 0; x < width; x++)
                    {
                        byte* p = rowPtr + x * 3;

                        // Leer canales en float
                        float b = p[0];
                        float g = p[1];
                        float r = p[2];

                        // Luminancia
                        float lum = 0.2126f * r + 0.7152f * g + 0.0722f * b;
                        if (lum < 1f) lum = 1f; // Evitar division por 0

                        // Dependiendo de si está por debajo o por encima de "mid"
                        float newLum;
                        if (lum < mid)
                        {
                            // Levantar sombras
                            // Normalizamos lum a [0..1], aplicamos potencia, y
                            // volvemos a escalar a [0..mid].
                            float norm = lum / mid; // en [0..1]
                            float lifted = (float)Math.Pow(norm, shadowPow);
                            newLum = lifted * mid;
                        }
                        else
                        {
                            // Comprimir luces
                            // Normalizamos lum a [mid..255], pasamos a [0..1], 
                            // aplicamos potencia, volvemos a escalar, y sumamos "mid".
                            // Sino, podemos normalizar a [0..(255-mid)].
                            // Distancia = lum - mid
                            float dist = lum - mid;
                            float range = 255f - mid;
                            if (range < 1f) range = 1f;
                            float norm = dist / range; // [0..1]
                            float compressed = (float)Math.Pow(norm, highlightPow);
                            newLum = (compressed * range) + mid;
                        }

                        // ratio = newLum / lum
                        float ratio = newLum / lum;

                        // Ajustamos cada canal
                        float nr = r * ratio;
                        float ng = g * ratio;
                        float nb = b * ratio;

                        // clamp
                        if (nr < 0f) nr = 0f; if (nr > 255f) nr = 255f;
                        if (ng < 0f) ng = 0f; if (ng > 255f) ng = 255f;
                        if (nb < 0f) nb = 0f; if (nb > 255f) nb = 255f;

                        // Guardar
                        p[2] = (byte)nr;
                        p[1] = (byte)ng;
                        p[0] = (byte)nb;
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }

        /// <summary>
        /// Aplica un efecto CRT básico con scanlines y desenfoque.
        /// </summary>
        /// <param name="bmp">El Bitmap a procesar (24 bpp).</param>
        /// <param name="scanlineIntensity">Intensidad de las líneas de escaneo (0..255).</param>
        /// <param name="blurAmount">Cantidad de desenfoque (0 = sin desenfoque).</param>
        /// <param name="chromaticAberration">Cantidad de aberración cromática (en píxeles).</param>
        /// <param name="vignetteStrength">Intensidad del viñeteado (0 = sin viñeteado).</param>
        public static unsafe void ApplyCRTEffect(
            Bitmap bmp,
            byte scanlineIntensity = 32,
            int blurAmount = 0,
            int chromaticAberration = 1,
            float vignetteStrength = 0.3f)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));

            // Validar parámetros
            if (scanlineIntensity > 255) scanlineIntensity = 255;
            if (blurAmount < 0) blurAmount = 0;
            if (chromaticAberration < 0) chromaticAberration = 0;
            if (vignetteStrength < 0f) vignetteStrength = 0f;
            if (vignetteStrength > 1f) vignetteStrength = 1f;

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                byte* ptrBase = (byte*)data.Scan0;

                // --- 1. Aplicar Scanlines ---
                for (int y = 0; y < height; y++)
                {
                    if (y % 2 == 0) // Cada 2 líneas
                    {
                        byte* rowPtr = ptrBase + y * stride;
                        for (int x = 0; x < width; x++)
                        {
                            // Reducimos el brillo de cada píxel
                            rowPtr[0] = (byte)Math.Max(0, rowPtr[0] - scanlineIntensity); // B
                            rowPtr[1] = (byte)Math.Max(0, rowPtr[1] - scanlineIntensity); // G
                            rowPtr[2] = (byte)Math.Max(0, rowPtr[2] - scanlineIntensity); // R
                            rowPtr += 3; // Siguiente píxel
                        }
                    }
                }

                // --- 2. Aplicar Blur (desenfoque básico) ---
                if (blurAmount > 0)
                {
                    for (int y = 1; y < height - 1; y++)
                    {
                        for (int x = 1; x < width - 1; x++)
                        {
                            // Puntero al píxel actual
                            byte* p = ptrBase + y * stride + x * 3;

                            // Vecinos
                            byte* pUp = p - stride;
                            byte* pDown = p + stride;
                            byte* pLeft = p - 3;
                            byte* pRight = p + 3;

                            // Promedio
                            p[0] = (byte)((p[0] + pUp[0] + pDown[0] + pLeft[0] + pRight[0]) / 5); // B
                            p[1] = (byte)((p[1] + pUp[1] + pDown[1] + pLeft[1] + pRight[1]) / 5); // G
                            p[2] = (byte)((p[2] + pUp[2] + pDown[2] + pLeft[2] + pRight[2]) / 5); // R
                        }
                    }
                }

                // --- 3. Aplicar Aberración Cromática ---
                if (chromaticAberration > 0)
                {
                    for (int y = 0; y < height - chromaticAberration; y++)
                    {
                        for (int x = chromaticAberration; x < width; x++)
                        {
                            byte* p = ptrBase + y * stride + x * 3;
                            byte* pOffset = p - chromaticAberration * 3; // Desplazar vecino izquierdo

                            // Mover el canal rojo un poco hacia la izquierda
                            p[2] = pOffset[2];
                        }
                    }
                }

                // --- 4. Aplicar Viñeteado ---
                if (vignetteStrength > 0f)
                {
                    float cx = width / 2f;
                    float cy = height / 2f;
                    float maxDist = (float)Math.Sqrt(cx * cx + cy * cy);

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            byte* p = ptrBase + y * stride + x * 3;

                            // Distancia desde el centro
                            float dx = x - cx;
                            float dy = y - cy;
                            float dist = (float)Math.Sqrt(dx * dx + dy * dy);

                            // Atenuación según la distancia
                            float vignette = 1f - (vignetteStrength * (dist / maxDist));
                            vignette = Math.Max(0f, vignette); // No ir por debajo de 0

                            // Aplicar viñeteado
                            p[0] = (byte)(p[0] * vignette); // B
                            p[1] = (byte)(p[1] * vignette); // G
                            p[2] = (byte)(p[2] * vignette); // R
                        }
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }

        /// <summary>
        /// Aplica el efecto Battlefield 4: un resplandor cálido y partículas en un lado de la imagen.
        /// </summary>
        /// <param name="bmp">El Bitmap a procesar (24 bpp).</param>
        /// <param name="glowColor">Color del resplandor (ej. naranja/amarillo).</param>
        /// <param name="intensity">Intensidad del resplandor (0..1).</param>
        /// <param name="particleCount">Cantidad de partículas simuladas.</param>
        public static void ApplyBattlefield4Effect(Bitmap bmp, Color glowColor, float intensity, int particleCount)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));
            if (intensity < 0f) intensity = 0f;
            if (intensity > 1f) intensity = 1f;
            if (particleCount < 0) particleCount = 0;

            // 1. Agregar resplandor cálido en el lado derecho
            ApplyGlow(bmp, glowColor, intensity);

            // 2. Añadir partículas de luz
            AddParticles(bmp, glowColor, particleCount);
        }

        /// <summary>
        /// Agrega un resplandor cálido desde el lado derecho de la imagen.
        /// </summary>
        private static void ApplyGlow(Bitmap bmp, Color glowColor, float intensity)
        {
            int width = bmp.Width;
            int height = bmp.Height;

            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Crear un degradado radial que emana desde el lado derecho
                using (var brush = new LinearGradientBrush(
                    new Point(width, 0),
                    new Point(0, height),
                    Color.FromArgb((int)(intensity * 255), glowColor),
                    Color.Transparent))
                {
                    g.FillRectangle(brush, new Rectangle(0, 0, width, height));
                }
            }
        }

        /// <summary>
        /// Añade partículas brillantes como chispas.
        /// </summary>
        private static void AddParticles(Bitmap bmp, Color particleColor, int count)
        {
            Random rand = new Random();
            using (Graphics g = Graphics.FromImage(bmp))
            {
                for (int i = 0; i < count; i++)
                {
                    // Generar posición aleatoria en el lado derecho
                    int x = rand.Next(bmp.Width / 2, bmp.Width);
                    int y = rand.Next(0, bmp.Height);

                    // Generar tamaño aleatorio para la partícula
                    int size = rand.Next(2, 6);

                    // Generar opacidad aleatoria
                    int alpha = rand.Next(100, 255);

                    using (Brush brush = new SolidBrush(Color.FromArgb(alpha, particleColor)))
                    {
                        g.FillEllipse(brush, x, y, size, size);
                    }
                }
            }
        }

        /// <summary>
        /// Aplica un efecto HDR optimizado a una imagen Bitmap utilizando un solo bucle for.
        /// </summary>
        /// <param name="bmp">La imagen Bitmap a procesar.</param>
        /// <param name="gamma">Corrección gamma. Valor predeterminado: 1.2f.</param>
        /// <param name="intensity">Intensidad del efecto. Valor predeterminado: 1.5f.</param>
        /// <param name="contrast">Contraste del efecto. Valor predeterminado: 1.2f.</param>
        public static unsafe void HDREffectOptimizedSingleLoop(Bitmap bmp, float gamma = 1f, float intensity = 1.0f, float contrast = 1.2f)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));

            // Escalado fijo para parámetros (ejemplo: multiplicar por 1000)
            const int scale = 1000;

            // Convertir parámetros flotantes a enteros con escala fija
            int gammaNumerator = (int)(gamma * scale);
            int gammaDenominator = scale;

            int intensityMultiplier = (int)(intensity * scale);
            int intensityDivisor = scale;

            int contrastMultiplier = (int)(contrast * scale);
            int contrastDivisor = scale;

            // Crear tabla de consulta para corrección gamma
            byte[] gammaLUT = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                int value = (i * gammaNumerator) / gammaDenominator;
                value = value > 255 ? 255 : (value < 0 ? 0 : value);
                gammaLUT[i] = (byte)value;
            }

            // Bloquea el bitmap para acceso directo a su memoria
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                byte* ptrBase = (byte*)data.Scan0;

                int totalPixels = width * height;
                byte* ptr = ptrBase;

                for (int i = 0; i < totalPixels; i++)
                {
                    // Leer los valores originales de B, G, R
                    int B = gammaLUT[ptr[0]];
                    int G = gammaLUT[ptr[1]];
                    int R = gammaLUT[ptr[2]];

                    // Aumentar la intensidad
                    R = (R * intensityMultiplier) / intensityDivisor;
                    G = (G * intensityMultiplier) / intensityDivisor;
                    B = (B * intensityMultiplier) / intensityDivisor;

                    // Aumentar el contraste
                    R = ((R - 128) * contrastMultiplier) / contrastDivisor + 128;
                    G = ((G - 128) * contrastMultiplier) / contrastDivisor + 128;
                    B = ((B - 128) * contrastMultiplier) / contrastDivisor + 128;

                    // Clamping manual para asegurar que los valores estén en [0, 255]
                    R = R > 255 ? 255 : (R < 0 ? 0 : R);
                    G = G > 255 ? 255 : (G < 0 ? 0 : G);
                    B = B > 255 ? 255 : (B < 0 ? 0 : B);

                    // Asignar los nuevos valores al píxel
                    ptr[0] = (byte)B; // B
                    ptr[1] = (byte)G; // G
                    ptr[2] = (byte)R; // R

                    // Avanzar al siguiente píxel (3 bytes por píxel en formato 24bppRgb)
                    ptr += 3;
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }

        /// <summary>
        /// Aplica un efecto HDR optimizado a una imagen Bitmap.
        /// </summary>
        /// <param name="bmp">La imagen Bitmap a procesar.</param>
        /// <param name="gamma">Corrección gamma. Valor predeterminado: 1.2f.</param>
        /// <param name="intensity">Intensidad del efecto. Valor predeterminado: 1.5f.</param>
        /// <param name="contrast">Contraste del efecto. Valor predeterminado: 1.2f.</param>
        public static unsafe void HDREffectOptimized(Bitmap bmp, float gamma = 1f, float intensity = 1.0f, float contrast = 1.2f)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));

            // Escalado fijo para parámetros (ejemplo: multiplicar por 1000)
            const int scale = 1000;

            // Convertir parámetros flotantes a enteros con escala fija
            int gammaNumerator = (int)(gamma * scale);
            int gammaDenominator = scale;

            int intensityMultiplier = (int)(intensity * scale);
            int intensityDivisor = scale;

            int contrastMultiplier = (int)(contrast * scale);
            int contrastDivisor = scale;

            // Bloquea el bitmap para acceso directo a su memoria
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                byte* ptrBase = (byte*)data.Scan0;

                for (int y = 0; y < height; y++)
                {
                    byte* rowPtr = ptrBase + y * stride;

                    for (int x = 0; x < width; x++)
                    {
                        byte* pPixel = rowPtr + x * 3;

                        // Obtener los valores originales
                        int B = pPixel[0];
                        int G = pPixel[1];
                        int R = pPixel[2];

                        // Aplicar corrección gamma usando aproximación lineal
                        R = (R * gammaNumerator) / gammaDenominator;
                        G = (G * gammaNumerator) / gammaDenominator;
                        B = (B * gammaNumerator) / gammaDenominator;

                        // Aumentar la intensidad
                        R = (R * intensityMultiplier) / intensityDivisor;
                        G = (G * intensityMultiplier) / intensityDivisor;
                        B = (B * intensityMultiplier) / intensityDivisor;

                        // Aumentar el contraste
                        R = ((R - 128) * contrastMultiplier) / contrastDivisor + 128;
                        G = ((G - 128) * contrastMultiplier) / contrastDivisor + 128;
                        B = ((B - 128) * contrastMultiplier) / contrastDivisor + 128;

                        // Clamping manual para asegurar que los valores estén en [0, 255]
                        R = R > 255 ? 255 : (R < 0 ? 0 : R);
                        G = G > 255 ? 255 : (G < 0 ? 0 : G);
                        B = B > 255 ? 255 : (B < 0 ? 0 : B);

                        // Asignar los nuevos valores al píxel
                        pPixel[2] = (byte)R; // R
                        pPixel[1] = (byte)G; // G
                        pPixel[0] = (byte)B; // B
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }


        /// <summary>
        /// Aplica un efecto de pixelación al Bitmap en su lugar.
        /// </summary>
        /// <param name="bmp">Bitmap a procesar.</param>
        /// <param name="blockSize">Tamaño del bloque de pixelación.</param>
        public static unsafe void PixelateEffect(Bitmap bmp, int blockSize = 10)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));
            if (blockSize <= 1) return;

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                byte* ptrBase = (byte*)data.Scan0;

                for (int y = 0; y < height; y += blockSize)
                {
                    for (int x = 0; x < width; x += blockSize)
                    {
                        int blockEndX = Math.Min(x + blockSize, width);
                        int blockEndY = Math.Min(y + blockSize, height);

                        // Calcular el color promedio del bloque
                        int sumR = 0, sumG = 0, sumB = 0, count = 0;

                        for (int yy = y; yy < blockEndY; yy++)
                        {
                            byte* row = ptrBase + yy * stride;
                            for (int xx = x; xx < blockEndX; xx++)
                            {
                                byte* pixel = row + xx * 3;
                                sumB += pixel[0];
                                sumG += pixel[1];
                                sumR += pixel[2];
                                count++;
                            }
                        }

                        byte avgB = (byte)(sumB / count);
                        byte avgG = (byte)(sumG / count);
                        byte avgR = (byte)(sumR / count);

                        // Aplicar el color promedio al bloque
                        for (int yy = y; yy < blockEndY; yy++)
                        {
                            byte* row = ptrBase + yy * stride;
                            for (int xx = x; xx < blockEndX; xx++)
                            {
                                byte* pixel = row + xx * 3;
                                pixel[0] = avgB;
                                pixel[1] = avgG;
                                pixel[2] = avgR;
                            }
                        }
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }


        /// <summary>
        /// Aplica un efecto de ondas (ripple) a un Bitmap en su lugar.
        /// </summary>
        /// <param name="bmp">Bitmap a procesar.</param>
        /// <param name="amplitude">Amplitud de las ondas.</param>
        /// <param name="frequency">Frecuencia de las ondas.</param>
        public static unsafe void RippleEffect(Bitmap bmp, int amplitude = 5, double frequency = 0.1)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                byte* ptrBase = (byte*)data.Scan0;

                for (int y = 0; y < height; y++)
                {
                    double offsetX = amplitude * Math.Sin(2 * Math.PI * frequency * y);
                    int intOffsetX = (int)Math.Round(offsetX);

                    for (int x = 0; x < width; x++)
                    {
                        int nx = x + intOffsetX;
                        if (nx < 0 || nx >= width) continue;

                        byte* pSrc = ptrBase + y * stride + nx * 3;
                        byte* pDest = ptrBase + y * stride + x * 3;

                        pDest[0] = pSrc[0]; // B
                        pDest[1] = pSrc[1]; // G
                        pDest[2] = pSrc[2]; // R
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }

        /// <summary>
        /// Aplica un efecto de líneas de escaneo al Bitmap en su lugar.
        /// </summary>
        /// <param name="bmp">Bitmap a procesar.</param>
        /// <param name="lineSpacing">Espaciado entre las líneas.</param>
        public static unsafe void ScanlinesEffect(Bitmap bmp, int lineSpacing = 2)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                byte* ptrBase = (byte*)data.Scan0;

                for (int y = 0; y < height; y++)
                {
                    if (y % lineSpacing == 0) continue;

                    byte* row = ptrBase + y * stride;
                    for (int x = 0; x < width; x++)
                    {
                        byte* pixel = row + x * 3;

                        // Oscurecemos el píxel para simular la línea de escaneo
                        pixel[0] = (byte)(pixel[0] / 2); // B
                        pixel[1] = (byte)(pixel[1] / 2); // G
                        pixel[2] = (byte)(pixel[2] / 2); // R
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }

        /// <summary>
        /// Aplica un efecto Cartoon (estilo dibujo animado) al Bitmap en su lugar.
        /// Optimizado para máxima velocidad.
        /// </summary>
        /// <param name="bmp">Bitmap a procesar.</param>
        /// <param name="edgeThreshold">Umbral para la detección de bordes.</param>
        /// <param name="colorReductionFactor">Factor de reducción de colores (mayor es más "cartoon").</param>
        public static unsafe void CartoonEffectOptimized(Bitmap bmp, int edgeThreshold = 50, int colorReductionFactor = 10)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                byte* ptrBase = (byte*)data.Scan0;

                // Procesar en un solo recorrido
                for (int y = 0; y < height - 1; y++)
                {
                    byte* currentRow = ptrBase + y * stride;
                    byte* nextRow = ptrBase + (y + 1) * stride;

                    for (int x = 0; x < width - 1; x++)
                    {
                        int pixelIndex = x * 3;

                        // Píxel actual
                        byte b = currentRow[pixelIndex];
                        byte g = currentRow[pixelIndex + 1];
                        byte r = currentRow[pixelIndex + 2];

                        // Píxel derecha
                        byte bRight = currentRow[pixelIndex + 3];
                        byte gRight = currentRow[pixelIndex + 4];
                        byte rRight = currentRow[pixelIndex + 5];

                        // Píxel abajo
                        byte bDown = nextRow[pixelIndex];
                        byte gDown = nextRow[pixelIndex + 1];
                        byte rDown = nextRow[pixelIndex + 2];

                        // Detección de bordes
                        int diffHorizontal = Math.Abs(r - rRight) + Math.Abs(g - gRight) + Math.Abs(b - bRight);
                        int diffVertical = Math.Abs(r - rDown) + Math.Abs(g - gDown) + Math.Abs(b - bDown);

                        if (Math.Max(diffHorizontal, diffVertical) > edgeThreshold)
                        {
                            // Píxel negro si es un borde
                            currentRow[pixelIndex] = 0;     // B
                            currentRow[pixelIndex + 1] = 0; // G
                            currentRow[pixelIndex + 2] = 0; // R
                        }
                        else
                        {
                            // Reducción de colores
                            currentRow[pixelIndex] = (byte)((b / colorReductionFactor) * colorReductionFactor);     // B
                            currentRow[pixelIndex + 1] = (byte)((g / colorReductionFactor) * colorReductionFactor); // G
                            currentRow[pixelIndex + 2] = (byte)((r / colorReductionFactor) * colorReductionFactor); // R
                        }
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }

        /// <summary>
        /// Aplica un efecto estilo Anime (tonos pastel) a un Bitmap en su lugar.
        /// Optimizado para máxima velocidad.
        /// </summary>
        /// <param name="bmp">Bitmap a procesar.</param>
        /// <param name="pastelFactor">Factor de pastelización (0.1f a 1.0f; menor = más pastel).</param>
        public static unsafe void AnimePastelEffectOptimized(Bitmap bmp, float pastelFactor = 0.7f)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));

            if (pastelFactor < 0.1f || pastelFactor > 1.0f)
                throw new ArgumentOutOfRangeException(nameof(pastelFactor), "El pastelFactor debe estar entre 0.1f y 1.0f.");

            // Precomputar el LUT para pastelización
            byte[] lut = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                lut[i] = (byte)Core.Utils.MathExtensions.Clamp((int)(i * pastelFactor + 255 * (1 - pastelFactor)), 0, 255);
            }

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                byte* ptrBase = (byte*)data.Scan0;

                for (int y = 0; y < height; y++)
                {
                    byte* row = ptrBase + y * stride;
                    for (int x = 0; x < width; x++)
                    {
                        int pixelIndex = x * 3;

                        // Acceder a los valores originales (B, G, R) y aplicar el LUT
                        row[pixelIndex] = lut[row[pixelIndex]];         // B
                        row[pixelIndex + 1] = lut[row[pixelIndex + 1]]; // G
                        row[pixelIndex + 2] = lut[row[pixelIndex + 2]]; // R
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }

        /// <summary>
        /// Aplica un efecto Bloom (resplandor) a un Bitmap con máxima optimización.
        /// </summary>
        /// <param name="bmp">Bitmap a procesar.</param>
        /// <param name="threshold">Umbral para detectar áreas brillantes (0-255).</param>
        /// <param name="blurRadius">Radio del desenfoque para el resplandor.</param>
        public static unsafe void BloomEffectOptimized(Bitmap bmp, int threshold = 200, int blurRadius = 5)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));

            if (threshold < 0 || threshold > 255)
                throw new ArgumentOutOfRangeException(nameof(threshold), "El umbral debe estar entre 0 y 255.");

            if (blurRadius < 1)
                throw new ArgumentOutOfRangeException(nameof(blurRadius), "El radio de desenfoque debe ser mayor o igual a 1.");

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                byte* ptrBase = (byte*)data.Scan0;

                // Buffer para las áreas brillantes
                byte[] brightBuffer = new byte[height * stride];

                // Paso 1: Detección de áreas brillantes
                fixed (byte* brightPtr = brightBuffer)
                {
                    for (int y = 0; y < height; y++)
                    {
                        byte* row = ptrBase + y * stride;
                        byte* brightRow = brightPtr + y * stride;

                        for (int x = 0; x < width; x++)
                        {
                            int pixelIndex = x * 3;

                            // Calcular brillo (promedio de R, G, B)
                            int brightness = (row[pixelIndex + 2] + row[pixelIndex + 1] + row[pixelIndex]) / 3;

                            // Guardar píxeles brillantes en el buffer
                            if (brightness >= threshold)
                            {
                                brightRow[pixelIndex] = row[pixelIndex];         // B
                                brightRow[pixelIndex + 1] = row[pixelIndex + 1]; // G
                                brightRow[pixelIndex + 2] = row[pixelIndex + 2]; // R
                            }
                        }
                    }

                    // Paso 2: Aplicar desenfoque (box blur)
                    for (int y = blurRadius; y < height - blurRadius; y++)
                    {
                        for (int x = blurRadius; x < width - blurRadius; x++)
                        {
                            int pixelIndex = y * stride + x * 3;

                            int sumB = 0, sumG = 0, sumR = 0, count = 0;

                            // Promediar píxeles vecinos dentro del radio
                            for (int ky = -blurRadius; ky <= blurRadius; ky++)
                            {
                                byte* neighborRow = brightPtr + (y + ky) * stride;

                                for (int kx = -blurRadius; kx <= blurRadius; kx++)
                                {
                                    int neighborIndex = (x + kx) * 3;
                                    sumB += neighborRow[neighborIndex];
                                    sumG += neighborRow[neighborIndex + 1];
                                    sumR += neighborRow[neighborIndex + 2];
                                    count++;
                                }
                            }

                            // Aplicar desenfoque al píxel actual
                            ptrBase[pixelIndex] = (byte)Core.Utils.MathExtensions.Clamp(ptrBase[pixelIndex] + sumB / count, 0, 255);         // B
                            ptrBase[pixelIndex + 1] = (byte)Core.Utils.MathExtensions.Clamp(ptrBase[pixelIndex + 1] + sumG / count, 0, 255); // G
                            ptrBase[pixelIndex + 2] = (byte)Core.Utils.MathExtensions.Clamp(ptrBase[pixelIndex + 2] + sumR / count, 0, 255); // R
                        }
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }

        /// <summary>
        /// Aplica un efecto Sharingan a un Bitmap en la región especificada.
        /// </summary>
        /// <param name="bmp">Bitmap a procesar.</param>
        /// <param name="centerX">Coordenada X del centro del ojo.</param>
        /// <param name="centerY">Coordenada Y del centro del ojo.</param>
        /// <param name="radius">Radio del efecto Sharingan.</param>
        public static void SharinganEffect(Bitmap bmp, int centerX, int centerY, int radius)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Paso 1: Fondo rojo del Sharingan
                using (Brush redBrush = new SolidBrush(Color.FromArgb(255, 255, 0, 0)))
                {
                    g.FillEllipse(redBrush, centerX - radius, centerY - radius, radius * 2, radius * 2);
                }

                // Paso 2: Círculos negros concéntricos
                using (Pen blackPen = new Pen(Color.Black, 2))
                {
                    g.DrawEllipse(blackPen, centerX - radius, centerY - radius, radius * 2, radius * 2);
                    g.DrawEllipse(blackPen, centerX - (radius / 2), centerY - (radius / 2), radius, radius);
                }

                // Paso 3: Marcas del Sharingan
                int marks = 3; // Tres marcas clásicas del Sharingan
                double angleStep = 360.0 / marks;

                for (int i = 0; i < marks; i++)
                {
                    double angle = i * angleStep;
                    double radian = angle * Math.PI / 180;

                    int markX = centerX + (int)(Math.Cos(radian) * (radius / 2));
                    int markY = centerY + (int)(Math.Sin(radian) * (radius / 2));

                    using (Brush blackBrush = new SolidBrush(Color.Black))
                    {
                        g.FillEllipse(blackBrush, markX - 10, markY - 10, 20, 20);
                    }
                }

                // Paso 4: Pequeño círculo negro en el centro
                using (Brush blackBrush = new SolidBrush(Color.Black))
                {
                    g.FillEllipse(blackBrush, centerX - (radius / 10), centerY - (radius / 10), radius / 5, radius / 5);
                }
            }
        }


        /// <summary>
        /// Aplica un efecto de relámpagos aleatorios estilo Dragon Ball Z.
        /// </summary>
        /// <param name="bmp">Bitmap a procesar.</param>
        /// <param name="lightningCount">Cantidad de rayos a generar.</param>
        /// <param name="branchProbability">Probabilidad de generar ramas (0.0 a 1.0).</param>
        public static unsafe void LightningEffect(Bitmap bmp, int lightningCount = 10, double branchProbability = 0.3)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                byte* ptrBase = (byte*)data.Scan0;

                Random random = new Random();

                // Generar múltiples rayos
                for (int i = 0; i < lightningCount; i++)
                {
                    // Punto inicial aleatorio en la parte superior
                    int startX = random.Next(width);
                    int startY = 0;

                    // Generar un rayo principal
                    GenerateLightning(ptrBase, stride, width, height, startX, startY, random, branchProbability);
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }

        private static unsafe void GenerateLightning(byte* ptrBase, int stride, int width, int height, int startX, int startY, Random random, double branchProbability)
        {
            int x = startX;
            int y = startY;

            // Color base del rayo
            byte r = 0;    // Rojo
            byte g = 191;  // Verde
            byte b = 255;  // Azul brillante

            while (y < height)
            {
                // Dibujar el píxel actual
                if (x >= 0 && x < width && y >= 0 && y < height)
                {
                    byte* pixel = ptrBase + y * stride + x * 3;
                    pixel[0] = b; // Azul
                    pixel[1] = g; // Verde
                    pixel[2] = r; // Rojo
                }

                // Desplazamiento aleatorio en la dirección del rayo
                x += random.Next(-2, 3); // Movimiento horizontal (-2 a 2)
                y += random.Next(1, 4);  // Movimiento vertical (1 a 3)

                // Generar una rama aleatoria
                if (random.NextDouble() < branchProbability)
                {
                    int branchX = x;
                    int branchY = y;

                    // Generar una rama con menos iteraciones
                    for (int i = 0; i < 5; i++)
                    {
                        branchX += random.Next(-2, 3);
                        branchY += random.Next(1, 3);

                        if (branchX >= 0 && branchX < width && branchY >= 0 && branchY < height)
                        {
                            byte* branchPixel = ptrBase + branchY * stride + branchX * 3;
                            branchPixel[0] = (byte)(b / 2); // Azul más tenue
                            branchPixel[1] = (byte)(g / 2); // Verde más tenue
                            branchPixel[2] = (byte)(r / 2); // Rojo más tenue
                        }
                    }
                }
            }
        }


        #region "Fake MotionBlurEffect"

        /// <summary>
        /// Applies a Motion Blur effect to the Bitmap in an optimized way.
        /// </summary>
        /// <param name="bmp">Bitmap to process.</param>
        /// <param name="blurLength">Length of the blur.</param>
        /// <param name="angle">Angle of the blur in degrees (0 = horizontal, 90 = vertical).</param>
        public static unsafe void MotionBlurEffectOptimized(Bitmap bmp, int blurLength = 10, float angle = 0f)
        {
            if (bmp == null)
                throw new ArgumentNullException(nameof(bmp));
            if (blurLength <= 1) return;

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            try
            {
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                byte* ptrBase = (byte*)data.Scan0;

                // Calculate displacement based on the angle
                double radians = angle * Math.PI / 180.0;
                int dx = (int)Math.Round(Math.Cos(radians) * blurLength);
                int dy = (int)Math.Round(Math.Sin(radians) * blurLength);

                // Handle special cases for horizontal and vertical blur
                if (dx == 0)
                {
                    MotionBlurVertical(ptrBase, width, height, stride, blurLength);
                    return;
                }
                if (dy == 0)
                {
                    MotionBlurHorizontal(ptrBase, width, height, stride, blurLength);
                    return;
                }

                // Create temporary buffers for faster access
                byte[] tempBufferR = new byte[width * height];
                byte[] tempBufferG = new byte[width * height];
                byte[] tempBufferB = new byte[width * height];

                // Separate color channels into temporary buffers
                for (int y = 0; y < height; y++)
                {
                    byte* row = ptrBase + y * stride;
                    for (int x = 0; x < width; x++)
                    {
                        tempBufferR[y * width + x] = row[x * 3 + 2];
                        tempBufferG[y * width + x] = row[x * 3 + 1];
                        tempBufferB[y * width + x] = row[x * 3 + 0];
                    }
                }

                // Apply blur to each color channel separately
                ApplyBlur(tempBufferR, width, height, dx, dy);
                ApplyBlur(tempBufferG, width, height, dx, dy);
                ApplyBlur(tempBufferB, width, height, dx, dy);

                // Combine blurred channels back into the original image
                for (int y = 0; y < height; y++)
                {
                    byte* row = ptrBase + y * stride;
                    for (int x = 0; x < width; x++)
                    {
                        row[x * 3 + 2] = tempBufferR[y * width + x];
                        row[x * 3 + 1] = tempBufferG[y * width + x];
                        row[x * 3 + 0] = tempBufferB[y * width + x];
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }

        private static unsafe void MotionBlurHorizontal(byte* ptrBase, int width, int height, int stride, int blurLength)
        {
            for (int y = 0; y < height; y++)
            {
                byte* row = ptrBase + y * stride;
                for (int x = 0; x < width; x++)
                {
                    int sumR = 0, sumG = 0, sumB = 0;
                    int count = 0;

                    for (int k = -blurLength / 2; k <= blurLength / 2; k++)
                    {
                        int nx = x + k;
                        if (nx >= 0 && nx < width)
                        {
                            sumB += row[nx * 3 + 0];
                            sumG += row[nx * 3 + 1];
                            sumR += row[nx * 3 + 2];
                            count++;
                        }
                    }

                    row[x * 3 + 0] = (byte)(sumB / count);
                    row[x * 3 + 1] = (byte)(sumG / count);
                    row[x * 3 + 2] = (byte)(sumR / count);
                }
            }
        }

        private static unsafe void MotionBlurVertical(byte* ptrBase, int width, int height, int stride, int blurLength)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    byte* row = ptrBase + y * stride;
                    int sumR = 0, sumG = 0, sumB = 0;
                    int count = 0;

                    for (int k = -blurLength / 2; k <= blurLength / 2; k++)
                    {
                        int ny = y + k;
                        if (ny >= 0 && ny < height)
                        {
                            byte* neighborRow = ptrBase + ny * stride;
                            sumB += neighborRow[x * 3 + 0];
                            sumG += neighborRow[x * 3 + 1];
                            sumR += neighborRow[x * 3 + 2];
                            count++;
                        }
                    }

                    row[x * 3 + 0] = (byte)(sumB / count);
                    row[x * 3 + 1] = (byte)(sumG / count);
                    row[x * 3 + 2] = (byte)(sumR / count);
                }
            }
        }

        private static unsafe void ApplyBlur(byte[] buffer, int width, int height, int dx, int dy)
        {
            int clampedDx = Math.Max(Math.Abs(dx), 1);
            int clampedDy = Math.Max(Math.Abs(dy), 1);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int sum = 0;
                    int count = 0;

                    for (int ky = -clampedDy; ky <= clampedDy; ky++)
                    {
                        int ny = y + ky * dy;
                        if (ny >= 0 && ny < height)
                        {
                            for (int kx = -clampedDx; kx <= clampedDx; kx++)
                            {
                                int nx = x + kx * dx;
                                if (nx >= 0 && nx < width)
                                {
                                    sum += buffer[ny * width + nx];
                                    count++;
                                }
                            }
                        }
                    }

                    buffer[y * width + x] = (byte)(sum / count);
                }
            }
        }


        #endregion



    }

}
