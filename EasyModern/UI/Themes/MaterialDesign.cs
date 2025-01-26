using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class MaterialDesign : ITheme
    {
        public string ID { get; set; } = "theme.material";

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var colors = style.Colors;

            //
            // Colores de base (Material Design Light)
            // 
            // * Fondo claro: #FAFAFA (casi blanco)
            // * Texto principal: #212121 (gris oscuro)
            // * Bordes y líneas sutiles: #E0E0E0 (gris claro)
            // * Color primario (acento): #2196F3 (azul)
            //   -> Hover y Active son versiones ligeramente más oscuras
            //

            // Convertimos colores hex a Vector4(R,G,B,A)
            // Ejemplo: #2196F3 => (0.13f, 0.59f, 0.95f, 1.0f)
            Vector4 colBackground = new Vector4(0.98f, 0.98f, 0.98f, 1.00f); // #FAFAFA
            Vector4 colWindowBg = new Vector4(1.00f, 1.00f, 1.00f, 1.00f); // #FFFFFF (para ChildBg, si quieres)
            Vector4 colText = new Vector4(0.13f, 0.13f, 0.13f, 1.00f); // #212121
            Vector4 colBorder = new Vector4(0.88f, 0.88f, 0.88f, 1.00f); // #E0E0E0

            Vector4 colAccent = new Vector4(0.13f, 0.59f, 0.95f, 1.00f); // #2196F3
            Vector4 colAccentHovered = new Vector4(0.10f, 0.52f, 0.85f, 1.00f); // un poco más oscuro
            Vector4 colAccentActive = new Vector4(0.08f, 0.42f, 0.75f, 1.00f); // aún más oscuro

            // Si quieres un color secundario (por ejemplo, un rosa):
            // Vector4 colSecondary     = new Vector4(0.96f, 0.25f, 0.49f, 1.00f); // #F50057
            // Ajusta según tu gusto.

            //
            // Ajustes de estilo para dar una sensación de “tarjetas” con bordes sutiles
            //
            style.WindowRounding = 4.0f;
            style.FrameRounding = 3.0f;
            style.ScrollbarRounding = 4.0f;
            style.GrabRounding = 3.0f;
            style.TabRounding = 3.0f;

            style.WindowPadding = new Vector2(12.0f, 12.0f);
            style.FramePadding = new Vector2(8.0f, 4.0f);
            style.ItemSpacing = new Vector2(8.0f, 6.0f);
            style.ItemInnerSpacing = new Vector2(6.0f, 4.0f);

            // Aquí podrías simular “Elevation” jugando con el alpha de ChildBg o PopupBg
            // y con pequeños bordes / sombreado. ImGui no incluye sombras nativas,
            // pero sí puedes diferenciar la capa usando colores con alpha menor.

            style.Alpha = 1.0f;   // Opacidad global
            style.DisabledAlpha = 0.60f;  // Elementos deshabilitados

            //
            // Asignación de colores
            //

            // Texto, Ventanas, Fondos, etc.
            colors[(int)ImGuiCol.Text] = colText;
            colors[(int)ImGuiCol.TextDisabled] = new Vector4(colText.X, colText.Y, colText.Z, 0.40f);
            colors[(int)ImGuiCol.WindowBg] = colBackground;
            colors[(int)ImGuiCol.ChildBg] = colWindowBg;
            colors[(int)ImGuiCol.PopupBg] = new Vector4(colWindowBg.X, colWindowBg.Y, colWindowBg.Z, 0.98f);

            // Bordes
            if (!Core.Instances.Settings.RGB_Color) colors[(int)ImGuiCol.Border] = colBorder;
            colors[(int)ImGuiCol.BorderShadow] = new Vector4(0, 0, 0, 0);

            // Frames y cajas
            colors[(int)ImGuiCol.FrameBg] = new Vector4(1.0f, 1.0f, 1.0f, 0.90f);
            colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(1.0f, 1.0f, 1.0f, 0.95f);
            colors[(int)ImGuiCol.FrameBgActive] = new Vector4(1.0f, 1.0f, 1.0f, 1.00f);

            // Título
            colors[(int)ImGuiCol.TitleBg] = colBackground;
            colors[(int)ImGuiCol.TitleBgActive] = new Vector4(colBackground.X, colBackground.Y, colBackground.Z, 1.00f);
            colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(colBackground.X, colBackground.Y, colBackground.Z, 0.75f);

            // Barra de menú
            colors[(int)ImGuiCol.MenuBarBg] = new Vector4(1.0f, 1.0f, 1.0f, 0.90f);

            // Scrollbar
            colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(colBackground.X, colBackground.Y, colBackground.Z, 0.90f);
            colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(colBorder.X, colBorder.Y, colBorder.Z, 1.0f);
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(colBorder.X, colBorder.Y, colBorder.Z, 0.85f);
            colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(colBorder.X, colBorder.Y, colBorder.Z, 0.75f);

            // Check, Sliders, etc.
            colors[(int)ImGuiCol.CheckMark] = colAccent;
            colors[(int)ImGuiCol.SliderGrab] = colAccent;
            colors[(int)ImGuiCol.SliderGrabActive] = colAccentHovered;

            // Botones
            colors[(int)ImGuiCol.Button] = new Vector4(colAccent.X, colAccent.Y, colAccent.Z, 0.15f);
            colors[(int)ImGuiCol.ButtonHovered] = new Vector4(colAccent.X, colAccent.Y, colAccent.Z, 0.25f);
            colors[(int)ImGuiCol.ButtonActive] = new Vector4(colAccent.X, colAccent.Y, colAccent.Z, 0.40f);

            // Headers (CollapsingHeader, TreeNode, etc.)
            colors[(int)ImGuiCol.Header] = new Vector4(colAccent.X, colAccent.Y, colAccent.Z, 0.15f);
            colors[(int)ImGuiCol.HeaderHovered] = new Vector4(colAccent.X, colAccent.Y, colAccent.Z, 0.25f);
            colors[(int)ImGuiCol.HeaderActive] = new Vector4(colAccent.X, colAccent.Y, colAccent.Z, 0.40f);

            // Separadores / Resize Grips
            colors[(int)ImGuiCol.Separator] = colBorder;
            colors[(int)ImGuiCol.SeparatorHovered] = colAccentHovered;
            colors[(int)ImGuiCol.SeparatorActive] = colAccentActive;
            colors[(int)ImGuiCol.ResizeGrip] = new Vector4(colAccent.X, colAccent.Y, colAccent.Z, 0.15f);
            colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(colAccent.X, colAccent.Y, colAccent.Z, 0.25f);
            colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(colAccent.X, colAccent.Y, colAccent.Z, 0.40f);

            // Tabs
            colors[(int)ImGuiCol.Tab] = new Vector4(colAccent.X, colAccent.Y, colAccent.Z, 0.15f);
            colors[(int)ImGuiCol.TabHovered] = new Vector4(colAccent.X, colAccent.Y, colAccent.Z, 0.25f);

            // Plots (gráficas)
            colors[(int)ImGuiCol.PlotLines] = colAccent;
            colors[(int)ImGuiCol.PlotLinesHovered] = colAccentHovered;
            colors[(int)ImGuiCol.PlotHistogram] = colAccent;
            colors[(int)ImGuiCol.PlotHistogramHovered] = colAccentHovered;

            // Tablas
            colors[(int)ImGuiCol.TableHeaderBg] = new Vector4(1.0f, 1.0f, 1.0f, 0.85f);
            colors[(int)ImGuiCol.TableBorderStrong] = colBorder;
            colors[(int)ImGuiCol.TableBorderLight] = new Vector4(colBorder.X, colBorder.Y, colBorder.Z, 0.6f);
            colors[(int)ImGuiCol.TableRowBg] = new Vector4(0f, 0f, 0f, 0f);
            colors[(int)ImGuiCol.TableRowBgAlt] = new Vector4(0f, 0f, 0f, 0.02f);

            // Texto seleccionado, Drag&Drop, etc.
            colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(colAccent.X, colAccent.Y, colAccent.Z, 0.35f);
            colors[(int)ImGuiCol.DragDropTarget] = new Vector4(colAccent.X, colAccent.Y, colAccent.Z, 0.90f);
            colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1f, 1f, 1f, 0.70f);
            colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0f, 0f, 0f, 0.20f);
            colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0f, 0f, 0f, 0.30f);

            // (Opcional) Docking
            // colors[(int)ImGuiCol.DockingPreview]      = new Vector4(colAccent.X, colAccent.Y, colAccent.Z, 0.20f);
            // colors[(int)ImGuiCol.DockingEmptyBg]      = colBackground;

            return true;
        }
    }
}
