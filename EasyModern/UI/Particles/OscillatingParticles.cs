using Hexa.NET.ImGui;
using System;
using System.Numerics;

namespace EasyModern.UI.Particles
{
    internal class OscillatingParticles
    {
        private const int ParticleCount = 200;
        private readonly Vector2[] particlePositions = new Vector2[ParticleCount];
        private readonly Vector2[] particleTargetPositions = new Vector2[ParticleCount];
        private readonly float[] particleSpeeds = new float[ParticleCount];
        private readonly float[] particleRadii = new float[ParticleCount];
        private readonly float[] particleAmplitudes = new float[ParticleCount];
        private readonly float[] particleFrequencies = new float[ParticleCount];
        private readonly float[] timeOffsets = new float[ParticleCount];
        public Vector4 Color = new Vector4(1.0f, 0.2f, 0.2f, 1.0f);
        private readonly Random random = new Random();

        public void UpdateParticles(ImDrawListPtr drawList, Vector2 screenSize, float animationSpeedMultiplier = 1.0f)
        {
            if (drawList.IsNull) return;

            float deltaTime = ImGui.GetIO().DeltaTime * animationSpeedMultiplier;

            for (int i = 0; i < ParticleCount; i++)
            {
                if (particlePositions[i] == Vector2.Zero)
                {
                    particlePositions[i] = new Vector2(
                        random.Next((int)screenSize.X),
                        15.0f
                    );

                    particleSpeeds[i] = 1 + random.Next(25);
                    particleRadii[i] = random.Next(2, 5);
                    particleAmplitudes[i] = random.Next(10, 50);
                    particleFrequencies[i] = random.Next(1, 5) * 0.1f;
                    timeOffsets[i] = random.Next(360) * (float)(Math.PI / 180);

                    particleTargetPositions[i] = new Vector2(
                        random.Next((int)screenSize.X),
                        screenSize.Y * 2
                    );
                }

                float time = deltaTime * particleFrequencies[i] + timeOffsets[i];
                float oscillation = (float)Math.Sin(time) * particleAmplitudes[i];

                particlePositions[i] = Vector2.Lerp(
                    particlePositions[i],
                    particleTargetPositions[i],
                    deltaTime * (particleSpeeds[i] / 60)
                );
                particlePositions[i].X += oscillation;

                if (particlePositions[i].Y > screenSize.Y)
                {
                    particlePositions[i] = Vector2.Zero;
                }

                drawList.AddCircleFilled(
                    particlePositions[i],
                    particleRadii[i],
                    ImGui.ColorConvertFloat4ToU32(Color)
                );
            }
        }
    }
}
