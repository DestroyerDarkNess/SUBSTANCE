using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class GrayStyleTheme : ITheme
    {
        public string ID { get; set; } = "theme.gray-style";

        public bool Apply()
        {
            // Obtenemos el estilo
            var style = ImGui.GetStyle();
            var colors = style.Colors;

            //
            // Ajustes de estilo (Padding, Rounding, etc.)
            //
            style.WindowPadding = new Vector2(5f, 5f);
            style.FramePadding = new Vector2(5f, 5f);
            style.ItemSpacing = new Vector2(5f, 5f);
            style.ItemInnerSpacing = new Vector2(2f, 2f);
            style.TouchExtraPadding = new Vector2(0f, 0f);
            style.IndentSpacing = 0f;
            style.ScrollbarSize = 10f;
            style.GrabMinSize = 10f;

            //
            // Bordes
            //
            style.WindowBorderSize = 1f;
            style.ChildBorderSize = 1f;
            style.PopupBorderSize = 1f;
            style.FrameBorderSize = 1f;
            style.TabBorderSize = 1f;

            //
            // Redondeo
            //
            style.WindowRounding = 5f;
            style.ChildRounding = 5f;
            style.FrameRounding = 5f;
            style.PopupRounding = 5f;
            style.ScrollbarRounding = 5f;
            style.GrabRounding = 5f;
            style.TabRounding = 5f;

            //
            // Alineaciones
            //
            style.WindowTitleAlign = new Vector2(0.5f, 0.5f);
            style.ButtonTextAlign = new Vector2(0.5f, 0.5f);
            style.SelectableTextAlign = new Vector2(0.5f, 0.5f);

            //
            // Colores
            //
            colors[(int)ImGuiCol.Text] = new Vector4(1.00f, 1.00f, 1.00f, 1.00f);
            colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.50f, 0.50f, 0.50f, 1.00f);
            colors[(int)ImGuiCol.WindowBg] = new Vector4(0.07f, 0.07f, 0.07f, 1.00f);
            colors[(int)ImGuiCol.ChildBg] = new Vector4(0.07f, 0.07f, 0.07f, 1.00f);
            colors[(int)ImGuiCol.PopupBg] = new Vector4(0.07f, 0.07f, 0.07f, 1.00f);
            colors[(int)ImGuiCol.Border] = new Vector4(0.25f, 0.25f, 0.26f, 0.54f);
            colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
            colors[(int)ImGuiCol.FrameBg] = new Vector4(0.12f, 0.12f, 0.12f, 1.00f);
            colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.25f, 0.25f, 0.26f, 1.00f);
            colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.25f, 0.25f, 0.26f, 1.00f);
            colors[(int)ImGuiCol.TitleBg] = new Vector4(0.12f, 0.12f, 0.12f, 1.00f);
            colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.12f, 0.12f, 0.12f, 1.00f);
            colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.12f, 0.12f, 0.12f, 1.00f);
            colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.12f, 0.12f, 0.12f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.12f, 0.12f, 0.12f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.00f, 0.00f, 0.00f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.41f, 0.41f, 0.41f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.51f, 0.51f, 0.51f, 1.00f);
            colors[(int)ImGuiCol.CheckMark] = new Vector4(1.00f, 1.00f, 1.00f, 1.00f);
            colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.21f, 0.20f, 0.20f, 1.00f);
            colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.21f, 0.20f, 0.20f, 1.00f);
            colors[(int)ImGuiCol.Button] = new Vector4(0.12f, 0.12f, 0.12f, 1.00f);
            colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.21f, 0.20f, 0.20f, 1.00f);
            colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.41f, 0.41f, 0.41f, 1.00f);
            colors[(int)ImGuiCol.Header] = new Vector4(0.12f, 0.12f, 0.12f, 1.00f);
            colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.20f, 0.20f, 0.20f, 1.00f);
            colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.47f, 0.47f, 0.47f, 1.00f);
            colors[(int)ImGuiCol.Separator] = new Vector4(0.12f, 0.12f, 0.12f, 1.00f);
            colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(0.12f, 0.12f, 0.12f, 1.00f);
            colors[(int)ImGuiCol.SeparatorActive] = new Vector4(0.12f, 0.12f, 0.12f, 1.00f);
            colors[(int)ImGuiCol.ResizeGrip] = new Vector4(1.00f, 1.00f, 1.00f, 0.25f);
            colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(1.00f, 1.00f, 1.00f, 0.67f);
            colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(1.00f, 1.00f, 1.00f, 0.95f);

            colors[(int)ImGuiCol.Tab] = new Vector4(0.12f, 0.12f, 0.12f, 1.00f);
            colors[(int)ImGuiCol.TabHovered] = new Vector4(0.28f, 0.28f, 0.28f, 1.00f);

            colors[(int)ImGuiCol.PlotLines] = new Vector4(0.61f, 0.61f, 0.61f, 1.00f);
            colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(1.00f, 0.43f, 0.35f, 1.00f);
            colors[(int)ImGuiCol.PlotHistogram] = new Vector4(0.90f, 0.70f, 0.00f, 1.00f);
            colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(1.00f, 0.60f, 0.00f, 1.00f);

            colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(1.00f, 0.00f, 0.00f, 0.35f);
            colors[(int)ImGuiCol.DragDropTarget] = new Vector4(1.00f, 1.00f, 0.00f, 0.90f);
            colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1.00f, 1.00f, 1.00f, 0.70f);
            colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.80f, 0.80f, 0.80f, 0.20f);
            colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.00f, 0.00f, 0.00f, 0.70f);

            return true;
        }
    }
}
