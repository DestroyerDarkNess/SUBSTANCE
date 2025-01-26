using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class Codz : ITheme
    {
        public string ID { get; set; } = "theme.codz";

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var colors = style.Colors;

            style.WindowRounding = 5.3f;
            style.FrameRounding = 2.3f;
            style.ScrollbarRounding = 0.0f;

            colors[(int)ImGuiCol.Text] = new Vector4(0.90f, 0.90f, 0.90f, 0.90f);
            colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.60f, 0.60f, 0.60f, 1.00f);
            colors[(int)ImGuiCol.WindowBg] = new Vector4(0.09f, 0.09f, 0.15f, 1.00f);
            colors[(int)ImGuiCol.ChildBg] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
            colors[(int)ImGuiCol.PopupBg] = new Vector4(0.05f, 0.05f, 0.10f, 0.85f);
            if (!Core.Instances.Settings.RGB_Color) colors[(int)ImGuiCol.Border] = new Vector4(0.70f, 0.70f, 0.70f, 0.65f);
            colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
            colors[(int)ImGuiCol.FrameBg] = new Vector4(0.00f, 0.00f, 0.01f, 1.00f);
            colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.90f, 0.80f, 0.80f, 0.40f);
            colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.90f, 0.65f, 0.65f, 0.45f);
            colors[(int)ImGuiCol.TitleBg] = new Vector4(0.00f, 0.00f, 0.00f, 0.83f);
            colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.40f, 0.40f, 0.80f, 0.20f);
            colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.00f, 0.00f, 0.00f, 0.87f);
            colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.01f, 0.01f, 0.02f, 0.80f);
            colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.20f, 0.25f, 0.30f, 0.60f);
            colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.55f, 0.53f, 0.55f, 0.51f);
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.56f, 0.56f, 0.56f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.56f, 0.56f, 0.56f, 0.91f);
            colors[(int)ImGuiCol.CheckMark] = new Vector4(0.90f, 0.90f, 0.90f, 0.83f);
            colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.70f, 0.70f, 0.70f, 0.62f);
            colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.30f, 0.30f, 0.30f, 0.84f);
            colors[(int)ImGuiCol.Button] = new Vector4(0.48f, 0.72f, 0.89f, 0.49f);
            colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.50f, 0.69f, 0.99f, 0.68f);
            colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.80f, 0.50f, 0.50f, 1.00f);
            colors[(int)ImGuiCol.Header] = new Vector4(0.30f, 0.69f, 1.00f, 0.53f);
            colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.44f, 0.61f, 0.86f, 1.00f);
            colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.38f, 0.62f, 0.83f, 1.00f);
            colors[(int)ImGuiCol.ResizeGrip] = new Vector4(1.00f, 1.00f, 1.00f, 0.85f);
            colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(1.00f, 1.00f, 1.00f, 0.60f);
            colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(1.00f, 1.00f, 1.00f, 0.90f);
            colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.00f, 0.00f, 1.00f, 0.35f);
            colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.20f, 0.20f, 0.20f, 0.35f);

            return true;
        }
    }

}
