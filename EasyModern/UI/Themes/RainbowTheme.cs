using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class RainbowTheme : ITheme
    {
        public string ID { get; set; } = "theme.rainbow";

        // Acumulador de HUE (hue oscilará en 0..1 cíclicamente)
        private static float hue = 0.0f;

        // Velocidad de rotación del arcoíris (Hue por segundo)
        private const float SPEED = 0.1f;

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var io = ImGui.GetIO();

            // Incrementamos hue
            hue += io.DeltaTime * SPEED;
            // Si excede 1.0f, vuelve a 0 => ciclo
            if (hue > 1.0f)
                hue -= 1.0f;

            //
            // Convertir (hue, 1.0, 1.0) en RGB
            // (Saturación=1.0f, Valor=1.0f para colores vivos)
            //
            Vector3 rgb = HSVtoRGB(hue, 1.0f, 1.0f);
            // Creamos el color final (alpha=1.0f)
            Vector4 rainbowColor = new Vector4(rgb.X, rgb.Y, rgb.Z, 1.0f);

            //
            // Ajustes estáticos de estilo
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
            // Colores de fondo y texto (fijos)
            //
            var colors = style.Colors;
            Vector4 bgColor = new Vector4(0.10f, 0.10f, 0.10f, 1.00f);
            Vector4 textColor = new Vector4(0.90f, 0.90f, 0.90f, 1.00f);
            Vector4 borderColor = new Vector4(0.35f, 0.35f, 0.35f, 1.00f);

            colors[(int)ImGuiCol.Text] = textColor;
            colors[(int)ImGuiCol.TextDisabled] = new Vector4(textColor.X, textColor.Y, textColor.Z, 0.40f);
            colors[(int)ImGuiCol.WindowBg] = bgColor;
            colors[(int)ImGuiCol.ChildBg] = bgColor;
            colors[(int)ImGuiCol.PopupBg] = new Vector4(bgColor.X, bgColor.Y, bgColor.Z, 0.95f);
            if (!Core.Instances.Settings.RGB_Color) colors[(int)ImGuiCol.Border] = borderColor;
            colors[(int)ImGuiCol.BorderShadow] = new Vector4(0, 0, 0, 0);

            // Fondo de frames
            colors[(int)ImGuiCol.FrameBg] = new Vector4(0.20f, 0.20f, 0.20f, 1.0f);
            colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.25f, 0.25f, 0.25f, 1.0f);
            colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.30f, 0.30f, 0.30f, 1.0f);

            // Título y menú
            colors[(int)ImGuiCol.TitleBg] = rainbowColor;
            colors[(int)ImGuiCol.TitleBgActive] = new Vector4(bgColor.X + 0.05f, bgColor.Y + 0.05f, bgColor.Z + 0.05f, 1.0f);
            colors[(int)ImGuiCol.TitleBgCollapsed] = bgColor;
            colors[(int)ImGuiCol.MenuBarBg] = new Vector4(bgColor.X + 0.05f, bgColor.Y + 0.05f, bgColor.Z + 0.05f, 1.0f);

            // Scrollbar
            colors[(int)ImGuiCol.ScrollbarBg] = bgColor;
            colors[(int)ImGuiCol.ScrollbarGrab] = borderColor;
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(borderColor.X, borderColor.Y, borderColor.Z, 0.8f);
            colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(borderColor.X, borderColor.Y, borderColor.Z, 0.6f);

            //
            // Aquí asignamos el “rainbowColor” como color de acento
            //
            colors[(int)ImGuiCol.CheckMark] = rainbowColor;
            colors[(int)ImGuiCol.SliderGrab] = rainbowColor;
            colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(rainbowColor.X, rainbowColor.Y, rainbowColor.Z, 0.8f);

            // Botones
            colors[(int)ImGuiCol.Button] = new Vector4(rainbowColor.X, rainbowColor.Y, rainbowColor.Z, 0.15f);
            colors[(int)ImGuiCol.ButtonHovered] = new Vector4(rainbowColor.X, rainbowColor.Y, rainbowColor.Z, 0.35f);
            colors[(int)ImGuiCol.ButtonActive] = new Vector4(rainbowColor.X, rainbowColor.Y, rainbowColor.Z, 0.60f);

            // Headers
            colors[(int)ImGuiCol.Header] = new Vector4(rainbowColor.X, rainbowColor.Y, rainbowColor.Z, 0.15f);
            colors[(int)ImGuiCol.HeaderHovered] = new Vector4(rainbowColor.X, rainbowColor.Y, rainbowColor.Z, 0.35f);
            colors[(int)ImGuiCol.HeaderActive] = new Vector4(rainbowColor.X, rainbowColor.Y, rainbowColor.Z, 0.60f);

            // Separadores, Resize grip
            colors[(int)ImGuiCol.Separator] = new Vector4(rainbowColor.X, rainbowColor.Y, rainbowColor.Z, 0.3f);
            colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(rainbowColor.X, rainbowColor.Y, rainbowColor.Z, 0.6f);
            colors[(int)ImGuiCol.SeparatorActive] = rainbowColor;

            colors[(int)ImGuiCol.ResizeGrip] = new Vector4(rainbowColor.X, rainbowColor.Y, rainbowColor.Z, 0.25f);
            colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(rainbowColor.X, rainbowColor.Y, rainbowColor.Z, 0.50f);
            colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(rainbowColor.X, rainbowColor.Y, rainbowColor.Z, 0.75f);

            // Tabs
            colors[(int)ImGuiCol.Tab] = new Vector4(bgColor.X + 0.05f, bgColor.Y + 0.05f, bgColor.Z + 0.05f, 1f);
            colors[(int)ImGuiCol.TabHovered] = new Vector4(rainbowColor.X, rainbowColor.Y, rainbowColor.Z, 0.35f);

            // Plots (gráficas)
            colors[(int)ImGuiCol.PlotLines] = rainbowColor;
            colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(rainbowColor.X, rainbowColor.Y, rainbowColor.Z, 1.0f);
            colors[(int)ImGuiCol.PlotHistogram] = rainbowColor;
            colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(rainbowColor.X, rainbowColor.Y, rainbowColor.Z, 1.0f);

            // Tablas
            colors[(int)ImGuiCol.TableHeaderBg] = new Vector4(bgColor.X + 0.1f, bgColor.Y + 0.1f, bgColor.Z + 0.1f, 1f);
            colors[(int)ImGuiCol.TableBorderStrong] = borderColor;
            colors[(int)ImGuiCol.TableBorderLight] = new Vector4(borderColor.X, borderColor.Y, borderColor.Z, 0.6f);
            colors[(int)ImGuiCol.TableRowBg] = new Vector4(0f, 0f, 0f, 0f);
            colors[(int)ImGuiCol.TableRowBgAlt] = new Vector4(1f, 1f, 1f, 0.03f);

            // Texto seleccionado, Drag&Drop, etc.
            colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(rainbowColor.X, rainbowColor.Y, rainbowColor.Z, 0.35f);
            colors[(int)ImGuiCol.DragDropTarget] = new Vector4(rainbowColor.X, rainbowColor.Y, rainbowColor.Z, 0.90f);
            colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1f, 1f, 1f, 0.70f);
            colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.2f, 0.2f, 0.2f, 0.2f);
            colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(bgColor.X, bgColor.Y, bgColor.Z, 0.7f);

            return true;
        }

        /// <summary>
        /// Convierte un color HSV a RGB.
        /// h, s, v en [0..1]
        /// </summary>
        private Vector3 HSVtoRGB(float h, float s, float v)
        {
            // Basado en el algoritmo standard.
            // h: [0..1], s: [0..1], v: [0..1]
            float r, g, b;
            if (s == 0.0f)
            {
                // gris
                r = g = b = v;
            }
            else
            {
                float hf = h * 6.0f;
                int i = (int)Math.Floor(hf);
                float f = hf - i;
                float p = v * (1.0f - s);
                float q = v * (1.0f - s * f);
                float t = v * (1.0f - s * (1.0f - f));

                switch (i)
                {
                    case 0:
                        r = v; g = t; b = p;
                        break;
                    case 1:
                        r = q; g = v; b = p;
                        break;
                    case 2:
                        r = p; g = v; b = t;
                        break;
                    case 3:
                        r = p; g = q; b = v;
                        break;
                    case 4:
                        r = t; g = p; b = v;
                        break;
                    default:
                        r = v; g = p; b = q;
                        break;
                }
            }
            return new Vector3(r, g, b);
        }
    }
}
