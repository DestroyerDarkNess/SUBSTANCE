using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class DarkGreen : ITheme
    {
        public string ID { get; set; } = "theme.darkgreen";

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var colors = style.Colors;

            colors[(int)ImGuiCol.Text] = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.5f, 0.5f, 0.5f, 1.0f);
            colors[(int)ImGuiCol.WindowBg] = new Vector4(0.043f, 0.047f, 0.059f, 0.5f);
            colors[(int)ImGuiCol.ChildBg] = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            colors[(int)ImGuiCol.PopupBg] = new Vector4(0.08f, 0.08f, 0.08f, 0.94f);
            if (!Core.Instances.Settings.RGB_Color) colors[(int)ImGuiCol.Border] = new Vector4(0.0f, 1.0f, 0.5f, 1.0f);
            colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            colors[(int)ImGuiCol.FrameBg] = new Vector4(0.153f, 0.153f, 0.200f, 1.0f);
            colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.2f, 0.2f, 0.25f, 1.0f);
            colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.3f, 0.3f, 0.35f, 1.0f);
            colors[(int)ImGuiCol.TitleBg] = new Vector4(0.08f, 0.08f, 0.08f, 1.0f);
            colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.1f, 0.1f, 0.1f, 1.0f);
            colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.0f, 0.0f, 0.0f, 0.61f);
            colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.14f, 0.14f, 0.14f, 1.0f);
            colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.02f, 0.02f, 0.02f, 0.53f);
            colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.31f, 0.31f, 0.31f, 1.0f);
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.41f, 0.41f, 0.41f, 1.0f);
            colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.51f, 0.51f, 0.51f, 1.0f);
            colors[(int)ImGuiCol.CheckMark] = new Vector4(0.26f, 0.59f, 0.98f, 1.0f);
            colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.24f, 0.52f, 0.88f, 1.0f);
            colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.26f, 0.59f, 0.98f, 1.0f);
            colors[(int)ImGuiCol.Button] = new Vector4(0.153f, 0.153f, 0.200f, 1.0f);
            colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.2f, 0.2f, 0.25f, 1.0f);
            colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.3f, 0.3f, 0.35f, 1.0f);
            colors[(int)ImGuiCol.Header] = new Vector4(0.153f, 0.153f, 0.200f, 1.0f);
            colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.2f, 0.2f, 0.25f, 1.0f);
            colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.3f, 0.3f, 0.35f, 1.0f);
            colors[(int)ImGuiCol.Separator] = new Vector4(0.14f, 0.14f, 0.14f, 1.0f);
            colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(0.2f, 0.2f, 0.25f, 1.0f);
            colors[(int)ImGuiCol.SeparatorActive] = new Vector4(0.3f, 0.3f, 0.35f, 1.0f);
            colors[(int)ImGuiCol.ResizeGrip] = new Vector4(0.26f, 0.59f, 0.98f, 0.25f);
            colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(0.26f, 0.59f, 0.98f, 0.67f);
            colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(0.26f, 0.59f, 0.98f, 0.95f);
            colors[(int)ImGuiCol.PlotLines] = new Vector4(0.61f, 0.61f, 0.61f, 1.0f);
            colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(1.0f, 0.43f, 0.35f, 1.0f);
            colors[(int)ImGuiCol.PlotHistogram] = new Vector4(0.9f, 0.7f, 0.0f, 1.0f);
            colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(1.0f, 0.6f, 0.0f, 1.0f);
            colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.26f, 0.59f, 0.98f, 0.35f);
            colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.8f, 0.8f, 0.8f, 0.35f);

            return true;
        }
    }
}
