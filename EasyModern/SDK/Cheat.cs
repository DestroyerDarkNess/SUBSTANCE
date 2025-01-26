using EasyModern.Core.Effects;
using EasyModern.Core.Utils;
using EasyModern.UI.Widgets;
using Hexa.NET.ImGui;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using Matrix = SharpDX.Matrix;

namespace EasyModern.SDK
{
    public class Cheat
    {
        // Game Data
        private List<Player> players = null;
        private List<Player> Server_Enemys = null;
        private Player localPlayer = null;
        private Matrix viewProj, m_ViewMatrixInverse;
        //private float real_gravity;
        private int spectatorCount = 0;
        private int RadarScale = 1;
        private bool IsTargetting;
        private string LastTargetName;
        private int WantedJetSpeed = 299;
        private int MaxJetSpeed = 302;
        private float[] FiringRate, SpreadValue;
        private int WeaponID = 0;
        string[] Jets;

        // Color   


        // Screen Size
        private SharpDX.Rectangle rect;

        //
        // Summary:
        //     Gets the Point that specifies the center of the rectangle.
        //
        // Value:
        //     The center.
        public Point rect_Center => new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);

        ImDrawListPtr drawList;

        SharinganCrosshair SharinganCrosshair = null;
        AlertWindow alert = null;

        public Cheat()
        {
            players = new List<Player>();
            Server_Enemys = new List<Player>();
            //RPM.OpenProcess(Core.Instances.GameProcess.Id);
            drawList = ImGui.GetBackgroundDrawList();

            SharinganCrosshair = new SharinganCrosshair
            {
                Scale = 1.5f,
                Animate = (Core.Instances.Settings.Crosshair_AnimationSpeed > 0),
                AnimationSpeed = Core.Instances.Settings.Crosshair_AnimationSpeed,
                Transparent = true
            };

            alert = new AlertWindow(
               new Vector4(0.18f, 0.0f, 0.03f, 0.5f), // Fondo (#2d0007)
               new Vector4(1.0f, 0.0f, 0.2f, 1.0f),  // Borde rojo (#ff0033)
               new Vector4(1.0f, 0.5f, 0.0f, 1.0f),  // Círculo naranja
               new Vector4(1.0f, 0.0f, 0.2f, 0.4f),  // Sombra interna
               animationSpeed: 3.0f
            );

            FiringRate = new float[]
           {
                1000,
                1200,
                1500,
                2000
           };

            SpreadValue = new float[]
            {
                0.00f,
                0.00f,
                0.05f,
                0.10f,
                0.15f,
                0.20f,
                0.25f,
                0.30f,
                0.35f,
                0.40f,
                0.45f,
                0.50f,
                1.00f
            };


            Core.Instances.ImageManager.AddImage("Radar_Default", Properties.Resources._3_white);
            Core.Instances.ImageManager.AddImage("team_blue", Properties.Resources.teammate);
            Core.Instances.ImageManager.AddImage("team_red", Properties.Resources.enemi);
            Core.Instances.ImageManager.AddImage("T99", Properties.Resources._10_white);
            Core.Instances.ImageManager.AddImage("M1ABRAMS", Properties.Resources._10_white);
            Core.Instances.ImageManager.AddImage("T90", Properties.Resources._10_white);
            Core.Instances.ImageManager.AddImage("LAV25", Properties.Resources._4_white);
            Core.Instances.ImageManager.AddImage("ZBD09", Properties.Resources._4_white);
            Core.Instances.ImageManager.AddImage("AME_BTR90", Properties.Resources._4_white);
            Core.Instances.ImageManager.AddImage("Z11", Properties.Resources._11_white);
            Core.Instances.ImageManager.AddImage("AH6", Properties.Resources._11_white);
            Core.Instances.ImageManager.AddImage("KA60", Properties.Resources._13_white);
            Core.Instances.ImageManager.AddImage("UH1Y", Properties.Resources._13_white);
            Core.Instances.ImageManager.AddImage("Z9", Properties.Resources._13_white);
            Core.Instances.ImageManager.AddImage("HIMARS", Properties.Resources._7_white);
            Core.Instances.ImageManager.AddImage("AAV", Properties.Resources._7_white);
            Core.Instances.ImageManager.AddImage("MI28", Properties.Resources._12_white);
            Core.Instances.ImageManager.AddImage("AH1Z", Properties.Resources._12_white);
            Core.Instances.ImageManager.AddImage("Z10", Properties.Resources._12_white);
            Core.Instances.ImageManager.AddImage("9K22", Properties.Resources._5_white);
            Core.Instances.ImageManager.AddImage("LAVAD", Properties.Resources._5_white);
            Core.Instances.ImageManager.AddImage("PGZ95", Properties.Resources._5_white);
            Core.Instances.ImageManager.AddImage("A10", Properties.Resources._9_white);
            Core.Instances.ImageManager.AddImage("Q5", Properties.Resources._9_white);
            Core.Instances.ImageManager.AddImage("SU39", Properties.Resources._9_white);
            Core.Instances.ImageManager.AddImage("AME_F35", Properties.Resources.plane);
            Core.Instances.ImageManager.AddImage("J20", Properties.Resources.plane);
            Core.Instances.ImageManager.AddImage("PAKFA", Properties.Resources.plane);
            Core.Instances.ImageManager.AddImage("DV15", Properties.Resources._6_white);
            Core.Instances.ImageManager.AddImage("PWC", Properties.Resources._6_white);
            Core.Instances.ImageManager.AddImage("CB90", Properties.Resources._6_white);

            Jets = new string[] { "A10", "Q5", "SU39", "AME_F35", "J20", "PAKFA" };

            //RPM.RemoteNop("user32.dll", "GetWindowDisplayAffinity");
        }


