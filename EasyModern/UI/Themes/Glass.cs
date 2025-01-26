using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class Glass : ITheme
    {
        public string ID { get; set; } = "theme.glass";

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var colors = style.Colors;

            // Estilo general
            //style.WindowPadding = new Vector2(15, 15);
            //style.FramePadding = new Vector2(8, 6);
            //style.ItemSpacing = new Vector2(12, 8);
            style.ScrollbarSize = 15.0f;
            style.WindowRounding = 10.0f;
            style.FrameRounding = 5.0f;
            style.ScrollbarRounding = 12.0f;
            style.GrabRounding = 8.0f;
            style.TabRounding = 6.0f;

            // Definir colores con transparencia (alpha entre 0.3 y 0.6)
            colors[(int)ImGuiCol.Text] = new Vector4(1.0f, 1.0f, 1.0f, 0.6f);
            colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.8f, 0.8f, 0.8f, 0.4f);
            colors[(int)ImGuiCol.WindowBg] = new Vector4(0.1f, 0.1f, 0.1f, 0.5f);
            colors[(int)ImGuiCol.ChildBg] = new Vector4(0.15f, 0.15f, 0.15f, 0.0f);
            colors[(int)ImGuiCol.PopupBg] = new Vector4(0.2f, 0.2f, 0.2f, 0.5f);
            if (!Core.Instances.Settings.RGB_Color) colors[(int)ImGuiCol.Border] = new Vector4(0.8f, 0.8f, 0.8f, 0.3f);
            colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            colors[(int)ImGuiCol.FrameBg] = new Vector4(0.2f, 0.2f, 0.2f, 0.5f);
            colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.3f, 0.3f, 0.3f, 0.5f);
            colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.4f, 0.4f, 0.4f, 0.6f);
            colors[(int)ImGuiCol.TitleBg] = new Vector4(0.1f, 0.1f, 0.1f, 0.5f);
            colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.2f, 0.2f, 0.2f, 0.6f);
            colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.1f, 0.1f, 0.1f, 0.4f);
            colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.15f, 0.15f, 0.15f, 0.5f);
            colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.05f, 0.05f, 0.05f, 0.3f);
            colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.4f, 0.4f, 0.4f, 0.6f);
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.5f, 0.5f, 0.5f, 0.6f);
            colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.6f, 0.6f, 0.6f, 0.6f);
            colors[(int)ImGuiCol.CheckMark] = new Vector4(0.9f, 0.9f, 0.9f, 0.6f);
            colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.5f, 0.5f, 0.5f, 0.5f);
            colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.6f, 0.6f, 0.6f, 0.6f);
            colors[(int)ImGuiCol.Button] = new Vector4(0.2f, 0.2f, 0.2f, 0.4f);
            colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.3f, 0.3f, 0.3f, 0.5f);
            colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.4f, 0.4f, 0.4f, 0.6f);
            colors[(int)ImGuiCol.Header] = new Vector4(0.2f, 0.2f, 0.2f, 0.4f);
            colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.3f, 0.3f, 0.3f, 0.5f);
            colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.4f, 0.4f, 0.4f, 0.6f);
            colors[(int)ImGuiCol.Separator] = new Vector4(0.5f, 0.5f, 0.5f, 0.5f);
            colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(0.6f, 0.6f, 0.6f, 0.6f);
            colors[(int)ImGuiCol.SeparatorActive] = new Vector4(0.7f, 0.7f, 0.7f, 0.6f);
            colors[(int)ImGuiCol.ResizeGrip] = new Vector4(0.8f, 0.8f, 0.8f, 0.4f);
            colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(0.9f, 0.9f, 0.9f, 0.5f);
            colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(1.0f, 1.0f, 1.0f, 0.6f);
            colors[(int)ImGuiCol.PlotLines] = new Vector4(0.5f, 0.5f, 0.5f, 0.4f);
            colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(0.7f, 0.7f, 0.7f, 0.5f);
            colors[(int)ImGuiCol.PlotHistogram] = new Vector4(0.8f, 0.8f, 0.8f, 0.5f);
            colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(0.9f, 0.9f, 0.9f, 0.6f);
            colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.2f, 0.2f, 0.2f, 0.5f);
            colors[(int)ImGuiCol.DragDropTarget] = new Vector4(0.9f, 0.9f, 0.9f, 0.5f);
            colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(0.8f, 0.8f, 0.8f, 0.5f);
            colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.1f, 0.1f, 0.1f, 0.3f);
            colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.1f, 0.1f, 0.1f, 0.3f);

            return true;
        }
    }

}
