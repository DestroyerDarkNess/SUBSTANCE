using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class GruvboxDark : ITheme
    {
        public string ID { get; set; } = "theme.gruvbox-dark";

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var colors = style.Colors;

            // ----------------------------
            // Paleta Gruvbox Dark
            // ----------------------------
            // Background (bg)
            Vector4 bg0 = new Vector4(0.16f, 0.16f, 0.16f, 1.00f); // #282828
            Vector4 bg1 = new Vector4(0.235f, 0.219f, 0.214f, 1.00f); // #3c3836
            Vector4 bg2 = new Vector4(0.314f, 0.289f, 0.277f, 1.00f); // #504945
            Vector4 bg3 = new Vector4(0.401f, 0.360f, 0.329f, 1.00f); // #665c54

            // Foreground / Texto (fg)
            Vector4 fg0 = new Vector4(0.92f, 0.86f, 0.70f, 1.00f); // #ebdbb2
            Vector4 fg1 = new Vector4(0.84f, 0.77f, 0.63f, 1.00f); // #d5c4a1
            Vector4 fg2 = new Vector4(0.74f, 0.68f, 0.57f, 1.00f); // #bdae93
            Vector4 fg3 = new Vector4(0.98f, 0.95f, 0.78f, 1.00f); // #fbf1c7

            // Colores "brillantes" (Aurora)
            Vector4 red = new Vector4(0.98f, 0.29f, 0.20f, 1.00f); // #fb4934
            Vector4 orange = new Vector4(1.00f, 0.50f, 0.10f, 1.00f); // #fe8019
            Vector4 yellow = new Vector4(0.98f, 0.74f, 0.18f, 1.00f); // #fabd2f
            Vector4 green = new Vector4(0.72f, 0.73f, 0.15f, 1.00f); // #b8bb26
            Vector4 aqua = new Vector4(0.55f, 0.75f, 0.49f, 1.00f); // #8ec07c
            Vector4 blue = new Vector4(0.51f, 0.64f, 0.59f, 1.00f); // #83a598
            Vector4 purple = new Vector4(0.83f, 0.53f, 0.61f, 1.00f); // #d3869b

            // Opcional: Color “faded orange” (#d65d0e) u otro (para variedad):
            Vector4 fadeOrange = new Vector4(0.84f, 0.36f, 0.05f, 1.00f); // #d65d0e

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
            colors[(int)ImGuiCol.Text] = fg0;
            colors[(int)ImGuiCol.TextDisabled] = fg2;
            colors[(int)ImGuiCol.WindowBg] = bg0;
            colors[(int)ImGuiCol.ChildBg] = bg0;
            colors[(int)ImGuiCol.PopupBg] = new Vector4(bg1.X, bg1.Y, bg1.Z, 0.95f);
            if (!Core.Instances.Settings.RGB_Color) colors[(int)ImGuiCol.Border] = bg2;
            colors[(int)ImGuiCol.BorderShadow] = new Vector4(0, 0, 0, 0);
            colors[(int)ImGuiCol.FrameBg] = bg1;
            colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(bg2.X + 0.05f, bg2.Y + 0.05f, bg2.Z + 0.05f, 1.0f);
            colors[(int)ImGuiCol.FrameBgActive] = bg3;
            colors[(int)ImGuiCol.TitleBg] = bg0;
            colors[(int)ImGuiCol.TitleBgActive] = bg1;
            colors[(int)ImGuiCol.TitleBgCollapsed] = bg0;
            colors[(int)ImGuiCol.MenuBarBg] = bg1;
            colors[(int)ImGuiCol.ScrollbarBg] = bg0;
            colors[(int)ImGuiCol.ScrollbarGrab] = bg2;
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = bg3;
            colors[(int)ImGuiCol.ScrollbarGrabActive] = bg2;
            colors[(int)ImGuiCol.CheckMark] = green;
            colors[(int)ImGuiCol.SliderGrab] = aqua;
            colors[(int)ImGuiCol.SliderGrabActive] = green;
            colors[(int)ImGuiCol.Button] = bg2;
            colors[(int)ImGuiCol.ButtonHovered] = new Vector4(bg2.X + 0.07f, bg2.Y + 0.07f, bg2.Z + 0.07f, 1.0f);
            colors[(int)ImGuiCol.ButtonActive] = bg3;
            colors[(int)ImGuiCol.Header] = bg2;
            colors[(int)ImGuiCol.HeaderHovered] = bg3;
            colors[(int)ImGuiCol.HeaderActive] = aqua;
            colors[(int)ImGuiCol.Separator] = bg2;
            colors[(int)ImGuiCol.SeparatorHovered] = aqua;
            colors[(int)ImGuiCol.SeparatorActive] = blue;
            colors[(int)ImGuiCol.ResizeGrip] = bg2;
            colors[(int)ImGuiCol.ResizeGripHovered] = aqua;
            colors[(int)ImGuiCol.ResizeGripActive] = blue;

            // Tabs
            colors[(int)ImGuiCol.Tab] = bg1;
            colors[(int)ImGuiCol.TabHovered] = bg2;

            // Plots
            colors[(int)ImGuiCol.PlotLines] = aqua;
            colors[(int)ImGuiCol.PlotLinesHovered] = red;
            colors[(int)ImGuiCol.PlotHistogram] = yellow;
            colors[(int)ImGuiCol.PlotHistogramHovered] = orange;

            // Tablas
            colors[(int)ImGuiCol.TableHeaderBg] = bg1;
            colors[(int)ImGuiCol.TableBorderStrong] = bg2;
            colors[(int)ImGuiCol.TableBorderLight] = new Vector4(bg2.X, bg2.Y, bg2.Z, 0.6f);
            colors[(int)ImGuiCol.TableRowBg] = new Vector4(0f, 0f, 0f, 0f);
            colors[(int)ImGuiCol.TableRowBgAlt] = new Vector4(1f, 1f, 1f, 0.03f);

            // Texto seleccionado, Drag&Drop, etc.
            colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(aqua.X, aqua.Y, aqua.Z, 0.35f);
            colors[(int)ImGuiCol.DragDropTarget] = new Vector4(aqua.X, aqua.Y, aqua.Z, 0.90f);
            colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1f, 1f, 1f, 0.70f);
            colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.2f, 0.2f, 0.2f, 0.2f);
            colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(bg0.X, bg0.Y, bg0.Z, 0.7f);

            return true;
        }
    }
}