        public bool Update()
        {
            try
            {

                System.Drawing.Size Overlay_Size = Core.Instances.OverlayWindow.Size;
                rect = new SharpDX.Rectangle(0, 0, Overlay_Size.Width, Overlay_Size.Height);
                drawList = ImGui.GetBackgroundDrawList();
                drawList.Flags = ImDrawListFlags.None;

                DrawCrosshair();
                DrawFov();

                if (Core.Instances.Settings.ShowMenu && Core.Instances.Settings.ESP && Core.Instances.Settings.ESP_Preview)
                    RenderESPPreview();

                if (Core.Instances.Settings.Radar)
                {
                    DrawRadar(5, 5, Core.Instances.Settings.Radar_Scale, Core.Instances.Settings.Radar_Scale);
                }

                return true;
            }
            catch { return false; }

        }

        private Player dummyPlayer = new Player()
        {
            Name = "Preview Player",
            Health = 80f,
            MaxHealth = 100f,
            Distance = 50f,
            Origin = new Vector3(0, 0, 0),
            //Bone = new Bone()
            //{
            //    BONE_HEAD = new Vector3(0, 1.70f, 0),
            //    BONE_NECK = new Vector3(0, 1.50f, 0),
            //    BONE_SPINE2 = new Vector3(0, 1.20f, 0),
            //    BONE_SPINE1 = new Vector3(0, 0.90f, 0),
            //    BONE_SPINE = new Vector3(0, 0.60f, 0),
            //    BONE_LEFTSHOULDER = new Vector3(-0.25f, 1.20f, 0),
            //    BONE_RIGHTSHOULDER = new Vector3(0.25f, 1.20f, 0),
            //    BONE_LEFTELBOWROLL = new Vector3(-0.45f, 1.00f, 0),
            //    BONE_RIGHTELBOWROLL = new Vector3(0.45f, 1.00f, 0),
            //    BONE_LEFTHAND = new Vector3(-0.55f, 0.80f, 0),
            //    BONE_RIGHTHAND = new Vector3(0.55f, 0.80f, 0),
            //    BONE_LEFTKNEEROLL = new Vector3(-0.25f, 0.20f, 0),
            //    BONE_RIGHTKNEEROLL = new Vector3(0.25f, 0.20f, 0),
            //    BONE_LEFTFOOT = new Vector3(-0.25f, -0.05f, 0),
            //    BONE_RIGHTFOOT = new Vector3(0.25f, -0.05f, 0)
            //},
            Team = 9999,
            Pose = 0
        };

