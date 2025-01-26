using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EasyModern.Core.Effects
{




    public class Battlefield4Effect
    {
        private readonly int width;
        private readonly int height;
        private readonly List<BulletTracer> tracers;
        private readonly Random rand;

        public float BulletSpeed { get; set; } = 15f; // Velocidad de las balas
        public float BulletLength { get; set; } = 100f; // Longitud del tubo luminoso
        public float BulletThickness { get; set; } = 10f; // Grosor del tubo luminoso
        public int MaxBullets { get; set; } = 8; // Máximo número de balas en pantalla

        /// <summary>
        /// Representa una bala trazadora.
        /// </summary>
        private class BulletTracer
        {
            public PointF Position { get; set; } // Posición actual
            public float Depth { get; set; }     // Profundidad (perspectiva)
            public float Angle { get; set; }    // Ángulo de trayectoria
            public Color Color { get; set; }    // Color de la bala
        }

        public Battlefield4Effect(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.tracers = new List<BulletTracer>();
            this.rand = new Random();
        }

        /// <summary>
        /// Genera una nueva bala trazadora.
        /// </summary>
        private void SpawnTracer()
        {
            if (tracers.Count >= MaxBullets) return;

            tracers.Add(new BulletTracer
            {
                Position = new PointF(rand.Next(-50, 50), rand.Next(height / 4, height * 3 / 4)),
                Depth = (float)(rand.NextDouble() * 0.5 + 0.5), // Profundidad entre 0.5 y 1
                Angle = (float)(rand.NextDouble() * Math.PI / 6 - Math.PI / 12), // Ligera inclinación
                Color = Color.FromArgb(255, 255, 165, 0) // Naranja cálido
            });
        }

        /// <summary>
        /// Actualiza y dibuja el efecto en la imagen.
        /// </summary>
        public void ApplyEffect(Bitmap bmp)
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Generar balas si hay espacio
                SpawnTracer();

                // Dibujar y mover balas existentes
                for (int i = tracers.Count - 1; i >= 0; i--)
                {
                    var tracer = tracers[i];

                    // Dibujar bala como tubo luminoso
                    DrawLuminousTracer(g, tracer);

                    // Actualizar posición
                    tracer.Position = new PointF(
                        tracer.Position.X + BulletSpeed * tracer.Depth,
                        tracer.Position.Y + (float)Math.Tan(tracer.Angle) * BulletSpeed * tracer.Depth);

                    // Remover si sale de la pantalla
                    if (tracer.Position.X > width + 100)
                    {
                        tracers.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// Dibuja una bala trazadora como un tubo luminoso.
        /// </summary>
        private void DrawLuminousTracer(Graphics g, BulletTracer tracer)
        {
            float startX = tracer.Position.X;
            float startY = tracer.Position.Y;
            float endX = startX - BulletLength * tracer.Depth;
            float endY = startY - (float)Math.Tan(tracer.Angle) * BulletLength * tracer.Depth;

            // Crear gradiente lineal para simular el efecto de tubo luminoso
            using (LinearGradientBrush brush = new LinearGradientBrush(
                new PointF(endX, endY),
                new PointF(startX, startY),
                Color.FromArgb(50, tracer.Color), // Color al final del tubo (transparente)
                tracer.Color))                   // Color brillante al inicio
            {
                // Dibujar el tubo luminoso
                using (Pen pen = new Pen(brush, BulletThickness * tracer.Depth))
                {
                    pen.StartCap = LineCap.Round;
                    pen.EndCap = LineCap.Round;
                    g.DrawLine(pen, endX, endY, startX, startY);
                }
            }
        }
    }

}
