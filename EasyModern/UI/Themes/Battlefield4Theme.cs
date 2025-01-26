using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class BattlefieldTheme : ITheme
    {
        public string ID { get; set; } = "theme.battlefield";

        public bool Apply()
        {
            // Colores base sacados del CSS (aprox):
            //
            // :root {
            //   --black: #000000;
            //   --white: #FFFFFF;
            //   --orange: #FF9900;
            //   --green: #57B06D;
            //   ...
            // }
            //
            // y varios rgba(7,7,7,x), etc.

            var style = ImGui.GetStyle();
            var colors = style.Colors;

            // Ajustamos los parámetros principales del estilo.
            style.WindowPadding = new Vector2(8.0f, 8.0f);
            style.WindowRounding = 0.0f;   // El CSS elimina casi todos los bordes redondeados
            style.FramePadding = new Vector2(5.0f, 3.0f);
            style.FrameRounding = 0.0f;
            style.ItemSpacing = new Vector2(6.0f, 6.0f);
            style.ItemInnerSpacing = new Vector2(6.0f, 4.0f);
            style.IndentSpacing = 20.0f;
            style.ScrollbarSize = 12.0f;
            style.ScrollbarRounding = 0.0f;
            style.GrabMinSize = 10.0f;
            style.GrabRounding = 0.0f;
            style.WindowBorderSize = 1.0f;
            style.FrameBorderSize = 1.0f;

            // Paleta principal
            // 
            // Basada en un fondo muy oscuro (#000) y elementos con 
            // “var(--a-black-dark) = rgba(7, 7, 7, 0.8)”, etc.
            // Acentos en naranja (#FF9900) y toques en verde (#57B06D).
            // 
            // NOTA: Ajusta la opacidad a tu gusto.

            Vector4 colBlack = new Vector4(0.00f, 0.00f, 0.00f, 1.00f); // #000
            Vector4 colWhite = new Vector4(1.00f, 1.00f, 1.00f, 1.00f); // #fff
            Vector4 colOrange = new Vector4(1.00f, 0.60f, 0.00f, 1.00f); // #FF9900 (con un pelín más saturado)
            Vector4 colGreen = new Vector4(0.34f, 0.69f, 0.43f, 1.00f); // #57B06D
            Vector4 colGrayDark = new Vector4(0.07f, 0.07f, 0.07f, 0.8f);  // rgba(7,7,7,0.8)
            Vector4 colGrayLight = new Vector4(0.07f, 0.07f, 0.07f, 0.5f);  // rgba(7,7,7,0.5f)

            // Ejemplo de paleta:
            // – Ventanas: gris muy oscuro.
            // – Bordes: un poco de “blanco” con opacidad.
            // – Botones: “transparent/negro” y hover en “naranja”.

            // Fondo principal de la ventana
            colors[(int)ImGuiCol.WindowBg] = colGrayDark;  // (#070707 con alpha 0.8)

            // Fondo de frames (inputs, etc)
            colors[(int)ImGuiCol.FrameBg] = new Vector4(0.07f, 0.07f, 0.07f, 0.60f);
            colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.07f, 0.07f, 0.07f, 0.80f);
            colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.07f, 0.07f, 0.07f, 1.00f);

            // Title Bg
            colors[(int)ImGuiCol.TitleBg] = colGrayDark;
            colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.07f, 0.07f, 0.07f, 1.00f);
            colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.07f, 0.07f, 0.07f, 0.50f);

            // Texto
            colors[(int)ImGuiCol.Text] = colWhite;
            colors[(int)ImGuiCol.TextDisabled] = new Vector4(1.0f, 1.0f, 1.0f, 0.5f);

            // Bordes
            colors[(int)ImGuiCol.Border] = new Vector4(1.0f, 1.0f, 1.0f, 0.2f);
            colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);

            // Scrollbar
            colors[(int)ImGuiCol.ScrollbarBg] = colGrayLight;
            colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(1.0f, 1.0f, 1.0f, 0.3f);
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(1.0f, 1.0f, 1.0f, 0.4f);
            colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(1.0f, 1.0f, 1.0f, 0.6f);

            // Sliders
            colors[(int)ImGuiCol.SliderGrab] = colOrange;
            colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(colOrange.X, colOrange.Y, colOrange.Z, 0.9f);

            // Botones
            colors[(int)ImGuiCol.Button] = new Vector4(0.07f, 0.07f, 0.07f, 0.6f);
            colors[(int)ImGuiCol.ButtonHovered] = colOrange;
            colors[(int)ImGuiCol.ButtonActive] = new Vector4(1f, 1.07f, 1.07f, 1f); // new Vector4(colOrange.X, colOrange.Y, colOrange.Z, 0.8f);

            // Check Mark
            colors[(int)ImGuiCol.CheckMark] = colOrange;

            // Header (collapsing, popups select, etc)
            colors[(int)ImGuiCol.Header] = new Vector4(1.0f, 1.0f, 1.0f, 0.1f);
            colors[(int)ImGuiCol.HeaderHovered] = new Vector4(colOrange.X, colOrange.Y, colOrange.Z, 0.8f);
            colors[(int)ImGuiCol.HeaderActive] = new Vector4(colOrange.X, colOrange.Y, colOrange.Z, 0.9f);

            // Separador
            colors[(int)ImGuiCol.Separator] = new Vector4(1.0f, 1.0f, 1.0f, 0.1f);
            colors[(int)ImGuiCol.SeparatorHovered] = colOrange;
            colors[(int)ImGuiCol.SeparatorActive] = colOrange;

            // Tabs
            colors[(int)ImGuiCol.Tab] = new Vector4(1f, 1f, 1f, 0.1f);
            colors[(int)ImGuiCol.TabHovered] = colOrange;

            // Plot
            colors[(int)ImGuiCol.PlotLines] = colGreen; // #57B06D
            colors[(int)ImGuiCol.PlotLinesHovered] = colOrange;
            colors[(int)ImGuiCol.PlotHistogram] = colGreen;
            colors[(int)ImGuiCol.PlotHistogramHovered] = colOrange;

            // Text Selected Bg
            colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(colOrange.X, colOrange.Y, colOrange.Z, 0.35f);

            // Drag/Drop
            colors[(int)ImGuiCol.DragDropTarget] = new Vector4(colOrange.X, colOrange.Y, colOrange.Z, 0.90f);

            // Modal
            colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.07f, 0.07f, 0.07f, 0.75f);

            // Retornar true si se aplicó correctamente
            return true;
        }
    }
}
