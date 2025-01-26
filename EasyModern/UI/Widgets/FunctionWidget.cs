using Hexa.NET.ImGui;
using System;
using System.Numerics;

namespace EasyModern.UI.Widgets
{
    public class FunctionWidget
    {
        public string ID { get; set; } = "func.default";
        public string Title { get; set; } = "Mi Función";
        public string Description { get; set; } = "Esta función se encarga de realizar operaciones complejas.";
        public Vector2 Size { get; set; } = new Vector2(300, 150);

        public Vector4 BackgroundColor { get; set; } = new Vector4(0.15f, 0.15f, 0.15f, 1.0f);
        public Vector4 TitleColor { get; set; } = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        public Vector4 DescriptionColor { get; set; } = new Vector4(0.8f, 0.8f, 0.8f, 1.0f);
        public Vector4 BorderColor { get; set; } = new Vector4(1, 1, 1, 1);

        public Vector4 OnColor { get; set; } = new Vector4(0.3f, 1.0f, 0.3f, 1.0f);
        public Vector4 OffColor { get; set; } = new Vector4(1.0f, 0.3f, 0.3f, 1.0f);
        public bool ShowStatusLabel { get; set; } = true;

        public float MarginX { get; set; } = 15.0f;
        public float MarginY { get; set; } = 10.0f;
        public float CornerSize { get; set; } = 15.0f;
        public float LineThickness { get; set; } = 1.0f;
        public float BorderPercent { get; set; } = 0.5f;

        public bool Animating { get; set; } = false;
        public float BorderOffset { get; set; } = 0.0f;

        public float AnimationSpeed { get; set; } = 0.5f;

