using Hexa.NET.ImGui;
using System;
using System.Numerics;

namespace EasyModern.UI.Widgets
{
    public class ColorPickerWidget
    {
        public string ID { get; set; } = "colorpicker.default";
        public string Title { get; set; } = "Color Picker";
        public string Description { get; set; } = "Selecciona un color.";
        public Vector2 Size { get; set; } = new Vector2(300, 150);

        public Vector4 BackgroundColor { get; set; } = new Vector4(0.15f, 0.15f, 0.15f, 1.0f);
        public Vector4 TitleColor { get; set; } = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        public Vector4 DescriptionColor { get; set; } = new Vector4(0.8f, 0.8f, 0.8f, 1.0f);

        // Propiedades del selector de color
        public Vector4 SelectedColor = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
        public bool EnableAlpha { get; set; } = true; // Controla si se muestra la barra de alpha

        public event EventHandler ColorChanged;

        protected virtual void OnColorChanged(EventArgs e) => ColorChanged?.Invoke(this, e);

        public void Render()
        {
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

            float wrapPos = localMax.X - 15.0f;
            ImGui.PushTextWrapPos(wrapPos);

            // Renderizar el título
            ImGui.SetCursorPos(new Vector2(localMin.X + 15.0f, localMin.Y + 10.0f));
            ImGui.PushFont(Core.Instances.fontManager.GetFont("widget_title"));
            ImGui.TextColored(titleColor, Title);
            ImGui.PopFont();

            // Renderizar la descripción
            Vector2 titleSize = ImGui.CalcTextSize(Title);
            float lineHeight = ImGui.GetTextLineHeight();
            float descriptionTopOffset = lineHeight + 5.0f;

            ImGui.SetCursorPos(new Vector2(localMin.X + 15.0f, localMin.Y + 10.0f + descriptionTopOffset));
            ImGui.PushFont(Core.Instances.fontManager.GetFont("widget_des"));
            ImGui.TextColored(descColor, Description);
            ImGui.PopFont();

            ImGui.PopTextWrapPos();

            // Renderizar el selector de color en la esquina inferior derecha
            float margin = 15.0f; // Margen estándar
            Vector2 colorPickerPos = new Vector2(
                localMax.X - margin * 2, // Posicionar en la esquina inferior derecha con un ancho fijo de 100
                localMax.Y - margin * 2  // Ajustar para mantener margen inferior
            );
            ImGui.SetCursorPos(colorPickerPos);
            ImGui.PushItemWidth(100.0f); // Ancho fijo para el selector de color

            Vector4 oldColor = SelectedColor;

            if (EnableAlpha)
            {
                // Selector de color RGBA
                if (ImGui.ColorEdit4("##ColorPicker" + ID, ref SelectedColor, ImGuiColorEditFlags.NoInputs))
                {
                    if (!oldColor.Equals(SelectedColor))
                    {
                        OnColorChanged(EventArgs.Empty);
                    }
                }
            }
            else
            {
                // Selector de color RGB (sin alpha)
                //if (ImGui.ColorEdit3("##ColorPicker" + ID, ref SelectedColor, ImGuiColorEditFlags.NoInputs))
                //{
                //    if (!oldColor.Equals(SelectedColor))
                //    {
                //        OnColorChanged(EventArgs.Empty);
                //    }
                //}
            }

            ImGui.PopItemWidth();
            ImGui.EndChild();
        }
    }


}
