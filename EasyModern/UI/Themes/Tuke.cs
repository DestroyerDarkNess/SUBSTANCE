using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class Tuke : ITheme
    {
        public string ID { get; set; } = "theme.tuke";

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var colors = style.Colors;

            colors[(int)ImGuiCol.Text] = new Vector4(1.00f, 1.00f, 1.00f, 1.00f);
            colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.24f, 0.24f, 0.24f, 1.00f);
            colors[(int)ImGuiCol.WindowBg] = new Vector4(0.14f, 0.14f, 0.14f, 1.00f);
            colors[(int)ImGuiCol.ChildBg] = new Vector4(0.14f, 0.14f, 0.14f, 1.00f);
            colors[(int)ImGuiCol.PopupBg] = new Vector4(0.14f, 0.14f, 0.14f, 1.00f);
            if (!Core.Instances.Settings.RGB_Color) colors[(int)ImGuiCol.Border] = new Vector4(0.43f, 0.43f, 0.50f, 0.50f);
            colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
            colors[(int)ImGuiCol.FrameBg] = new Vector4(0.20f, 0.20f, 0.20f, 1.00f);
            colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.24f, 0.24f, 0.24f, 1.00f);
            colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.24f, 0.24f, 0.24f, 1.00f);
            colors[(int)ImGuiCol.TitleBg] = new Vector4(0.14f, 0.14f, 0.14f, 1.00f);
            colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.14f, 0.14f, 0.14f, 1.00f);
            colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.14f, 0.14f, 0.14f, 1.00f);
            colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.20f, 0.20f, 0.20f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.14f, 0.14f, 0.14f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.24f, 0.24f, 0.24f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.24f, 0.24f, 0.24f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.24f, 0.24f, 0.24f, 1.00f);
            colors[(int)ImGuiCol.CheckMark] = new Vector4(0.86f, 0.93f, 0.89f, 1.00f);
            colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.24f, 0.24f, 0.24f, 1.00f);
            colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.24f, 0.24f, 0.24f, 1.00f);
            colors[(int)ImGuiCol.Button] = new Vector4(0.20f, 0.20f, 0.20f, 1.00f);
            colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.24f, 0.24f, 0.24f, 1.00f);
            colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.24f, 0.24f, 0.24f, 1.00f);
            colors[(int)ImGuiCol.Header] = new Vector4(0.24f, 0.24f, 0.24f, 1.00f);
            colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.24f, 0.24f, 0.24f, 1.00f);
            colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.24f, 0.24f, 0.24f, 1.00f);
            colors[(int)ImGuiCol.Separator] = new Vector4(0.43f, 0.43f, 0.50f, 0.50f);
            colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(0.43f, 0.43f, 0.50f, 0.50f);
            colors[(int)ImGuiCol.SeparatorActive] = new Vector4(0.43f, 0.43f, 0.50f, 0.50f);
            colors[(int)ImGuiCol.ResizeGrip] = new Vector4(0.24f, 0.24f, 0.24f, 1.00f);
            colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(0.24f, 0.24f, 0.24f, 1.00f);
            colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(0.24f, 0.24f, 0.24f, 1.00f);
            colors[(int)ImGuiCol.Tab] = new Vector4(0.14f, 0.14f, 0.14f, 1.00f);
            colors[(int)ImGuiCol.TabHovered] = new Vector4(0.24f, 0.24f, 0.24f, 1.00f);
            colors[(int)ImGuiCol.PlotLines] = new Vector4(0.86f, 0.93f, 0.89f, 1.00f);
            colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(0.86f, 0.93f, 0.89f, 1.00f);
            colors[(int)ImGuiCol.PlotHistogram] = new Vector4(0.86f, 0.93f, 0.89f, 1.00f);
            colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(0.86f, 0.93f, 0.89f, 1.00f);
            colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.24f, 0.24f, 0.24f, 1.00f);
            colors[(int)ImGuiCol.DragDropTarget] = new Vector4(0.86f, 0.93f, 0.89f, 1.00f);
            colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(0.86f, 0.93f, 0.89f, 1.00f);
            colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.14f, 0.14f, 0.14f, 1.00f);
            colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.14f, 0.14f, 0.14f, 1.00f);

            style.WindowRounding = 0.0f;
            style.FrameRounding = 0.0f;
            style.GrabRounding = 0.0f;
            style.ScrollbarRounding = 0.0f;
            style.TabRounding = 0.0f;
            return true;
        }
    }

}
