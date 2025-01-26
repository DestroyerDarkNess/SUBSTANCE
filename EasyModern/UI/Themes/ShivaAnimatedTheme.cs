using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class ShivaAnimatedTheme : ITheme
    {
        public string ID { get; set; } = "theme.shiva-animated-bg";

        // Tiempo acumulado para animar el hue
        private static float hueTime = 0.0f;

        public bool Apply()
        {
            var io = ImGui.GetIO();
            var style = ImGui.GetStyle();
            var colors = style.Colors;

            // 1) Incrementar el tiempo
            // Ajusta la velocidad de cambio de color (0.1f es lenta, 1.0f sería muy rápida).
            hueTime += io.DeltaTime * 0.1f;

            // 2) Calculamos hue en [0..1]
            float hue = hueTime % 1.0f;

            //
            // Definimos saturación/valor para el fondo y el acento:
            //
            // - Fondo: sBg < sAccent, vBg < vAccent => color más oscuro
            // - Acento: sAccent > sBg, vAccent > vBg => color más vivo/brillante
            //
            const float sBg = 0.5f, vBg = 0.15f;     // Fondo (oscuro)
            const float sAccent = 0.6f, vAccent = 0.4f; // Acento (más brillante)

            // 3) Obtenemos RGB del fondo y del acento
            Vector3 backgroundRGB = HSVtoRGB(hue, sBg, vBg);
            Vector3 accentRGB = HSVtoRGB(hue, sAccent, vAccent);

            // Creamos los vectores4 con alpha=1.0 (o lo que prefieras)
            Vector4 bgColor = new Vector4(backgroundRGB.X, backgroundRGB.Y, backgroundRGB.Z, 1.0f);
            Vector4 bgColorChild = new Vector4(backgroundRGB.X + 0.05f, backgroundRGB.Y + 0.05f, backgroundRGB.Z + 0.05f, 1.0f);
            Vector4 bgColorPopup = new Vector4(backgroundRGB.X, backgroundRGB.Y, backgroundRGB.Z, 0.95f);

            // Acento (base)
            Vector4 accentColor = new Vector4(accentRGB.X, accentRGB.Y, accentRGB.Z, 1.0f);
            // Para Hover y Active podemos aumentar un poco
            Vector4 accentHover = new Vector4(
                Math.Min(accentRGB.X * 1.3f, 1.0f),
                Math.Min(accentRGB.Y * 1.3f, 1.0f),
                Math.Min(accentRGB.Z * 1.3f, 1.0f),
                1.0f);
            Vector4 accentActive = new Vector4(
                Math.Min(accentRGB.X * 1.6f, 1.0f),
                Math.Min(accentRGB.Y * 1.6f, 1.0f),
                Math.Min(accentRGB.Z * 1.6f, 1.0f),
                1.0f);

            // ============= Ajustes de estilo (basado en “Shiva Red”) =============
            style.Alpha = 1.0f;
            style.WindowPadding = new Vector2(10.0f, 10.0f);
            style.WindowRounding = 12.0f;
            style.WindowBorderSize = 1.0f;
            style.WindowMinSize = new Vector2(50.0f, 50.0f);
            style.WindowTitleAlign = new Vector2(0.5f, 0.5f);

            style.ChildRounding = 8.0f;
            style.ChildBorderSize = 1.0f;
            style.PopupRounding = 8.0f;
            style.PopupBorderSize = 1.0f;

            style.FramePadding = new Vector2(12.0f, 6.0f);
            style.FrameRounding = 8.0f;
            style.FrameBorderSize = 1.0f;

            style.ItemSpacing = new Vector2(10.0f, 8.0f);
            style.ItemInnerSpacing = new Vector2(8.0f, 6.0f);
            style.IndentSpacing = 25.0f;

            style.ScrollbarSize = 16.0f;
            style.ScrollbarRounding = 12.0f;

            style.GrabMinSize = 14.0f;
            style.GrabRounding = 8.0f;
            style.TabRounding = 10.0f;

            style.ButtonTextAlign = new Vector2(0.5f, 0.5f);
            style.SelectableTextAlign = new Vector2(0.5f, 0.5f);

            // ============= Colores base (fondo, texto) =============
            colors[(int)ImGuiCol.Text] = new Vector4(0.95f, 0.96f, 0.98f, 1.0f);
            colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.60f, 0.60f, 0.60f, 1.0f);

            // Fondo animado
            colors[(int)ImGuiCol.WindowBg] = bgColor;
            colors[(int)ImGuiCol.ChildBg] = bgColorChild;
            colors[(int)ImGuiCol.PopupBg] = bgColorPopup;

            // Bordes (ligeramente relacionados con el accent para un look integrado)
            colors[(int)ImGuiCol.Border] = new Vector4(
                accentColor.X + 0.2f,
                accentColor.Y + 0.2f,
                accentColor.Z + 0.2f,
                0.5f);
            colors[(int)ImGuiCol.BorderShadow] = new Vector4(0, 0, 0, 0);

            // Frame, Title, Scrollbar en un gris intermedio
            colors[(int)ImGuiCol.FrameBg] = new Vector4(bgColor.X + 0.1f, bgColor.Y + 0.1f, bgColor.Z + 0.1f, 1.0f);
            colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(bgColor.X + 0.2f, bgColor.Y + 0.2f, bgColor.Z + 0.2f, 1.0f);
            colors[(int)ImGuiCol.FrameBgActive] = new Vector4(bgColor.X + 0.35f, bgColor.Y + 0.35f, bgColor.Z + 0.35f, 1.0f);

            colors[(int)ImGuiCol.TitleBg] = new Vector4(bgColor.X, bgColor.Y, bgColor.Z, 1.0f);
            colors[(int)ImGuiCol.TitleBgActive] = new Vector4(bgColor.X + 0.1f, bgColor.Y + 0.1f, bgColor.Z + 0.1f, 1.0f);
            colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(bgColor.X, bgColor.Y, bgColor.Z, 0.75f);

            colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(bgColor.X, bgColor.Y, bgColor.Z, 0.6f);
            // Scrollbar se colorea con accent
            colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(accentColor.X, accentColor.Y, accentColor.Z, 0.8f);
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(accentHover.X, accentHover.Y, accentHover.Z, 0.8f);
            colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(accentActive.X, accentActive.Y, accentActive.Z, 1.0f);

            // ============= Acento animado para Buttons, Headers, Tabs, etc. =============
            colors[(int)ImGuiCol.Button] = accentColor;
            colors[(int)ImGuiCol.ButtonHovered] = accentHover;
            colors[(int)ImGuiCol.ButtonActive] = accentActive;

            colors[(int)ImGuiCol.Header] = accentColor;
            colors[(int)ImGuiCol.HeaderHovered] = accentHover;
            colors[(int)ImGuiCol.HeaderActive] = accentActive;

            colors[(int)ImGuiCol.Tab] = accentColor;
            colors[(int)ImGuiCol.TabHovered] = accentHover;
            //colors[(int)ImGuiCol.TabActive] = accentActive;

            // Plots
            colors[(int)ImGuiCol.PlotLines] = accentColor;
            colors[(int)ImGuiCol.PlotLinesHovered] = accentHover;
            colors[(int)ImGuiCol.PlotHistogram] = accentColor;
            colors[(int)ImGuiCol.PlotHistogramHovered] = accentHover;

            // CheckMark con el acento
            colors[(int)ImGuiCol.CheckMark] = accentColor;

            // Texto seleccionado, Drag & Drop, Nav
            colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(accentHover.X, accentHover.Y, accentHover.Z, 0.35f);
            colors[(int)ImGuiCol.DragDropTarget] = new Vector4(accentHover.X, accentHover.Y, accentHover.Z, 0.90f);
            //colors[(int)ImGuiCol.NavHighlight] = accentColor;
            colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(accentHover.X, accentHover.Y, accentHover.Z, 0.70f);

            // Resto
            colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.20f, 0.20f, 0.25f, 0.20f);
            colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.20f, 0.20f, 0.25f, 0.35f);

            return true;
        }

        /// <summary>
        /// Convierte un color HSV a RGB (h, s, v ∈ [0..1]).
        /// Devuelve Vector3(r,g,b).
        /// </summary>
        private Vector3 HSVtoRGB(float h, float s, float v)
        {
            if (s == 0.0f)
            {
                // Gris (sin saturación)
                return new Vector3(v, v, v);
            }

            float hf = h * 6.0f;
            int i = (int)Math.Floor(hf);
            float f = hf - i;
            float p = v * (1.0f - s);
            float q = v * (1.0f - s * f);
            float t = v * (1.0f - s * (1.0f - f));

            switch (i)
            {
                case 0: return new Vector3(v, t, p);
                case 1: return new Vector3(q, v, p);
                case 2: return new Vector3(p, v, t);
                case 3: return new Vector3(p, q, v);
                case 4: return new Vector3(t, p, v);
                default: return new Vector3(v, p, q);
            }
        }
    }
}
