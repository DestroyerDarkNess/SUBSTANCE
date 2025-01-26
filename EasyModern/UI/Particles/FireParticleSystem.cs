using Hexa.NET.ImGui;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace EasyModern.UI.Particles
{
    public class FireParticleSystem
    {
        private class Particle
        {
            public Vector2 Position;
            public Vector2 Velocity;
            public Vector4 Color;
            public float Life;
        }

        private readonly List<Particle> particles = new List<Particle>();
        private readonly Random random = new Random();
        public bool Enabled { get; set; } = true;
        public float Speed { get; set; } = 1.0f; // Controla la velocidad del fuego
        private const int MaxParticles = 200;

        public void Update(float deltaTime, Vector2 emitterPosition)
        {
            if (!Enabled) return;

            // Generar nuevas partículas
            for (int i = 0; i < 10; i++)
            {
                particles.Add(new Particle
                {
                    Position = emitterPosition + new Vector2(RandomFloat(-10, 10), RandomFloat(-5, 5)),
                    Velocity = new Vector2(RandomFloat(-20, 20), RandomFloat(-50, -20)),
                    Color = new Vector4(1.0f, RandomFloat(0.2f, 0.5f), 0.0f, 1.0f),
                    Life = RandomFloat(1.0f, 2.0f)
                });
            }

            // Actualizar las partículas existentes
            for (int i = particles.Count - 1; i >= 0; i--)
            {
                Particle p = particles[i];
                p.Life -= deltaTime * Speed;
                if (p.Life <= 0)
                {
                    particles.RemoveAt(i);
                    continue;
                }

                p.Position += p.Velocity * deltaTime * Speed;
                p.Color.W = Math.Max(0, p.Life / 2.0f); // Desvanecimiento
            }

            // Limitar el número de partículas
            while (particles.Count > MaxParticles)
            {
                particles.RemoveAt(0);
            }
        }

        public void Render(ImDrawListPtr drawList)
        {
            if (!Enabled) return;

            foreach (var particle in particles)
            {
                drawList.AddCircleFilled(particle.Position, 5.0f, ImGui.GetColorU32(particle.Color));
            }
        }

        private float RandomFloat(float min, float max)
        {
            return (float)(random.NextDouble() * (max - min) + min);
        }
    }
}
