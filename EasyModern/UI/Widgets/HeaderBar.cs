using Hexa.NET.ImGui;
using System;
using System.Numerics;

namespace EasyModern.UI.Widgets
{
    public class HeaderBar
    {
        public string ID { get; set; } = "header.default";

        // Propiedades principales
        public Vector2 Size { get; set; } = new Vector2(10, 50);
        public Vector4 BackgroundColor { get; set; } = new Vector4(0.1f, 0.1f, 0.1f, 1.0f);

        // Propiedades del label izquierdo
        public string LeftLabelText { get; set; } = "Izquierda";
        public Vector4 LeftLabelColor { get; set; } = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        public float LeftLabelIndent { get; set; } = 15.0f;

        // Propiedades del label derecho
        public string RightLabelText { get; set; } = "Derecha";
        public Vector4 RightLabelColor { get; set; } = new Vector4(0.580f, 0.580f, 0.596f, 1.000f);
        public float RightLabelIndent { get; set; } = 15.0f;

        // Efecto de texto progresivo
        public float TextRevealDelay { get; set; } = 0.2f; // Retraso configurable (segundos por carácter)
        private double animationTimer = 0.0;
        private int currentCharIndex = 0;

        // Generador de caracteres aleatorios
        private static readonly char[] randomChars = "!@#$%^&*()-_=+[]{}|;:'\",.<>?/\\~`".ToCharArray();
        private Random random = new Random();

        public void ResetAnimationTimer()
        {
            animationTimer = 0.0;
            currentCharIndex = 0;
        }


        // Método para actualizar el texto animado
        public string GetAnimatedText(double deltaTime)
        {
            if (currentCharIndex < LeftLabelText.Length)
            {
                animationTimer += deltaTime;
                if (animationTimer >= TextRevealDelay)
                {
                    animationTimer -= TextRevealDelay;
                    currentCharIndex++;
                }

                // Generar texto parcialmente revelado
                string revealedText = LeftLabelText.Substring(0, currentCharIndex);
                string randomPart = GenerateRandomPart(LeftLabelText.Length - currentCharIndex);
                return revealedText + randomPart;
            }
            else
            {
                return LeftLabelText; // Todo el texto ya revelado
            }
        }

        // Generar caracteres aleatorios para el texto oculto
        private string GenerateRandomPart(int length)
        {
            char[] randomPart = new char[length];
            for (int i = 0; i < length; i++)
            {
                randomPart[i] = randomChars[random.Next(randomChars.Length)];
            }
            return new string(randomPart);
        }

        // Método para renderizar la barra
        public void Render(double deltaTime)
        {
            ImGui.BeginChild("Section" + ID, new Vector2(Size.X, Size.Y), ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse);
            ImGui.PushFont(Core.Instances.fontManager.GetFont("widget_header"));
            Vector2 widgetMin = ImGui.GetCursorScreenPos();
            Vector2 widgetMax = new Vector2(widgetMin.X + Size.X, widgetMin.Y + Size.Y);
            ImDrawListPtr drawList = ImGui.GetWindowDrawList();

            // Dibujar fondo de la barra
            drawList.AddRectFilled(widgetMin, widgetMax, ImGui.ColorConvertFloat4ToU32(BackgroundColor));

            // Calcular posiciones de los labels
            float centerY = widgetMin.Y + (Size.Y / 2) - (ImGui.GetTextLineHeight() / 2);

            // Dibujar el label izquierdo con efecto de texto progresivo
            string animatedText = GetAnimatedText(deltaTime);
            Vector2 leftLabelPos = new Vector2(widgetMin.X + LeftLabelIndent, centerY);
            ImGui.SetCursorScreenPos(leftLabelPos);
            ImGui.PushStyleColor(ImGuiCol.Text, LeftLabelColor);
            ImGui.Text(animatedText);
            ImGui.PopStyleColor();

            // Dibujar el label derecho
            float rightLabelX = widgetMax.X - RightLabelIndent - ImGui.CalcTextSize(RightLabelText).X;
            Vector2 rightLabelPos = new Vector2(rightLabelX, centerY);
            ImGui.SetCursorScreenPos(rightLabelPos);
            ImGui.PushStyleColor(ImGuiCol.Text, RightLabelColor);
            ImGui.Text(RightLabelText);
            ImGui.PopStyleColor();
            ImGui.PopFont();
            ImGui.EndChild();
        }
    }

}
