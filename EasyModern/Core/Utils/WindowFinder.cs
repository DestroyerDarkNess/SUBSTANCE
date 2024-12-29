using System;
using System.Runtime.InteropServices;
using System.Text;

namespace EasyModern.Core.Utils
{
    public class WindowFinder
    {

        #region " Pinvoke "
        [DllImport("user32.dll")] private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")] private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")] private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        #endregion

        #region " Declares "

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        public delegate void OnMemoryReady(object sender, bool Status, int ProcessId = 0);
        public event OnMemoryReady OnProcReady = null;

        private string TargetWindowName { get; set; } = string.Empty;

        private int Count = 0;

        #endregion


        public void Find(string window_title)
        {
            Count = 0;
            TargetWindowName = window_title;
            if (EnumWindows(EnumWindowsCallback, IntPtr.Zero) == true && Count == 0) { OnProcReady?.Invoke(this, false, 0); }
        }

        private bool EnumWindowsCallback(IntPtr hWnd, IntPtr lParam)
        {
            var windowTitle = new StringBuilder(256);
            GetWindowText(hWnd, windowTitle, windowTitle.Capacity);

            if (string.Equals(TargetWindowName, windowTitle.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                Count += 1;

                int processId = 0;
                GetWindowThreadProcessId(hWnd, out processId);

                OnProcReady?.Invoke(this, processId != 0, processId);

                return false;
            }

            return true;
        }

    }
}
