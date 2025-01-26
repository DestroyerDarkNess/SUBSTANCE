using System;
using System.Numerics;

namespace EasyModern.Core.Utils
{
    public class ColorRGB
    {
        private float red, green, blue;
        private float targetRed, targetGreen, targetBlue;
        public float transitionSpeed { get; set; } = 1.0f;

        private readonly Random random = new Random();

        public ColorRGB()
        {
            // Inicializa el color con valores aleatorios
            red = random.Next(0, 256) / 255f;
            green = random.Next(0, 256) / 255f;
            blue = random.Next(0, 256) / 255f;

            SetRandomTargetColor();
        }

        public float Alpha { get; set; } = 1.0f;

        public Vector4 CurrentColor
        {
            get => new Vector4(red, green, blue, Alpha);
            set
            {
                red = value.X;
                green = value.Y;
                blue = value.Z;
            }
        }

        public void SetTransitionSpeed(float speed)
        {
            transitionSpeed = Core.Utils.MathExtensions.Clamp(speed, 0.1f, 10.0f); // Limitar entre 0.1 y 10
        }

        public void Update(float deltaTime)
        {
            // Gradualmente cambia el color actual hacia el color objetivo
            red = Lerp(red, targetRed, deltaTime * transitionSpeed);
            green = Lerp(green, targetGreen, deltaTime * transitionSpeed);
            blue = Lerp(blue, targetBlue, deltaTime * transitionSpeed);

            // Si el color actual está cerca del objetivo, genera un nuevo objetivo
            if (Math.Abs(red - targetRed) < 0.01f &&
                Math.Abs(green - targetGreen) < 0.01f &&
                Math.Abs(blue - targetBlue) < 0.01f)
            {
                SetRandomTargetColor();
            }
        }

        private void SetRandomTargetColor()
        {
            targetRed = random.Next(0, 256) / 255f;
            targetGreen = random.Next(0, 256) / 255f;
            targetBlue = random.Next(0, 256) / 255f;
        }

        private float Lerp(float start, float end, float t)
        {
            return start + (end - start) * Core.Utils.MathExtensions.Clamp(t, 0.0f, 1.0f);
        }
    }
}