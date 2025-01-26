using EasyModern.SDK;
using Hexa.NET.ImGui;
using System;
using System.Numerics;

namespace EasyModern.Core.Effects
{
    public class SharinganCrosshair
    {
        // Configuraciones principales
        public Vector4 BaseColor { get; set; } = new Vector4(1.0f, 0.0f, 0.0f, 1.0f); // Fondo rojo
        public Vector4 BorderColor { get; set; } = new Vector4(0.0f, 0.0f, 0.0f, 1.0f); // Borde negro
        public Vector4 PatternColor { get; set; } = new Vector4(0.0f, 0.0f, 0.0f, 1.0f); // Marcas negras
        public float Scale { get; set; } = 1.0f; // Escala del Sharingan
        public bool Animate { get; set; } = false; // Activar animación
        public float AnimationSpeed { get; set; } = 0.05f; // Velocidad de animación
        public bool Transparent { get; set; } = false; // Activar transparencia

        // Estado interno
        private float _currentAngle = 0.0f;

        /// <summary>
        /// Dibuja el Sharingan Crosshair usando ImGui.
        /// </summary>
        public void DrawCrosshair(ImDrawListPtr drawList, float centerX, float centerY)
        {
            // Guardar estado original del antialiasing
            bool originalAntiAliasedLines = drawList.Flags.HasFlag(ImDrawListFlags.AntiAliasedLines);

            // Habilitar antialiasing
            drawList.Flags |= ImDrawListFlags.AntiAliasedLines;

            // Ajustar los colores si la transparencia está activa
            Vector4 baseColor = Transparent ? new Vector4(BaseColor.X, BaseColor.Y, BaseColor.Z, 0.5f) : BaseColor;
            Vector4 borderColor = Transparent ? new Vector4(BorderColor.X, BorderColor.Y, BorderColor.Z, 0.5f) : BorderColor;
            Vector4 patternColor = Transparent ? new Vector4(PatternColor.X, PatternColor.Y, PatternColor.Z, 0.5f) : PatternColor;

            // Escalar los radios para que el diseño completo tenga 20x20 píxeles con Scale = 1.0
            float outerRadius = 10.0f * Scale; // Radio exterior (fondo rojo)
            float innerRadius = outerRadius * 0.6f; // Radio del círculo negro interno
            float markRadius = outerRadius * 0.1f; // Radio de las marcas negras
            float borderThickness = outerRadius * 0.1f; // Grosor del borde
            float centerCircleRadius = outerRadius * 0.2f; // Radio del pequeño círculo central
            int circleSegments = 64; // Segmentos para círculos suaves

            // Paso 1: Dibujar el círculo rojo con borde negro
            ImGuiDrawingUtils.DrawFillCircle(drawList, (int)centerX, (int)centerY, outerRadius, baseColor, circleSegments);
            ImGuiDrawingUtils.DrawCircle(drawList, (int)centerX, (int)centerY, outerRadius, borderColor, borderThickness, circleSegments);

            // Pequeño círculo del centro del ojo (responsivo)
            ImGuiDrawingUtils.DrawFillCircle(drawList, (int)centerX, (int)centerY, centerCircleRadius, borderColor, circleSegments);

            // Paso 2: Dibujar el círculo negro interno 
            ImGuiDrawingUtils.DrawCircle(drawList, (int)centerX, (int)centerY, innerRadius, borderColor, 1, circleSegments);

            // Paso 3: Dibujar las marcas del Sharingan
            int marks = 3; // Cantidad de marcas (3 tomoe)
            float angleStep = (float)(2 * Math.PI / marks);

            for (int i = 0; i < marks; i++)
            {
                float angle = i * angleStep + _currentAngle;
                float markX = centerX + (float)(Math.Cos(angle) * innerRadius * 0.9f);
                float markY = centerY + (float)(Math.Sin(angle) * innerRadius * 0.9f);

                // Dibujar una marca tipo "tomoe"
                DrawTomoe(drawList, markX, markY, markRadius, angle, patternColor, circleSegments);
            }

            // Paso 4: Actualizar animación si está activa
            if (Animate)
            {
                _currentAngle += AnimationSpeed;
                if (_currentAngle >= 2 * Math.PI)
                {
                    _currentAngle -= (float)(2 * Math.PI);
                }
            }

            // Restaurar el estado original del antialiasing
            drawList.Flags = originalAntiAliasedLines
                ? drawList.Flags | ImDrawListFlags.AntiAliasedLines
                : drawList.Flags & ~ImDrawListFlags.AntiAliasedLines;
        }

        /// <summary>
        /// Dibuja una marca tipo "tomoe" (forma de garra).
        /// </summary>
        private void DrawTomoe(ImDrawListPtr drawList, float x, float y, float radius, float angle, Vector4 color, int segments)
        {
            // Base: círculo principal
            ImGuiDrawingUtils.DrawFillCircle(drawList, (int)x, (int)y, radius, color, segments);

            // Calcular puntos para la forma de la garra
            float clawRadius = radius * 0.6f; // Tamaño de la garra
            float clawAngle = angle + 0.5f; // Desplazamiento angular para la garra
            float clawX1 = x + (float)(Math.Cos(clawAngle) * clawRadius);
            float clawY1 = y + (float)(Math.Sin(clawAngle) * clawRadius);

            clawAngle = angle - 0.5f; // Ajustar para crear una curva
            float clawX2 = x + (float)(Math.Cos(clawAngle) * clawRadius * 0.8f);
            float clawY2 = y + (float)(Math.Sin(clawAngle) * clawRadius * 0.8f);

            float clawTipX = x + (float)(Math.Cos(angle) * radius * 1.5f);
            float clawTipY = y + (float)(Math.Sin(angle) * radius * 1.5f);

            // Dibujar la garra como un triángulo curvo
            drawList.AddTriangleFilled(
                new Vector2(clawX1, clawY1),
                new Vector2(clawX2, clawY2),
                new Vector2(clawTipX, clawTipY),
                ImGui.ColorConvertFloat4ToU32(color)
            );
        }
    }
}
