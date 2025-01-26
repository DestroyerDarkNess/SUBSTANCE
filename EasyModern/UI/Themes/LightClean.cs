using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class LightClean : ITheme
    {
        public string ID { get; set; } = "theme.lightclean";

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var colors = style.Colors;


            style.Alpha = 1.0f;
            style.DisabledAlpha = 0.6000000238418579f;
            style.WindowPadding = new Vector2(15.0f, 15.0f);
            style.WindowRounding = 5.0f;
            style.WindowBorderSize = 1.0f;
            style.WindowMinSize = new Vector2(32.0f, 32.0f);
            style.WindowTitleAlign = new Vector2(0.0f, 0.5f);
            style.WindowMenuButtonPosition = ImGuiDir.Left;
            style.ChildRounding = 0.0f;
            style.ChildBorderSize = 1.0f;
            style.PopupRounding = 0.0f;
            style.PopupBorderSize = 1.0f;
            style.FramePadding = new Vector2(5.0f, 5.0f);
            style.FrameRounding = 4.0f;
            style.FrameBorderSize = 0.0f;
            style.ItemSpacing = new Vector2(12.0f, 8.0f);
            style.ItemInnerSpacing = new Vector2(8.0f, 6.0f);
            style.CellPadding = new Vector2(4.0f, 2.0f);
            style.IndentSpacing = 25.0f;
            style.ColumnsMinSpacing = 6.0f;
            style.ScrollbarSize = 15.0f;
            style.ScrollbarRounding = 9.0f;
            style.GrabMinSize = 5.0f;
            style.GrabRounding = 3.0f;
            style.TabRounding = 4.0f;
            style.TabBorderSize = 0.0f;
            style.TabMinWidthForCloseButton = 0.0f;
            style.ColorButtonPosition = ImGuiDir.Right;
            style.ButtonTextAlign = new Vector2(0.5f, 0.5f);
            style.SelectableTextAlign = new Vector2(0.0f, 0.0f);

            style.Colors[(int)ImGuiCol.Text] = new Vector4(0.4000000059604645f, 0.3882353007793427f, 0.3764705955982208f, 1.0f);
            style.Colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.4000000059604645f, 0.3882353007793427f, 0.3764705955982208f, 0.7699999809265137f);
            style.Colors[(int)ImGuiCol.WindowBg] = new Vector4(0.9176470637321472f, 0.9098039269447327f, 0.8784313797950745f, 0.699999988079071f);
            style.Colors[(int)ImGuiCol.ChildBg] = new Vector4(1.0f, 0.9764705896377563f, 0.9490196108818054f, 0.5799999833106995f);
            style.Colors[(int)ImGuiCol.PopupBg] = new Vector4(1.0f, 0.9764705896377563f, 0.9490196108818054f, 1.0f);
            if (!Core.Instances.Settings.RGB_Color) style.Colors[(int)ImGuiCol.Border] = new Vector4(0.8392156958580017f, 0.8274509906768799f, 0.800000011920929f, 0.6499999761581421f);
            style.Colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.9176470637321472f, 0.9098039269447327f, 0.8784313797950745f, 0.0f);
            style.Colors[(int)ImGuiCol.FrameBg] = new Vector4(1.0f, 0.9764705896377563f, 0.9490196108818054f, 1.0f);
            style.Colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.9882352948188782f, 1.0f, 0.4000000059604645f, 0.7799999713897705f);
            style.Colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.2588235437870026f, 1.0f, 0.0f, 1.0f);
            style.Colors[(int)ImGuiCol.TitleBg] = new Vector4(1.0f, 0.9764705896377563f, 0.9490196108818054f, 1.0f);
            style.Colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.2470588237047195f, 1.0f, 0.0f, 1.0f);
            style.Colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(1.0f, 0.9764705896377563f, 0.9490196108818054f, 0.75f);
            style.Colors[(int)ImGuiCol.MenuBarBg] = new Vector4(1.0f, 0.9764705896377563f, 0.9490196108818054f, 0.4699999988079071f);
            style.Colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(1.0f, 0.9764705896377563f, 0.9490196108818054f, 1.0f);
            style.Colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.0f, 0.0f, 0.0f, 0.2099999934434891f);
            style.Colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.8980392217636108f, 0.9098039269447327f, 0.0f, 0.7799999713897705f);
            style.Colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.2470588237047195f, 1.0f, 0.0f, 1.0f);
            style.Colors[(int)ImGuiCol.CheckMark] = new Vector4(0.2470588237047195f, 1.0f, 0.0f, 0.800000011920929f);
            style.Colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.0f, 0.0f, 0.0f, 0.1400000005960464f);
            style.Colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.2470588237047195f, 1.0f, 0.0f, 1.0f);
            style.Colors[(int)ImGuiCol.Button] = new Vector4(229, 233, 230, 255);
            style.Colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.0f, 0.0f, 0.0f, 0.1400000005960464f);
            style.Colors[(int)ImGuiCol.ButtonActive] = new Vector4(229, 233, 230, 255);
            style.Colors[(int)ImGuiCol.Header] = new Vector4(0.2470588237047195f, 1.0f, 0.0f, 0.7599999904632568f);
            style.Colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.2470588237047195f, 1.0f, 0.0f, 0.8600000143051147f);
            style.Colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.2470588237047195f, 1.0f, 0.0f, 1.0f);
            style.Colors[(int)ImGuiCol.Separator] = new Vector4(0.0f, 0.0f, 0.0f, 0.3199999928474426f);
            style.Colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(0.2470588237047195f, 1.0f, 0.0f, 0.7799999713897705f);
            style.Colors[(int)ImGuiCol.SeparatorActive] = new Vector4(0.2470588237047195f, 1.0f, 0.0f, 1.0f);
            style.Colors[(int)ImGuiCol.ResizeGrip] = new Vector4(0.0f, 0.0f, 0.0f, 0.03999999910593033f);
            style.Colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(0.2470588237047195f, 1.0f, 0.0f, 0.7799999713897705f);
            style.Colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(0.2470588237047195f, 1.0f, 0.0f, 1.0f);
            style.Colors[(int)ImGuiCol.Tab] = new Vector4(0.1764705926179886f, 0.3490196168422699f, 0.5764706134796143f, 0.8619999885559082f);
            style.Colors[(int)ImGuiCol.TabHovered] = new Vector4(0.2588235437870026f, 0.5882353186607361f, 0.9764705896377563f, 0.800000011920929f);

            style.Colors[(int)ImGuiCol.PlotLines] = new Vector4(0.4000000059604645f, 0.3882353007793427f, 0.3764705955982208f, 0.6299999952316284f);
            style.Colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(0.2470588237047195f, 1.0f, 0.0f, 1.0f);
            style.Colors[(int)ImGuiCol.PlotHistogram] = new Vector4(0.4000000059604645f, 0.3882353007793427f, 0.3764705955982208f, 0.6299999952316284f);
            style.Colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(0.2470588237047195f, 1.0f, 0.0f, 1.0f);
            style.Colors[(int)ImGuiCol.TableHeaderBg] = new Vector4(0.1882352977991104f, 0.1882352977991104f, 0.2000000029802322f, 1.0f);
            style.Colors[(int)ImGuiCol.TableBorderStrong] = new Vector4(0.3098039329051971f, 0.3098039329051971f, 0.3490196168422699f, 1.0f);
            style.Colors[(int)ImGuiCol.TableBorderLight] = new Vector4(0.2274509817361832f, 0.2274509817361832f, 0.2470588237047195f, 1.0f);
            style.Colors[(int)ImGuiCol.TableRowBg] = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            style.Colors[(int)ImGuiCol.TableRowBgAlt] = new Vector4(1.0f, 1.0f, 1.0f, 0.05999999865889549f);
            style.Colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.2470588237047195f, 1.0f, 0.0f, 0.4300000071525574f);
            style.Colors[(int)ImGuiCol.DragDropTarget] = new Vector4(1.0f, 1.0f, 0.0f, 0.8999999761581421f);
            style.Colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1.0f, 1.0f, 1.0f, 0.699999988079071f);
            style.Colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.800000011920929f, 0.800000011920929f, 0.800000011920929f, 0.2000000029802322f);
            style.Colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(1.0f, 0.9764705896377563f, 0.9490196108818054f, 0.7300000190734863f);


            return true;
        }
    }
}
