﻿using Hexa.NET.ImGui;
using System;
using System.Numerics;

namespace EasyModern.UI.Widgets
{
    public class CheckWidget
    {
        public string ID { get; set; } = "check.default";
        public string Title { get; set; } = "Check Widget";
        public string Description { get; set; } = "Este widget incluye funcionalidad de selección.";
        public Vector2 Size { get; set; } = new Vector2(300, 150);

        public Vector4 BackgroundColor { get; set; } = new Vector4(0.15f, 0.15f, 0.15f, 1.0f);
        public Vector4 TitleColor { get; set; } = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        public Vector4 DescriptionColor { get; set; } = new Vector4(0.8f, 0.8f, 0.8f, 1.0f);
        public Vector4 BorderColor { get; set; } = new Vector4(1, 1, 1, 1);

        public float MarginX { get; set; } = 15.0f;
        public float MarginY { get; set; } = 10.0f;
        public float CornerSize { get; set; } = 15.0f;
        public float LineThickness { get; set; } = 1.0f;
        public float BorderPercent { get; set; } = 0.5f;

        public float BorderOffset { get; set; } = 0.0f;

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

        // Propiedades del botón ícono (reemplaza el label ON/OFF)
        public string BottomRightIconName { get; set; } = "check_icon";
        public Vector4 BottomRightIconBgColor { get; set; } = new Vector4(0.3f, 1.0f, 0.3f, 1.0f);
        public bool IconButtonVisible { get; set; } = true;
        public float IconButtonRounding { get; set; } = 0.0f;
        public float IconButtonSize { get; set; } = 15.0f;

        public bool Enabled { get; set; } = true;

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
            Vector4 iconBgColor = BottomRightIconBgColor;

            if (!Enabled)
            {
                // Gris oscuro para fondo
                bgColor = new Vector4(0.2f, 0.2f, 0.2f, 1.0f);
                // Gris claro para texto, borde y botón
                Vector4 grayLight = new Vector4(0.7f, 0.7f, 0.7f, 1.0f);
                titleColor = grayLight;
                descColor = grayLight;
                borderColor = grayLight;
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

            // Borde
            float targetAlpha = Checked ? 1.0f : 0.0f;
            currentBorderAlpha += (targetAlpha - currentBorderAlpha) * FadeSpeed;
            Vector4 adjustedBorderColor = new Vector4(borderColor.X, borderColor.Y, borderColor.Z, borderColor.W * currentBorderAlpha);
            uint borderColU32 = ImGui.ColorConvertFloat4ToU32(adjustedBorderColor);

            if (currentBorderAlpha > 0.01f)
            {
                DrawStaticBorder(drawList, widgetMin, widgetMax, BorderPercent, borderColU32, LineThickness);
            }

            // Área del botón ícono (cuadrado del tamaño IconButtonSize)
            Vector2 iconBoxMin = new Vector2(widgetMax.X - MarginX - IconButtonSize, widgetMax.Y - MarginY - IconButtonSize);
            Vector2 iconBoxMax = iconBoxMin + new Vector2(IconButtonSize, IconButtonSize);

            // Dibujar ícono si existe y es visible
            if (IconButtonVisible && CheckedChanged != null)
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
                bool insideIcon = (mousePos.X >= iconBoxMin.X && mousePos.X < iconBoxMax.X &&
                                   mousePos.Y >= iconBoxMin.Y && mousePos.Y < iconBoxMax.Y);

                if (Enabled)
                {
                    // Cambiar el estado Checked al hacer clic en cualquier parte del widget
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
    }

}
