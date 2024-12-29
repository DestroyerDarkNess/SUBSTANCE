using Hexa.NET.ImGui;
using System;
using System.Numerics;

namespace EasyModern.UI.Widgets
{
    public class ComboBoxWidget
    {
        public string ID { get; set; } = "combo.default";
        public string Title { get; set; } = "Mi Widget";
        public string Description { get; set; } = "Este widget tiene un combobox interactivo.";
        public Vector2 Size { get; set; } = new Vector2(300, 150);

        public Vector4 BackgroundColor { get; set; } = new Vector4(0.15f, 0.15f, 0.15f, 1.0f);
        public Vector4 TitleColor { get; set; } = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        public Vector4 DescriptionColor { get; set; } = new Vector4(0.8f, 0.8f, 0.8f, 1.0f);

        public float MarginX { get; set; } = 15.0f;
        public float MarginY { get; set; } = 10.0f;
        public float ComboBoxMargin { get; set; } = 10.0f;

        public string[] ComboBoxItems { get; set; } = new string[] { "Opción 1", "Opción 2", "Opción 3" };
        public int SelectedIndex = 0;

        // Propiedades para estilos del ComboBox
        public Vector4 ComboBoxTextColor { get; set; } = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        public Vector4 ComboBoxBackgroundColor { get; set; } = new Vector4(0.2f, 0.2f, 0.2f, 1.0f);
        public Vector4 ComboBoxHoveredColor { get; set; } = new Vector4(0.25f, 0.25f, 0.25f, 1.0f);
        public Vector4 ComboBoxActiveColor { get; set; } = new Vector4(0.3f, 0.3f, 0.3f, 1.0f);
        public Vector4 ComboBoxItemHoveredColor { get; set; } = new Vector4(0.4f, 0.4f, 0.4f, 1.0f);
        public Vector4 ComboBoxItemSelectedColor { get; set; } = new Vector4(0.6f, 0.6f, 0.6f, 1.0f); // Fondo del ítem seleccionado

        public event EventHandler SelectedIndexChanged;

        protected virtual void OnSelectedIndexChanged(EventArgs e) => SelectedIndexChanged?.Invoke(this, e);

        public void Render()
        {
            Size = new Vector2(Size.X, Math.Max(Size.Y, MarginY + 2 * ImGui.GetTextLineHeightWithSpacing() + ComboBoxMargin));

            ImGui.BeginChild("Section" + ID, new Vector2(Size.X, Size.Y), ImGuiWindowFlags.NoScrollbar);

            Vector4 bgColor = BackgroundColor;
            Vector4 titleColor = TitleColor;
            Vector4 descColor = DescriptionColor;

            Vector2 widgetMin = ImGui.GetCursorScreenPos();
            Vector2 widgetMax = new Vector2(widgetMin.X + Size.X, widgetMin.Y + Size.Y);
            ImDrawListPtr drawList = ImGui.GetWindowDrawList();

            drawList.AddRectFilled(widgetMin, widgetMax, ImGui.ColorConvertFloat4ToU32(bgColor));

            Vector2 localMin = widgetMin - ImGui.GetWindowPos();
            Vector2 localMax = widgetMax - ImGui.GetWindowPos();

            float wrapPos = localMax.X - MarginX;
            ImGui.PushTextWrapPos(wrapPos);

            ImGui.SetCursorPos(new Vector2(localMin.X + MarginX, localMin.Y + MarginY));
            ImGui.PushFont(Core.Instances.fontManager.GetFont("widget_title"));
            ImGui.TextColored(titleColor, Title);
            ImGui.PopFont();

            Vector2 titleSize = ImGui.CalcTextSize(Title);
            float lineHeight = ImGui.GetTextLineHeight();
            float descriptionTopOffset = lineHeight + (MarginY * 0.5f);

            ImGui.SetCursorPos(new Vector2(localMin.X + MarginX, localMin.Y + MarginY + descriptionTopOffset));
            ImGui.PushFont(Core.Instances.fontManager.GetFont("widget_des"));
            ImGui.TextColored(descColor, Description);
            ImGui.PopFont();

            ImGui.PopTextWrapPos();

            float comboBoxY = localMax.Y - ComboBoxMargin - ImGui.GetTextLineHeightWithSpacing();
            ImGui.SetCursorPos(new Vector2(localMin.X + MarginX, comboBoxY));

            ImGui.PushItemWidth(Size.X - (2 * MarginX));

            // Establecer estilos para el ComboBox y su botón
            ImGui.PushStyleColor(ImGuiCol.FrameBg, ImGui.ColorConvertFloat4ToU32(ComboBoxBackgroundColor)); // Fondo del ComboBox
            ImGui.PushStyleColor(ImGuiCol.Text, ImGui.ColorConvertFloat4ToU32(ComboBoxTextColor));         // Texto cerrado
            ImGui.PushStyleColor(ImGuiCol.FrameBgHovered, ImGui.ColorConvertFloat4ToU32(ComboBoxHoveredColor)); // Fondo hover
            ImGui.PushStyleColor(ImGuiCol.FrameBgActive, ImGui.ColorConvertFloat4ToU32(ComboBoxActiveColor));   // Fondo activo
            ImGui.PushStyleColor(ImGuiCol.Button, ImGui.ColorConvertFloat4ToU32(ComboBoxBackgroundColor)); // Fondo del botón
            ImGui.PushStyleColor(ImGuiCol.ButtonHovered, ImGui.ColorConvertFloat4ToU32(ComboBoxHoveredColor)); // Hover del botón
            ImGui.PushStyleColor(ImGuiCol.ButtonActive, ImGui.ColorConvertFloat4ToU32(ComboBoxActiveColor));   // Fondo activo del botón
            ImGui.PushStyleColor(ImGuiCol.HeaderHovered, ImGui.ColorConvertFloat4ToU32(ComboBoxItemHoveredColor)); // Hover de los ítems
            ImGui.PushStyleColor(ImGuiCol.Header, ImGui.ColorConvertFloat4ToU32(ComboBoxItemSelectedColor)); // Fondo del ítem seleccionado
            ImGui.PushStyleColor(ImGuiCol.HeaderActive, ImGui.ColorConvertFloat4ToU32(ComboBoxItemSelectedColor)); // Fondo del ítem seleccionado activo

            if (ImGui.Combo("##ComboBox" + ID, ref SelectedIndex, ComboBoxItems, ComboBoxItems.Length))
            {
                OnSelectedIndexChanged(EventArgs.Empty);
            }

            ImGui.PopStyleColor(10); // Restaurar estilos

            ImGui.PopItemWidth();

            ImGui.EndChild();
        }
    }


}
