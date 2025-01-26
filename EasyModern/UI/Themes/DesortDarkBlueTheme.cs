using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class DesortDarkBlueTheme : ITheme
    {
        public string ID { get; set; } = "theme.desort-dark-blue";

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var colors = style.Colors;

            //
            // Ajustes generales de estilo
            //
            style.WindowRounding = 4.0f;
            style.FrameRounding = 3.0f;
            style.ScrollbarRounding = 4.0f;
            style.GrabRounding = 3.0f;
            style.TabRounding = 3.0f;

            style.WindowPadding = new Vector2(10.0f, 10.0f);
            style.FramePadding = new Vector2(6.0f, 3.0f);
            style.ItemSpacing = new Vector2(8.0f, 5.0f);
            style.ItemInnerSpacing = new Vector2(6.0f, 4.0f);

            style.Alpha = 1.0f;
            style.DisabledAlpha = 0.60f;

            //
            // Colores base (fondo oscuro, texto claro, bordes)
            //
            Vector4 bgColor = new Vector4(0.13f, 0.14f, 0.20f, 1.00f); // #212234 ~ Gris-azulado oscuro
            Vector4 textColor = new Vector4(0.85f, 0.87f, 0.90f, 1.00f); // #D9DCE6 ~ Gris claro
            Vector4 borderColor = new Vector4(0.35f, 0.38f, 0.45f, 1.00f); // #5A6072

            //
            // Acento azul
            //
            Vector4 accentBlue = new Vector4(0.26f, 0.57f, 0.96f, 1.00f); // #4292F5 (Azul vivo)
            Vector4 accentHover = new Vector4(0.24f, 0.52f, 0.86f, 1.00f); // Un poco más oscuro
            Vector4 accentActive = new Vector4(0.20f, 0.40f, 0.70f, 1.00f); // Más oscuro

            //
            // Asignar colores
            //

            // Texto y fondo
            colors[(int)ImGuiCol.Text] = textColor;
            colors[(int)ImGuiCol.TextDisabled] = new Vector4(textColor.X, textColor.Y, textColor.Z, 0.40f);
            colors[(int)ImGuiCol.WindowBg] = bgColor;
            colors[(int)ImGuiCol.ChildBg] = bgColor;
            colors[(int)ImGuiCol.PopupBg] = new Vector4(bgColor.X, bgColor.Y, bgColor.Z, 0.98f);

            // Bordes
            if (!Core.Instances.Settings.RGB_Color) colors[(int)ImGuiCol.Border] = borderColor;
            colors[(int)ImGuiCol.BorderShadow] = new Vector4(0, 0, 0, 0);

            // Fondo de frames
            colors[(int)ImGuiCol.FrameBg] = new Vector4(bgColor.X + 0.07f, bgColor.Y + 0.07f, bgColor.Z + 0.07f, 1.0f);
            colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(bgColor.X + 0.10f, bgColor.Y + 0.10f, bgColor.Z + 0.10f, 1.0f);
            colors[(int)ImGuiCol.FrameBgActive] = new Vector4(bgColor.X + 0.15f, bgColor.Y + 0.15f, bgColor.Z + 0.15f, 1.0f);

            // Título
            colors[(int)ImGuiCol.TitleBg] = bgColor;
            colors[(int)ImGuiCol.TitleBgActive] = new Vector4(bgColor.X + 0.05f, bgColor.Y + 0.05f, bgColor.Z + 0.05f, 1.0f);
            colors[(int)ImGuiCol.TitleBgCollapsed] = bgColor;

            // Menú, Scrollbar
            colors[(int)ImGuiCol.MenuBarBg] = new Vector4(bgColor.X + 0.02f, bgColor.Y + 0.02f, bgColor.Z + 0.02f, 1.0f);
            colors[(int)ImGuiCol.ScrollbarBg] = bgColor;
            colors[(int)ImGuiCol.ScrollbarGrab] = borderColor;
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(borderColor.X, borderColor.Y, borderColor.Z, 0.8f);
            colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(borderColor.X, borderColor.Y, borderColor.Z, 0.6f);

            //
            // Acento azul
            //

            // Check, Sliders
            colors[(int)ImGuiCol.CheckMark] = accentBlue;
            colors[(int)ImGuiCol.SliderGrab] = accentBlue;
            colors[(int)ImGuiCol.SliderGrabActive] = accentHover;

            // Botones
            colors[(int)ImGuiCol.Button] = new Vector4(accentBlue.X, accentBlue.Y, accentBlue.Z, 0.15f);
            colors[(int)ImGuiCol.ButtonHovered] = new Vector4(accentBlue.X, accentBlue.Y, accentBlue.Z, 0.35f);
            colors[(int)ImGuiCol.ButtonActive] = new Vector4(accentBlue.X, accentBlue.Y, accentBlue.Z, 0.60f);

            // Headers
            colors[(int)ImGuiCol.Header] = new Vector4(accentBlue.X, accentBlue.Y, accentBlue.Z, 0.15f);
            colors[(int)ImGuiCol.HeaderHovered] = new Vector4(accentBlue.X, accentBlue.Y, accentBlue.Z, 0.35f);
            colors[(int)ImGuiCol.HeaderActive] = new Vector4(accentBlue.X, accentBlue.Y, accentBlue.Z, 0.60f);

            // Separadores, Resize Grip
            colors[(int)ImGuiCol.Separator] = borderColor;
            colors[(int)ImGuiCol.SeparatorHovered] = accentHover;
            colors[(int)ImGuiCol.SeparatorActive] = accentActive;

            colors[(int)ImGuiCol.ResizeGrip] = new Vector4(accentBlue.X, accentBlue.Y, accentBlue.Z, 0.20f);
            colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(accentBlue.X, accentBlue.Y, accentBlue.Z, 0.50f);
            colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(accentBlue.X, accentBlue.Y, accentBlue.Z, 0.75f);

            // Tabs
            colors[(int)ImGuiCol.Tab] = new Vector4(bgColor.X + 0.05f, bgColor.Y + 0.05f, bgColor.Z + 0.05f, 1f);
            colors[(int)ImGuiCol.TabHovered] = new Vector4(accentBlue.X, accentBlue.Y, accentBlue.Z, 0.35f);

            // Plots (gráficas)
            colors[(int)ImGuiCol.PlotLines] = accentBlue;
            colors[(int)ImGuiCol.PlotLinesHovered] = accentHover;
            colors[(int)ImGuiCol.PlotHistogram] = accentBlue;
            colors[(int)ImGuiCol.PlotHistogramHovered] = accentHover;

            // Tablas
            colors[(int)ImGuiCol.TableHeaderBg] = new Vector4(bgColor.X + 0.07f, bgColor.Y + 0.07f, bgColor.Z + 0.07f, 1f);
            colors[(int)ImGuiCol.TableBorderStrong] = borderColor;
            colors[(int)ImGuiCol.TableBorderLight] = new Vector4(borderColor.X, borderColor.Y, borderColor.Z, 0.5f);
            colors[(int)ImGuiCol.TableRowBg] = new Vector4(0f, 0f, 0f, 0f);
            colors[(int)ImGuiCol.TableRowBgAlt] = new Vector4(1f, 1f, 1f, 0.03f);

            // Texto seleccionado, Drag&Drop, etc.
            colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(accentBlue.X, accentBlue.Y, accentBlue.Z, 0.35f);
            colors[(int)ImGuiCol.DragDropTarget] = new Vector4(accentBlue.X, accentBlue.Y, accentBlue.Z, 0.90f);
            colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1f, 1f, 1f, 0.70f);
            colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.2f, 0.2f, 0.2f, 0.2f);
            colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(bgColor.X, bgColor.Y, bgColor.Z, 0.7f);

            return true;
        }
    }
}
