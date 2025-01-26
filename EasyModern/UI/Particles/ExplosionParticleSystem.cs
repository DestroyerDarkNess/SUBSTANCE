using Hexa.NET.ImGui;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace EasyModern.UI.Particles
{
    public class ExplosionParticleSystem
    {
        private class Particle
        {
            public Vector2 Position;
            public Vector2 Velocity;
            public float Life; // Tiempo de vida restante
            public Vector4 Color;

            public Particle(Vector2 position, Vector2 velocity, float life, Vector4 color)
            {
                Position = position;
                Velocity = velocity;
                Life = life;
                Color = color;
            }
        }

        private List<Particle> particles;
        private Random random;

        public bool Enabled { get; set; }
        public int ParticleCount { get; set; }
        public float ExplosionRadius { get; set; }
        public float Lifetime { get; set; }
        public Vector2 Center { get; set; } // Centro dinámico de la explosión

        public ExplosionParticleSystem(Vector2 center)
        {
            particles = new List<Particle>();
            random = new Random();
            Enabled = true;
            ParticleCount = 100;
            ExplosionRadius = 100.0f;
            Lifetime = 1.5f; // Partículas desaparecen después de 1.5 segundos
            Center = Vector2.Zero; // Inicialización por defecto
            TriggerExplosion(center);
        }

        public void TriggerExplosion(Vector2 center)
        {
            particles.Clear();
            Center = center;

            for (int i = 0; i < ParticleCount; i++)
            {
                float angle = RandomFloat(0.0f, (float)(2.0 * Math.PI));
                float speed = RandomFloat(50.0f, ExplosionRadius);

                Vector2 velocity = new Vector2(
                    speed * (float)Math.Cos(angle),
                    speed * (float)Math.Sin(angle)
                );

                particles.Add(new Particle(
                    center,
                    velocity,
                    Lifetime,
                    new Vector4(
                        RandomFloat(0.7f, 1.0f), // R
                        RandomFloat(0.2f, 0.5f), // G
                        RandomFloat(0.0f, 0.2f), // B
                        1.0f                     // A
                    )
                ));
            }
        }

        public void Update(float deltaTime)
        {
            if (!Enabled) return;

            for (int i = particles.Count - 1; i >= 0; i--)
            {
                Particle particle = particles[i];
                particle.Life -= deltaTime;

                if (particle.Life <= 0)
                {
                    particles.RemoveAt(i);
                    continue;
                }

                particle.Position += particle.Velocity * deltaTime;
                particle.Color.W = particle.Life / Lifetime; // Desvanece el color con el tiempo

                // Actualización dinámica del centro
                Vector2 directionToCenter = Center - particle.Position;
                float distanceToCenter = directionToCenter.Length();

                if (distanceToCenter > 0.0f)
                {
                    directionToCenter /= distanceToCenter; // Normaliza
                    particle.Velocity += directionToCenter * deltaTime * 50.0f; // Ajusta la velocidad hacia el centro
                }
            }
        }

        public void Render(ImDrawListPtr drawList)
        {
            if (!Enabled) return;

            foreach (var particle in particles)
            {
                drawList.AddCircleFilled(particle.Position, 3.0f, ImGui.GetColorU32(particle.Color));
            }
        }

        private float RandomFloat(float min, float max)
        {
            return (float)(random.NextDouble() * (max - min) + min);
        }
    }
}
