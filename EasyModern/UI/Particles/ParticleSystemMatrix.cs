using Hexa.NET.ImGui;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace EasyModern.UI.Particles
{
    internal class ParticleSystemMatrix
    {
        public bool Enabled { get; set; } = true; // Propiedad para habilitar o deshabilitar el sistema
        public float Speed { get; set; } = 1.0f;  // Velocidad de animación
        public int ParticleCount { get; set; } = 100; // Número de columnas en la lluvia Matrix
        public Vector4 CharColor { get; set; } = new Vector4(0.0f, 1.0f, 0.0f, 1.0f); // Color verde neón para las partículas

        private Random _random = new Random();
        private List<Particle> _particles = new List<Particle>();

        public ParticleSystemMatrix(int width, int height)
        {
            InitializeParticles(width, height);
        }

        // Estructura de cada partícula (carácter)
        private class Particle
        {
            public int X; // Posición X (columna)
            public float Y; // Posición Y (fila)
            public float Speed; // Velocidad individual
            public char Char; // Carácter que se dibuja
        }

        // Inicializa partículas (columnas de lluvia)
        private void InitializeParticles(int width, int height)
        {
            _particles.Clear();
            for (int i = 0; i < ParticleCount; i++)
            {
                _particles.Add(new Particle
                {
                    X = _random.Next(0, width / 10),
                    Y = _random.Next(-height / 10, height / 10),
                    Speed = 1.0f + (float)_random.NextDouble() * 3.0f, // Velocidad variable
                    Char = (char)_random.Next(33, 126) // Genera caracteres ASCII aleatorios
                });
            }
        }

        // Actualiza las partículas en función del deltaTime
        public void Update(float deltaTime, int width, int height)
        {
            if (!Enabled) return;

            foreach (var particle in _particles)
            {
                particle.Y += particle.Speed * Speed * deltaTime * 50; // Actualiza posición Y
                if (particle.Y > height / 10) // Reinicia la partícula si supera el borde inferior
                {
                    particle.Y = -1;
                    particle.Char = (char)_random.Next(33, 126); // Nuevo carácter aleatorio
                }
            }
        }

        // Dibuja las partículas usando el DrawList de ImGui
        public void Render(ImDrawListPtr drawList, int width, int height)
        {
            if (!Enabled) return;

            foreach (var particle in _particles)
            {
                var xPos = particle.X * 10; // Escala de columna
                var yPos = particle.Y * 10; // Escala de fila
                drawList.AddText(new Vector2(xPos, yPos), ImGui.ColorConvertFloat4ToU32(CharColor), particle.Char.ToString());
            }
        }
    }

}
