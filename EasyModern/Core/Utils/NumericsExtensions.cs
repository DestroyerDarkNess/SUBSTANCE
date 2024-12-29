namespace EasyModern.Core.Utils
{
    using System;
    using System.Numerics;

    public static class NumericsExtensions
    {
        private const float DefaultEpsilon = 1e-6f;

        /// <summary>
        /// Normaliza el vector <paramref name="input"/> y devuelve el resultado en <paramref name="result"/>.
        /// </summary>
        /// <param name="input">Referencia al vector original a normalizar.</param>
        /// <param name="result">El vector resultante con longitud 1 (si la longitud original no era cero).</param>
        public static void Normalize(ref Vector2 input, out Vector2 result)
        {
            float length = input.Length();
            if (Math.Abs(length) < DefaultEpsilon)
            {
                result = Vector2.Zero;
            }
            else
            {
                float factor = 1f / length;
                result = new Vector2(input.X * factor, input.Y * factor);
            }
        }

        /// <summary>
        /// Determina si un Vector2 es (0,0) dentro de un margen de error.
        /// </summary>
        public static bool IsZero(this Vector2 v, float epsilon = DefaultEpsilon)
        {
            return Math.Abs(v.X) < epsilon && Math.Abs(v.Y) < epsilon;
        }

        /// <summary>
        /// Determina si un Vector3 es (0,0,0) dentro de un margen de error.
        /// </summary>
        public static bool IsZero(this Vector3 v, float epsilon = DefaultEpsilon)
        {
            return Math.Abs(v.X) < epsilon &&
                   Math.Abs(v.Y) < epsilon &&
                   Math.Abs(v.Z) < epsilon;
        }

        /// <summary>
        /// Determina si un Vector4 es (0,0,0,0) dentro de un margen de error.
        /// </summary>
        public static bool IsZero(this Vector4 v, float epsilon = DefaultEpsilon)
        {
            return Math.Abs(v.X) < epsilon &&
                   Math.Abs(v.Y) < epsilon &&
                   Math.Abs(v.Z) < epsilon &&
                   Math.Abs(v.W) < epsilon;
        }
    }

}
