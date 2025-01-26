using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class CommunistRed : ITheme
    {
        public string ID { get; set; } = "theme.communistred";

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var colors = style.Colors;

            colors[(int)ImGuiCol.Text] = new Vector4(1.00f, 0.84f, 0.00f, 1.00f); // Dorado para el texto
            colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.60f, 0.60f, 0.60f, 1.00f);
            colors[(int)ImGuiCol.WindowBg] = new Vector4(0.10f, 0.00f, 0.00f, 1.00f); // Fondo rojo oscuro
            colors[(int)ImGuiCol.ChildBg] = new Vector4(0.08f, 0.00f, 0.00f, 1.00f);
            colors[(int)ImGuiCol.PopupBg] = new Vector4(0.15f, 0.00f, 0.00f, 1.00f);
            if (!Core.Instances.Settings.RGB_Color) colors[(int)ImGuiCol.Border] = new Vector4(0.80f, 0.10f, 0.10f, 1.00f); // Rojo para bordes
            colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
            colors[(int)ImGuiCol.FrameBg] = new Vector4(0.20f, 0.00f, 0.00f, 1.00f);
            colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.60f, 0.00f, 0.00f, 1.00f); // Rojo intenso al hover
            colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.90f, 0.00f, 0.00f, 1.00f); // Rojo vibrante activo
            colors[(int)ImGuiCol.TitleBg] = new Vector4(0.60f, 0.00f, 0.00f, 1.00f);
            colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.90f, 0.00f, 0.00f, 1.00f);
            colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.40f, 0.00f, 0.00f, 1.00f);
            colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.15f, 0.00f, 0.00f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.10f, 0.00f, 0.00f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.60f, 0.00f, 0.00f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.80f, 0.00f, 0.00f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.90f, 0.00f, 0.00f, 1.00f);
            colors[(int)ImGuiCol.CheckMark] = new Vector4(1.00f, 0.84f, 0.00f, 1.00f); // Dorado para el checkmark
            colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.90f, 0.00f, 0.00f, 1.00f);
            colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(1.00f, 0.20f, 0.20f, 1.00f);
            colors[(int)ImGuiCol.Button] = new Vector4(0.60f, 0.00f, 0.00f, 1.00f);
            colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.80f, 0.10f, 0.10f, 1.00f);
            colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.90f, 0.20f, 0.20f, 1.00f);
            colors[(int)ImGuiCol.Header] = new Vector4(0.60f, 0.00f, 0.00f, 1.00f);
            colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.80f, 0.10f, 0.10f, 1.00f);
            colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.90f, 0.20f, 0.20f, 1.00f);
            colors[(int)ImGuiCol.Separator] = new Vector4(0.80f, 0.10f, 0.10f, 1.00f);
            colors[(int)ImGuiCol.ResizeGrip] = new Vector4(0.60f, 0.00f, 0.00f, 1.00f);
            colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(0.80f, 0.10f, 0.10f, 1.00f);
            colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(0.90f, 0.20f, 0.20f, 1.00f);

            style.WindowRounding = 5.0f;
            style.FrameRounding = 3.0f;
            style.GrabRounding = 3.0f;
            style.ScrollbarRounding = 3.0f;
            style.TabRounding = 3.0f;

            style.WindowPadding = new Vector2(10.0f, 10.0f);
            style.FramePadding = new Vector2(5.0f, 5.0f);
            style.ItemSpacing = new Vector2(8.0f, 4.0f);
            return true;
        }
    }

}
