using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace EasyModern.Core.Capture
{
    public static class GameCapture
    {
        #region Estructuras y DllImport

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        // Para PrintWindow
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, uint nFlags);

        // Para CopyFromScreen
        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }

        [DllImport("user32.dll")]
        private static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);

        #endregion

        /// <summary>
        /// Captura una sub-región de la ventana **usando CopyFromScreen**.
        /// Esto incluirá cualquier overlay o ventana superpuesta.
        /// 
        /// <paramref name="regionInClientCoords"/> son coords relativas
        /// al área cliente del juego. 
        /// </summary>
        public static Bitmap CaptureClientRegion(IntPtr gameWindowHandle, Rectangle regionInClientCoords)
        {
            if (gameWindowHandle == IntPtr.Zero)
                return null;

            // Obtener tamaño client
            if (!GetClientRect(gameWindowHandle, out RECT clientRect))
                return null;

            int clientWidth = clientRect.Right - clientRect.Left;
            int clientHeight = clientRect.Bottom - clientRect.Top;
            if (clientWidth <= 0 || clientHeight <= 0)
                return null;

            // Ajustar la sub-región (clamp a [0..clientWidth, 0..clientHeight])
            Rectangle clippedRegion = Rectangle.Intersect(
                new Rectangle(0, 0, clientWidth, clientHeight),
                regionInClientCoords
            );
            if (clippedRegion.Width <= 0 || clippedRegion.Height <= 0)
                return null;

            // Crear bitmap
            Bitmap bmp = new Bitmap(clippedRegion.Width, clippedRegion.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Convertir (clippedRegion.X, clippedRegion.Y) a coords de pantalla
                POINT ptClient = new POINT { X = clippedRegion.X, Y = clippedRegion.Y };
                ClientToScreen(gameWindowHandle, ref ptClient);

                // CopyFromScreen
                g.CopyFromScreen(
                    ptClient.X,
                    ptClient.Y,
                    0,
                    0,
                    new Size(clippedRegion.Width, clippedRegion.Height),
                    CopyPixelOperation.SourceCopy
                );
            }

            return bmp;
        }

        /// <summary>
        /// Captura una sub-región del **área cliente** de la ventana 
        /// **usando PrintWindow con PW_CLIENTONLY**. 
        /// Esto excluye otras ventanas superpuestas, siempre que el juego lo soporte.
        /// Devuelve null si PrintWindow falla (p.ej. el juego no lo implementa).
        /// </summary>
        public static Bitmap CaptureClientRegionPrintWindow(IntPtr gameWindowHandle, Rectangle regionInClient)
        {
            if (gameWindowHandle == IntPtr.Zero)
                return null;

            // 1) Obtener tamaño total del cliente
            if (!GetClientRect(gameWindowHandle, out RECT clientRect))
                return null;

            int clientWidth = clientRect.Right - clientRect.Left;
            int clientHeight = clientRect.Bottom - clientRect.Top;
            if (clientWidth <= 0 || clientHeight <= 0)
                return null;

            // 2) Intersectar la sub-región
            Rectangle totalClient = new Rectangle(0, 0, clientWidth, clientHeight);
            Rectangle clipped = Rectangle.Intersect(totalClient, regionInClient);
            if (clipped.Width <= 0 || clipped.Height <= 0)
                return null;

            // 3) Creamos un bitmap del tamaño TOTAL del cliente
            using (Bitmap fullClientBmp = new Bitmap(clientWidth, clientHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb))
            {
                using (Graphics g = Graphics.FromImage(fullClientBmp))
                {
                    IntPtr hDC = g.GetHdc();
                    try
                    {
                        // PrintWindow con bandera PW_CLIENTONLY = 1
                        bool success = PrintWindow(gameWindowHandle, hDC, 1);
                        if (!success)
                        {
                            return null; // Falla => null
                        }
                    }
                    finally
                    {
                        g.ReleaseHdc(hDC);
                    }
                }

                // 4) Extraer la sub-región
                // Clonamos la parte que realmente queríamos
                Bitmap sub = fullClientBmp.Clone(clipped, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                return sub;
            }
        }

        /// <summary>
        /// (Opcional) Captura todo el cliente con PrintWindow (sin sub-región).
        /// </summary>
        public static Bitmap CaptureClientPrintWindow(IntPtr gameWindowHandle)
        {
            if (gameWindowHandle == IntPtr.Zero)
                return null;

            if (!GetClientRect(gameWindowHandle, out RECT cr))
                return null;

            int w = cr.Right - cr.Left;
            int h = cr.Bottom - cr.Top;
            if (w <= 0 || h <= 0)
                return null;

            Bitmap bmp = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                IntPtr hDC = g.GetHdc();
                try
                {
                    bool success = PrintWindow(gameWindowHandle, hDC, 1); // PW_CLIENTONLY
                    if (!success)
                    {
                        bmp.Dispose();
                        return null;
                    }
                }
                finally
                {
                    g.ReleaseHdc(hDC);
                }
            }
            return bmp;
        }
    }

}