        public void RenderESPPreview()
        {

            ImGui.SetNextWindowSize(new Vector2(150, 200), ImGuiCond.FirstUseEver);
            ImGui.SetNextWindowPos(new Vector2(80, 80), ImGuiCond.FirstUseEver);

            ImGuiWindowFlags flags = ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar
                                   | ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.NoNav;

            if (!ImGui.Begin("ESP Preview", flags))
            {
                ImGui.End();
                return;
            }

            // Región de contenido disponible
            Vector2 contentSize = ImGui.GetContentRegionAvail();
            // Origen de dibujo (esquina superior-izquierda de la ventana)
            Vector2 startPos = ImGui.GetCursorScreenPos();
            // Centro hipotético para dibujar “dummy” (simulando world->screen)
            Vector2 center = new Vector2(
                startPos.X + contentSize.X / 2,
                startPos.Y + contentSize.Y / 2
            );

            var drawList = ImGui.GetWindowDrawList();

            // 1) Simular proyección de FOOT/HEAD (falso 2D)
            Vector2 headPos = new Vector2(center.X, center.Y - 50);
            Vector2 footPos = new Vector2(center.X, center.Y + 50);

            float boxHeight = footPos.Y - headPos.Y;
            float boxWidth = boxHeight / 2.0f;
            float xBox = headPos.X - boxWidth / 2.0f;

            // Color de ejemplo
            Vector4 color = Core.Instances.Settings.ESP_Enemy_Color;

            // Dibujar la caja según la config
            if (Core.Instances.Settings.ESP_Box)
            {
                switch (Core.Instances.Settings.ESP_BoxType)
                {
                    case 1:
                        // Rect con solo esquinas
                        ImGuiDrawingUtils.DrawCornerRect(
                            drawList,
                            (int)xBox, (int)headPos.Y,
                            (int)boxWidth, (int)boxHeight,
                            color, 1.0f, 10
                        );
                        break;

                    case 2:
                        // Caso "3D": dibujamos una caja con perspectiva de lado
                        // Definimos 8 vértices en espacio local. 
                        // Supongamos un ancho=boxWidth, alto=boxHeight, “profundidad” 30 px
                        // para que se note la “profundidad”.
                        float halfW = boxWidth / 2;
                        float halfH = boxHeight;
                        float depth = 30f; // Profundidad simulada

                        // El “centro” de la caja 3D lo ponemos en la mitad de la altura. 
                        // => la base inferior en footPos, la parte superior en headPos.
                        // Eje Y crece hacia abajo. Eje X crece hacia la derecha.
                        // Eje Z será “hacia adentro” de la pantalla, lo simulamos en la proyección.

                        // Vértices (x,y,z). El “y” va de 0..-boxHeight 
                        // (así que la base está en Y=0, la cima en Y=-boxHeight).
                        // El “x” va de -halfW.. +halfW.
                        // El “z” en [0.. -depth] (negativo para “hacia adentro”).

                        // Base (y=0):
                        Vector3 v0 = new Vector3(-halfW, 0, 0);         // bottom-left-front
                        Vector3 v1 = new Vector3(halfW, 0, 0);         // bottom-right-front
                        Vector3 v2 = new Vector3(halfW, 0, -depth);    // bottom-right-back
                        Vector3 v3 = new Vector3(-halfW, 0, -depth);    // bottom-left-back

                        // Top (y=-boxHeight):
                        Vector3 v4 = new Vector3(-halfW, -halfH, 0);      // top-left-front
                        Vector3 v5 = new Vector3(halfW, -halfH, 0);      // top-right-front
                        Vector3 v6 = new Vector3(halfW, -halfH, -depth); // top-right-back
                        Vector3 v7 = new Vector3(-halfW, -halfH, -depth); // top-left-back

                        // Para simular la caja con un “ángulo” de ~15° en Y. 
                        float angleY = 15.0f * (float)(Math.PI / 180.0);
                        Matrix4x4 rotY = Matrix4x4.CreateRotationY(angleY);

                        // Trasladar la caja de modo que su base coincida con footPos
                        // => su base en “(0,0,0)” local => footPos en la UI
                        // => Al final multiplicamos la coords [x,y,z] por rotY y luego 
                        //    proyectamos a 2D con la simulación “fake perspective”.

                        Vector2 Transform3Dto2D(Vector3 local)
                        {
                            // 1) Rotación Y
                            Vector3 rotated = Vector3.Transform(local, rotY);

                            // 2) Simulación de una proyección (z > 0 => +escala?). 
                            //    Para z negativo => se “acerca” a la pantalla.
                            float fakeDepth = rotated.Z * 0.003f; // Factor 0.003 => ajustable
                            float scale = 1.0f / (1.0f - fakeDepth);

                            // 3) Origen en footPos => X = footPos.X + rotated.x * scale
                            //    Y = footPos.Y + rotated.y * scale
                            float screenX = footPos.X + (rotated.X * scale);
                            float screenY = footPos.Y + (rotated.Y * scale);

                            return new Vector2(screenX, screenY);
                        }

                        // Proyectar las 8 esquinas
                        Vector2 p0 = Transform3Dto2D(v0);
                        Vector2 p1 = Transform3Dto2D(v1);
                        Vector2 p2 = Transform3Dto2D(v2);
                        Vector2 p3 = Transform3Dto2D(v3);
                        Vector2 p4 = Transform3Dto2D(v4);
                        Vector2 p5 = Transform3Dto2D(v5);
                        Vector2 p6 = Transform3Dto2D(v6);
                        Vector2 p7 = Transform3Dto2D(v7);

                        uint c = ImGui.ColorConvertFloat4ToU32(color);

                        // Dibujamos las aristas
                        drawList.AddLine(p0, p1, c);
                        drawList.AddLine(p1, p2, c);
                        drawList.AddLine(p2, p3, c);
                        drawList.AddLine(p3, p0, c);

                        drawList.AddLine(p4, p5, c);
                        drawList.AddLine(p5, p6, c);
                        drawList.AddLine(p6, p7, c);
                        drawList.AddLine(p7, p4, c);

                        drawList.AddLine(p0, p4, c);
                        drawList.AddLine(p1, p5, c);
                        drawList.AddLine(p2, p6, c);
                        drawList.AddLine(p3, p7, c);
                        break;

                    default:
                        // Rect clásico
                        ImGuiDrawingUtils.DrawRect(
                            drawList,
                            (int)xBox, (int)headPos.Y,
                            (int)boxWidth, (int)boxHeight,
                            color, 1.0f
                        );
                        break;
                }
            }

            // 5) Nombre
            if (Core.Instances.Settings.ESP_Name)
            {
                string drawText = dummyPlayer.Name;
                if (Core.Instances.Settings.ESP_Distance_InName)
                    drawText += $" [{(int)dummyPlayer.Distance}m]";

                float nameOffsetY = 20.0f;
                ImGuiDrawingUtils.DrawTextCenter(
                    drawList,
                    (int)xBox, (int)(headPos.Y - nameOffsetY - 10),
                    (int)boxWidth, 10,
                    drawText,
                    new Vector4(1f, 1f, 1f, 1f),
                    false
                );
            }

            // 6) Distancia (si no la incluimos en el nombre)
            if (Core.Instances.Settings.ESP_Distance && !Core.Instances.Settings.ESP_Distance_InName)
            {
                ImGuiDrawingUtils.DrawText(
                    drawList,
                    (int)xBox, (int)footPos.Y + 2,
                    $"{(int)dummyPlayer.Distance}m",
                    new Vector4(1f, 1f, 1f, 1f),
                    false
                );
            }

            // 7) Barra de vida
            if (Core.Instances.Settings.ESP_Health)
            {
                int ch = (int)dummyPlayer.Health;
                int mh = (int)dummyPlayer.MaxHealth;

                float HealthHeight = 5.0f;
                float healthBarY = headPos.Y - 8f;

                if (Core.Instances.Settings.ESP_Position == 3) // Derecha
                {
                    xBox += boxWidth;
                }

                if (Core.Instances.Settings.ESP_Orientation == 1) // vertical
                {
                    //xBox -= 2f;
                    healthBarY += 9f;
                    HealthHeight = boxHeight;
                    boxWidth = 5.0f;
                }

                if (Core.Instances.Settings.ESP_Position == 1) // abajo
                {
                    healthBarY += boxHeight + 8f;
                }
                else if (Core.Instances.Settings.ESP_Position == 2) // izquierda
                {
                    xBox -= 4f;
                }


                DrawHealthBar(
                     drawList,
                     (int)xBox, (int)healthBarY,
                     (int)boxWidth, (int)HealthHeight,
                     ch, mh,
                     Core.Instances.Settings.ESP_Orientation,  // o "vertical"
                     Core.Instances.Settings.ESP_Position,         // "bottom", "left", o "right"
                     true, 1,
                     new Vector4(0, 1, 0, 1),
                     new Vector4(0.2f, 0.2f, 0.2f, 0.8f)
                 );
            }

            // 8) Skeleton / Bone (opcional)
            //if (Core.Instances.Settings.ESP_Bone)
            //{
            //    Vector2 neckPos = new Vector2(center.X, center.Y - 35);
            //    Vector2 spinePos = new Vector2(center.X, center.Y - 10);

            //    ImGuiDrawingUtils.DrawLine(drawList, (int)headPos.X, (int)headPos.Y,
            //                               (int)neckPos.X, (int)neckPos.Y,
            //                               new Vector4(1.0f, 0.0f, 0.0f, 0.5f));
            //    ImGuiDrawingUtils.DrawLine(drawList, (int)neckPos.X, (int)neckPos.Y,
            //                               (int)spinePos.X, (int)spinePos.Y,
            //                               new Vector4(1.0f, 0.0f, 0.0f, 0.5f));
            //}

            // 9) ESP Line: línea desde la base de la ventana (centro inferior)
            // hasta el centro inferior de la caja. 
            if (Core.Instances.Settings.ESP_Line)
            {
                Vector2 windowPos = ImGui.GetWindowPos();
                Vector2 windowSize = ImGui.GetWindowSize();

                float bottomWindowX = windowPos.X + (windowSize.X / 2);
                float bottomWindowY = windowPos.Y + windowSize.Y;

                Vector2 footVec = new Vector2(footPos.X, footPos.Y);

                drawList.AddLine(
                    new Vector2(bottomWindowX, bottomWindowY),
                    footVec,
                    ImGui.ColorConvertFloat4ToU32(Core.Instances.Settings.ESP_Enemy_Line_Color), // color
                    1.0f
                );
            }

            ImGui.End();
        }

