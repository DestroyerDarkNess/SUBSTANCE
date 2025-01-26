using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class Retro80sNeonTheme : ITheme
    {
        public string ID { get; set; } = "theme.retro80s-neon";

        // Acumulador de tiempo para la animación
        private static float animTime = 0.0f;

        // Frecuencia del parpadeo (cuántos ciclos de sin en 1 segundo)
        private const float FREQUENCY = 10.0f;

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var io = ImGui.GetIO();

            // Incrementar el tiempo
            animTime += io.DeltaTime;

            // Calculamos un factor de parpadeo con una onda senoidal rápida
            // Por ejemplo, flicker oscila en [0.8..1.0] => parpadeo leve
            float flicker = 0.8f + 0.2f * (float)Math.Sin(animTime * FREQUENCY);

            //
            // Paleta base “Retro 80s”:
            //  - Fondo negro o casi negro
            //  - Texto muy claro
            //  - Accentos neon (magenta, cian, etc.)
            //
            Vector4 bgColor = new Vector4(0.05f, 0.05f, 0.05f, 1.00f); // Fondo oscuro
            Vector4 textColor = new Vector4(0.95f, 0.95f, 0.95f, 1.00f); // Texto claro
            Vector4 borderColor = new Vector4(0.25f, 0.25f, 0.25f, 1.00f); // Gris para bordes

            // Color base neon (magenta fosforescente)
            // #FF00FF => (1.0, 0.0, 1.0) pero podemos elegir un rosa un poco más suave
            Vector4 neonPinkBase = new Vector4(1.00f, 0.30f, 0.70f, 1.00f);

            // Podemos “multiplicar” la parte RGB por flicker para simular el parpadeo en luminosidad
            // (en lugar de o además de alpha). 
            // Por ejemplo:
            Vector4 neonPinkFlicker = new Vector4(
                neonPinkBase.X * flicker,
                neonPinkBase.Y * flicker,
                neonPinkBase.Z * flicker,
                1.0f // alpha fijo
            );

            //
            // Ajustes estáticos del estilo
            //
            style.WindowRounding = 4.0f;
            style.FrameRounding = 3.0f;
            style.ScrollbarRounding = 4.0f;
            style.GrabRounding = 3.0f;
            style.TabRounding = 3.0f;

            style.WindowPadding = new Vector2(12.0f, 12.0f);
            style.FramePadding = new Vector2(6.0f, 4.0f);
            style.ItemSpacing = new Vector2(8.0f, 6.0f);
            style.ItemInnerSpacing = new Vector2(6.0f, 4.0f);

            style.Alpha = 1.0f;  // Opacidad global
            style.DisabledAlpha = 0.60f;

            //
            // Asignar colores
            //
            var colors = style.Colors;

            // Texto y fondo
            colors[(int)ImGuiCol.Text] = textColor;
            colors[(int)ImGuiCol.TextDisabled] = new Vector4(textColor.X, textColor.Y, textColor.Z, 0.40f);
            colors[(int)ImGuiCol.WindowBg] = bgColor;
            colors[(int)ImGuiCol.ChildBg] = bgColor;
            colors[(int)ImGuiCol.PopupBg] = new Vector4(bgColor.X, bgColor.Y, bgColor.Z, 0.95f);

            // Bordes
            if (!Core.Instances.Settings.RGB_Color) colors[(int)ImGuiCol.Border] = borderColor;
            colors[(int)ImGuiCol.BorderShadow] = new Vector4(0, 0, 0, 0);

            // Frame BG
            colors[(int)ImGuiCol.FrameBg] = new Vector4(0.15f, 0.15f, 0.15f, 1.00f);
            colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.25f, 0.25f, 0.25f, 1.00f);
            colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.35f, 0.35f, 0.35f, 1.00f);

            // Título
            colors[(int)ImGuiCol.TitleBg] = bgColor;
            colors[(int)ImGuiCol.TitleBgActive] = new Vector4(bgColor.X + 0.05f, bgColor.Y + 0.05f, bgColor.Z + 0.05f, 1f);
            colors[(int)ImGuiCol.TitleBgCollapsed] = bgColor;

            // Menú, Scrollbar
            colors[(int)ImGuiCol.MenuBarBg] = new Vector4(bgColor.X + 0.05f, bgColor.Y + 0.05f, bgColor.Z + 0.05f, 1f);
            colors[(int)ImGuiCol.ScrollbarBg] = bgColor;
            colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.3f, 0.3f, 0.3f, 1.0f);
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.4f, 0.4f, 0.4f, 1.0f);
            colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.5f, 0.5f, 0.5f, 1.0f);

            //
            // Colores neon con parpadeo
            //
            // Check, Slider
            colors[(int)ImGuiCol.CheckMark] = neonPinkFlicker;
            colors[(int)ImGuiCol.SliderGrab] = neonPinkFlicker;
            colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(neonPinkFlicker.X, neonPinkFlicker.Y, neonPinkFlicker.Z, 0.8f);

            // Botones
            // Usamos alpha base + multiplicación flicker
            colors[(int)ImGuiCol.Button] = new Vector4(neonPinkFlicker.X, neonPinkFlicker.Y, neonPinkFlicker.Z, 0.25f);
            colors[(int)ImGuiCol.ButtonHovered] = new Vector4(neonPinkFlicker.X, neonPinkFlicker.Y, neonPinkFlicker.Z, 0.45f);
            colors[(int)ImGuiCol.ButtonActive] = new Vector4(neonPinkFlicker.X, neonPinkFlicker.Y, neonPinkFlicker.Z, 0.70f);

            // Headers
            colors[(int)ImGuiCol.Header] = new Vector4(neonPinkFlicker.X, neonPinkFlicker.Y, neonPinkFlicker.Z, 0.25f);
            colors[(int)ImGuiCol.HeaderHovered] = new Vector4(neonPinkFlicker.X, neonPinkFlicker.Y, neonPinkFlicker.Z, 0.45f);
            colors[(int)ImGuiCol.HeaderActive] = new Vector4(neonPinkFlicker.X, neonPinkFlicker.Y, neonPinkFlicker.Z, 0.70f);

            // Separadores, Resize grips
            colors[(int)ImGuiCol.Separator] = new Vector4(neonPinkFlicker.X, neonPinkFlicker.Y, neonPinkFlicker.Z, 0.3f);
            colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(neonPinkFlicker.X, neonPinkFlicker.Y, neonPinkFlicker.Z, 0.6f);
            colors[(int)ImGuiCol.SeparatorActive] = neonPinkFlicker;

            colors[(int)ImGuiCol.ResizeGrip] = new Vector4(neonPinkFlicker.X, neonPinkFlicker.Y, neonPinkFlicker.Z, 0.25f);
            colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(neonPinkFlicker.X, neonPinkFlicker.Y, neonPinkFlicker.Z, 0.50f);
            colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(neonPinkFlicker.X, neonPinkFlicker.Y, neonPinkFlicker.Z, 0.75f);

            // Tabs
            colors[(int)ImGuiCol.Tab] = new Vector4(bgColor.X + 0.1f, bgColor.Y + 0.1f, bgColor.Z + 0.1f, 1.0f);
            colors[(int)ImGuiCol.TabHovered] = new Vector4(neonPinkFlicker.X, neonPinkFlicker.Y, neonPinkFlicker.Z, 0.45f);

            // Plots (gráficas)
            colors[(int)ImGuiCol.PlotLines] = neonPinkFlicker;
            colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(neonPinkFlicker.X, neonPinkFlicker.Y, neonPinkFlicker.Z, 1.0f);
            colors[(int)ImGuiCol.PlotHistogram] = neonPinkFlicker;
            colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(neonPinkFlicker.X, neonPinkFlicker.Y, neonPinkFlicker.Z, 1.0f);

            // Tablas
            colors[(int)ImGuiCol.TableHeaderBg] = new Vector4(bgColor.X + 0.1f, bgColor.Y + 0.1f, bgColor.Z + 0.1f, 1.0f);
            colors[(int)ImGuiCol.TableBorderStrong] = borderColor;
            colors[(int)ImGuiCol.TableBorderLight] = new Vector4(borderColor.X, borderColor.Y, borderColor.Z, 0.6f);
            colors[(int)ImGuiCol.TableRowBg] = new Vector4(0f, 0f, 0f, 0f);
            colors[(int)ImGuiCol.TableRowBgAlt] = new Vector4(1f, 1f, 1f, 0.03f);

            // Texto seleccionado, Drag&Drop, etc.
            colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(neonPinkFlicker.X, neonPinkFlicker.Y, neonPinkFlicker.Z, 0.35f);
            colors[(int)ImGuiCol.DragDropTarget] = new Vector4(neonPinkFlicker.X, neonPinkFlicker.Y, neonPinkFlicker.Z, 0.90f);
            colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1f, 1f, 1f, 0.70f);
            colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.2f, 0.2f, 0.2f, 0.2f);
            colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(bgColor.X, bgColor.Y, bgColor.Z, 0.7f);

            return true;
        }
    }
}
