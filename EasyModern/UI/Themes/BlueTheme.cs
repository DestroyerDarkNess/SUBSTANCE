using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class BlueTheme : ITheme
    {
        public string ID { get; set; } = "theme.blue";

        public bool Apply()
        {
            // Obtenemos el estilo
            var style = ImGui.GetStyle();
            var colors = style.Colors;

            // Asignación de colores
            colors[(int)ImGuiCol.Text] = new Vector4(1.00f, 1.00f, 1.00f, 1.00f);
            colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.60f, 0.60f, 0.60f, 1.00f);
            colors[(int)ImGuiCol.WindowBg] = new Vector4(0.11f, 0.10f, 0.11f, 1.00f);

            // "ChildWindowBg" => "ChildBg"
            colors[(int)ImGuiCol.ChildBg] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);

            colors[(int)ImGuiCol.PopupBg] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
            colors[(int)ImGuiCol.Border] = new Vector4(0.86f, 0.86f, 0.86f, 1.00f);
            colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);

            colors[(int)ImGuiCol.FrameBg] = new Vector4(0.21f, 0.20f, 0.21f, 0.60f);
            colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.00f, 0.46f, 0.65f, 1.00f);
            colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.00f, 0.46f, 0.65f, 1.00f);

            colors[(int)ImGuiCol.TitleBg] = new Vector4(0.00f, 0.46f, 0.65f, 1.00f);
            colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.00f, 0.46f, 0.65f, 1.00f);
            colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.00f, 0.46f, 0.65f, 1.00f);

            colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.00f, 0.46f, 0.65f, 1.00f);

            colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.00f, 0.46f, 0.65f, 0.00f);
            colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.00f, 0.46f, 0.65f, 0.44f);
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.00f, 0.46f, 0.65f, 0.74f);
            colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.00f, 0.46f, 0.65f, 1.00f);

            // "ComboBg" no existe en las versiones modernas. Se puede usar PopupBg si fuera necesario.
            // colors[(int)ImGuiCol.ComboBg] = new Vector4(0.15f, 0.14f, 0.15f, 1.00f);

            colors[(int)ImGuiCol.CheckMark] = new Vector4(0.00f, 0.46f, 0.65f, 1.00f);
            colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.00f, 0.46f, 0.65f, 1.00f);
            colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.00f, 0.46f, 0.65f, 1.00f);

            colors[(int)ImGuiCol.Button] = new Vector4(0.00f, 0.46f, 0.65f, 1.00f);
            colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.00f, 0.46f, 0.65f, 1.00f);
            colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.00f, 0.46f, 0.65f, 1.00f);

            colors[(int)ImGuiCol.Header] = new Vector4(0.00f, 0.46f, 0.65f, 1.00f);
            colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.00f, 0.46f, 0.65f, 1.00f);
            colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.00f, 0.46f, 0.65f, 1.00f);

            colors[(int)ImGuiCol.ResizeGrip] = new Vector4(1.00f, 1.00f, 1.00f, 0.30f);
            colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(1.00f, 1.00f, 1.00f, 0.60f);
            colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(1.00f, 1.00f, 1.00f, 0.90f);

            // "CloseButton" y "CloseButtonHovered"/Active ya no se usan en Dear ImGui actual
            // Los dejamos por compatibilidad, en caso de que tu versión los maneje:
            // colors[(int)ImGuiCol.CloseButton]         = new Vector4(1.00f, 0.10f, 0.24f, 0.00f);
            // colors[(int)ImGuiCol.CloseButtonHovered]  = new Vector4(0.00f, 0.10f, 0.24f, 0.00f);
            // colors[(int)ImGuiCol.CloseButtonActive]   = new Vector4(1.00f, 0.10f, 0.24f, 0.00f);

            colors[(int)ImGuiCol.PlotLines] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
            colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
            colors[(int)ImGuiCol.PlotHistogram] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
            colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);

            colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);

            // "ModalWindowDarkening" => "ModalWindowDimBg" en las versiones modernas
            colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);

            return true;
        }
    }
}