        /// <summary>
        /// Dibuja una barra de vida con distintas opciones de orientación y posición.
        /// </summary>
        /// <param name="drawList">La lista de dibujo de ImGui.</param>
        /// <param name="x">Coordenada X de referencia (esquina de la caja ESP).</param>
        /// <param name="y">Coordenada Y de referencia (esquina de la caja ESP).</param>
        /// <param name="boxWidth">Ancho de la caja ESP.</param>
        /// <param name="boxHeight">Alto de la caja ESP (headToFoot).</param>
        /// <param name="currentHealth">Salud actual.</param>
        /// <param name="maxHealth">Salud máxima.</param>
        /// <param name="orientation">"horizontal" o "vertical".</param>
        /// <param name="position">"top", "bottom", "left", "right". Depende de orientation.</param>
        /// <param name="drawBackground">Si true, dibuja un fondo detrás de la barra.</param>
        /// <param name="barThickness">Grosor de la barra (si horizontal => alto; si vertical => ancho).</param>
        /// <param name="healthColor">Color principal de la barra de vida.</param>
        /// <param name="bgColor">Color de fondo si drawBackground es true.</param>
        public static void DrawHealthBar(
            ImDrawListPtr drawList,
            int x, int y,
            int boxWidth, int boxHeight,
            int currentHealth, int maxHealth,
            int orientation = 0,
            int position = 0,
            bool drawBackground = true,
            int barThickness = 3,
            Vector4 healthColor = default,
            Vector4 bgColor = default)
        {
            // Asegurarnos de que color por defecto sea verde si no se pasa
            if (healthColor.Equals(default(Vector4)))
                healthColor = new Vector4(0.0f, 1.0f, 0.0f, 1.0f);  // Verde

            // Fondo gris oscuro si no se pasa
            if (bgColor.Equals(default(Vector4)))
                bgColor = new Vector4(0.2f, 0.2f, 0.2f, 0.8f);  // Gris oscuro semitransparente

            // Por seguridad, si maxHealth < currentHealth
            if (maxHealth < currentHealth)
                maxHealth = currentHealth;
            if (maxHealth <= 0)
                maxHealth = 1;

            // Calculamos el porcentaje (0..1)
            float healthPerc = Math.Max(0, Math.Min(1.0f, currentHealth / (float)maxHealth));

            // Convertimos a uint para ImGui
            uint colHealthU32 = ImGui.ColorConvertFloat4ToU32(healthColor);
            uint colBgU32 = ImGui.ColorConvertFloat4ToU32(bgColor);

            // Dependiendo de orientation y position, calculamos las coordenadas
            if (orientation == 0)
            {
                // La barra será horizontal y medirá "barThickness" en alto,
                // y su ancho será boxWidth * healthPerc.
                // position => "top" o "bottom".

                int barX, barY;           // donde empieza
                int barW, barH;           // ancho y alto de la barra

                barH = barThickness;
                barW = (int)(boxWidth * healthPerc);

                if (position == 0)
                {
                    // Barra arriba de la caja => Y es el mismo que la parte superior de la caja
                    barX = x;
                    barY = y - barThickness;
                    // subimos la barra sobre la caja
                }
                else
                {
                    // "bottom"
                    // Barra debajo => Y es la parte inferior de la caja
                    barX = x;
                    barY = y + boxHeight;
                }

                // Dibujar fondo si se solicita:
                if (drawBackground)
                {
                    int bgW = boxWidth;
                    int bgH = barThickness;
                    drawList.AddRectFilled(
                        new Vector2(barX, barY),
                        new Vector2(barX + bgW, barY + bgH),
                        colBgU32);
                }

                // Dibujamos la barra de vida
                drawList.AddRectFilled(
                    new Vector2(barX, barY),
                    new Vector2(barX + barW, barY + barH),
                    colHealthU32
                );
            }
            else
            {
                // orientation == "vertical"
                // La barra será vertical y medirá "barThickness" en ancho,
                // y su alto será boxHeight * healthPerc.
                // position => "left" o "right".

                int barX, barY;
                int barW, barH;

                barW = barThickness;
                barH = (int)(boxHeight * healthPerc);

                if (position == 2)
                {
                    // Barra a la izquierda => X es el mismo que la parte izquierda de la caja
                    barX = x - barThickness; // la ponemos a la izquierda
                    barY = y + (boxHeight - barH);
                    // porque va de abajo hacia arriba
                }
                else
                {
                    // "right"
                    // Barra a la derecha => X es x + boxWidth
                    barX = x + boxWidth;
                    barY = y + (boxHeight - barH);
                }

                // Dibujar fondo si se solicita
                if (drawBackground)
                {
                    int bgW = barThickness;
                    int bgH = boxHeight;

                    drawList.AddRectFilled(
                        new Vector2(barX, y),
                        new Vector2(barX + bgW, y + bgH),
                        colBgU32
                    );
                }

                // Dibujar la parte que corresponde al currentHealth
                drawList.AddRectFilled(
                    new Vector2(barX, barY),
                    new Vector2(barX + barW, y + boxHeight),
                    colHealthU32
                );
            }
        }
        public void DrawFov()
        {
            if (Core.Instances.Settings.AIM_Draw_Fov)
            {
                Vector4 CrosshairColour = Core.Instances.Settings.AIM_Fov_Color;
                int num = Core.Instances.Settings.AIM_Fov;
                ImGuiDrawingUtils.DrawCircle(drawList, rect_Center.X, rect_Center.Y, rect.Width / 100 * num, CrosshairColour, 1, 100);
            }
        }

