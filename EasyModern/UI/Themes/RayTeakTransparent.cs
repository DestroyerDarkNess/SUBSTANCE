using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class RayTeakTransparent : ITheme
    {
        public string ID { get; set; } = "theme.RayTeak-translucent";

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var colors = style.Colors;

            // Ajustes de estilo
            style.WindowPadding = new Vector2(15f, 15f);
            style.WindowRounding = 5.0f;
            style.FramePadding = new Vector2(5f, 5f);
            style.FrameRounding = 4.0f;
            style.ItemSpacing = new Vector2(12f, 8f);
            style.ItemInnerSpacing = new Vector2(8f, 6f);
            style.IndentSpacing = 25.0f;
            style.ScrollbarSize = 15.0f;
            style.ScrollbarRounding = 9.0f;
            style.GrabMinSize = 5.0f;
            style.GrabRounding = 3.0f;

            // En caso de que quieras reducir toda la opacidad global (opcional):
            // style.Alpha = 0.90f; // por ejemplo, 90% opaco en todo.

            //
            // Paleta de colores con transparencia (alpha menor)
            //

            // Texto principal y deshabilitado (sin mucha transparencia)
            colors[(int)ImGuiCol.Text] = new Vector4(0.80f, 0.80f, 0.83f, 1.00f);
            colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.24f, 0.23f, 0.29f, 1.00f);

            // Ventanas y fondos (aquí bajamos alpha para “transparencia”)
            colors[(int)ImGuiCol.WindowBg] = new Vector4(0.06f, 0.05f, 0.07f, 0.60f);
            // ChildBg con un poco más de transparencia
            colors[(int)ImGuiCol.ChildBg] = new Vector4(0.07f, 0.07f, 0.09f, 0f);
            // Popups semitransparentes
            colors[(int)ImGuiCol.PopupBg] = new Vector4(0.07f, 0.07f, 0.09f, 0.70f);

            colors[(int)ImGuiCol.Border] = new Vector4(0.80f, 0.80f, 0.83f, 0.70f);
            colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.92f, 0.91f, 0.88f, 0.00f);

            // Fondo de frames, también más translúcido
            colors[(int)ImGuiCol.FrameBg] = new Vector4(0.10f, 0.09f, 0.12f, 0.60f);
            colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.24f, 0.23f, 0.29f, 0.80f);
            colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.56f, 0.56f, 0.58f, 0.80f);

            colors[(int)ImGuiCol.TitleBg] = new Vector4(0.10f, 0.09f, 0.12f, 0.60f);
            colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(1.00f, 0.98f, 0.95f, 0.40f);
            colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.07f, 0.07f, 0.09f, 0.70f);

            colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.10f, 0.09f, 0.12f, 0.60f);

            // Scrollbar semitransparente
            colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.10f, 0.09f, 0.12f, 0.40f);
            colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.80f, 0.80f, 0.83f, 0.31f);
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.56f, 0.56f, 0.58f, 0.80f);
            colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.06f, 0.05f, 0.07f, 0.90f);

            // El "ComboBg" original -> usar PopupBg o comentarlo
            // colors[(int)ImGuiCol.ComboBg]           = ...

            colors[(int)ImGuiCol.CheckMark] = new Vector4(0.80f, 0.80f, 0.83f, 0.85f);
            colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.80f, 0.80f, 0.83f, 0.50f);
            colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.06f, 0.05f, 0.07f, 0.70f);

            // Botones con algo de transparencia
            colors[(int)ImGuiCol.Button] = new Vector4(0.10f, 0.09f, 0.12f, 0.60f);
            colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.24f, 0.23f, 0.29f, 0.90f);
            colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.56f, 0.56f, 0.58f, 0.90f);

            // Headers
            colors[(int)ImGuiCol.Header] = new Vector4(0.10f, 0.09f, 0.12f, 0.60f);
            colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.56f, 0.56f, 0.58f, 0.80f);
            colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.06f, 0.05f, 0.07f, 0.70f);

            // Resize grips
            colors[(int)ImGuiCol.ResizeGrip] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
            colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(0.56f, 0.56f, 0.58f, 0.80f);
            colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(0.06f, 0.05f, 0.07f, 0.80f);


            // Plots
            colors[(int)ImGuiCol.PlotLines] = new Vector4(0.40f, 0.39f, 0.38f, 0.63f);
            colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(0.25f, 1.00f, 0.00f, 1.00f);
            colors[(int)ImGuiCol.PlotHistogram] = new Vector4(0.40f, 0.39f, 0.38f, 0.63f);
            colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(0.25f, 1.00f, 0.00f, 1.00f);

            // Texto seleccionado
            colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.25f, 1.00f, 0.00f, 0.43f);

            // ModalWindowDimBg (antiguo ModalWindowDarkening), con algo de alpha
            colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(1.00f, 0.98f, 0.95f, 0.50f);

            return true;
        }
    }
}
