using Hexa.NET.ImGui;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace EasyModern.UI.Particles
{
    public class StormParticleSystem
    {
        private class RainDrop
        {
            public Vector2 Position;
            public Vector2 Velocity;
            public float Life;
        }

        private class Lightning
        {
            public float Timer;
            public float Intensity;
        }

        private readonly List<RainDrop> rainDrops = new List<RainDrop>();
        private readonly Random random = new Random();
        private Lightning lightning = null;

        public bool Enabled { get; set; } = true;
        public float RainSpeed { get; set; } = 1.0f; // Velocidad de la lluvia
        public float LightningChance { get; set; } = 0.02f; // Probabilidad de un rayo
        private const int MaxRainDrops = 300;

        public void Update(float deltaTime, Vector2 screenSize)
        {
            if (!Enabled) return;

            // Generar nuevas gotas de lluvia
            for (int i = 0; i < 10; i++)
            {
                rainDrops.Add(new RainDrop
                {
                    Position = new Vector2(RandomFloat(0, screenSize.X), RandomFloat(-20, 0)),
                    Velocity = new Vector2(RandomFloat(-10, 10), RandomFloat(200, 400) * RainSpeed),
                    Life = RandomFloat(1.0f, 2.0f)
                });
            }

            // Actualizar las gotas de lluvia existentes
            for (int i = rainDrops.Count - 1; i >= 0; i--)
            {
                RainDrop drop = rainDrops[i];
                drop.Position += drop.Velocity * deltaTime;
                drop.Life -= deltaTime;

                if (drop.Position.Y > screenSize.Y || drop.Life <= 0)
                {
                    rainDrops.RemoveAt(i);
                }
            }

            // Generar un rayo ocasional
            if (random.NextDouble() < LightningChance && lightning == null)
            {
                lightning = new Lightning
                {
                    Timer = RandomFloat(0.1f, 0.3f),
                    Intensity = RandomFloat(0.5f, 1.0f)
                };
            }

            // Actualizar el rayo
            if (lightning != null)
            {
                lightning.Timer -= deltaTime;
                if (lightning.Timer <= 0)
                {
                    lightning = null;
                }
            }
        }

        public void Render(ImDrawListPtr drawList, Vector2 screenSize)
        {
            if (!Enabled) return;

            // Renderizar gotas de lluvia
            foreach (var drop in rainDrops)
            {
                Vector2 endPosition = drop.Position + new Vector2(0, drop.Velocity.Y * 0.1f);
                drawList.AddLine(drop.Position, endPosition, ImGui.GetColorU32(new Vector4(0.5f, 0.5f, 1.0f, 0.7f)), 1.0f);
            }

            // Renderizar un rayo si está activo
            if (lightning != null)
            {
                Vector4 flashColor = new Vector4(1.0f, 1.0f, 1.0f, lightning.Intensity);
                drawList.AddRectFilled(Vector2.Zero, screenSize, ImGui.GetColorU32(flashColor));
            }
        }

        private float RandomFloat(float min, float max)
        {
            return (float)(random.NextDouble() * (max - min) + min);
        }
    }
}
