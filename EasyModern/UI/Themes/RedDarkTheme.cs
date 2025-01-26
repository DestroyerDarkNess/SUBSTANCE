using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class RedDarkTheme : ITheme
    {
        public string ID { get; set; } = "theme.red-dark2";

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var colors = style.Colors;

            // Ajustes de estilo
            style.WindowPadding = new Vector2(8f, 8f);
            style.WindowRounding = 6f;
            style.ChildRounding = 5f;  // Antes "ChildWindowRounding"
            style.FramePadding = new Vector2(5f, 3f);
            style.FrameRounding = 3.0f;
            style.ItemSpacing = new Vector2(5f, 4f);
            style.ItemInnerSpacing = new Vector2(4f, 4f);
            style.IndentSpacing = 21f;
            style.ScrollbarSize = 10.0f;
            style.ScrollbarRounding = 13f;
            style.GrabMinSize = 8f;
            style.GrabRounding = 1f;
            style.WindowTitleAlign = new Vector2(0.5f, 0.5f);
            style.ButtonTextAlign = new Vector2(0.5f, 0.5f);

            // Colores
            colors[(int)ImGuiCol.Text] = new Vector4(0.95f, 0.96f, 0.98f, 1.00f);
            colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.29f, 0.29f, 0.29f, 1.00f);
            colors[(int)ImGuiCol.WindowBg] = new Vector4(0.14f, 0.14f, 0.14f, 1.00f);

            // "ChildWindowBg" => "ChildBg"
            colors[(int)ImGuiCol.ChildBg] = new Vector4(0.12f, 0.12f, 0.12f, 1.00f);

            colors[(int)ImGuiCol.PopupBg] = new Vector4(0.08f, 0.08f, 0.08f, 0.94f);
            colors[(int)ImGuiCol.Border] = new Vector4(0.14f, 0.14f, 0.14f, 1.00f);
            colors[(int)ImGuiCol.BorderShadow] = new Vector4(1.00f, 1.00f, 1.00f, 0.10f);

            colors[(int)ImGuiCol.FrameBg] = new Vector4(0.22f, 0.22f, 0.22f, 1.00f);
            colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.18f, 0.18f, 0.18f, 1.00f);
            colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.09f, 0.12f, 0.14f, 1.00f);

            colors[(int)ImGuiCol.TitleBg] = new Vector4(0.14f, 0.14f, 0.14f, 0.81f);
            colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.14f, 0.14f, 0.14f, 1.00f);
            colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.00f, 0.00f, 0.00f, 0.51f);

            colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.20f, 0.20f, 0.20f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.02f, 0.02f, 0.02f, 0.39f);
            colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.36f, 0.36f, 0.36f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.18f, 0.22f, 0.25f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.24f, 0.24f, 0.24f, 1.00f);

            // "ComboBg" no existe en builds más nuevas, se maneja con PopupBg, 
            // pero si tu versión es antigua:
            colors[(int)ImGuiCol.PopupBg] /*(ComboBg)*/= new Vector4(0.24f, 0.24f, 0.24f, 1.00f);

            colors[(int)ImGuiCol.CheckMark] = new Vector4(1.00f, 0.28f, 0.28f, 1.00f);
            colors[(int)ImGuiCol.SliderGrab] = new Vector4(1.00f, 0.28f, 0.28f, 1.00f);
            colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(1.00f, 0.28f, 0.28f, 1.00f);

            colors[(int)ImGuiCol.Button] = new Vector4(1.00f, 0.28f, 0.28f, 1.00f);
            colors[(int)ImGuiCol.ButtonHovered] = new Vector4(1.00f, 0.39f, 0.39f, 1.00f);
            colors[(int)ImGuiCol.ButtonActive] = new Vector4(1.00f, 0.21f, 0.21f, 1.00f);

            colors[(int)ImGuiCol.Header] = new Vector4(1.00f, 0.28f, 0.28f, 1.00f);
            colors[(int)ImGuiCol.HeaderHovered] = new Vector4(1.00f, 0.39f, 0.39f, 1.00f);
            colors[(int)ImGuiCol.HeaderActive] = new Vector4(1.00f, 0.21f, 0.21f, 1.00f);

            colors[(int)ImGuiCol.ResizeGrip] = new Vector4(1.00f, 0.28f, 0.28f, 1.00f);
            colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(1.00f, 0.39f, 0.39f, 1.00f);
            colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(1.00f, 0.19f, 0.19f, 1.00f);


            colors[(int)ImGuiCol.PlotLines] = new Vector4(0.61f, 0.61f, 0.61f, 1.00f);
            colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(1.00f, 0.43f, 0.35f, 1.00f);
            colors[(int)ImGuiCol.PlotHistogram] = new Vector4(1.00f, 0.21f, 0.21f, 1.00f);
            colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(1.00f, 0.18f, 0.18f, 1.00f);

            colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(1.00f, 0.32f, 0.32f, 1.00f);

            // "ModalWindowDarkening" => "ModalWindowDimBg" en builds recientes
            colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.26f, 0.26f, 0.26f, 0.60f);

            return true;
        }
    }
}
