using Hexa.NET.ImGui;
using System;
using System.Numerics;

namespace EasyModern.UI.Widgets
{
    public class TrackBarWidget
    {
        public string ID { get; set; } = "trackbar.default";
        public string Title { get; set; } = "TrackBar Widget";
        public string Description { get; set; } = "Este widget tiene un trackbar interactivo.";
        public Vector2 Size { get; set; } = new Vector2(300, 150);

        public Vector4 BackgroundColor { get; set; } = new Vector4(0.15f, 0.15f, 0.15f, 1.0f);
        public Vector4 TitleColor { get; set; } = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        public Vector4 DescriptionColor { get; set; } = new Vector4(0.8f, 0.8f, 0.8f, 1.0f);

        // Propiedades del TrackBar
        public float Minimum { get; set; } = 0.0f;
        public float Maximum { get; set; } = 100.0f;
        public float Value = 50.0f;
        public bool FloatValue = true;

        public Vector4 TrackBarBackgroundColor { get; set; } = new Vector4(0.2f, 0.2f, 0.2f, 1.0f);
        public Vector4 TrackBarFillColor { get; set; } = new Vector4(0.3f, 0.7f, 0.3f, 1.0f);
        public Vector4 TrackBarHoveredColor { get; set; } = new Vector4(0.4f, 0.8f, 0.4f, 1.0f);
        public Vector4 TrackBarThumbColor { get; set; } = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);

        public event EventHandler ValueChanged;

        protected virtual void OnValueChanged(EventArgs e) => ValueChanged?.Invoke(this, e);

        public void Render()
        {
            Size = new Vector2(Size.X, Math.Max(Size.Y, ImGui.GetTextLineHeightWithSpacing() * 3));

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

            // Renderizar el TrackBar
            float trackBarY = localMax.Y - 25.0f;
            ImGui.SetCursorPos(new Vector2(localMin.X + 15.0f, trackBarY));
            ImGui.PushItemWidth(Size.X - 30.0f);

            // Aplicar colores personalizados al TrackBar
            ImGui.PushStyleColor(ImGuiCol.FrameBg, ImGui.ColorConvertFloat4ToU32(TrackBarBackgroundColor));
            ImGui.PushStyleColor(ImGuiCol.SliderGrab, ImGui.ColorConvertFloat4ToU32(TrackBarThumbColor));
            ImGui.PushStyleColor(ImGuiCol.FrameBgHovered, ImGui.ColorConvertFloat4ToU32(TrackBarHoveredColor));
            ImGui.PushStyleColor(ImGuiCol.SliderGrabActive, ImGui.ColorConvertFloat4ToU32(TrackBarFillColor));


            if (FloatValue)
            {
                float oldValue = Value;
                if (ImGui.SliderFloat("##TrackBar" + ID, ref Value, Minimum, Maximum))
                {
                    if (Math.Abs(Value - oldValue) > float.Epsilon)
                    {
                        OnValueChanged(EventArgs.Empty);
                    }
                }
            }
            else
            {
                int oldValue = (int)Value;
                if (ImGui.SliderInt("##TrackBar" + ID, ref oldValue, (int)Minimum, (int)Maximum))
                {
                    if (Math.Abs(Value - oldValue) > float.Epsilon)
                    {
                        OnValueChanged(EventArgs.Empty);
                    }
                }
                Value = oldValue;
            }


            ImGui.PopStyleColor(4);
            ImGui.PopItemWidth();

            ImGui.EndChild();
        }
    }

}
