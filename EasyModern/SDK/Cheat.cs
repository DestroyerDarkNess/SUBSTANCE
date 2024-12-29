using EasyModern.Core.Utils;
using Hexa.NET.ImGui;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using Matrix = SharpDX.Matrix;

namespace EasyModern.SDK
{
    public class Cheat
    {
        // Game Data
        private List<Player> players = null;
        private Player localPlayer = null;


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

        public Cheat()
        {
            players = new List<Player>();
            //RPM.OpenProcess(Core.Instances.GameProcess.Id); Open Game Memory
            drawList = ImGui.GetBackgroundDrawList();

            Core.Instances.ImageManager.AddImage("Radar_Default", Properties.Resources._3_white);
            Core.Instances.ImageManager.AddImage("team_blue", Properties.Resources._1_team_blue);
            Core.Instances.ImageManager.AddImage("team_red", Properties.Resources._2_team_red);
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
            Core.Instances.ImageManager.AddImage("AME_F35", Properties.Resources._8_white);
            Core.Instances.ImageManager.AddImage("J20", Properties.Resources._8_white);
            Core.Instances.ImageManager.AddImage("PAKFA", Properties.Resources._8_white);
            Core.Instances.ImageManager.AddImage("DV15", Properties.Resources._6_white);
            Core.Instances.ImageManager.AddImage("PWC", Properties.Resources._6_white);
            Core.Instances.ImageManager.AddImage("CB90", Properties.Resources._6_white);

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
                Read();

                if (Core.Instances.Settings.Radar)
                {
                    DrawRadar(Core.Instances.Settings.Radar_Scale, 5, 5, Core.Instances.Settings.Radar_Scale, Core.Instances.Settings.Radar_Scale);
                }

                return true;
            }
            catch { return false; }

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


        private long pLocalPlayer = 0;
        private long pLocalSoldier = 0;

        private void Read()
        {
            // 1) Leer y asignar al localPlayer
            if (!ReadLocalPlayer())
                return;

            // 2) Aplicar configuraciones de arma/cheats al localPlayer
            ProcessLocalPlayerWeaponCheats();

            // 3) Leer a los demás jugadores
            ReadOtherPlayers();

            // 4) Dibujados / UI específicos (Esp, Aimbot, etc.)
            DrawEsp();
            HandleAimbot();
        }

        /// <summary>
        /// Lee el puntero al jugador local y asigna la información relevante en 'localPlayer'.
        /// Carga pLocalPlayer y pLocalSoldier en campos privados para su uso posterior.
        /// Devuelve false si no se pudo leer (o está inválido).
        /// </summary>
        private bool ReadLocalPlayer()
        {
            // Limpiamos la lista de players y creamos 'localPlayer' nuevo
            players.Clear();
            localPlayer = new Player();


            return true; // Terminamos con éxito
        }


        /// <summary>
        /// Aplica la lógica de cheats al arma del jugador local (OneHitKill, RateOfFire, NoGravity, etc.).
        /// Asume que pLocalPlayer y pLocalSoldier ya fueron establecidos en ReadLocalPlayer().
        /// </summary>
        private void ProcessLocalPlayerWeaponCheats()
        {

        }

        /// <summary>
        /// Lee y procesa la información de los demás jugadores (enemigos/aliados),
        /// incluyendo su posición, salud, etc. y llama a la lógica de Radar/ESP si corresponde.
        /// </summary>
        private void ReadOtherPlayers()
        {

        }

        private void DrawEsp()
        {

        }

        private void HandleAimbot()
        {

        }

        private bool GetAimKey()
        {
            Keys Key = localPlayer.IsVehicleWeapon ? Keys.LShiftKey : Keys.RButton;
            return Core.Instances.InputImguiEmu.IsKeyDown(Key);
        }

        private Player DistanceSortPlayers(List<Player> players)
        {
            return players.OrderBy(p => p.Distance).FirstOrDefault();
        }

        /// <summary>
        /// Ordena la lista de jugadores por distancia al crosshair ascendente y retorna el más cercano.
        /// </summary>
        private Player DistanceCrosshairSortPlayers(List<Player> players)
        {
            return players.OrderBy(p => p.DistanceCrosshair).FirstOrDefault();
        }

        /// <summary>
        /// Multiplica un vector3 por la matriz (rot + scale) y
        /// retorna el resultado. (Igual a tu Multiply original).
        /// </summary>
        private Vector3 Multiply(Vector3 vec, Matrix mat)
        {
            return new Vector3(
                mat.M11 * vec.X + mat.M21 * vec.Y + mat.M31 * vec.Z,
                mat.M12 * vec.X + mat.M22 * vec.Y + mat.M32 * vec.Z,
                mat.M13 * vec.X + mat.M23 * vec.Y + mat.M33 * vec.Z
            );
        }

        #region Draw Functions


        private void DrawProgress(int X, int Y, int W, int H, int Value, int MaxValue)
        {
            int progress = (int)(Value / ((float)MaxValue / 100));
            int w = (int)((float)W / 100 * progress);

            Vector4 color = new Vector4(0, 255, 0, 255);
            if (progress >= 20) color = new Vector4(173, 255, 47, 255);
            if (progress >= 40) color = new Vector4(255, 255, 0, 255);
            if (progress >= 60) color = new Vector4(255, 165, 0, 255);
            if (progress >= 80) color = new Vector4(255, 0, 0, 255);

            ImGuiDrawingUtils.DrawFillRect(drawList, X, Y - 1, W + 1, H + 2, new Vector4(0.000f, 0.000f, 0.000f, 1.000f));
            if (w >= 2)
            {
                ImGuiDrawingUtils.DrawFillRect(drawList, X + 1, Y, w - 1, H, color);
            }
        }

        private void DrawRadar(float RadarScale, int X, int Y, int W, int H)
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
                                    int w = 30;
                                    int h = 30;
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

                                int w = 20;   // ancho del ícono
                                int h = 20;   // alto del ícono
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

        #endregion

    }
}
