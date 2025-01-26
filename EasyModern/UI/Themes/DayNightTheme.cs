using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class DayNightTheme : ITheme
    {
        public string ID { get; set; } = "theme.day-night";

        // Acumulador de tiempo estático para no reiniciar en cada Apply
        private static float dayNightTimer = 0.0f;

        // Duración del ciclo completo (en segundos)
        private const float CYCLE_DURATION = 60.0f; // 1 minuto

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var io = ImGui.GetIO();

            // Incrementamos el tiempo
            dayNightTimer += io.DeltaTime;

            // Calculamos un factor 't' en [0..1] para la interpolación
            // dayNightTimer % CYCLE_DURATION => tiempo en el rango [0..CYCLE_DURATION)
            float t = (dayNightTimer % CYCLE_DURATION) / CYCLE_DURATION;

            //
            // Definir colores "día" y "noche"
            //
            // - Día: Fondo muy claro, texto oscuro
            // - Noche: Fondo oscuro, texto claro
            //
            Vector4 dayBg = new Vector4(0.95f, 0.97f, 1.00f, 1.00f); // un tono muy claro
            Vector4 nightBg = new Vector4(0.08f, 0.09f, 0.12f, 1.00f); // un fondo oscuro

            Vector4 dayText = new Vector4(0.10f, 0.10f, 0.10f, 1.00f); // texto oscuro
            Vector4 nightText = new Vector4(0.90f, 0.90f, 0.90f, 1.00f); // texto claro

            // Usamos Lerp para el fondo y el texto
            Vector4 finalBg = Lerp(dayBg, nightBg, t);
            Vector4 finalText = Lerp(dayText, nightText, t);

            //
            // Ajustes “estáticos” de estilo
            //
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

            //
            // Asignamos los colores interpolados
            //
            var colors = style.Colors;

            // Ventanas y texto
            colors[(int)ImGuiCol.Text] = finalText;
            // TextoDisabled -> menos alpha
            colors[(int)ImGuiCol.TextDisabled] = new Vector4(finalText.X, finalText.Y, finalText.Z, 0.45f);

            colors[(int)ImGuiCol.WindowBg] = finalBg;
            colors[(int)ImGuiCol.ChildBg] = finalBg;
            colors[(int)ImGuiCol.PopupBg] = new Vector4(finalBg.X, finalBg.Y, finalBg.Z, 0.95f);

            // Bordes (usamos algo más oscuro para día, y algo más claro para noche)
            Vector4 dayBorder = new Vector4(0.80f, 0.80f, 0.85f, 1.00f);
            Vector4 nightBorder = new Vector4(0.30f, 0.30f, 0.35f, 1.00f);
            Vector4 finalBorder = Lerp(dayBorder, nightBorder, t);

            if (!Core.Instances.Settings.RGB_Color) colors[(int)ImGuiCol.Border] = finalBorder;
            colors[(int)ImGuiCol.BorderShadow] = new Vector4(0, 0, 0, 0);

            // FrameBg
            Vector4 dayFrameBg = new Vector4(0.90f, 0.92f, 0.95f, 1.00f);
            Vector4 nightFrameBg = new Vector4(0.15f, 0.16f, 0.20f, 1.00f);
            Vector4 finalFrameBg = Lerp(dayFrameBg, nightFrameBg, t);

            colors[(int)ImGuiCol.FrameBg] = finalFrameBg;
            colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(finalFrameBg.X + 0.05f, finalFrameBg.Y + 0.05f, finalFrameBg.Z + 0.05f, 1.0f);
            colors[(int)ImGuiCol.FrameBgActive] = new Vector4(finalFrameBg.X + 0.10f, finalFrameBg.Y + 0.10f, finalFrameBg.Z + 0.10f, 1.0f);

            // Título
            colors[(int)ImGuiCol.TitleBg] = finalBg;
            colors[(int)ImGuiCol.TitleBgActive] = new Vector4(finalBg.X + 0.05f, finalBg.Y + 0.05f, finalBg.Z + 0.05f, 1.0f);
            colors[(int)ImGuiCol.TitleBgCollapsed] = finalBg;

            // Menú, Scrollbar
            colors[(int)ImGuiCol.MenuBarBg] = new Vector4(finalBg.X + 0.05f, finalBg.Y + 0.05f, finalBg.Z + 0.05f, 1.0f);
            colors[(int)ImGuiCol.ScrollbarBg] = finalBg;
            colors[(int)ImGuiCol.ScrollbarGrab] = finalBorder;
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(finalBorder.X, finalBorder.Y, finalBorder.Z, 0.8f);
            colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(finalBorder.X, finalBorder.Y, finalBorder.Z, 0.6f);

            //
            // Podemos definir acentos más claros de día, más intensos de noche
            // Por simplicidad, aquí definimos un color (azul) y lo oscurecemos según t
            //
            Vector4 dayAccent = new Vector4(0.20f, 0.50f, 0.90f, 1.00f); // Azul claro
            Vector4 nightAccent = new Vector4(0.10f, 0.30f, 0.60f, 1.00f); // Azul más oscuro
            Vector4 finalAccent = Lerp(dayAccent, nightAccent, t);

            // Check, Sliders
            colors[(int)ImGuiCol.CheckMark] = finalAccent;
            colors[(int)ImGuiCol.SliderGrab] = finalAccent;
            colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(finalAccent.X, finalAccent.Y, finalAccent.Z, 0.8f);

            // Botones
            colors[(int)ImGuiCol.Button] = new Vector4(finalAccent.X, finalAccent.Y, finalAccent.Z, 0.15f);
            colors[(int)ImGuiCol.ButtonHovered] = new Vector4(finalAccent.X, finalAccent.Y, finalAccent.Z, 0.35f);
            colors[(int)ImGuiCol.ButtonActive] = new Vector4(finalAccent.X, finalAccent.Y, finalAccent.Z, 0.60f);

            // Headers
            colors[(int)ImGuiCol.Header] = new Vector4(finalAccent.X, finalAccent.Y, finalAccent.Z, 0.15f);
            colors[(int)ImGuiCol.HeaderHovered] = new Vector4(finalAccent.X, finalAccent.Y, finalAccent.Z, 0.35f);
            colors[(int)ImGuiCol.HeaderActive] = new Vector4(finalAccent.X, finalAccent.Y, finalAccent.Z, 0.60f);

            // Separadores, Resize grips
            colors[(int)ImGuiCol.Separator] = finalBorder;
            colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(finalAccent.X, finalAccent.Y, finalAccent.Z, 0.75f);
            colors[(int)ImGuiCol.SeparatorActive] = finalAccent;

            colors[(int)ImGuiCol.ResizeGrip] = new Vector4(finalAccent.X, finalAccent.Y, finalAccent.Z, 0.25f);
            colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(finalAccent.X, finalAccent.Y, finalAccent.Z, 0.50f);
            colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(finalAccent.X, finalAccent.Y, finalAccent.Z, 0.75f);

            // Tabs
            colors[(int)ImGuiCol.Tab] = finalFrameBg;
            colors[(int)ImGuiCol.TabHovered] = new Vector4(finalAccent.X, finalAccent.Y, finalAccent.Z, 0.35f);

            // Plots
            colors[(int)ImGuiCol.PlotLines] = finalAccent;
            colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(finalAccent.X, finalAccent.Y, finalAccent.Z, 1.0f);
            colors[(int)ImGuiCol.PlotHistogram] = finalAccent;
            colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(finalAccent.X, finalAccent.Y, finalAccent.Z, 1.0f);

            // Tablas
            colors[(int)ImGuiCol.TableHeaderBg] = new Vector4(finalBg.X + 0.1f, finalBg.Y + 0.1f, finalBg.Z + 0.1f, 1f);
            colors[(int)ImGuiCol.TableBorderStrong] = finalBorder;
            colors[(int)ImGuiCol.TableBorderLight] = new Vector4(finalBorder.X, finalBorder.Y, finalBorder.Z, 0.6f);
            colors[(int)ImGuiCol.TableRowBg] = new Vector4(0f, 0f, 0f, 0f);
            colors[(int)ImGuiCol.TableRowBgAlt] = new Vector4(1f, 1f, 1f, 0.03f);

            // Texto seleccionado, Drag&Drop, etc.
            colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(finalAccent.X, finalAccent.Y, finalAccent.Z, 0.35f);
            colors[(int)ImGuiCol.DragDropTarget] = new Vector4(finalAccent.X, finalAccent.Y, finalAccent.Z, 0.90f);
            colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1f, 1f, 1f, 0.70f);
            colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.2f, 0.2f, 0.2f, 0.2f);
            colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(finalBg.X, finalBg.Y, finalBg.Z, 0.7f);

            return true;
        }

        // Función de interpolación lineal (Lerp) entre 2 Vector4
        private Vector4 Lerp(Vector4 a, Vector4 b, float t)
        {
            return new Vector4(
                a.X + (b.X - a.X) * t,
                a.Y + (b.Y - a.Y) * t,
                a.Z + (b.Z - a.Z) * t,
                a.W + (b.W - a.W) * t
            );
        }
    }
}
