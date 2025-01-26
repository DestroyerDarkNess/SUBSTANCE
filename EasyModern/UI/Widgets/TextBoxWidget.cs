using Hexa.NET.ImGui;
using System;
using System.Numerics;

namespace EasyModern.UI.Widgets
{
    public class TextBoxWidget
    {
        public string ID { get; set; } = "textbox.default";
        public string Title { get; set; } = "Mi Widget";
        public string Description { get; set; } = "Este widget tiene un textbox interactivo.";
        public Vector2 Size { get; set; } = new Vector2(300, 150);

        public Vector4 BackgroundColor { get; set; } = new Vector4(0.15f, 0.15f, 0.15f, 1.0f);
        public Vector4 TitleColor { get; set; } = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        public Vector4 DescriptionColor { get; set; } = new Vector4(0.8f, 0.8f, 0.8f, 1.0f);

        public float MarginX { get; set; } = 15.0f;
        public float MarginY { get; set; } = 10.0f;
        public float TextBoxMargin { get; set; } = 10.0f;

        // Texto actual del TextBox
        public string CurrentText = string.Empty;

        // Longitud máxima para el InputText
        public uint MaxTextLength { get; set; } = 128;

        // Propiedades para estilos del TextBox
        public Vector4 InputTextColor { get; set; } = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        public Vector4 InputTextBackgroundColor { get; set; } = new Vector4(0.2f, 0.2f, 0.2f, 1.0f);
        public Vector4 InputTextHoveredColor { get; set; } = new Vector4(0.25f, 0.25f, 0.25f, 1.0f);
        public Vector4 InputTextActiveColor { get; set; } = new Vector4(0.3f, 0.3f, 0.3f, 1.0f);

        // Evento que se dispara cuando cambia el texto (mismos parámetros que el ComboBox).
        public event EventHandler SelectedIndexChanged;

        // Para mantener compatibilidad con la firma:
        protected virtual void OnSelectedIndexChanged(EventArgs e) => SelectedIndexChanged?.Invoke(this, e);

        public void Render()
        {
            // Ajuste de alto mínimo
            Size = new Vector2(Size.X, Math.Max(Size.Y, MarginY + 2 * ImGui.GetTextLineHeightWithSpacing() + TextBoxMargin));

            ImGui.BeginChild("TextBoxSection" + ID, new Vector2(Size.X, Size.Y), ImGuiWindowFlags.NoScrollbar);

            Vector4 bgColor = BackgroundColor;
            Vector4 titleColor = TitleColor;
            Vector4 descColor = DescriptionColor;

            Vector2 widgetMin = ImGui.GetCursorScreenPos();
            Vector2 widgetMax = new Vector2(widgetMin.X + Size.X, widgetMin.Y + Size.Y);
            ImDrawListPtr drawList = ImGui.GetWindowDrawList();

            // Fondo principal del widget
            drawList.AddRectFilled(widgetMin, widgetMax, ImGui.ColorConvertFloat4ToU32(bgColor));

            Vector2 localMin = widgetMin - ImGui.GetWindowPos();
            Vector2 localMax = widgetMax - ImGui.GetWindowPos();

            float wrapPos = localMax.X - MarginX;
            ImGui.PushTextWrapPos(wrapPos);

            // Título
            ImGui.SetCursorPos(new Vector2(localMin.X + MarginX, localMin.Y + MarginY));
            ImGui.PushFont(Core.Instances.fontManager.GetFont("widget_title"));
            ImGui.TextColored(titleColor, Title);
            ImGui.PopFont();

            Vector2 titleSize = ImGui.CalcTextSize(Title);
            float lineHeight = ImGui.GetTextLineHeight();
            float descriptionTopOffset = lineHeight + (MarginY * 0.5f);

            // Descripción
            ImGui.SetCursorPos(new Vector2(localMin.X + MarginX, localMin.Y + MarginY + descriptionTopOffset));
            ImGui.PushFont(Core.Instances.fontManager.GetFont("widget_des"));
            ImGui.TextColored(descColor, Description);
            ImGui.PopFont();

            ImGui.PopTextWrapPos();

            // Posicionar el TextBox cerca de la parte inferior del widget
            float textBoxY = localMax.Y - TextBoxMargin - ImGui.GetTextLineHeightWithSpacing();
            ImGui.SetCursorPos(new Vector2(localMin.X + MarginX, textBoxY));
            ImGui.PushItemWidth(Size.X - (2 * MarginX));

            // Establecer estilos para el InputText
            ImGui.PushStyleColor(ImGuiCol.FrameBg, ImGui.ColorConvertFloat4ToU32(InputTextBackgroundColor)); // Fondo
            ImGui.PushStyleColor(ImGuiCol.Text, ImGui.ColorConvertFloat4ToU32(InputTextColor));              // Texto
            ImGui.PushStyleColor(ImGuiCol.FrameBgHovered, ImGui.ColorConvertFloat4ToU32(InputTextHoveredColor));
            ImGui.PushStyleColor(ImGuiCol.FrameBgActive, ImGui.ColorConvertFloat4ToU32(InputTextActiveColor));

            // Renderizar el TextBox
            // Devuelve true si el contenido ha cambiado
            if (ImGui.InputText("##TextBox" + ID, ref CurrentText, MaxTextLength))
            {
                // Disparamos el mismo evento que en el combo para mantener compatibilidad
                OnSelectedIndexChanged(EventArgs.Empty);
            }

            ImGui.PopStyleColor(4);
            ImGui.PopItemWidth();

            ImGui.EndChild();
        }
    }
}
