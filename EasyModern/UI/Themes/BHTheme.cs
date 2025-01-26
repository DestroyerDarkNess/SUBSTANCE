using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class BHTheme : ITheme
    {
        public string ID { get; set; } = "theme.BlastHack";

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var colors = style.Colors;

            // Ajustes generales de estilo
            style.WindowPadding = new Vector2(6f, 4f);
            style.WindowRounding = 5.0f;
            style.ChildRounding = 5.0f;  // Antes "ChildWindowRounding"
            style.FramePadding = new Vector2(5f, 2f);
            style.FrameRounding = 5.0f;
            style.ItemSpacing = new Vector2(7f, 5f);
            style.ItemInnerSpacing = new Vector2(1f, 1f);
            style.TouchExtraPadding = new Vector2(0f, 0f);
            style.IndentSpacing = 6.0f;
            style.ScrollbarSize = 12.0f;
            style.ScrollbarRounding = 5.0f;
            style.GrabMinSize = 20.0f;
            style.GrabRounding = 2.0f;
            style.WindowTitleAlign = new Vector2(0.5f, 0.5f);

            // Colores
            colors[(int)ImGuiCol.Text] = new Vector4(1.00f, 1.00f, 1.00f, 1.00f);
            colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.28f, 0.30f, 0.35f, 1.00f);
            colors[(int)ImGuiCol.WindowBg] = new Vector4(0.16f, 0.18f, 0.22f, 1.00f);

            // "ChildWindowBg" => "ChildBg"
            colors[(int)ImGuiCol.ChildBg] = new Vector4(0.19f, 0.22f, 0.26f, 1.00f);

            colors[(int)ImGuiCol.PopupBg] = new Vector4(0.05f, 0.05f, 0.10f, 0.90f);
            colors[(int)ImGuiCol.Border] = new Vector4(0.19f, 0.22f, 0.26f, 1.00f);
            colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);

            colors[(int)ImGuiCol.FrameBg] = new Vector4(0.19f, 0.22f, 0.26f, 1.00f);
            colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.22f, 0.25f, 0.30f, 1.00f);
            colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.22f, 0.25f, 0.29f, 1.00f);

            colors[(int)ImGuiCol.TitleBg] = new Vector4(0.19f, 0.22f, 0.26f, 1.00f);
            colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.19f, 0.22f, 0.26f, 1.00f);
            colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.19f, 0.22f, 0.26f, 0.59f);

            colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.19f, 0.22f, 0.26f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.20f, 0.25f, 0.30f, 0.60f);
            colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.41f, 0.55f, 0.78f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.49f, 0.63f, 0.86f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.49f, 0.63f, 0.86f, 1.00f);

            // "ComboBg" => obsoleto en builds recientes; 
            // se suele manejar con PopupBg, pero si tu versión lo usa:
            // colors[(int)ImGuiCol.PopupBg] = new Vector4(0.20f, 0.20f, 0.20f, 0.99f);

            colors[(int)ImGuiCol.CheckMark] = new Vector4(0.90f, 0.90f, 0.90f, 0.50f);
            colors[(int)ImGuiCol.SliderGrab] = new Vector4(1.00f, 1.00f, 1.00f, 0.30f);
            colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.80f, 0.50f, 0.50f, 1.00f);

            colors[(int)ImGuiCol.Button] = new Vector4(0.41f, 0.55f, 0.78f, 1.00f);
            colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.49f, 0.62f, 0.85f, 1.00f);
            colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.49f, 0.62f, 0.85f, 1.00f);

            colors[(int)ImGuiCol.Header] = new Vector4(0.19f, 0.22f, 0.26f, 1.00f);
            colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.22f, 0.24f, 0.28f, 1.00f);
            colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.22f, 0.24f, 0.28f, 1.00f);

            colors[(int)ImGuiCol.Separator] = new Vector4(0.41f, 0.55f, 0.78f, 1.00f);
            colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(0.41f, 0.55f, 0.78f, 1.00f);
            colors[(int)ImGuiCol.SeparatorActive] = new Vector4(0.41f, 0.55f, 0.78f, 1.00f);

            colors[(int)ImGuiCol.ResizeGrip] = new Vector4(0.41f, 0.55f, 0.78f, 1.00f);
            colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(0.49f, 0.61f, 0.83f, 1.00f);
            colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(0.49f, 0.62f, 0.83f, 1.00f);


            colors[(int)ImGuiCol.PlotLines] = new Vector4(1.00f, 1.00f, 1.00f, 1.00f);
            colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(0.90f, 0.70f, 0.00f, 1.00f);
            colors[(int)ImGuiCol.PlotHistogram] = new Vector4(0.90f, 0.70f, 0.00f, 1.00f);
            colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(1.00f, 0.60f, 0.00f, 1.00f);

            colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.41f, 0.55f, 0.78f, 1.00f);

            // "ModalWindowDarkening" => "ModalWindowDimBg" en builds recientes
            colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.16f, 0.18f, 0.22f, 0.76f);

            return true;
        }
    }
}
