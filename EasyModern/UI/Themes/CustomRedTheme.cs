using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class CustomRedTheme : ITheme
    {
        public string ID { get; set; } = "theme.custom-red";

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var colors = style.Colors;

            // Ajustes de estilo
            style.WindowRounding = 2.0f;
            style.WindowTitleAlign = new Vector2(0.5f, 0.84f);
            style.ChildRounding = 2.0f;         // Antes "ChildWindowRounding"
            style.FrameRounding = 2.0f;
            style.ItemSpacing = new Vector2(5.0f, 4.0f);
            style.ScrollbarSize = 13.0f;
            style.ScrollbarRounding = 0.0f;
            style.GrabMinSize = 8.0f;
            style.GrabRounding = 1.0f;

            // Colores
            // Equivalencias: 
            // - ChildWindowBg -> ChildBg
            // - ComboBg -> se usa PopupBg en Dear ImGui actual
            // - ModalWindowDarkening -> ModalWindowDimBg (en versiones recientes)

            colors[(int)ImGuiCol.FrameBg] = new Vector4(0.48f, 0.16f, 0.16f, 0.54f);
            colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.98f, 0.26f, 0.26f, 0.40f);
            colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.98f, 0.26f, 0.26f, 0.67f);

            colors[(int)ImGuiCol.TitleBg] = new Vector4(0.04f, 0.04f, 0.04f, 1.00f);
            colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.48f, 0.16f, 0.16f, 1.00f);
            colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.00f, 0.00f, 0.00f, 0.51f);

            colors[(int)ImGuiCol.CheckMark] = new Vector4(0.98f, 0.26f, 0.26f, 1.00f);
            colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.88f, 0.26f, 0.24f, 1.00f);
            colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.98f, 0.26f, 0.26f, 1.00f);

            colors[(int)ImGuiCol.Button] = new Vector4(0.98f, 0.26f, 0.26f, 0.40f);
            colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.98f, 0.26f, 0.26f, 1.00f);
            colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.98f, 0.06f, 0.06f, 1.00f);

            colors[(int)ImGuiCol.Header] = new Vector4(0.98f, 0.26f, 0.26f, 0.31f);
            colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.98f, 0.26f, 0.26f, 0.80f);
            colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.98f, 0.26f, 0.26f, 1.00f);

            // Separator en Dear ImGui usa ImGuiCol.Separator (y .SeparatorHovered, .SeparatorActive)
            // Fijamos .Separator = .Border
            // .Border está más abajo. Se recomienda hacerlo después de setear .Border si se requiere.
            // Por conveniencia, lo hacemos aquí con un second pass:
            //   colors[(int)ImGuiCol.Separator] = colors[(int)ImGuiCol.Border]; 
            //   etc.
            // Pero lo dejaremos “inline” después de definir .Border.

            colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(0.75f, 0.10f, 0.10f, 0.78f);
            colors[(int)ImGuiCol.SeparatorActive] = new Vector4(0.75f, 0.10f, 0.10f, 1.00f);

            colors[(int)ImGuiCol.ResizeGrip] = new Vector4(0.98f, 0.26f, 0.26f, 0.25f);
            colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(0.98f, 0.26f, 0.26f, 0.67f);
            colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(0.98f, 0.26f, 0.26f, 0.95f);

            colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.98f, 0.26f, 0.26f, 0.35f);
            colors[(int)ImGuiCol.Text] = new Vector4(1.00f, 1.00f, 1.00f, 1.00f);
            colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.50f, 0.50f, 0.50f, 1.00f);

            colors[(int)ImGuiCol.WindowBg] = new Vector4(0.06f, 0.06f, 0.06f, 0.94f);
            // “ChildWindowBg” => “ChildBg”
            colors[(int)ImGuiCol.ChildBg] = new Vector4(1.00f, 1.00f, 1.00f, 0.00f);
            // “PopupBg”
            colors[(int)ImGuiCol.PopupBg] = new Vector4(0.08f, 0.08f, 0.08f, 0.94f);

            // No existe “ComboBg” en versiones recientes, lo reemplazamos con .PopupBg si deseado
            // colors[(int)ImGuiCol.ComboBg] = .PopupBg (ya incluido en la versión actual)

            colors[(int)ImGuiCol.Border] = new Vector4(0.43f, 0.43f, 0.50f, 0.50f);
            colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);

            colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.14f, 0.14f, 0.14f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.02f, 0.02f, 0.02f, 0.53f);
            colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.31f, 0.31f, 0.31f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.41f, 0.41f, 0.41f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.51f, 0.51f, 0.51f, 1.00f);

            // En ImGui moderno, “CloseButton” y “CloseButtonHovered” ya no existen.
            // Si tu versión las tiene, puedes usar:
            // colors[(int)ImGuiCol.CloseButton]          = new Vector4(0.41f, 0.41f, 0.41f, 0.50f);
            // colors[(int)ImGuiCol.CloseButtonHovered]   = new Vector4(0.98f, 0.39f, 0.36f, 1.00f);
            // colors[(int)ImGuiCol.CloseButtonActive]    = new Vector4(0.98f, 0.39f, 0.36f, 1.00f);

            colors[(int)ImGuiCol.PlotLines] = new Vector4(0.61f, 0.61f, 0.61f, 1.00f);
            colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(1.00f, 0.43f, 0.35f, 1.00f);
            colors[(int)ImGuiCol.PlotHistogram] = new Vector4(0.90f, 0.70f, 0.00f, 1.00f);
            colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(1.00f, 0.60f, 0.00f, 1.00f);

            // “ModalWindowDarkening” se llama “ModalWindowDimBg” en versiones recientes
            // si quieres mantener la compatibilidad:
            colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.80f, 0.80f, 0.80f, 0.35f);

            // Ajustamos el .Separator = .Border (comentado en la parte superior)
            colors[(int)ImGuiCol.Separator] = colors[(int)ImGuiCol.Border];

            return true;
        }
    }
}
