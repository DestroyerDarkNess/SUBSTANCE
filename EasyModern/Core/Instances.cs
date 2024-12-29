using EasyImGui;
using System.Diagnostics;

namespace EasyModern.Core
{
    public enum OverlayMode
    {
        Normal = 0, // Normal mode, without the features of an overlay.
        InGame = 1, // This mode is the classic overlay, totally external but when interacting with the imgui window the game will lose focus.
        InGameEmbed = 2 // This mode requires a WndProc hook to the game process, and its behavior causes the game to not lose focus from the window.
    }

    public static class Instances
    {

        public static Overlay OverlayWindow = null;
        public static Texture.ImageManager ImageManager = null;
        public static Input.InputImguiEmu InputImguiEmu = null;
        public static Font.FontManager fontManager = null;

        public static OverlayMode OverlayMode = OverlayMode.Normal;
        public static string GameWindowTitle = "Counter-Strike";
        public static Process GameProcess = null;

        public static SDK.Cheat Cheat = null;
        public static SDK.ConfigManager Settings = SDK.ConfigManager.LoadConfig();
    }
}
