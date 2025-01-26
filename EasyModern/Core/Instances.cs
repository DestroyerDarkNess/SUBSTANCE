using EasyImGui;
using EasyModern.Core.Drawing;
using EasyModern.Core.Effects;
using EasyModern.Core.Model;
using EasyModern.Core.Utils;
using System.Collections.Generic;
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
        public static string GameWindowTitle = "Counter-Strike"; // "Counter-Strike"; // "Form1";  
        public static Process GameProcess = null;
        public static SDK.Cheat Cheat = null;
        public static SDK.ConfigManager Settings = SDK.ConfigManager.LoadConfig();
        //Effects/Shaders 
        public static ColorRGB RGBColors = new ColorRGB() { transitionSpeed = 2.0f };
        public static List<ITheme> Themes = null;
        public static Themer.ThemerApplier Theme = null;
        public static TextureDrawing TextureDrawing = null;
        public static TextureEffectManager TextureEffectManager = null;
    }
}
