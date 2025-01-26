using Hexa.NET.ImGui;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace EasyModern.UI.Particles
{
    internal class ParticleSystemChristmas
    {
        public bool Enabled { get; set; } = true; // Habilitar o deshabilitar el sistema
        public float Speed { get; set; } = 1.0f; // Velocidad de animación
        public int ParticleCount { get; set; } = 150; // Cantidad de partículas (copos de nieve)
        public Vector4 SnowflakeColor { get; set; } = new Vector4(1.0f, 1.0f, 1.0f, 1.0f); // Color de los copos de nieve

        private Random _random = new Random();
        private List<Snowflake> _snowflakes = new List<Snowflake>();

        private class Snowflake
        {
            public float X; // Posición X
            public float Y; // Posición Y
            public float Size; // Tamaño del copo
            public float Speed; // Velocidad del copo
            public float Alpha; // Transparencia del copo
        }

        public ParticleSystemChristmas(int width, int height)
        {
            InitializeSnowflakes(width, height);
        }

        private void InitializeSnowflakes(int width, int height)
        {
            _snowflakes.Clear();
            for (int i = 0; i < ParticleCount; i++)
            {
                _snowflakes.Add(new Snowflake
                {
                    X = _random.Next(0, width), // Posición horizontal aleatoria
                    Y = _random.Next(0, height), // Posición vertical aleatoria
                    Size = 2.0f + (float)_random.NextDouble() * 3.0f, // Tamaño del copo (entre 2 y 5 píxeles)
                    Speed = 1.0f + (float)_random.NextDouble() * 2.0f, // Velocidad del copo
                    Alpha = 0.5f + (float)_random.NextDouble() * 0.5f // Transparencia variable
                });
            }
        }

        public void Update(float deltaTime, int width, int height)
        {
            if (!Enabled) return;

            foreach (var snowflake in _snowflakes)
            {
                // Mueve el copo hacia abajo
                snowflake.Y += snowflake.Speed * Speed * deltaTime * 50;

                // Oscilación horizontal para un movimiento más natural
                snowflake.X += (float)Math.Sin(snowflake.Y * 0.05f) * 0.5f;

                // Reinicia el copo cuando salga del área visible
                if (snowflake.Y > height || snowflake.X < 0 || snowflake.X > width)
                {
                    snowflake.Y = -snowflake.Size;
                    snowflake.X = _random.Next(0, width);
                    snowflake.Speed = 1.0f + (float)_random.NextDouble() * 2.0f;
                    snowflake.Alpha = 0.5f + (float)_random.NextDouble() * 0.5f;
                }
            }
        }

        public void Render(ImDrawListPtr drawList, int width, int height)
        {
            if (!Enabled) return;

            foreach (var snowflake in _snowflakes)
            {
                uint color = ImGui.ColorConvertFloat4ToU32(new Vector4(SnowflakeColor.X, SnowflakeColor.Y, SnowflakeColor.Z, snowflake.Alpha));
                drawList.AddCircleFilled(new Vector2(snowflake.X, snowflake.Y), snowflake.Size, color);
            }
        }
    }

}
