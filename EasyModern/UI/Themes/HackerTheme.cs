using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class HackerTheme : ITheme
    {
        public string ID { get; set; } = "theme.hacker";

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var colors = style.Colors;

            // Colores oscuros con acentos verdes y rojos para efectos "hacker"
            colors[(int)ImGuiCol.Text] = new Vector4(0.0f, 1.0f, 0.0f, 1.0f); // Verde brillante
            colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.0f, 0.5f, 0.0f, 1.0f);
            colors[(int)ImGuiCol.WindowBg] = new Vector4(0.0f, 0.0f, 0.0f, 1.0f); // Fondo negro puro
            colors[(int)ImGuiCol.ChildBg] = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
            colors[(int)ImGuiCol.PopupBg] = new Vector4(0.1f, 0.1f, 0.1f, 1.0f); // Negro tenue
            if (!Core.Instances.Settings.RGB_Color) colors[(int)ImGuiCol.Border] = new Vector4(0.0f, 1.0f, 0.0f, 0.7f); // Bordes verdes translúcidos
            colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            colors[(int)ImGuiCol.FrameBg] = new Vector4(0.0f, 0.2f, 0.0f, 1.0f); // Verde oscuro
            colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.0f, 0.5f, 0.0f, 1.0f); // Verde medio
            colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.0f, 1.0f, 0.0f, 1.0f); // Verde brillante
            colors[(int)ImGuiCol.TitleBg] = new Vector4(0.0f, 0.3f, 0.0f, 1.0f); // Verde oscuro
            colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.0f, 0.5f, 0.0f, 1.0f); // Verde medio
            colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.0f, 0.2f, 0.0f, 0.7f);
            colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.0f, 0.1f, 0.0f, 1.0f);
            colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.0f, 0.1f, 0.0f, 1.0f);
            colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.0f, 0.5f, 0.0f, 1.0f);
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.0f, 0.7f, 0.0f, 1.0f);
            colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
            colors[(int)ImGuiCol.CheckMark] = new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
            colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.0f, 0.5f, 0.0f, 1.0f);
            colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
            colors[(int)ImGuiCol.Button] = new Vector4(0.0f, 0.3f, 0.0f, 1.0f);
            colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.0f, 0.7f, 0.0f, 1.0f);
            colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
            colors[(int)ImGuiCol.Header] = new Vector4(0.0f, 0.5f, 0.0f, 1.0f);
            colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.0f, 0.7f, 0.0f, 1.0f);
            colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
            colors[(int)ImGuiCol.Separator] = new Vector4(0.0f, 1.0f, 0.0f, 0.7f);
            colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(0.0f, 0.9f, 0.0f, 1.0f);
            colors[(int)ImGuiCol.SeparatorActive] = new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
            colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.0f, 1.0f, 0.0f, 0.5f);

            // Propiedades de estilo para resaltar el diseño
            style.WindowRounding = 0.0f;
            style.FrameRounding = 3.0f;
            style.GrabRounding = 2.0f;
            style.ScrollbarRounding = 2.0f;

            style.WindowPadding = new Vector2(10.0f, 10.0f);
            style.FramePadding = new Vector2(5.0f, 5.0f);
            style.ItemSpacing = new Vector2(10.0f, 6.0f);

            // Transparencia global
            style.Alpha = 1.0f; // Opaco para intensificar la estética "hacker"

            return true;
        }
    }

}
