using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class Dracula : ITheme
    {
        public string ID { get; set; } = "theme.dracula";

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var colors = style.Colors;

            // Paleta base Dracula
            // Reference: https://draculatheme.com/contribute#color-palette
            Vector4 bg = new Vector4(0.156f, 0.164f, 0.211f, 1.00f); // #282A36
            Vector4 fg = new Vector4(0.973f, 0.973f, 0.949f, 1.00f); // #F8F8F2
            Vector4 current = new Vector4(0.267f, 0.278f, 0.353f, 1.00f); // #44475A
            Vector4 comment = new Vector4(0.384f, 0.447f, 0.643f, 1.00f); // #6272A4
            Vector4 cyan = new Vector4(0.545f, 0.914f, 0.992f, 1.00f); // #8BE9FD
            Vector4 green = new Vector4(0.313f, 0.980f, 0.482f, 1.00f); // #50FA7B
            Vector4 orange = new Vector4(1.000f, 0.722f, 0.424f, 1.00f); // #FFB86C
            Vector4 pink = new Vector4(1.000f, 0.475f, 0.776f, 1.00f); // #FF79C6
            Vector4 purple = new Vector4(0.741f, 0.576f, 0.976f, 1.00f); // #BD93F9
            Vector4 red = new Vector4(1.000f, 0.333f, 0.333f, 1.00f); // #FF5555
            Vector4 yellow = new Vector4(0.945f, 0.980f, 0.549f, 1.00f); // #F1FA8C

            // Estilo general
            style.WindowRounding = 6.0f;
            style.FrameRounding = 4.0f;
            style.ScrollbarRounding = 6.0f;
            style.GrabRounding = 4.0f;
            style.TabRounding = 4.0f;

            style.WindowPadding = new Vector2(10.0f, 10.0f);
            style.FramePadding = new Vector2(5.0f, 3.0f);
            style.ItemSpacing = new Vector2(8.0f, 5.0f);
            style.ItemInnerSpacing = new Vector2(6.0f, 4.0f);

            style.Alpha = 1.0f;   // Opacidad global
            style.DisabledAlpha = 0.60f;  // Opacidad para elementos deshabilitados

            // Colores principales
            colors[(int)ImGuiCol.Text] = fg;
            colors[(int)ImGuiCol.TextDisabled] = comment;
            colors[(int)ImGuiCol.WindowBg] = bg;
            colors[(int)ImGuiCol.ChildBg] = bg;
            colors[(int)ImGuiCol.PopupBg] = new Vector4(bg.X, bg.Y, bg.Z, 0.95f);
            if (!Core.Instances.Settings.RGB_Color) colors[(int)ImGuiCol.Border] = current;
            colors[(int)ImGuiCol.BorderShadow] = new Vector4(0, 0, 0, 0);
            colors[(int)ImGuiCol.FrameBg] = current;
            colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(current.X + 0.05f, current.Y + 0.05f, current.Z + 0.05f, 1.0f);
            colors[(int)ImGuiCol.FrameBgActive] = purple;
            colors[(int)ImGuiCol.TitleBg] = bg;
            colors[(int)ImGuiCol.TitleBgActive] = current;
            colors[(int)ImGuiCol.TitleBgCollapsed] = bg;
            colors[(int)ImGuiCol.MenuBarBg] = current;
            colors[(int)ImGuiCol.ScrollbarBg] = bg;
            colors[(int)ImGuiCol.ScrollbarGrab] = current;
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = comment;
            colors[(int)ImGuiCol.ScrollbarGrabActive] = purple;
            colors[(int)ImGuiCol.CheckMark] = green;
            colors[(int)ImGuiCol.SliderGrab] = cyan;
            colors[(int)ImGuiCol.SliderGrabActive] = green;
            colors[(int)ImGuiCol.Button] = current;
            colors[(int)ImGuiCol.ButtonHovered] = new Vector4(current.X + 0.06f, current.Y + 0.06f, current.Z + 0.06f, 1.0f);
            colors[(int)ImGuiCol.ButtonActive] = purple;
            colors[(int)ImGuiCol.Header] = current;
            colors[(int)ImGuiCol.HeaderHovered] = new Vector4(current.X + 0.05f, current.Y + 0.05f, current.Z + 0.05f, 1.0f);
            colors[(int)ImGuiCol.HeaderActive] = purple;
            colors[(int)ImGuiCol.Separator] = current;
            colors[(int)ImGuiCol.SeparatorHovered] = purple;
            colors[(int)ImGuiCol.SeparatorActive] = pink;
            colors[(int)ImGuiCol.ResizeGrip] = current;
            colors[(int)ImGuiCol.ResizeGripHovered] = purple;
            colors[(int)ImGuiCol.ResizeGripActive] = pink;

            // Tabs
            colors[(int)ImGuiCol.Tab] = current;
            colors[(int)ImGuiCol.TabHovered] = purple;

            // Plots
            colors[(int)ImGuiCol.PlotLines] = cyan;
            colors[(int)ImGuiCol.PlotLinesHovered] = pink;
            colors[(int)ImGuiCol.PlotHistogram] = green;
            colors[(int)ImGuiCol.PlotHistogramHovered] = pink;

            // Tablas
            colors[(int)ImGuiCol.TableHeaderBg] = current;
            colors[(int)ImGuiCol.TableBorderStrong] = current;
            colors[(int)ImGuiCol.TableBorderLight] = new Vector4(current.X, current.Y, current.Z, 0.5f);
            colors[(int)ImGuiCol.TableRowBg] = new Vector4(0f, 0f, 0f, 0f);
            colors[(int)ImGuiCol.TableRowBgAlt] = new Vector4(1f, 1f, 1f, 0.03f);

            // Texto seleccionado, Drag&Drop, etc.
            colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(purple.X, purple.Y, purple.Z, 0.35f);
            colors[(int)ImGuiCol.DragDropTarget] = new Vector4(pink.X, pink.Y, pink.Z, 0.90f);
            colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1f, 1f, 1f, 0.70f);
            colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.2f, 0.2f, 0.2f, 0.2f);
            colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.1f, 0.1f, 0.1f, 0.7f);

            // (Opcional) Si usas Docking
            // colors[(int)ImGuiCol.DockingPreview]      = new Vector4(pink.X, pink.Y, pink.Z, 0.30f);
            // colors[(int)ImGuiCol.DockingEmptyBg]      = bg;

            return true;
        }
    }
}
