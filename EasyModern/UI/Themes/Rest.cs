using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class Rest : ITheme
    {
        public string ID { get; set; } = "theme.rest";

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var colors = style.Colors;

            style.Alpha = 1.0f;
            style.DisabledAlpha = 0.5f;
            style.WindowPadding = new Vector2(13.0f, 10.0f);
            style.WindowRounding = 0.0f;
            style.WindowBorderSize = 1.0f;
            style.WindowMinSize = new Vector2(32.0f, 32.0f);
            style.WindowTitleAlign = new Vector2(0.5f, 0.5f);
            style.WindowMenuButtonPosition = ImGuiDir.Right;
            style.ChildRounding = 3.0f;
            style.ChildBorderSize = 1.0f;
            style.PopupRounding = 5.0f;
            style.PopupBorderSize = 1.0f;
            style.FramePadding = new Vector2(20.0f, 8.100000381469727f);
            style.FrameRounding = 2.0f;
            style.FrameBorderSize = 0.0f;
            style.ItemSpacing = new Vector2(3.0f, 3.0f);
            style.ItemInnerSpacing = new Vector2(3.0f, 8.0f);
            style.CellPadding = new Vector2(6.0f, 14.10000038146973f);
            style.IndentSpacing = 0.0f;
            style.ColumnsMinSpacing = 10.0f;
            style.ScrollbarSize = 10.0f;
            style.ScrollbarRounding = 2.0f;
            style.GrabMinSize = 12.10000038146973f;
            style.GrabRounding = 1.0f;
            style.TabRounding = 2.0f;
            style.TabBorderSize = 0.0f;
            style.TabMinWidthForCloseButton = 5.0f;
            style.ColorButtonPosition = ImGuiDir.Right;
            style.ButtonTextAlign = new Vector2(0.5f, 0.5f);
            style.SelectableTextAlign = new Vector2(0.0f, 0.0f);

            style.Colors[(int)ImGuiCol.Text] = new Vector4(0.9803921580314636f, 0.9803921580314636f, 0.9803921580314636f, 1.0f);
            style.Colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.4980392158031464f, 0.4980392158031464f, 0.4980392158031464f, 1.0f);
            style.Colors[(int)ImGuiCol.WindowBg] = new Vector4(0.09411764889955521f, 0.09411764889955521f, 0.09411764889955521f, 1.0f);
            style.Colors[(int)ImGuiCol.ChildBg] = new Vector4(0.1568627506494522f, 0.1568627506494522f, 0.1568627506494522f, 1.0f);
            style.Colors[(int)ImGuiCol.PopupBg] = new Vector4(0.09411764889955521f, 0.09411764889955521f, 0.09411764889955521f, 1.0f);
            if (!Core.Instances.Settings.RGB_Color) style.Colors[(int)ImGuiCol.Border] = new Vector4(1.0f, 1.0f, 1.0f, 0.09803921729326248f);
            style.Colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            style.Colors[(int)ImGuiCol.FrameBg] = new Vector4(1.0f, 1.0f, 1.0f, 0.09803921729326248f);
            style.Colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(1.0f, 1.0f, 1.0f, 0.1568627506494522f);
            style.Colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.0f, 0.0f, 0.0f, 0.0470588244497776f);
            style.Colors[(int)ImGuiCol.TitleBg] = new Vector4(0.1176470592617989f, 0.1176470592617989f, 0.1176470592617989f, 1.0f);
            style.Colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.1568627506494522f, 0.1568627506494522f, 0.1568627506494522f, 1.0f);
            style.Colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.1176470592617989f, 0.1176470592617989f, 0.1176470592617989f, 1.0f);
            style.Colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            style.Colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.0f, 0.0f, 0.0f, 0.1098039224743843f);
            style.Colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(1.0f, 1.0f, 1.0f, 0.3921568691730499f);
            style.Colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(1.0f, 1.0f, 1.0f, 0.4705882370471954f);
            style.Colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.0f, 0.0f, 0.0f, 0.09803921729326248f);
            style.Colors[(int)ImGuiCol.CheckMark] = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            style.Colors[(int)ImGuiCol.SliderGrab] = new Vector4(1.0f, 1.0f, 1.0f, 0.3921568691730499f);
            style.Colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(1.0f, 1.0f, 1.0f, 0.3137255012989044f);
            style.Colors[(int)ImGuiCol.Button] = new Vector4(1.0f, 1.0f, 1.0f, 0.09803921729326248f);
            style.Colors[(int)ImGuiCol.ButtonHovered] = new Vector4(1.0f, 1.0f, 1.0f, 0.1568627506494522f);
            style.Colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.0f, 0.0f, 0.0f, 0.0470588244497776f);
            style.Colors[(int)ImGuiCol.Header] = new Vector4(1.0f, 1.0f, 1.0f, 0.09803921729326248f);
            style.Colors[(int)ImGuiCol.HeaderHovered] = new Vector4(1.0f, 1.0f, 1.0f, 0.1568627506494522f);
            style.Colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.0f, 0.0f, 0.0f, 0.0470588244497776f);
            style.Colors[(int)ImGuiCol.Separator] = new Vector4(1.0f, 1.0f, 1.0f, 0.1568627506494522f);
            style.Colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(1.0f, 1.0f, 1.0f, 0.2352941185235977f);
            style.Colors[(int)ImGuiCol.SeparatorActive] = new Vector4(1.0f, 1.0f, 1.0f, 0.2352941185235977f);
            style.Colors[(int)ImGuiCol.ResizeGrip] = new Vector4(1.0f, 1.0f, 1.0f, 0.1568627506494522f);
            style.Colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(1.0f, 1.0f, 1.0f, 0.2352941185235977f);
            style.Colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(1.0f, 1.0f, 1.0f, 0.2352941185235977f);
            style.Colors[(int)ImGuiCol.Tab] = new Vector4(1.0f, 1.0f, 1.0f, 0.09803921729326248f);
            style.Colors[(int)ImGuiCol.TabHovered] = new Vector4(1.0f, 1.0f, 1.0f, 0.1568627506494522f);
            style.Colors[(int)ImGuiCol.PlotLines] = new Vector4(1.0f, 1.0f, 1.0f, 0.3529411852359772f);
            style.Colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            style.Colors[(int)ImGuiCol.PlotHistogram] = new Vector4(1.0f, 1.0f, 1.0f, 0.3529411852359772f);
            style.Colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            style.Colors[(int)ImGuiCol.TableHeaderBg] = new Vector4(0.1568627506494522f, 0.1568627506494522f, 0.1568627506494522f, 1.0f);
            style.Colors[(int)ImGuiCol.TableBorderStrong] = new Vector4(1.0f, 1.0f, 1.0f, 0.3137255012989044f);
            style.Colors[(int)ImGuiCol.TableBorderLight] = new Vector4(1.0f, 1.0f, 1.0f, 0.196078434586525f);
            style.Colors[(int)ImGuiCol.TableRowBg] = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            style.Colors[(int)ImGuiCol.TableRowBgAlt] = new Vector4(1.0f, 1.0f, 1.0f, 0.01960784383118153f);
            style.Colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
            style.Colors[(int)ImGuiCol.DragDropTarget] = new Vector4(0.168627455830574f, 0.2313725501298904f, 0.5372549295425415f, 1.0f);

            style.Colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1.0f, 1.0f, 1.0f, 0.699999988079071f);
            style.Colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.800000011920929f, 0.800000011920929f, 0.800000011920929f, 0.2000000029802322f);
            style.Colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.0f, 0.0f, 0.0f, 0.5647059082984924f);


            return true;
        }
    }
}
