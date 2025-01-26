using EasyModern.Core.Model;
using Hexa.NET.ImGui;

namespace EasyModern.UI.Themes
{
    internal class DefaultTheme : ITheme
    {
        public string ID { get; set; } = "theme.def";

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var colors = style.Colors;


            return true;
        }
    }
}
