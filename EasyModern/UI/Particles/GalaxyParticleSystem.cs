using Hexa.NET.ImGui;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace EasyModern.UI.Particles
{
    public class GalaxyParticleSystem
    {
        private class Star
        {
            public Vector2 Position;
            public float Radius;
            public float Angle;
            public float AngularSpeed;
            public Vector4 Color;

            public Star(Vector2 position, float radius, float angle, float angularSpeed, Vector4 color)
            {
                Position = position;
                Radius = radius;
                Angle = angle;
                AngularSpeed = angularSpeed;
                Color = color;
            }
        }

        private List<Star> stars;
        private Random random;

        public bool Enabled { get; set; }
        public int StarCount { get; set; }
        public float Speed { get; set; }

        public GalaxyParticleSystem(Vector2 center)
        {
            stars = new List<Star>();
            random = new Random();
            Enabled = true;
            StarCount = 300;
            Speed = 1.0f;
            Initialize(center);
        }

        public void Initialize(Vector2 center)
        {
            stars.Clear();

            for (int i = 0; i < StarCount; i++)
            {
                float radius = RandomFloat(50.0f, 300.0f);
                float angle = RandomFloat(0.0f, (float)(2.0 * Math.PI));

                stars.Add(new Star(
                    new Vector2(
                        center.X + radius * (float)Math.Cos(angle),
                        center.Y + radius * (float)Math.Sin(angle)
                    ),
                    radius,
                    angle,
                    RandomFloat(0.1f, 1.0f),
                    new Vector4(
                        RandomFloat(0.5f, 1.0f),
                        RandomFloat(0.5f, 1.0f),
                        RandomFloat(0.5f, 1.0f),
                        1.0f
                    )
                ));
            }
        }

        public void Update(float deltaTime, Vector2 center)
        {
            if (!Enabled) return;

            foreach (var star in stars)
            {
                star.Angle += star.AngularSpeed * Speed * deltaTime;

                star.Position = new Vector2(
                    center.X + star.Radius * (float)Math.Cos(star.Angle),
                    center.Y + star.Radius * (float)Math.Sin(star.Angle)
                );
            }
        }

        public void Render(ImDrawListPtr drawList)
        {
            if (!Enabled) return;

            foreach (var star in stars)
            {
                drawList.AddCircleFilled(star.Position, 2.0f, ImGui.GetColorU32(star.Color));
            }
        }

        private float RandomFloat(float min, float max)
        {
            return (float)(random.NextDouble() * (max - min) + min);
        }
    }
}