        public void DrawCrosshair()
        {
            if (Core.Instances.Settings.Crosshair)
            {

                Vector4 CrosshairColour = Core.Instances.Settings.Crosshair_Color;

                if (Core.Instances.Settings.RGB_Crosshair_Color)
                {
                    CrosshairColour = Core.Instances.RGBColors.CurrentColor;
                }

                float scale = Core.Instances.Settings.Crosshair_Scale;
                float CrosshairX = rect_Center.X;    // Coordenada X donde se dibuja el crosshair
                float CrosshairY = rect_Center.Y;    // Coordenada Y donde se dibuja el crosshair

                // Radio base, luego lo multiplicamos por 'scale' para ajustar el tamaño
                float CrosshairRadius = 10.0f * scale;
                bool CrosshairFilled = false;        // Indica si se dibuja relleno o no
                float CrosshairThickness = 1.0f;     // Grosor de línea cuando no está relleno

                // Selecciona el estilo de crosshair según Settings.Crosshair_Style
                switch (Core.Instances.Settings.Crosshair_Style)
                {
                    case 0:
                        // Estilo: Una cruz
                        drawList.AddLine(
                            new Vector2(CrosshairX, CrosshairY - CrosshairRadius),
                            new Vector2(CrosshairX, CrosshairY + CrosshairRadius),
                            ImGui.GetColorU32(CrosshairColour),
                            CrosshairThickness);

                        drawList.AddLine(
                            new Vector2(CrosshairX - CrosshairRadius, CrosshairY),
                            new Vector2(CrosshairX + CrosshairRadius, CrosshairY),
                            ImGui.GetColorU32(CrosshairColour),
                            CrosshairThickness);
                        break;

                    case 1:
                        // Estilo: Cuadrado (solo contorno)
                        {
                            float left = CrosshairX - CrosshairRadius;
                            float top = CrosshairY - CrosshairRadius;
                            float right = CrosshairX + CrosshairRadius;
                            float bottom = CrosshairY + CrosshairRadius;

                            drawList.AddRect(
                                new Vector2(left, top),
                                new Vector2(right, bottom),
                                ImGui.GetColorU32(CrosshairColour),
                                0.0f,
                                ImDrawFlags.None,
                                CrosshairThickness);
                        }
                        break;

                    case 2:
                        // Estilo: Triángulo (solo contorno)
                        {
                            Vector2 p1 = new Vector2(CrosshairX - CrosshairRadius, CrosshairY + CrosshairRadius);
                            Vector2 p2 = new Vector2(CrosshairX + CrosshairRadius, CrosshairY + CrosshairRadius);
                            Vector2 p3 = new Vector2(CrosshairX, CrosshairY - CrosshairRadius);

                            drawList.AddTriangle(
                                p1, p2, p3,
                                ImGui.GetColorU32(CrosshairColour),
                                CrosshairThickness);
                        }
                        break;

                    case 3:
                        // Estilo: Línea horizontal centrada
                        drawList.AddLine(
                            new Vector2(CrosshairX - CrosshairRadius, CrosshairY),
                            new Vector2(CrosshairX + CrosshairRadius, CrosshairY),
                            ImGui.GetColorU32(CrosshairColour),
                            CrosshairThickness);
                        break;

                    case 4:
                        // Estrella de David (dos triángulos superpuestos)
                        {
                            Vector2 p1 = new Vector2(CrosshairX, CrosshairY - CrosshairRadius);
                            Vector2 p2 = new Vector2(CrosshairX - CrosshairRadius, CrosshairY + (CrosshairRadius / 2f));
                            Vector2 p3 = new Vector2(CrosshairX + CrosshairRadius, CrosshairY + (CrosshairRadius / 2f));

                            Vector2 p4 = new Vector2(CrosshairX, CrosshairY + CrosshairRadius);
                            Vector2 p5 = new Vector2(CrosshairX - CrosshairRadius, CrosshairY - (CrosshairRadius / 2f));
                            Vector2 p6 = new Vector2(CrosshairX + CrosshairRadius, CrosshairY - (CrosshairRadius / 2f));

                            if (CrosshairFilled)
                            {
                                // Rellenar ambos triángulos
                                drawList.AddTriangleFilled(p1, p2, p3, ImGui.GetColorU32(CrosshairColour));
                                drawList.AddTriangleFilled(p4, p5, p6, ImGui.GetColorU32(CrosshairColour));
                            }
                            else
                            {
                                // Solo contorno
                                drawList.AddTriangle(p1, p2, p3, ImGui.GetColorU32(CrosshairColour), CrosshairThickness);
                                drawList.AddTriangle(p4, p5, p6, ImGui.GetColorU32(CrosshairColour), CrosshairThickness);
                            }
                        }
                        break;

                    case 5:
                        // Estilo: Dibuja un "*"
                        {
                            Vector2 starSize = ImGui.CalcTextSize("*");
                            float starX = CrosshairX - (starSize.X * 0.5f);
                            float starY = CrosshairY - (starSize.Y * 0.5f);
                            drawList.AddText(
                                new Vector2(starX, starY),
                                ImGui.GetColorU32(CrosshairColour),
                                "*"
                            );
                        }
                        break;

                    case 6:
                        // Estilo: Círculo relleno
                        drawList.AddCircleFilled(
                            new Vector2(CrosshairX, CrosshairY),
                            CrosshairRadius,
                            ImGui.GetColorU32(CrosshairColour));
                        break;

                    case 7:

                        SharinganCrosshair.Animate = (Core.Instances.Settings.Crosshair_AnimationSpeed > 0);
                        SharinganCrosshair.AnimationSpeed = Core.Instances.Settings.Crosshair_AnimationSpeed;
                        SharinganCrosshair.BaseColor = CrosshairColour;
                        SharinganCrosshair.Scale = scale;
                        SharinganCrosshair.DrawCrosshair(drawList, CrosshairX, CrosshairY);

                        break;

                    default:
                        // Por defecto, línea vertical + horizontal => Cruz
                        drawList.AddLine(
                            new Vector2(CrosshairX, CrosshairY - CrosshairRadius),
                            new Vector2(CrosshairX, CrosshairY + CrosshairRadius),
                            ImGui.GetColorU32(CrosshairColour),
                            CrosshairThickness);

                        drawList.AddLine(
                            new Vector2(CrosshairX - CrosshairRadius, CrosshairY),
                            new Vector2(CrosshairX + CrosshairRadius, CrosshairY),
                            ImGui.GetColorU32(CrosshairColour),
                            CrosshairThickness);
                        break;
                }
            }
        }

