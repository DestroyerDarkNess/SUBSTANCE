using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class HighContrast : ITheme
    {
        public string ID { get; set; } = "theme.high-contrast";

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var colors = style.Colors;

            // ----------------------------
            // Colores base de alto contraste
            // ----------------------------
            // Fondo muy oscuro (negro casi puro)
            Vector4 bg = new Vector4(0.00f, 0.00f, 0.00f, 1.00f); // #000000
            // Texto muy claro (blanco puro)
            Vector4 text = new Vector4(1.00f, 1.00f, 1.00f, 1.00f); // #FFFFFF

            // Bordes grises o blancos (dependiendo de preferencia)
            Vector4 border = new Vector4(1.00f, 1.00f, 1.00f, 1.00f); // Blanco

            // Colores de acento (altamente saturados)
            // Por ejemplo, amarillo #FFFF00, verde neón #00FF00, rojo #FF0000
            Vector4 accentYellow = new Vector4(1.00f, 1.00f, 0.00f, 1.00f); // #FFFF00
            Vector4 accentGreen = new Vector4(0.00f, 1.00f, 0.00f, 1.00f); // #00FF00
            Vector4 accentRed = new Vector4(1.00f, 0.00f, 0.00f, 1.00f); // #FF0000

            // Puedes elegir un color de acento principal para CheckMark, Botones activos, etc.
            // Por defecto, usaremos el amarillo para enfatizar.
            Vector4 accentPrimary = accentYellow;
            Vector4 accentHover = new Vector4(1.00f, 1.00f, 0.00f, 0.80f); // algo menos opaco
            Vector4 accentActive = new Vector4(1.00f, 1.00f, 0.00f, 0.60f); // aún más oscuro/menos opaco

            // ----------------------------
            // Ajustes generales de estilo
            // ----------------------------
            style.WindowRounding = 2.0f;
            style.FrameRounding = 2.0f;
            style.ScrollbarRounding = 2.0f;
            style.GrabRounding = 2.0f;
            style.TabRounding = 2.0f;

            style.WindowPadding = new Vector2(10.0f, 10.0f);
            style.FramePadding = new Vector2(5.0f, 3.0f);
            style.ItemSpacing = new Vector2(8.0f, 5.0f);
            style.ItemInnerSpacing = new Vector2(6.0f, 4.0f);

            // Mantener la opacidad total
            style.Alpha = 1.0f;
            style.DisabledAlpha = 0.60f;

            // ----------------------------
            // Asignación de colores
            // ----------------------------
            colors[(int)ImGuiCol.Text] = text;
            colors[(int)ImGuiCol.TextDisabled] = new Vector4(text.X, text.Y, text.Z, 0.50f);
            colors[(int)ImGuiCol.WindowBg] = bg;
            colors[(int)ImGuiCol.ChildBg] = bg;
            colors[(int)ImGuiCol.PopupBg] = new Vector4(bg.X, bg.Y, bg.Z, 0.95f);

            if (!Core.Instances.Settings.RGB_Color) colors[(int)ImGuiCol.Border] = border;
            colors[(int)ImGuiCol.BorderShadow] = new Vector4(0f, 0f, 0f, 0f);

            // Fondo de frames
            colors[(int)ImGuiCol.FrameBg] = new Vector4(0.15f, 0.15f, 0.15f, 1.00f);
            colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.25f, 0.25f, 0.25f, 1.00f);
            colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.35f, 0.35f, 0.35f, 1.00f);

            // Título
            colors[(int)ImGuiCol.TitleBg] = bg;
            colors[(int)ImGuiCol.TitleBgActive] = new Vector4(bg.X, bg.Y, bg.Z, 1.00f);
            colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(bg.X, bg.Y, bg.Z, 0.75f);

            // Barra de menú
            colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.10f, 0.10f, 0.10f, 1.00f);

            // Scrollbar
            colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(bg.X, bg.Y, bg.Z, 1.00f);
            colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.35f, 0.35f, 0.35f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.45f, 0.45f, 0.45f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.55f, 0.55f, 0.55f, 1.00f);

            // Check, Sliders, etc.
            colors[(int)ImGuiCol.CheckMark] = accentPrimary;
            colors[(int)ImGuiCol.SliderGrab] = accentPrimary;
            colors[(int)ImGuiCol.SliderGrabActive] = accentHover;

            // Botones (muy notables)
            colors[(int)ImGuiCol.Button] = new Vector4(accentPrimary.X, accentPrimary.Y, accentPrimary.Z, 0.30f);
            colors[(int)ImGuiCol.ButtonHovered] = new Vector4(accentPrimary.X, accentPrimary.Y, accentPrimary.Z, 0.50f);
            colors[(int)ImGuiCol.ButtonActive] = new Vector4(accentPrimary.X, accentPrimary.Y, accentPrimary.Z, 0.70f);

            // Headers (collapsing header, tree node, etc.)
            colors[(int)ImGuiCol.Header] = new Vector4(accentPrimary.X, accentPrimary.Y, accentPrimary.Z, 0.30f);
            colors[(int)ImGuiCol.HeaderHovered] = new Vector4(accentPrimary.X, accentPrimary.Y, accentPrimary.Z, 0.50f);
            colors[(int)ImGuiCol.HeaderActive] = new Vector4(accentPrimary.X, accentPrimary.Y, accentPrimary.Z, 0.70f);

            // Separadores / Resize grips
            colors[(int)ImGuiCol.Separator] = border;
            colors[(int)ImGuiCol.SeparatorHovered] = accentHover;
            colors[(int)ImGuiCol.SeparatorActive] = accentActive;

            colors[(int)ImGuiCol.ResizeGrip] = new Vector4(accentPrimary.X, accentPrimary.Y, accentPrimary.Z, 0.30f);
            colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(accentPrimary.X, accentPrimary.Y, accentPrimary.Z, 0.50f);
            colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(accentPrimary.X, accentPrimary.Y, accentPrimary.Z, 0.70f);

            // Tabs
            colors[(int)ImGuiCol.Tab] = new Vector4(0.15f, 0.15f, 0.15f, 1.0f);
            colors[(int)ImGuiCol.TabHovered] = new Vector4(accentPrimary.X, accentPrimary.Y, accentPrimary.Z, 0.45f);

            // Plots
            colors[(int)ImGuiCol.PlotLines] = accentGreen;
            colors[(int)ImGuiCol.PlotLinesHovered] = accentRed;
            colors[(int)ImGuiCol.PlotHistogram] = accentYellow;
            colors[(int)ImGuiCol.PlotHistogramHovered] = accentRed;

            // Tablas
            colors[(int)ImGuiCol.TableHeaderBg] = new Vector4(0.15f, 0.15f, 0.15f, 1.0f);
            colors[(int)ImGuiCol.TableBorderStrong] = border;
            colors[(int)ImGuiCol.TableBorderLight] = new Vector4(border.X, border.Y, border.Z, 0.6f);
            colors[(int)ImGuiCol.TableRowBg] = new Vector4(0f, 0f, 0f, 0f);
            colors[(int)ImGuiCol.TableRowBgAlt] = new Vector4(1f, 1f, 1f, 0.03f);

            // Texto seleccionado, Drag&Drop, etc.
            colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(accentPrimary.X, accentPrimary.Y, accentPrimary.Z, 0.35f);
            colors[(int)ImGuiCol.DragDropTarget] = new Vector4(accentPrimary.X, accentPrimary.Y, accentPrimary.Z, 0.90f);
            colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1f, 1f, 1f, 0.70f);
            colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0f, 0f, 0f, 0.50f);
            colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0f, 0f, 0f, 0.70f);

            // (Opcional) Si usas Docking
            // colors[(int)ImGuiCol.DockingPreview]      = new Vector4(accentPrimary.X, accentPrimary.Y, accentPrimary.Z, 0.30f);
            // colors[(int)ImGuiCol.DockingEmptyBg]      = bg;

            return true;
        }
    }
}
