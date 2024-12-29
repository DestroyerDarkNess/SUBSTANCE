using SharpDX;
using System.Numerics;

namespace EasyModern.Core.Utils
{

    public static class SharpDXExtensions
    {
        /// <summary>
        /// Convierte un <see cref="SharpDX.Color"/> a un <see cref="Vector4"/> en formato RGBA con valores de 0.0f a 1.0f.
        /// </summary>
        /// <param name="color">El color de SharpDX que deseas convertir.</param>
        /// <returns>Un <see cref="Vector4"/> con componentes RGBA normalizados.</returns>
        public static System.Numerics.Vector4 DxToVector4(this Color color)
        {
            return new System.Numerics.Vector4(
                color.R / 255.0f,
                color.G / 255.0f,
                color.B / 255.0f,
                color.A / 255.0f
            );
        }

        /// <summary>
        /// Convierte un <see cref="SharpDX.Color"/> a un <see cref="Vector4"/> en formato RGBA con valores de 0.0f a 1.0f.
        /// </summary>
        /// <param name="color">El color de SharpDX que deseas convertir.</param>
        /// <returns>Un <see cref="Vector4"/> con componentes RGBA normalizados.</returns>
        public static System.Numerics.Vector3 ToNVector3(this SharpDX.Vector3 vec)
        {
            return new System.Numerics.Vector3(
                vec.X,
                vec.Y,
                vec.Z);
        }
    }

}