        private bool _checked = false;
        public bool Checked
        {
            get => _checked;
            set
            {
                if (_checked != value)
                {
                    _checked = value;
                    OnCheckedChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler CheckedChanged;
        protected virtual void OnCheckedChanged(EventArgs e) => CheckedChanged?.Invoke(this, e);

        public float FadeSpeed { get; set; } = 0.1f;
        private float currentBorderAlpha = 0.0f;

        // Propiedades del botón ícono
        public string BottomRightIconName { get; set; } = "atom_icon";
        public Vector4 BottomRightIconBgColor { get; set; } = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        public bool IconButtonVisible { get; set; } = true;
        public float IconButtonRounding { get; set; } = 5.0f;
        public float IconButtonSize { get; set; } = 20.0f;  // Tamaño manual del botón ícono, cuadrado

        public bool Enabled { get; set; } = true;

        // Evento para el botón ícono
        public event EventHandler ButtonClicked;
        protected virtual void OnButtonClicked(EventArgs e) => ButtonClicked?.Invoke(this, e);

        public void Render()
        {
            ImGui.BeginChild("Section" + ID, new Vector2(Size.X, Size.Y + 1), ImGuiWindowFlags.NoScrollbar);

            // Asegurar que BorderPercent esté entre 0 y 1
            BorderPercent = Math.Max(0.0f, Math.Min(1.0f, BorderPercent));

            // Ajustar colores si el widget está deshabilitado
            Vector4 bgColor = BackgroundColor;
            Vector4 titleColor = TitleColor;
            Vector4 descColor = DescriptionColor;
            Vector4 borderColor = BorderColor;
            Vector4 onColor = OnColor;
            Vector4 offColor = OffColor;
            Vector4 iconBgColor = BottomRightIconBgColor;

            if (!Enabled)
            {
                bgColor = new Vector4(0.2f, 0.2f, 0.2f, 1.0f);
                Vector4 grayLight = new Vector4(0.7f, 0.7f, 0.7f, 1.0f);
                titleColor = grayLight;
                descColor = grayLight;
                borderColor = grayLight;
                onColor = grayLight;
                offColor = grayLight;
                iconBgColor = grayLight;
            }

            Vector2 widgetMin = ImGui.GetCursorScreenPos();
            Vector2 widgetMax = new Vector2(widgetMin.X + Size.X, widgetMin.Y + Size.Y);
            ImDrawListPtr drawList = ImGui.GetWindowDrawList();

            // Dibujar fondo
            drawList.AddRectFilled(widgetMin, widgetMax, ImGui.ColorConvertFloat4ToU32(bgColor));

            Vector2 localMin = widgetMin - ImGui.GetWindowPos();
            Vector2 localMax = widgetMax - ImGui.GetWindowPos();

            float wrapPos = localMax.X - MarginX;
            ImGui.PushTextWrapPos(wrapPos);

            // Título
            ImGui.SetCursorPos(new Vector2(localMin.X + MarginX, localMin.Y + MarginY));
            Vector4 actualTitleColor = Checked ? titleColor : descColor;
            ImGui.PushFont(Core.Instances.fontManager.GetFont("widget_title"));
            ImGui.TextColored(actualTitleColor, Title);
            ImGui.PopFont();
            Vector2 titleSize = ImGui.CalcTextSize(Title);
            float lineHeight = ImGui.GetTextLineHeight();
            float descriptionTopOffset = lineHeight + (MarginY * 0.5f);

            // Descripción
            ImGui.SetCursorPos(new Vector2(localMin.X + MarginX, localMin.Y + MarginY + descriptionTopOffset));
            ImGui.PushFont(Core.Instances.fontManager.GetFont("widget_des"));
            ImGui.TextColored(descColor, Description);
            ImGui.PopFont();

            if (ShowStatusLabel)
            {
                // ON/OFF
                string onOffText = Checked ? "ON" : "OFF";
                Vector2 onOffSize = ImGui.CalcTextSize(onOffText);

                float bottomY = localMax.Y - MarginY - onOffSize.Y;
                float bottomX = localMin.X + MarginX;
                ImGui.SetCursorPos(new Vector2(bottomX, bottomY));
                Vector4 onOffVec = Checked ? onColor : offColor;
                ImGui.TextColored(onOffVec, onOffText);

                ImGui.PopTextWrapPos();
            }

            // Borde
            float targetAlpha = Checked ? 1.0f : 0.0f;
            currentBorderAlpha += (targetAlpha - currentBorderAlpha) * FadeSpeed;
            Vector4 adjustedBorderColor = new Vector4(borderColor.X, borderColor.Y, borderColor.Z, borderColor.W * currentBorderAlpha);
            uint borderColU32 = ImGui.ColorConvertFloat4ToU32(adjustedBorderColor);

            if (currentBorderAlpha > 0.01f)
            {
                // Ajustamos la animación del offset usando la nueva propiedad AnimationSpeed
                if (Animating)
                {
                    float dt = ImGui.GetIO().DeltaTime;
                    BorderOffset += dt * AnimationSpeed;  // Controla la velocidad
                    BorderOffset %= 1.0f;                 // Ciclo infinito en [0..1)

                    DrawAnimatedBorder(drawList, widgetMin, widgetMax, BorderPercent, borderColU32, LineThickness);
                }
                else
                {
                    DrawStaticBorder(drawList, widgetMin, widgetMax, BorderPercent, borderColU32, LineThickness);
                }
            }

            // Área del botón ícono (cuadrado del tamaño IconButtonSize)
            Vector2 iconBoxMin = new Vector2(widgetMax.X - MarginX - IconButtonSize, widgetMax.Y - MarginY - IconButtonSize);
            Vector2 iconBoxMax = iconBoxMin + new Vector2(IconButtonSize, IconButtonSize);

            // Dibujar ícono si existe y es visible
            if (IconButtonVisible && ButtonClicked != null)
            {
                uint iconBgU32 = ImGui.ColorConvertFloat4ToU32(iconBgColor);
                drawList.AddRectFilled(iconBoxMin, iconBoxMax, iconBgU32, IconButtonRounding);

                ImTextureID icon = Core.Instances.ImageManager.GetImage(BottomRightIconName);
                if (!icon.IsNull)
                {
                    drawList.AddImage(icon, iconBoxMin, iconBoxMax);
                }
            }

            // Un solo InvisibleButton para todo el widget
            ImGui.SetCursorScreenPos(widgetMin);
            if (ImGui.InvisibleButton(ID, Size))
            {
                Vector2 mousePos = ImGui.GetIO().MousePos;
                bool insideIcon = false;
                if (IconButtonVisible && ButtonClicked != null)
                {
                    insideIcon = (mousePos.X >= iconBoxMin.X && mousePos.X < iconBoxMax.X &&
                                  mousePos.Y >= iconBoxMin.Y && mousePos.Y < iconBoxMax.Y);
                }

                if (insideIcon && Enabled)
                {
                    // Clic en el botón ícono
                    ButtonClicked?.Invoke(this, EventArgs.Empty);
                }
                else if (Enabled)
                {
                    // Clic en el resto del widget
                    Checked = !Checked;
                }
            }

            ImGui.EndChild();
        }

        private void DrawStaticBorder(ImDrawListPtr drawList, Vector2 widgetMin, Vector2 widgetMax, float borderPercent, uint borderColU32, float lineThickness)
        {
            var (topMinX, topMidX, topMidX2, topMaxX) = CalcHorizontalLines(widgetMin.X, widgetMax.X, CornerSize, borderPercent);
            var (botMinX, botMidX, botMidX2, botMaxX) = CalcHorizontalLines(widgetMin.X, widgetMax.X, CornerSize, borderPercent);
            var (leftMinY, leftMidY, leftMidY2, leftMaxY) = CalcVerticalLines(widgetMin.Y, widgetMax.Y, CornerSize, borderPercent);
            var (rightMinY, rightMidY, rightMidY2, rightMaxY) = CalcVerticalLines(widgetMin.Y, widgetMax.Y, CornerSize, borderPercent);

            float rightX = widgetMax.X - 1;

            drawList.AddLine(new Vector2(topMinX, widgetMin.Y), new Vector2(topMidX, widgetMin.Y), borderColU32, lineThickness);
            drawList.AddLine(new Vector2(topMidX2, widgetMin.Y), new Vector2(topMaxX, widgetMin.Y), borderColU32, lineThickness);

            drawList.AddLine(new Vector2(botMinX, widgetMax.Y), new Vector2(botMidX, widgetMax.Y), borderColU32, lineThickness);
            drawList.AddLine(new Vector2(botMidX2, widgetMax.Y), new Vector2(botMaxX, widgetMax.Y), borderColU32, lineThickness);

            drawList.AddLine(new Vector2(widgetMin.X, leftMinY), new Vector2(widgetMin.X, leftMidY), borderColU32, lineThickness);
            drawList.AddLine(new Vector2(widgetMin.X, leftMidY2), new Vector2(widgetMin.X, leftMaxY), borderColU32, lineThickness);

            drawList.AddLine(new Vector2(rightX, rightMinY), new Vector2(rightX, rightMidY), borderColU32, lineThickness);
            drawList.AddLine(new Vector2(rightX, rightMidY2), new Vector2(rightX, rightMaxY), borderColU32, lineThickness);
        }

        private void DrawAnimatedBorder(ImDrawListPtr drawList, Vector2 widgetMin, Vector2 widgetMax, float borderPercent, uint borderColU32, float lineThickness)
        {
            float length = borderPercent;
            float start = BorderOffset;
            float end = start + length;

            int steps = 30;

            if (end > 1.0f)
            {
                float firstSegment = 1.0f - start;
                float secondSegment = end - 1.0f;

                Vector2 startPos = GetPointOnPerimeter(widgetMin, widgetMax, start);
                for (int i = 1; i <= steps; i++)
                {
                    float t = start + (firstSegment * i / steps);
                    Vector2 pos = GetPointOnPerimeter(widgetMin, widgetMax, t);
                    drawList.AddLine(startPos, pos, borderColU32, lineThickness);
                    startPos = pos;
                }

                startPos = GetPointOnPerimeter(widgetMin, widgetMax, 0.0f);
                for (int i = 1; i <= steps; i++)
                {
                    float t = (secondSegment * i / steps);
                    Vector2 pos = GetPointOnPerimeter(widgetMin, widgetMax, t);
                    drawList.AddLine(startPos, pos, borderColU32, lineThickness);
                    startPos = pos;
                }
            }
            else
            {
                Vector2 startPos = GetPointOnPerimeter(widgetMin, widgetMax, start);
                float segmentLength = end - start;
                for (int i = 1; i <= steps; i++)
                {
                    float t = start + (segmentLength * i / steps);
                    Vector2 pos = GetPointOnPerimeter(widgetMin, widgetMax, t);
                    drawList.AddLine(startPos, pos, borderColU32, lineThickness);
                    startPos = pos;
                }
            }
        }

        private (float, float, float, float) CalcHorizontalLines(float minX, float maxX, float cSize, float p)
        {
            float totalLength = maxX - minX;
            float initialGap = totalLength - 2 * cSize;
            float gapActual = initialGap * (1.0f - p);
            float expand = (initialGap - gapActual) / 2.0f;

            float leftLineEnd = minX + cSize + expand;
            float rightLineStart = maxX - cSize - expand;

            return (minX, leftLineEnd, rightLineStart, maxX);
        }

        private (float, float, float, float) CalcVerticalLines(float minY, float maxY, float cSize, float p)
        {
            float totalLength = maxY - minY;
            float initialGap = totalLength - 2 * cSize;
            float gapActual = initialGap * (1.0f - p);
            float expand = (initialGap - gapActual) / 2.0f;

            float topLineEnd = minY + cSize + expand;
            float bottomLineStart = maxY - cSize - expand;

            return (minY, topLineEnd, bottomLineStart, maxY);
        }

        private Vector2 GetPointOnPerimeter(Vector2 min, Vector2 max, float t)
        {
            float width = max.X - min.X;
            float height = max.Y - min.Y;
            float perimeter = 2 * (width + height);
            float distance = t * perimeter;

            float topLength = width;
            float rightLength = height;
            float bottomLength = width;
            float leftLength = height;

            if (distance <= topLength)
            {
                return new Vector2(min.X + distance, min.Y);
            }
            distance -= topLength;

            if (distance <= rightLength)
            {
                return new Vector2(max.X - 1, min.Y + distance);
            }
            distance -= rightLength;

            if (distance <= bottomLength)
            {
                return new Vector2(max.X - distance, max.Y);
            }
            distance -= bottomLength;

            return new Vector2(min.X, max.Y - distance);
        }
    }
}
