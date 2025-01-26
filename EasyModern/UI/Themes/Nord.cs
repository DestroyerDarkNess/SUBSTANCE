using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class Nord : ITheme
    {
        public string ID { get; set; } = "theme.nord";

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var colors = style.Colors;

            // ----------------------------
            // Paleta Nord en Vector4(R,G,B,A)
            // ----------------------------
            Vector4 nord0 = new Vector4(0.18f, 0.20f, 0.25f, 1.00f); // #2E3440
            Vector4 nord1 = new Vector4(0.23f, 0.26f, 0.32f, 1.00f); // #3B4252
            Vector4 nord2 = new Vector4(0.26f, 0.30f, 0.37f, 1.00f); // #434C5E
            Vector4 nord3 = new Vector4(0.30f, 0.34f, 0.42f, 1.00f); // #4C566A

            Vector4 nord4 = new Vector4(0.85f, 0.87f, 0.91f, 1.00f); // #D8DEE9
            Vector4 nord5 = new Vector4(0.90f, 0.91f, 0.94f, 1.00f); // #E5E9F0
            Vector4 nord6 = new Vector4(0.93f, 0.94f, 0.96f, 1.00f); // #ECEFF4

            Vector4 nord7 = new Vector4(0.56f, 0.74f, 0.73f, 1.00f); // #8FBCBB
            Vector4 nord8 = new Vector4(0.53f, 0.75f, 0.82f, 1.00f); // #88C0D0
            Vector4 nord9 = new Vector4(0.51f, 0.63f, 0.76f, 1.00f); // #81A1C1
            Vector4 nord10 = new Vector4(0.37f, 0.51f, 0.67f, 1.00f); // #5E81AC

            Vector4 nord11 = new Vector4(0.75f, 0.38f, 0.41f, 1.00f); // #BF616A
            Vector4 nord12 = new Vector4(0.82f, 0.53f, 0.44f, 1.00f); // #D08770
            Vector4 nord13 = new Vector4(0.92f, 0.79f, 0.54f, 1.00f); // #EBCB8B
            Vector4 nord14 = new Vector4(0.64f, 0.75f, 0.55f, 1.00f); // #A3BE8C
            Vector4 nord15 = new Vector4(0.71f, 0.56f, 0.68f, 1.00f); // #B48EAD

            // ----------------------------
            // Ajustes generales de estilo
            // ----------------------------
            style.WindowRounding = 6.0f;
            style.FrameRounding = 4.0f;
            style.ScrollbarRounding = 6.0f;
            style.GrabRounding = 4.0f;
            style.TabRounding = 4.0f;

            style.WindowPadding = new Vector2(10.0f, 10.0f);
            style.FramePadding = new Vector2(5.0f, 3.0f);
            style.ItemSpacing = new Vector2(8.0f, 5.0f);
            style.ItemInnerSpacing = new Vector2(6.0f, 4.0f);

            style.Alpha = 1.0f;
            style.DisabledAlpha = 0.60f;

            // ----------------------------
            // Asignación de colores
            // ----------------------------
            colors[(int)ImGuiCol.Text] = nord6;      // Texto principal en tono claro
            colors[(int)ImGuiCol.TextDisabled] = nord3;      // Texto deshabilitado
            colors[(int)ImGuiCol.WindowBg] = nord0;      // Fondo de ventana
            colors[(int)ImGuiCol.ChildBg] = nord0;
            colors[(int)ImGuiCol.PopupBg] = new Vector4(nord1.X, nord1.Y, nord1.Z, 0.95f);
            if (!Core.Instances.Settings.RGB_Color) colors[(int)ImGuiCol.Border] = nord2;
            colors[(int)ImGuiCol.BorderShadow] = new Vector4(0, 0, 0, 0);
            colors[(int)ImGuiCol.FrameBg] = nord1;      // Fondo de frames
            colors[(int)ImGuiCol.FrameBgHovered] = nord2;
            colors[(int)ImGuiCol.FrameBgActive] = nord3;
            colors[(int)ImGuiCol.TitleBg] = nord0;
            colors[(int)ImGuiCol.TitleBgActive] = nord1;
            colors[(int)ImGuiCol.TitleBgCollapsed] = nord0;
            colors[(int)ImGuiCol.MenuBarBg] = nord1;
            colors[(int)ImGuiCol.ScrollbarBg] = nord0;
            colors[(int)ImGuiCol.ScrollbarGrab] = nord2;
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = nord3;
            colors[(int)ImGuiCol.ScrollbarGrabActive] = nord2;

            // Puedes usar la gama "Frost" (#8FBCBB, #88C0D0, etc.) para checks/sliders
            colors[(int)ImGuiCol.CheckMark] = nord8;
            colors[(int)ImGuiCol.SliderGrab] = nord8;
            colors[(int)ImGuiCol.SliderGrabActive] = nord9;

            // Botones
            colors[(int)ImGuiCol.Button] = nord2;
            colors[(int)ImGuiCol.ButtonHovered] = new Vector4(nord2.X + 0.05f, nord2.Y + 0.05f, nord2.Z + 0.05f, 1.0f);
            colors[(int)ImGuiCol.ButtonActive] = nord3;

            // Headers (e.g. CollapsingHeader, etc.)
            colors[(int)ImGuiCol.Header] = nord2;
            colors[(int)ImGuiCol.HeaderHovered] = nord3;
            colors[(int)ImGuiCol.HeaderActive] = nord3;

            // Separadores / Resize grips
            colors[(int)ImGuiCol.Separator] = nord2;
            colors[(int)ImGuiCol.SeparatorHovered] = nord8;
            colors[(int)ImGuiCol.SeparatorActive] = nord9;
            colors[(int)ImGuiCol.ResizeGrip] = nord2;
            colors[(int)ImGuiCol.ResizeGripHovered] = nord3;
            colors[(int)ImGuiCol.ResizeGripActive] = nord8;

            // Tabs
            colors[(int)ImGuiCol.Tab] = nord1;
            colors[(int)ImGuiCol.TabHovered] = nord2;

            // Plots (gráficos)
            colors[(int)ImGuiCol.PlotLines] = nord8;
            colors[(int)ImGuiCol.PlotLinesHovered] = nord11;   // Aurora: #BF616A (rojo)
            colors[(int)ImGuiCol.PlotHistogram] = nord13;   // Aurora: #EBCB8B (amarillo)
            colors[(int)ImGuiCol.PlotHistogramHovered] = nord12;   // Aurora: #D08770 (naranja)

            // Tablas
            colors[(int)ImGuiCol.TableHeaderBg] = nord1;
            colors[(int)ImGuiCol.TableBorderStrong] = nord2;
            colors[(int)ImGuiCol.TableBorderLight] = new Vector4(nord2.X, nord2.Y, nord2.Z, 0.6f);
            colors[(int)ImGuiCol.TableRowBg] = new Vector4(0f, 0f, 0f, 0f);
            colors[(int)ImGuiCol.TableRowBgAlt] = new Vector4(1f, 1f, 1f, 0.03f);

            // Texto seleccionado, Drag&Drop, etc.
            colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(nord8.X, nord8.Y, nord8.Z, 0.35f);
            colors[(int)ImGuiCol.DragDropTarget] = new Vector4(nord8.X, nord8.Y, nord8.Z, 0.90f);
            colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1f, 1f, 1f, 0.70f);
            colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.2f, 0.2f, 0.2f, 0.2f);
            colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(nord0.X, nord0.Y, nord0.Z, 0.70f);

            // (Opcional) Si usas Docking
            // colors[(int)ImGuiCol.DockingPreview]      = new Vector4(nord8.X, nord8.Y, nord8.Z, 0.30f);
            // colors[(int)ImGuiCol.DockingEmptyBg]      = nord0;

            return true;
        }
    }
}
