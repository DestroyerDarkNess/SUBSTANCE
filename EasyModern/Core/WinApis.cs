using System;
using System.Runtime.InteropServices;

namespace EasyModern.Core
{
    public class WinApis
    {

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern uint SetWindowDisplayAffinity(IntPtr hwnd, uint dwAffinity);

        public const uint WDA_NONE = 0x00000000;
        public const uint WDA_EXCLUDEFROMCAPTURE = 0x00000011;

        [DllImport("user32.dll")]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, UIntPtr dwExtraInfo);

        public const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        public const uint MOUSEEVENTF_LEFTUP = 0x0004;

    }
}
