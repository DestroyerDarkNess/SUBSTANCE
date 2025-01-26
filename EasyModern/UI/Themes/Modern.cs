using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class Modern : ITheme
    {
        public string ID { get; set; } = "theme.modern";

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var colors = style.Colors;


            style.Alpha = 1.0f;
            style.DisabledAlpha = 0.300000011920929f;
            style.WindowPadding = new Vector2(10.10000038146973f, 10.10000038146973f);
            style.WindowRounding = 10.30000019073486f;
            style.WindowBorderSize = 1.0f;
            style.WindowMinSize = new Vector2(20.0f, 32.0f);
            style.WindowTitleAlign = new Vector2(0.5f, 0.5f);
            style.WindowMenuButtonPosition = ImGuiDir.Right;
            style.ChildRounding = 8.199999809265137f;
            style.ChildBorderSize = 1.0f;
            style.PopupRounding = 10.69999980926514f;
            style.PopupBorderSize = 1.0f;
            style.FramePadding = new Vector2(20.0f, 1.5f);
            style.FrameRounding = 4.800000190734863f;
            style.FrameBorderSize = 0.0f;
            style.ItemSpacing = new Vector2(9.699999809265137f, 5.300000190734863f);
            style.ItemInnerSpacing = new Vector2(5.400000095367432f, 9.300000190734863f);
            style.CellPadding = new Vector2(7.900000095367432f, 2.0f);
            style.IndentSpacing = 10.69999980926514f;
            style.ColumnsMinSpacing = 6.0f;
            style.ScrollbarSize = 12.10000038146973f;
            style.ScrollbarRounding = 20.0f;
            style.GrabMinSize = 10.0f;
            style.GrabRounding = 4.599999904632568f;
            style.TabRounding = 4.0f;
            style.TabBorderSize = 0.0f;
            style.TabMinWidthForCloseButton = 0.0f;
            style.ColorButtonPosition = ImGuiDir.Right;
            style.ButtonTextAlign = new Vector2(0.5f, 0.5f);
            style.SelectableTextAlign = new Vector2(0.0f, 0.0f);

            style.Colors[(int)ImGuiCol.Text] = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            style.Colors[(int)ImGuiCol.TextDisabled] = new Vector4(1.0f, 1.0f, 1.0f, 0.3991416096687317f);
            style.Colors[(int)ImGuiCol.WindowBg] = new Vector4(0.03921568766236305f, 0.03921568766236305f, 0.03921568766236305f, 0.9399999976158142f);
            style.Colors[(int)ImGuiCol.ChildBg] = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            style.Colors[(int)ImGuiCol.PopupBg] = new Vector4(0.05098039284348488f, 0.05098039284348488f, 0.05098039284348488f, 0.9399999976158142f);
            if (!Core.Instances.Settings.RGB_Color) style.Colors[(int)ImGuiCol.Border] = new Vector4(0.4274509847164154f, 0.4274509847164154f, 0.4980392158031464f, 0.5f);
            style.Colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            style.Colors[(int)ImGuiCol.FrameBg] = new Vector4(0.0f, 0.0f, 0.0f, 0.4206008315086365f);
            style.Colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.1411764770746231f, 0.1411764770746231f, 0.1411764770746231f, 0.4000000059604645f);
            style.Colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.2313725501298904f, 0.2313725501298904f, 0.2313725501298904f, 0.8626609444618225f);
            style.Colors[(int)ImGuiCol.TitleBg] = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
            style.Colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.09411764889955521f, 0.09411764889955521f, 0.09411764889955521f, 1.0f);
            style.Colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.0f, 0.0f, 0.0f, 0.2918455004692078f);
            style.Colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.1372549086809158f, 0.1372549086809158f, 0.1372549086809158f, 1.0f);
            style.Colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.01960784383118153f, 0.01960784383118153f, 0.01960784383118153f, 0.5299999713897705f);
            style.Colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.3098039329051971f, 0.3098039329051971f, 0.3098039329051971f, 1.0f);
            style.Colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.407843142747879f, 0.407843142747879f, 0.407843142747879f, 1.0f);
            style.Colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.5098039507865906f, 0.5098039507865906f, 0.5098039507865906f, 1.0f);
            style.Colors[(int)ImGuiCol.CheckMark] = new Vector4(0.9803921580314636f, 0.2588235437870026f, 0.2588235437870026f, 1.0f);
            style.Colors[(int)ImGuiCol.SliderGrab] = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            style.Colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.2588235437870026f, 0.5882353186607361f, 0.9803921580314636f, 1.0f);
            style.Colors[(int)ImGuiCol.Button] = new Vector4(0.0f, 0.0f, 0.0f, 0.5793991088867188f);
            style.Colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.09803921729326248f, 0.09803921729326248f, 0.09803921729326248f, 1.0f);
            style.Colors[(int)ImGuiCol.ButtonActive] = new Vector4(1.0f, 0.2313725501298904f, 0.2313725501298904f, 1.0f);
            style.Colors[(int)ImGuiCol.Header] = new Vector4(0.0f, 0.0f, 0.0f, 0.454935610294342f);
            style.Colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.1803921610116959f, 0.1803921610116959f, 0.1803921610116959f, 0.800000011920929f);
            style.Colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.9764705896377563f, 0.2588235437870026f, 0.2588235437870026f, 1.0f);
            style.Colors[(int)ImGuiCol.Separator] = new Vector4(0.0f, 0.0f, 0.0f, 0.5f);
            style.Colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(0.09803921729326248f, 0.4000000059604645f, 0.7490196228027344f, 0.7799999713897705f);
            style.Colors[(int)ImGuiCol.SeparatorActive] = new Vector4(0.09803921729326248f, 0.4000000059604645f, 0.7490196228027344f, 1.0f);
            style.Colors[(int)ImGuiCol.ResizeGrip] = new Vector4(0.2588235437870026f, 0.5882353186607361f, 0.9764705896377563f, 0.2000000029802322f);
            style.Colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(0.2588235437870026f, 0.5882353186607361f, 0.9764705896377563f, 0.6700000166893005f);
            style.Colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(0.2588235437870026f, 0.5882353186607361f, 0.9764705896377563f, 0.949999988079071f);
            style.Colors[(int)ImGuiCol.Tab] = new Vector4(0.105882354080677f, 0.105882354080677f, 0.105882354080677f, 1.0f);
            style.Colors[(int)ImGuiCol.TabHovered] = new Vector4(1.0f, 0.364705890417099f, 0.6745098233222961f, 0.800000011920929f);
            style.Colors[(int)ImGuiCol.PlotLines] = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            style.Colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(1.0f, 0.4274509847164154f, 0.3490196168422699f, 1.0f);
            style.Colors[(int)ImGuiCol.PlotHistogram] = new Vector4(1.0f, 0.2156862765550613f, 0.2156862765550613f, 1.0f);
            style.Colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(1.0f, 0.2156862765550613f, 0.6980392336845398f, 1.0f);
            style.Colors[(int)ImGuiCol.TableHeaderBg] = new Vector4(1.0f, 0.2352941185235977f, 0.2352941185235977f, 1.0f);
            style.Colors[(int)ImGuiCol.TableBorderStrong] = new Vector4(1.0f, 0.3176470696926117f, 0.3176470696926117f, 1.0f);
            style.Colors[(int)ImGuiCol.TableBorderLight] = new Vector4(1.0f, 0.5647059082984924f, 0.5647059082984924f, 0.3690987229347229f);
            style.Colors[(int)ImGuiCol.TableRowBg] = new Vector4(0.7254902124404907f, 0.3372549116611481f, 1.0f, 0.0f);
            style.Colors[(int)ImGuiCol.TableRowBgAlt] = new Vector4(1.0f, 0.2745098173618317f, 0.2745098173618317f, 0.1115880012512207f);
            style.Colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.9764705896377563f, 0.2588235437870026f, 0.2588235437870026f, 1.0f);
            style.Colors[(int)ImGuiCol.DragDropTarget] = new Vector4(1.0f, 1.0f, 0.0f, 0.8999999761581421f);
            style.Colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1.0f, 1.0f, 1.0f, 0.4678111672401428f);
            style.Colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.0f, 0.0f, 0.0f, 0.733905553817749f);
            style.Colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.0f, 0.0f, 0.0f, 0.7982832789421082f);


            return true;
        }
    }
}
