namespace EasyModern.Core.Utils
{
    public static class MathExtensions
    {
        /// <summary>
        /// Restringe un valor a un rango específico.
        /// </summary>
        /// <param name="value">El valor a restringir.</param>
        /// <param name="min">El límite inferior del rango.</param>
        /// <param name="max">El límite superior del rango.</param>
        /// <returns>El valor restringido dentro del rango.</returns>
        public static int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        /// <summary>
        /// Restringe un valor de tipo float a un rango específico.
        /// </summary>
        /// <param name="value">El valor a restringir.</param>
        /// <param name="min">El límite inferior del rango.</param>
        /// <param name="max">El límite superior del rango.</param>
        /// <returns>El valor restringido dentro del rango.</returns>
        public static float Clamp(float value, float min, float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        /// <summary>
        /// Interpola linealmente entre dos ángulos en grados, manejando el wrap-around en 360 grados.
        /// </summary>
        /// <param name="a">Ángulo de inicio en grados.</param>
        /// <param name="b">Ángulo de destino en grados.</param>
        /// <param name="t">Factor de interpolación (0.0 a 1.0).</param>
        /// <returns>Ángulo interpolado en grados.</returns>
        public static float LerpAngle(float a, float b, float t)
        {
            // Asegurarse de que los ángulos estén dentro del rango [0, 360)
            a = NormalizeAngle(a);
            b = NormalizeAngle(b);

            // Calcular la diferencia más corta entre los ángulos
            float delta = b - a;
            if (delta > 180f)
            {
                delta -= 360f;
            }
            else if (delta < -180f)
            {
                delta += 360f;
            }

            // Interpolar y normalizar el resultado
            float result = a + delta * t;
            return NormalizeAngle(result);
        }

        /// <summary>
        /// Normaliza un ángulo a un valor dentro del rango [0, 360).
        /// </summary>
        /// <param name="angle">Ángulo en grados.</param>
        /// <returns>Ángulo normalizado en grados.</returns>
        public static float NormalizeAngle(float angle)
        {
            angle %= 360f;
            if (angle < 0f)
            {
                angle += 360f;
            }
            return angle;
        }

    }
}
