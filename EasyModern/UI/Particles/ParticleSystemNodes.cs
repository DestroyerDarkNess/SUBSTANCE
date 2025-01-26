using Hexa.NET.ImGui;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace EasyModern.UI.Particles
{
    internal class ParticleSystemNodes
    {
        public bool Enabled { get; set; } = true;
        public float Speed { get; set; } = 1.0f;
        public float ConnectionDistance { get; set; } = 100.0f;
        public int NodeCount { get; set; } = 100;
        public Vector4 NodeColor { get; set; } = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        public Vector4 LineColor { get; set; } = new Vector4(0.5f, 0.8f, 1.0f, 0.5f);

        private Random _random = new Random();
        private List<Node> _nodes = new List<Node>();

        private class Node
        {
            public float X;
            public float Y;
            public float VelocityX;
            public float VelocityY;
        }

        public ParticleSystemNodes(int width, int height)
        {
            InitializeNodes(width, height);
        }

        private void InitializeNodes(int width, int height)
        {
            _nodes.Clear();
            for (int i = 0; i < NodeCount; i++)
            {
                _nodes.Add(new Node
                {
                    X = _random.Next(0, width),
                    Y = _random.Next(0, height),
                    VelocityX = (float)(_random.NextDouble() * 2 - 1), // Movimiento en X entre -1 y 1
                    VelocityY = (float)(_random.NextDouble() * 2 - 1) // Movimiento en Y entre -1 y 1
                });
            }
        }

        public void Update(float deltaTime, int width, int height)
        {
            if (!Enabled) return;

            foreach (var node in _nodes)
            {
                node.X += node.VelocityX * Speed * deltaTime * 50;
                node.Y += node.VelocityY * Speed * deltaTime * 50;

                if (node.X < 0 || node.X > width)
                    node.VelocityX *= -1;

                if (node.Y < 0 || node.Y > height)
                    node.VelocityY *= -1;
            }
        }

        public void Render(ImDrawListPtr drawList, int width, int height)
        {
            if (!Enabled) return;

            foreach (var node in _nodes)
            {

                uint nodeColor = ImGui.ColorConvertFloat4ToU32(NodeColor);
                drawList.AddCircleFilled(new Vector2(node.X, node.Y), 3.0f, nodeColor);

                foreach (var otherNode in _nodes)
                {
                    float dx = otherNode.X - node.X;
                    float dy = otherNode.Y - node.Y;
                    float distance = (float)Math.Sqrt(dx * dx + dy * dy);

                    if (distance < ConnectionDistance)
                    {
                        float alpha = 1.0f - (distance / ConnectionDistance);
                        uint lineColor = ImGui.ColorConvertFloat4ToU32(new Vector4(LineColor.X, LineColor.Y, LineColor.Z, alpha * LineColor.W));
                        drawList.AddLine(new Vector2(node.X, node.Y), new Vector2(otherNode.X, otherNode.Y), lineColor, 1.0f);
                    }
                }
            }
        }
    }

}
