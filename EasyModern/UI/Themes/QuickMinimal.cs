using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class QuickMinimal : ITheme
    {
        public string ID { get; set; } = "theme.quickminimal";

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var colors = style.Colors;


            style.Alpha = 1.0f;
            style.DisabledAlpha = 0.300000011920929f;
            style.WindowPadding = new Vector2(6.5f, 2.700000047683716f);
            style.WindowRounding = 0.0f;
            style.WindowBorderSize = 1.0f;
            style.WindowMinSize = new Vector2(20.0f, 32.0f);
            style.WindowTitleAlign = new Vector2(0.0f, 0.6000000238418579f);
            style.WindowMenuButtonPosition = ImGuiDir.None;
            style.ChildRounding = 0.0f;
            style.ChildBorderSize = 1.0f;
            style.PopupRounding = 10.10000038146973f;
            style.PopupBorderSize = 1.0f;
            style.FramePadding = new Vector2(20.0f, 3.5f);
            style.FrameRounding = 0.0f;
            style.FrameBorderSize = 0.0f;
            style.ItemSpacing = new Vector2(4.400000095367432f, 4.0f);
            style.ItemInnerSpacing = new Vector2(4.599999904632568f, 3.599999904632568f);
            style.CellPadding = new Vector2(3.099999904632568f, 6.300000190734863f);
            style.IndentSpacing = 4.400000095367432f;
            style.ColumnsMinSpacing = 5.400000095367432f;
            style.ScrollbarSize = 8.800000190734863f;
            style.ScrollbarRounding = 9.0f;
            style.GrabMinSize = 9.399999618530273f;
            style.GrabRounding = 0.0f;
            style.TabRounding = 0.0f;
            style.TabBorderSize = 0.0f;
            style.TabMinWidthForCloseButton = 0.0f;
            style.ColorButtonPosition = ImGuiDir.Right;
            style.ButtonTextAlign = new Vector2(0.5f, 0.5f);
            style.SelectableTextAlign = new Vector2(0.0f, 0.0f);

            style.Colors[(int)ImGuiCol.Text] = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            style.Colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.4980392158031464f, 0.4980392158031464f, 0.4980392158031464f, 1.0f);
            style.Colors[(int)ImGuiCol.WindowBg] = new Vector4(0.05098039284348488f, 0.03529411926865578f, 0.03921568766236305f, 1.0f);
            style.Colors[(int)ImGuiCol.ChildBg] = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            style.Colors[(int)ImGuiCol.PopupBg] = new Vector4(0.0784313753247261f, 0.0784313753247261f, 0.0784313753247261f, 0.9399999976158142f);
            if (!Core.Instances.Settings.RGB_Color) style.Colors[(int)ImGuiCol.Border] = new Vector4(0.1019607856869698f, 0.1019607856869698f, 0.1019607856869698f, 0.5f);
            style.Colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            style.Colors[(int)ImGuiCol.FrameBg] = new Vector4(0.1607843190431595f, 0.1490196138620377f, 0.1921568661928177f, 1.0f);
            style.Colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.2784313857555389f, 0.250980406999588f, 0.3372549116611481f, 1.0f);
            style.Colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.2784313857555389f, 0.250980406999588f, 0.3372549116611481f, 1.0f);
            style.Colors[(int)ImGuiCol.TitleBg] = new Vector4(0.2784313857555389f, 0.250980406999588f, 0.3372549116611481f, 1.0f);
            style.Colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.2784313857555389f, 0.250980406999588f, 0.3372549116611481f, 1.0f);
            style.Colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.0f, 0.0f, 0.0f, 0.5099999904632568f);
            style.Colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.1372549086809158f, 0.1372549086809158f, 0.1372549086809158f, 1.0f);
            style.Colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.01960784383118153f, 0.01960784383118153f, 0.01960784383118153f, 0.5299999713897705f);
            style.Colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.3098039329051971f, 0.3098039329051971f, 0.3098039329051971f, 1.0f);
            style.Colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.407843142747879f, 0.407843142747879f, 0.407843142747879f, 1.0f);
            style.Colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.5098039507865906f, 0.5098039507865906f, 0.5098039507865906f, 1.0f);
            style.Colors[(int)ImGuiCol.CheckMark] = new Vector4(0.5450980663299561f, 0.4666666686534882f, 0.7176470756530762f, 1.0f);
            style.Colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.2784313857555389f, 0.250980406999588f, 0.3372549116611481f, 1.0f);
            style.Colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.2784313857555389f, 0.250980406999588f, 0.3372549116611481f, 1.0f);
            style.Colors[(int)ImGuiCol.Button] = new Vector4(0.2784313857555389f, 0.250980406999588f, 0.3372549116611481f, 1.0f);
            style.Colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.3450980484485626f, 0.294117659330368f, 0.4588235318660736f, 1.0f);
            style.Colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.3137255012989044f, 0.2588235437870026f, 0.4274509847164154f, 1.0f);
            style.Colors[(int)ImGuiCol.Header] = new Vector4(0.3176470696926117f, 0.2784313857555389f, 0.407843142747879f, 1.0f);
            style.Colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.4156862795352936f, 0.364705890417099f, 0.529411792755127f, 1.0f);
            style.Colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.4039215743541718f, 0.3529411852359772f, 0.5098039507865906f, 1.0f);
            style.Colors[(int)ImGuiCol.Separator] = new Vector4(0.4274509847164154f, 0.4274509847164154f, 0.4980392158031464f, 0.5f);
            style.Colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(0.2784313857555389f, 0.250980406999588f, 0.3372549116611481f, 1.0f);
            style.Colors[(int)ImGuiCol.SeparatorActive] = new Vector4(0.2784313857555389f, 0.250980406999588f, 0.3372549116611481f, 1.0f);
            style.Colors[(int)ImGuiCol.ResizeGrip] = new Vector4(0.2784313857555389f, 0.250980406999588f, 0.3372549116611481f, 1.0f);
            style.Colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(0.2784313857555389f, 0.250980406999588f, 0.3372549116611481f, 1.0f);
            style.Colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(0.2784313857555389f, 0.250980406999588f, 0.3372549116611481f, 1.0f);
            style.Colors[(int)ImGuiCol.Tab] = new Vector4(0.2784313857555389f, 0.250980406999588f, 0.3372549116611481f, 1.0f);
            style.Colors[(int)ImGuiCol.TabHovered] = new Vector4(0.3254902064800262f, 0.2862745225429535f, 0.4156862795352936f, 1.0f);

            style.Colors[(int)ImGuiCol.PlotLines] = new Vector4(0.6078431606292725f, 0.6078431606292725f, 0.6078431606292725f, 1.0f);
            style.Colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(1.0f, 0.4274509847164154f, 0.3490196168422699f, 1.0f);
            style.Colors[(int)ImGuiCol.PlotHistogram] = new Vector4(0.8980392217636108f, 0.6980392336845398f, 0.0f, 1.0f);
            style.Colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(1.0f, 0.6000000238418579f, 0.0f, 1.0f);
            style.Colors[(int)ImGuiCol.TableHeaderBg] = new Vector4(0.1882352977991104f, 0.1882352977991104f, 0.2000000029802322f, 1.0f);
            style.Colors[(int)ImGuiCol.TableBorderStrong] = new Vector4(0.3098039329051971f, 0.3098039329051971f, 0.3490196168422699f, 1.0f);
            style.Colors[(int)ImGuiCol.TableBorderLight] = new Vector4(0.2274509817361832f, 0.2274509817361832f, 0.2470588237047195f, 1.0f);
            style.Colors[(int)ImGuiCol.TableRowBg] = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            style.Colors[(int)ImGuiCol.TableRowBgAlt] = new Vector4(1.0f, 1.0f, 1.0f, 0.05999999865889549f);
            style.Colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.2588235437870026f, 0.5882353186607361f, 0.9764705896377563f, 0.3499999940395355f);
            style.Colors[(int)ImGuiCol.DragDropTarget] = new Vector4(1.0f, 1.0f, 0.0f, 0.8999999761581421f);

            style.Colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1.0f, 1.0f, 1.0f, 0.699999988079071f);
            style.Colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.800000011920929f, 0.800000011920929f, 0.800000011920929f, 0.2000000029802322f);
            style.Colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.800000011920929f, 0.800000011920929f, 0.800000011920929f, 0.3499999940395355f);


            return true;
        }
    }
}
