using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class BreathingTheme : ITheme
    {
        public string ID { get; set; } = "theme.breathing";

        // Acumulador de tiempo estático para que no se reinicie en cada Apply()
        private static float animTime = 0.0f;

        // Velocidad de “respiración” (ciclos por segundo aprox.)
        private const float SPEED = 2.0f;

        public bool Apply()
        {
            // Obtenemos estilos y deltaTime
            var style = ImGui.GetStyle();
            var io = ImGui.GetIO();

            // Incrementa el tiempo de animación en cada frame
            animTime += io.DeltaTime;

            // factor oscilará entre 0 y 1 (ejemplo de “pulso”)
            // factor = 0.5 + 0.5 * sin(t) => oscila en [0,1]
            float factor = 0.5f + 0.5f * (float)Math.Sin(animTime * SPEED);

            //
            // Ejemplo de paleta base (fija), con "accentColor" animada
            //
            Vector4 bgColor = new Vector4(0.10f, 0.10f, 0.10f, 1.00f); // Fondo oscuro
            Vector4 textColor = new Vector4(0.90f, 0.90f, 0.90f, 1.00f); // Texto claro
            Vector4 borderColor = new Vector4(0.35f, 0.35f, 0.35f, 1.00f); // Bordes

            // Color base de acento (sin animación en RGB, solo en alpha)
            // Lo puedes cambiar a tu gusto (ej. un azul, #56B6C2 = (0.34, 0.71, 0.76))
            Vector4 accentColor = new Vector4(0.35f, 0.75f, 0.95f, 1.0f); // Ej. #5AC0F2

            //
            // Ajustes básicos del estilo (estático)
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
            // Comenzamos asignando colores “base”
            //
            var colors = style.Colors;
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

            // Barra de título
            colors[(int)ImGuiCol.TitleBg] = bgColor;
            colors[(int)ImGuiCol.TitleBgActive] = new Vector4(bgColor.X + 0.05f, bgColor.Y + 0.05f, bgColor.Z + 0.05f, 1.0f);
            colors[(int)ImGuiCol.TitleBgCollapsed] = bgColor;

            // Menú, scrollbar
            colors[(int)ImGuiCol.MenuBarBg] = new Vector4(bgColor.X + 0.05f, bgColor.Y + 0.05f, bgColor.Z + 0.05f, 1.0f);
            colors[(int)ImGuiCol.ScrollbarBg] = bgColor;
            colors[(int)ImGuiCol.ScrollbarGrab] = borderColor;
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(borderColor.X, borderColor.Y, borderColor.Z, 0.8f);
            colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(borderColor.X, borderColor.Y, borderColor.Z, 0.6f);

            //
            // Aquí hacemos la “magia” del pulso en alpha
            // factor está en [0,1], lo multiplicamos por alpha base (1.0f)
            //
            float pulsatingAlpha = factor; // = factor * accentColor.W (si quieres mantener la alpha base)

            // CheckMark, Sliders, etc. usan la alpha pulsante
            colors[(int)ImGuiCol.CheckMark] = new Vector4(accentColor.X, accentColor.Y, accentColor.Z, pulsatingAlpha);
            colors[(int)ImGuiCol.SliderGrab] = new Vector4(accentColor.X, accentColor.Y, accentColor.Z, pulsatingAlpha);
            colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(accentColor.X, accentColor.Y, accentColor.Z, 0.75f + 0.25f * pulsatingAlpha);

            // Botones (podemos dejar un alpha base y sumar el factor)
            colors[(int)ImGuiCol.Button] = new Vector4(accentColor.X, accentColor.Y, accentColor.Z, 0.15f + 0.20f * pulsatingAlpha);
            colors[(int)ImGuiCol.ButtonHovered] = new Vector4(accentColor.X, accentColor.Y, accentColor.Z, 0.25f + 0.40f * pulsatingAlpha);
            colors[(int)ImGuiCol.ButtonActive] = new Vector4(accentColor.X, accentColor.Y, accentColor.Z, 0.40f + 0.60f * pulsatingAlpha);

            // Headers
            colors[(int)ImGuiCol.Header] = new Vector4(accentColor.X, accentColor.Y, accentColor.Z, 0.15f + 0.20f * pulsatingAlpha);
            colors[(int)ImGuiCol.HeaderHovered] = new Vector4(accentColor.X, accentColor.Y, accentColor.Z, 0.25f + 0.40f * pulsatingAlpha);
            colors[(int)ImGuiCol.HeaderActive] = new Vector4(accentColor.X, accentColor.Y, accentColor.Z, 0.40f + 0.60f * pulsatingAlpha);

            // Separadores, Resize grips
            colors[(int)ImGuiCol.Separator] = borderColor;
            colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(accentColor.X, accentColor.Y, accentColor.Z, 0.70f * pulsatingAlpha);
            colors[(int)ImGuiCol.SeparatorActive] = new Vector4(accentColor.X, accentColor.Y, accentColor.Z, 1.00f * pulsatingAlpha);

            colors[(int)ImGuiCol.ResizeGrip] = new Vector4(accentColor.X, accentColor.Y, accentColor.Z, 0.20f * pulsatingAlpha);
            colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(accentColor.X, accentColor.Y, accentColor.Z, 0.50f * pulsatingAlpha);
            colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(accentColor.X, accentColor.Y, accentColor.Z, 0.80f * pulsatingAlpha);

            // Tabs
            colors[(int)ImGuiCol.Tab] = new Vector4(bgColor.X + 0.05f, bgColor.Y + 0.05f, bgColor.Z + 0.05f, 1f);
            colors[(int)ImGuiCol.TabHovered] = new Vector4(accentColor.X, accentColor.Y, accentColor.Z, 0.25f + 0.40f * pulsatingAlpha);

            // Plots (gráficas)
            colors[(int)ImGuiCol.PlotLines] = new Vector4(accentColor.X, accentColor.Y, accentColor.Z, pulsatingAlpha);
            colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(accentColor.X, accentColor.Y, accentColor.Z, 1.0f);
            colors[(int)ImGuiCol.PlotHistogram] = new Vector4(accentColor.X, accentColor.Y, accentColor.Z, pulsatingAlpha);
            colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(accentColor.X, accentColor.Y, accentColor.Z, 1.0f);

            // Tablas
            colors[(int)ImGuiCol.TableHeaderBg] = new Vector4(bgColor.X + 0.1f, bgColor.Y + 0.1f, bgColor.Z + 0.1f, 1f);
            colors[(int)ImGuiCol.TableBorderStrong] = borderColor;
            colors[(int)ImGuiCol.TableBorderLight] = new Vector4(borderColor.X, borderColor.Y, borderColor.Z, 0.6f);
            colors[(int)ImGuiCol.TableRowBg] = new Vector4(0f, 0f, 0f, 0f);
            colors[(int)ImGuiCol.TableRowBgAlt] = new Vector4(1f, 1f, 1f, 0.03f);

            // Texto seleccionado, Drag&Drop, etc.
            colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(accentColor.X, accentColor.Y, accentColor.Z, 0.35f + 0.25f * pulsatingAlpha);
            colors[(int)ImGuiCol.DragDropTarget] = new Vector4(accentColor.X, accentColor.Y, accentColor.Z, 0.90f);
            colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1f, 1f, 1f, 0.70f);
            colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.2f, 0.2f, 0.2f, 0.2f);
            colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(bgColor.X, bgColor.Y, bgColor.Z, 0.7f);

            // Retornar true para indicar que el tema se aplicó correctamente.
            return true;
        }
    }
}
