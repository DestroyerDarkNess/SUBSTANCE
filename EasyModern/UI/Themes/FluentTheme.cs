using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class FluentTheme : ITheme
    {
        public string ID { get; set; } = "theme.fluent";

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var colors = style.Colors;

            // Colores base (inspirados en tu snippet)
            colors[(int)ImGuiCol.Text] = new Vector4(0.85f, 0.85f, 0.85f, 1.00f);
            colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.50f, 0.50f, 0.50f, 1.00f);
            colors[(int)ImGuiCol.WindowBg] = new Vector4(0.20f, 0.20f, 0.20f, 0.50f);
            colors[(int)ImGuiCol.ChildBg] = new Vector4(0.18f, 0.18f, 0.18f, 0.0f);
            colors[(int)ImGuiCol.PopupBg] = new Vector4(0.22f, 0.22f, 0.22f, 0.80f);
            if (!Core.Instances.Settings.RGB_Color) colors[(int)ImGuiCol.Border] = new Vector4(0.40f, 0.40f, 0.40f, 0.60f);
            colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
            colors[(int)ImGuiCol.FrameBg] = new Vector4(0.30f, 0.30f, 0.30f, 0.70f);
            colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.40f, 0.40f, 0.40f, 0.80f);
            colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.50f, 0.50f, 0.50f, 0.90f);
            colors[(int)ImGuiCol.TitleBg] = new Vector4(0.22f, 0.22f, 0.22f, 0.85f);
            colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.30f, 0.30f, 0.30f, 0.90f);
            colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.20f, 0.20f, 0.20f, 0.50f);
            colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.18f, 0.18f, 0.18f, 0.65f);
            colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.15f, 0.15f, 0.15f, 0.50f);
            colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.40f, 0.40f, 0.40f, 0.50f);
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.45f, 0.45f, 0.45f, 0.50f);
            colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.50f, 0.50f, 0.50f, 0.80f);
            colors[(int)ImGuiCol.CheckMark] = new Vector4(0.35f, 0.75f, 0.95f, 1.00f);
            colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.30f, 0.70f, 0.90f, 1.00f);
            colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.20f, 0.60f, 0.80f, 1.00f);
            colors[(int)ImGuiCol.Button] = new Vector4(0.30f, 0.30f, 0.30f, 0.70f);
            colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.40f, 0.40f, 0.40f, 0.80f);
            colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.50f, 0.50f, 0.50f, 0.90f);
            colors[(int)ImGuiCol.Header] = new Vector4(0.25f, 0.25f, 0.25f, 0.80f);
            colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.35f, 0.35f, 0.35f, 0.90f);
            colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.45f, 0.45f, 0.45f, 1.00f);
            colors[(int)ImGuiCol.Separator] = new Vector4(0.40f, 0.40f, 0.40f, 0.60f);
            colors[(int)ImGuiCol.ResizeGrip] = new Vector4(0.40f, 0.40f, 0.40f, 0.80f);
            colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(0.50f, 0.50f, 0.50f, 0.90f);
            colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(0.60f, 0.60f, 0.60f, 1.00f);

            // ------- Colores para TABS (faltaban en tu snippet) -------
            colors[(int)ImGuiCol.Tab] = new Vector4(0.25f, 0.25f, 0.25f, 0.80f);
            colors[(int)ImGuiCol.TabHovered] = new Vector4(0.35f, 0.35f, 0.35f, 0.90f);

            // ------- Colores para gráficas (Plots) -------
            colors[(int)ImGuiCol.PlotLines] = new Vector4(0.35f, 0.75f, 0.95f, 1.00f);
            colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(0.45f, 0.85f, 1.00f, 1.00f);
            colors[(int)ImGuiCol.PlotHistogram] = new Vector4(0.35f, 0.75f, 0.95f, 1.00f);
            colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(0.45f, 0.85f, 1.00f, 1.00f);

            // ------- Colores para Tablas -------
            colors[(int)ImGuiCol.TableHeaderBg] = new Vector4(0.30f, 0.30f, 0.30f, 0.65f);
            colors[(int)ImGuiCol.TableBorderStrong] = new Vector4(0.40f, 0.40f, 0.40f, 0.60f);
            colors[(int)ImGuiCol.TableBorderLight] = new Vector4(0.40f, 0.40f, 0.40f, 0.25f);
            colors[(int)ImGuiCol.TableRowBg] = new Vector4(0f, 0f, 0f, 0f);
            colors[(int)ImGuiCol.TableRowBgAlt] = new Vector4(1f, 1f, 1f, 0.03f);

            // ------- Otros (Texto seleccionado, Drag/Drop, etc.) -------
            colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.35f, 0.75f, 0.95f, 0.35f);
            colors[(int)ImGuiCol.DragDropTarget] = new Vector4(0.35f, 0.75f, 0.95f, 0.90f);
            colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1f, 1f, 1f, 0.70f);
            colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.20f, 0.20f, 0.20f, 0.50f);
            colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.20f, 0.20f, 0.20f, 0.70f);

            // (Opcional) Si usas Docking
            //colors[(int)ImGuiCol.DockingPreview]       = new Vector4(0.35f, 0.75f, 0.95f, 0.30f);
            //colors[(int)ImGuiCol.DockingEmptyBg]       = new Vector4(0.20f, 0.20f, 0.20f, 0.50f);

            // ------- Ajustes de estilo (ya en tu snippet) -------
            style.WindowRounding = 5.0f;
            style.FrameRounding = 4.0f;
            style.GrabRounding = 4.0f;
            style.ScrollbarRounding = 5.0f;
            style.TabRounding = 5.0f;

            style.WindowPadding = new Vector2(12.0f, 12.0f);
            style.FramePadding = new Vector2(6.0f, 6.0f);
            style.ItemSpacing = new Vector2(10.0f, 6.0f);
            style.ItemInnerSpacing = new Vector2(6.0f, 6.0f);

            style.Alpha = 0.50f; // Transparencia global
            style.DisabledAlpha = 0.50f;


            return true;
        }
    }

}
