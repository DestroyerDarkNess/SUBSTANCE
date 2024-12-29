using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EasyModern.Core.Utils
{
    public class Helper
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
            int X, int Y, int cx, int cy, uint uFlags);

        private const int GWL_STYLE = -16;
        private const int WS_BORDER = 0x00800000;
        private const int WS_CAPTION = 0x00C00000;
        private const int WS_SYSMENU = 0x00080000;
        private const int WS_THICKFRAME = 0x00040000;

        private const int SWP_NOZORDER = 0x0004;
        private const int SWP_NOACTIVATE = 0x0010;

        public static void MakeWindowBorderless(IntPtr hWnd)
        {
            // 1) Quitar estilos de la ventana (barra, bordes, etc.)
            int style = GetWindowLong(hWnd, GWL_STYLE);
            style &= ~WS_BORDER;
            style &= ~WS_CAPTION;
            style &= ~WS_SYSMENU;
            style &= ~WS_THICKFRAME;
            SetWindowLong(hWnd, GWL_STYLE, style);

            // 2) Ajustar posición y tamaño a la resolución actual
            var screenWidth = Screen.PrimaryScreen.Bounds.Width;
            var screenHeight = Screen.PrimaryScreen.Bounds.Height;

            SetWindowPos(hWnd, IntPtr.Zero,
                         0, 0,
                         screenWidth, screenHeight,
                         SWP_NOZORDER | SWP_NOACTIVATE);
        }

        public enum Measure
        {
            Milliseconds = 1,
            Seconds = 2,
            Minutes = 3,
            Hours = 4
        }

        public static void Sleep(long duration, Measure measure = Measure.Seconds)
        {
            DateTime startTime = DateTime.Now;

            switch (measure)
            {
                case Measure.Milliseconds:
                    while ((DateTime.Now - startTime).TotalMilliseconds < duration)
                    {
                        Application.DoEvents();
                    }
                    break;
                case Measure.Seconds:
                    while ((DateTime.Now - startTime).TotalSeconds < duration)
                    {
                        Application.DoEvents();
                    }
                    break;
                case Measure.Minutes:
                    while ((DateTime.Now - startTime).TotalMinutes < duration)
                    {
                        Application.DoEvents();
                    }
                    break;
                case Measure.Hours:
                    while ((DateTime.Now - startTime).TotalHours < duration)
                    {
                        Application.DoEvents();
                    }
                    break;
                default:
                    break;
            }
        }



    }

}

