using Hexa.NET.ImGui;
using System;
using System.Numerics;

namespace EasyModern.UI.Widgets
{
    public class AlertWindow
    {
        public Vector4 _boxBackgroundColor;
        public Vector4 _boxBorderColor;
        public Vector4 _circleBorderColor;
        private Vector4 _shadowColor;
        private float _animationSpeed;
        private float _borderSize;
        public Vector2 _position;

        public AlertWindow(Vector4 boxBackgroundColor, Vector4 boxBorderColor, Vector4 circleBorderColor, Vector4 shadowColor, float animationSpeed = 2.0f, float borderSize = 5.0f)
        {
            _boxBackgroundColor = boxBackgroundColor;
            _boxBorderColor = boxBorderColor;
            _circleBorderColor = circleBorderColor;
            _shadowColor = shadowColor;
            _animationSpeed = animationSpeed;
            _borderSize = borderSize;
        }

        public void Draw(string message)
        {
            // Calcular la animación de parpadeo
            float time = (float)(DateTime.Now.TimeOfDay.TotalSeconds * _animationSpeed);
            float blinkAlpha = ((float)Math.Sin(time) + 1.0f) / 2.0f;

            // Actualizar colores dinámicos con animación
            Vector4 dynamicBoxBorderColor = new Vector4(_boxBorderColor.X, _boxBorderColor.Y, _boxBorderColor.Z, _boxBorderColor.W * blinkAlpha);
            Vector4 dynamicCircleBorderColor = new Vector4(_circleBorderColor.X, _circleBorderColor.Y, _circleBorderColor.Z, _circleBorderColor.W * blinkAlpha);

            // Calcular tamaño dinámico según el mensaje
            ImGui.PushFont(Core.Instances.fontManager.GetFont("title"));
            Vector2 textSize = ImGui.CalcTextSize(message);
            ImGui.PopFont();

            float padding = 20.0f;
            float circleRadius = (textSize.Y / 2);
            float circleOffset = circleRadius * 2 + 20.0f;
            Vector2 windowSize = new Vector2(textSize.X + padding + circleOffset, textSize.Y + padding);

            // Centrar la posición inicial
            Vector2 windowPos = _position;
            windowPos.X -= windowSize.X / 2;

            // Configurar la ventana
            ImGui.SetNextWindowPos(windowPos, ImGuiCond.Always);
            ImGui.SetNextWindowSize(windowSize, ImGuiCond.Always);
            ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, _borderSize);
            ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(10, 10));
            ImGui.PushStyleColor(ImGuiCol.WindowBg, _boxBackgroundColor);
            ImGui.PushStyleColor(ImGuiCol.Border, dynamicBoxBorderColor);

            if (ImGui.Begin("Alert", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove))
            {
                // Dibujar sombra interna
                var drawList = ImGui.GetWindowDrawList();
                Vector2 windowMin = ImGui.GetWindowPos();
                Vector2 windowMax = windowMin + ImGui.GetWindowSize();
                drawList.AddRect(windowMin, windowMax, ImGui.GetColorU32(_shadowColor), 0.0f, ImDrawFlags.None, 10.0f);

                // Dibujar el círculo a la izquierda
                Vector2 center = windowMin + new Vector2(circleRadius + _borderSize + 10.0f, windowSize.Y / 2);
                drawList.AddCircle(center, circleRadius, ImGui.GetColorU32(dynamicCircleBorderColor), 64, 2.0f);

                // Texto dentro del círculo
                Vector2 textCircleSize = ImGui.CalcTextSize("!");
                drawList.AddText(center - textCircleSize / 2, ImGui.GetColorU32(new Vector4(1.0f, 1.0f, 1.0f, 1.0f)), "!");

                // Posicionar el texto principal a la derecha del círculo
                Vector2 textPosition = new Vector2(circleRadius + _borderSize + 30.0f, (windowSize.Y - textSize.Y) / 2);
                ImGui.SetCursorScreenPos(windowMin + textPosition);
                ImGui.TextWrapped(message);
            }

            ImGui.End();
            ImGui.PopStyleVar(2);
            ImGui.PopStyleColor(2);
        }

        public void UpdateSettings(Vector4 boxBackgroundColor, Vector4 boxBorderColor, Vector4 circleBorderColor, float animationSpeed)
        {
            _boxBackgroundColor = boxBackgroundColor;
            _boxBorderColor = boxBorderColor;
            _circleBorderColor = circleBorderColor;
            _animationSpeed = animationSpeed;
        }
    }

}