        private void DrawRadar(int X, int Y, int W, int H)
        {
            // Dibujar el fondo del radar
            ImGuiDrawingUtils.DrawFillRect(drawList, X, Y, W, H, new Vector4(0.071f, 0.071f, 0.071f, 1.000f));
            ImGuiDrawingUtils.DrawRect(drawList, X + 1, Y + 1, W - 2, H - 2, new Vector4(0.173f, 0.176f, 0.176f, 1.000f));

            // Dibujar las líneas del radar (horizontal y vertical)
            ImGuiDrawingUtils.DrawLine(drawList, X + W / 2, Y, X + W / 2, Y + H, new Vector4(0.173f, 0.176f, 0.176f, 1.000f));
            ImGuiDrawingUtils.DrawLine(drawList, X, Y + H / 2, X + W, Y + H / 2, new Vector4(0.173f, 0.176f, 0.176f, 1.000f));

            // Calcular y dibujar el triángulo de FOV
            float fovY = localPlayer.Fov.Y;
            fovY /= 1.34f;
            fovY -= (float)Math.PI / 2;
            float radarCenterX = X + W / 2;
            float radarCenterY = Y + H / 2;

            // Cálculo correcto de 'dot' como el radio del radar
            float dot = W / 2f; // Asumiendo que el radar es cuadrado

            int fov_x = (int)(dot * (float)Math.Cos(fovY) + radarCenterX);
            fovY += (float)Math.PI;
            int fov_x1 = (int)(dot * (float)Math.Cos(-fovY) + radarCenterX);

            ImGuiDrawingUtils.DrawTriangle(drawList, new System.Numerics.Vector2[]
            {
        new System.Numerics.Vector2(radarCenterX, radarCenterY),
        new System.Numerics.Vector2(fov_x, Y),
        new System.Numerics.Vector2(fov_x1, Y)
            }, new Vector4(0.173f, 0.176f, 0.176f, 1.000f));

            // Iterar sobre cada jugador para dibujarlos en el radar
            foreach (Player current in players.ToList())
            {
                if (current.IsValid())
                {
                    // Calcular la posición relativa del jugador
                    float r1 = localPlayer.Origin.Z - current.Origin.Z;
                    float r2 = localPlayer.Origin.X - current.Origin.X;
                    float x = r2 * (float)Math.Cos(-localPlayer.Yaw) - r1 * (float)Math.Sin(-localPlayer.Yaw);
                    float z = r2 * (float)Math.Sin(-localPlayer.Yaw) + r1 * (float)Math.Cos(-localPlayer.Yaw);
                    x *= RadarScale;
                    z *= RadarScale;
                    x += X + W / 2;
                    z += Y + H / 2;
                    Vector2 orgn = new Vector2(x, z);

                    // Calcular posición con ShootSpace
                    Vector3 pos = current.Origin + current.ShootSpace * 10f;
                    r1 = localPlayer.Origin.Z - pos.Z;
                    r2 = localPlayer.Origin.X - pos.X;
                    x = r2 * (float)Math.Cos(-localPlayer.Yaw) - r1 * (float)Math.Sin(-localPlayer.Yaw);
                    z = r2 * (float)Math.Sin(-localPlayer.Yaw) + r1 * (float)Math.Cos(-localPlayer.Yaw);
                    x *= RadarScale;
                    z *= RadarScale;
                    x += X + W / 2;
                    z += Y + H / 2;
                    Vector2 temp1 = new Vector2(x - orgn.X, z - orgn.Y);
                    Vector2 temp;
                    NumericsExtensions.Normalize(ref temp1, out temp);
                    Vector2 enemyPositionRadar = new Vector2(orgn.X, orgn.Y);

                    // Calcular ángulo de rotación
                    double angleToRotate = Math.Atan2(0.0, 1.0) - Math.Atan2(temp.X, temp.Y);
                    angleToRotate *= (180 / Math.PI); // Convertir a grados
                    angleToRotate = 180 + angleToRotate; // Invertir según sea necesario
                    angleToRotate *= (Math.PI / 180); // Convertir de nuevo a radianes

                    // Verificar si el jugador está dentro del área del radar
                    if (current.Distance >= 0f && current.Distance < W / (2 * RadarScale))
                    {
                        // Verificar que la posición en el radar esté dentro del rectángulo del radar
                        if (enemyPositionRadar.X >= X && enemyPositionRadar.X <= X + W &&
                            enemyPositionRadar.Y >= Y && enemyPositionRadar.Y <= Y + H)
                        {
                            if (current.InVehicle)
                            {
                                if (current.IsDriver && current.Team != localPlayer.Team)
                                {
                                    // Obtener el ícono correspondiente
                                    ImTextureID Icon = Core.Instances.ImageManager.GetImage(current.VehicleName);
                                    if (Icon.IsNull)
                                    {
                                        Icon = Core.Instances.ImageManager.GetImage("Radar_Default");
                                    }

                                    // Definir el rect del sprite en pantalla (ej: 30x30)
                                    int w = 10;
                                    int h = 10;
                                    int drawX = (int)enemyPositionRadar.X - w / 2;
                                    int drawY = (int)enemyPositionRadar.Y - h / 2;

                                    // Usa la rotación calculada en angleToRotate (radianes)
                                    float angleRadians = (float)angleToRotate;

                                    // Dibuja usando DrawImageRotated
                                    ImGuiDrawingUtils.DrawImageRotated(drawList, Icon, drawX, drawY, w, h, angleRadians);
                                }
                            }
                            else
                            {
                                // Infantería (sin rotación, o con rotación si lo deseas)
                                ImTextureID Icon = (current.Team == localPlayer.Team)
                                    ? Core.Instances.ImageManager.GetImage("team_blue")
                                    : Core.Instances.ImageManager.GetImage("team_red");

                                if (Icon.IsNull)
                                    Icon = Core.Instances.ImageManager.GetImage("team_red");

                                int w = 10;   // ancho del ícono
                                int h = 10;   // alto del ícono
                                int drawX = (int)enemyPositionRadar.X - w / 2;
                                int drawY = (int)enemyPositionRadar.Y - h / 2;

                                // Si no deseas rotación en infantería, llama a AddImage normal:
                                drawList.AddImage(
                                    Icon,
                                    new System.Numerics.Vector2(drawX, drawY),
                                    new System.Numerics.Vector2(drawX + w, drawY + h)
                                );

                                // Si deseas rotar la infantería, usa la misma función rotada:
                                // ImGuiDrawingUtils.DrawImageRotated(drawList, Icon, drawX, drawY, w, h, (float)angleToRotate);
                            }
                        }
                    }
                }
            }
        }

    }
}
