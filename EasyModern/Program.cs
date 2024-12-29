using EasyImGui;
using EasyImGui.Core;
using EasyModern.Core;
using EasyModern.Core.Model;
using EasyModern.Core.Utils;
using EasyModern.UI.Views;
using Hexa.NET.ImGui;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Windows.Forms;
using VNX;

namespace EasyModern
{
    internal class Program
    {


        static void Main(string[] args)
        {
            bool result = Diagnostic.RunDiagnostic();

            if (result)
            {
                Console.WriteLine("All diagnostics passed. The system is ready.");
            }
            else
            {
                Console.WriteLine("Some diagnostics failed. Please resolve the missing libraries, Press any key to continue.");
                Console.ReadKey();
            }

            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;


            //Process process = Process.GetProcessesByName("bf4").FirstOrDefault();

            //VNX.RemoteControl Control = new VNX.RemoteControl(process);

            //bool Compatible = Control.IsCompatibleProcess();

            //Console.WriteLine(process.Is64Bits() ? "x64 Detected" : "x86 Detected");
            //Console.WriteLine($"The Game {(Compatible ? "is" : "isn't")} compatible with this build.");

            ////Control.WaitInitialize();

            //if (Compatible)
            //{
            //    InjecInProc(Control, process);
            //    return;
            //}
            //else
            //{
            //    Console.WriteLine("Impossible to Inject, Continuing externally.  Press any key to continue...");

            StartOverlay();

            //}

            //Console.ReadKey();
        }

        public static bool LoadInto = false;
        public static void InjecInProc(RemoteControl Control, Process Process)
        {
            Control.LockEntryPoint();

            string CurrentAssembly = Assembly.GetExecutingAssembly().Location;

            int Ret = Control.CLRInvoke(CurrentAssembly, typeof(Program).FullName, "EntryPoint", Process.GetCurrentProcess().Id.ToString());

            Control.UnlockEntryPoint();

            Environment.Exit(0);
        }

        public static int EntryPoint(string Arg)
        {
            Process.GetProcessById(int.Parse(Arg))?.Kill();
            StartOverlay();

            return int.MaxValue;
        }

        public static Vector2 Gui_Size = new Vector2(1070, 550);

        public static void StartOverlay()
        {
            WindowFinder finder = new WindowFinder();

            finder.OnProcReady += (sender, status, processId) =>
            {

                Core.Instances.OverlayMode = status ? OverlayMode.InGameEmbed : OverlayMode.Normal;

                // Initialize the Overlay window with desired properties
                Core.Instances.OverlayWindow = new Overlay() { ResizableBorders = true, ShowInTaskbar = (Core.Instances.OverlayMode == OverlayMode.Normal), TopMost = false };
                Core.Instances.OverlayWindow.TransparencyKey = Color.Black;

                Core.Instances.OverlayWindow.FormClosing += (senderx, e) =>
                 {
                     SDK.ConfigManager.SaveConfig(Core.Instances.Settings);
                 };

                if (status && processId != 0)
                {
                    Core.Instances.GameProcess = System.Diagnostics.Process.GetProcessById(processId);
                    while (Core.Instances.GameProcess.MainWindowHandle == IntPtr.Zero) { }
                    Core.Instances.OverlayWindow.GameWindowHandle = Core.Instances.GameProcess.MainWindowHandle;
                }

                // Subscribe to configuration and initialization events
                Core.Instances.OverlayWindow.ImguiManager.ConfigContex += OnConfigContex;
                Core.Instances.OverlayWindow.OnImGuiReady += (object Sender, bool Status) =>
                {
                    if (Status)
                    {
                        Core.Instances.OverlayWindow.ImguiManager.Render += Render;
                    }
                    else
                    {
                        Console.WriteLine("Unable to initialize ImGui");
                    }
                };

                // Run the Overlay window application
                try
                {
                    Application.Run(Core.Instances.OverlayWindow);
                }
                catch (Exception Ex)
                {
                    System.Windows.Forms.MessageBox.Show(Ex.Message);
                    Environment.Exit(0);
                }


            };

            finder.Find(Core.Instances.GameWindowTitle);

        }

        public static void LoadMode()
        {
            if (Core.Instances.OverlayMode != OverlayMode.Normal)
            {
                //Core.Instances.OverlayWindow.Opacity = 0.8;
                Core.Instances.OverlayWindow.TopMost = true;
                Core.Instances.OverlayWindow.ResizableBorders = false;
                Core.Instances.OverlayWindow.NoActivateWindow = true;
                Core.Instances.OverlayWindow.Text = Core.Instances.GameProcess.MainWindowTitle;
            }
            else
            {
                // Set the size and position of the Overlay window
                Core.Instances.OverlayWindow.Size = new System.Drawing.Size((int)Gui_Size.X, (int)Gui_Size.Y);
                Core.Instances.OverlayWindow.Location = new System.Drawing.Point(
                    (Screen.PrimaryScreen.WorkingArea.Width / 2) - (Core.Instances.OverlayWindow.Width / 2),
                    (Screen.PrimaryScreen.WorkingArea.Height / 2) - (Core.Instances.OverlayWindow.Height / 2)
                );

                //Core.Instances.OverlayWindow.Size = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);

            }
        }

        private static bool OnConfigContex()
        {
            LoadMode();

            //Hexa.NET.ImGui.Backends.Win32.ImGuiImplWin32.EnableAlphaCompositing(Core.Instances.OverlayWindow.Handle);

            // Initialize InputImguiEmu for handling input
            Core.Instances.InputImguiEmu = new Core.Input.InputImguiEmu(Core.Instances.OverlayWindow.ImguiManager.IO);

            Core.Instances.InputImguiEmu.AddEvent(Keys.Insert, () =>
            {
                Core.Instances.Settings.ShowMenu = !Core.Instances.Settings.ShowMenu;
                if (!Core.Instances.Settings.ShowMenu) SDK.ConfigManager.SaveConfig(Core.Instances.Settings);
            });

            Core.Instances.InputImguiEmu.AddEvent(Keys.Escape, () =>
            {
                if (Core.Instances.Settings.ShowMenu) Core.Instances.Settings.ShowMenu = false;
            });

            Core.Instances.InputImguiEmu.AddEvent(Keys.F2, async () =>
            {
                Core.WinApis.SetWindowDisplayAffinity(Core.Instances.OverlayWindow.Handle, Core.WinApis.WDA_NONE);
                Core.Utils.Helper.Sleep(2);

                try
                {
                    Rectangle screenBounds = Core.Instances.OverlayWindow.Bounds;
                    using (Bitmap bitmap = new Bitmap(screenBounds.Width, screenBounds.Height))
                    {
                        using (Graphics g = Graphics.FromImage(bitmap))
                        {
                            g.CopyFromScreen(screenBounds.Location, Point.Empty, screenBounds.Size);
                        }

                        string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                        string fileName = $"SS_EasyModern_{timestamp}.png";

                        string picturesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

                        string filePath = Path.Combine(picturesFolder, fileName);

                        bitmap.Save(filePath, ImageFormat.Png);
                    }
                }
                catch { }

                Core.WinApis.SetWindowDisplayAffinity(Core.Instances.OverlayWindow.Handle, Core.WinApis.WDA_EXCLUDEFROMCAPTURE);
            });

            Core.Instances.fontManager = new Core.Font.FontManager();
            Core.Instances.fontManager.AddFont("global", Properties.Resources.ProtoMono_SemiBold);
            Core.Instances.fontManager.AddFont("title", Properties.Resources.ProtoMono_SemiBold, 15.0f);
            Core.Instances.fontManager.AddFont("title_2", Properties.Resources.ProtoMono_SemiBold, 10.0f);
            Core.Instances.fontManager.AddFont("navigation", Properties.Resources.ProtoMono_Light, 15.0f);
            Core.Instances.fontManager.AddFont("widget_title", Properties.Resources.ProtoMono_SemiBold, 15.0f);
            Core.Instances.fontManager.AddFont("widget_des", Properties.Resources.ProtoMono_Light, 10.0f);
            Core.Instances.fontManager.AddFont("widget_header", Properties.Resources.ProtoMono_SemiBold, 12.0f);

            // Initialize the ImageManager with the Direct3D device
            Core.Instances.ImageManager = new Core.Texture.ImageManager(Core.Instances.OverlayWindow.D3DDevice);

            // Configure ImGui settings
            Core.Instances.OverlayWindow.ImguiManager.IO.ConfigDebugIsDebuggerPresent = false;
            Core.Instances.OverlayWindow.ImguiManager.IO.ConfigErrorRecoveryEnableAssert = false;

            // Add images to the ImageManager
            Core.Instances.ImageManager.AddImage("atom_icon", Properties.Resources.atom_icon);
            Core.Instances.ImageManager.AddImage("aim_icon", Properties.Resources.aim_30);
            Core.Instances.ImageManager.AddImage("config_icon", Properties.Resources.config_icon2);
            Core.Instances.ImageManager.AddImage("check", Properties.Resources.check2);
            Core.Instances.ImageManager.AddImage("uncheck", Properties.Resources.uncheck2);

            //var Theme = new Themer.ThemerApplier(Core.Instances.OverlayWindow.Handle);
            //Theme.Apply(Themer.Themes.Acrylic);

            if (Core.Instances.Settings.ShowMenu == false) Core.Instances.Settings.ShowMenu = true;
            Core.Instances.OverlayWindow.Interactive(Core.Instances.OverlayMode == OverlayMode.Normal);

            views.Add(new View1() { Checked = true, Icon = Core.Instances.ImageManager.GetImage("aim_icon") });
            views.Add(new View2() { Icon = Core.Instances.ImageManager.GetImage("atom_icon") });
            views.Add(new View3() { Icon = Core.Instances.ImageManager.GetImage("atom_icon") });
            views.Add(new View4() { Icon = Core.Instances.ImageManager.GetImage("atom_icon") });

            Core.WinApis.SetWindowDisplayAffinity(Core.Instances.OverlayWindow.Handle, Core.WinApis.WDA_EXCLUDEFROMCAPTURE);

            return true;
        }

        private static bool SetChildWindow = false;
        private static bool UseCustomImguiCursor = true;
        private static bool SizeLocationSet = false;

        private static bool Render()
        {
            try
            {
                if (Core.Instances.OverlayMode != OverlayMode.Normal && Core.Instances.GameProcess.HasExited)
                {
                    SDK.ConfigManager.SaveConfig(Core.Instances.Settings);
                    Environment.Exit(0);
                }

                if (Core.Instances.OverlayMode == OverlayMode.InGameEmbed && !SetChildWindow)
                {
                    SetChildWindow = true;
                    Core.Instances.OverlayWindow.MakeOverlayChild(Core.Instances.OverlayWindow.Handle, Core.Instances.OverlayWindow.GameWindowHandle);
                }

                if (Core.Instances.OverlayMode != OverlayMode.Normal)
                {
                    if (Core.Instances.OverlayMode == OverlayMode.InGameEmbed) Core.Instances.OverlayWindow.Location = new System.Drawing.Point(0, 0);
                    Core.Instances.OverlayWindow.FitTo(Core.Instances.GameProcess.MainWindowHandle, true);
                    Core.Instances.OverlayWindow.PlaceAbove(Core.Instances.GameProcess.MainWindowHandle);
                }

                if (Core.Instances.OverlayMode == OverlayMode.InGameEmbed && UseCustomImguiCursor)
                {
                    Core.Instances.OverlayWindow.ImguiManager.IO.MouseDrawCursor = Core.Instances.Settings.ShowMenu;
                }

                if (Core.Instances.OverlayMode != OverlayMode.Normal && Core.Instances.InputImguiEmu != null)
                {
                    Core.Instances.InputImguiEmu.UpdateMouseState();
                    Core.Instances.InputImguiEmu.UpdateKeyboardState();
                }



                if (Core.Instances.Settings.ShowMenu)
                {
                    // Colores definidos
                    Vector4 backgroundColor = new Vector4(0.071f, 0.071f, 0.090f, 1.000f); // #454545

                    // Configuración del tamaño
                    Vector2 windowSize = new Vector2(Gui_Size.X, Gui_Size.Y);

                    if (Core.Instances.OverlayMode == OverlayMode.Normal) windowSize = ImGui.GetIO().DisplaySize;

                    // --- Background (sección global) ---
                    if (!SizeLocationSet)
                    {
                        if (Core.Instances.OverlayMode != OverlayMode.Normal) SizeLocationSet = true;
                        ImGui.SetNextWindowPos(new Vector2(0, 0));
                        ImGui.SetNextWindowSize(windowSize);
                    }

                    ImGui.PushStyleColor(ImGuiCol.WindowBg, backgroundColor); // Color del fondo
                    ImGui.Begin("Background", ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar);

                    // Llamamos a las secciones individuales
                    TitleBar();
                    MainSection();

                    ImGui.End(); // Fin del Background
                    ImGui.PopStyleColor();
                }

                Core.Instances.OverlayWindow.Interactive(Core.Instances.Settings.ShowMenu);

                if (Core.Instances.OverlayMode != OverlayMode.Normal)
                {
                    if (Core.Instances.Cheat == null) { Core.Instances.Cheat = new SDK.Cheat(); } else { bool result = Core.Instances.Cheat.Update(); }
                }

                return true;

            }
            catch { return false; }

        }

        /// <summary>
        /// Renderiza la barra del título y habilita el drag solo cuando el cursor está sobre ella.
        /// </summary>
        private static void TitleBar()
        {
            Vector4 titleBarColor = new Vector4(0.043f, 0.047f, 0.059f, 1.0f); // #0B0C0F
            float titleBarHeight = 50.0f;
            Vector2 windowSize = new Vector2(Gui_Size.X, Gui_Size.Y);

            if (Core.Instances.OverlayMode == OverlayMode.Normal) windowSize = ImGui.GetIO().DisplaySize;

            // --- Barra del Título ---
            ImGui.SetCursorPos(new Vector2(0, 0)); // Coloca la posición inicial
            ImGui.PushStyleColor(ImGuiCol.ChildBg, titleBarColor); // Color de la barra
            ImGui.BeginChild("TitleBar", new Vector2(windowSize.X, titleBarHeight), ImGuiWindowFlags.NoScrollbar);

            // Centramos el texto verticalmente
            float textHeight = ImGui.CalcTextSize("SUBSTANCE 2.0").Y;
            float verticalOffset = (titleBarHeight - textHeight) * 0.5f;

            // Texto Izquierdo: SUBSTANCE 2.0 con sangría
            ImGui.SetCursorPos(new Vector2(15, verticalOffset)); // Sangría de 15 píxeles
                                                                 //ImGui.PushFont(ImGui.GetFont()); 
            ImGui.PushFont(Core.Instances.fontManager.GetFont("title"));
            ImGui.TextColored(new Vector4(1, 1, 1, 1), "SUBSTANCE 2.0"); // Blanco
            ImGui.PopFont();
            // Texto Derecho: Past Owl [DEV]
            ImGui.PushFont(Core.Instances.fontManager.GetFont("title_2"));
            ImGui.SameLine(windowSize.X - 150); // Posición derecha
            ImGui.SetCursorPosY(verticalOffset);
            ImGui.TextColored(new Vector4(1, 1, 1, 1), "Past Owl");
            ImGui.SameLine();
            ImGui.TextColored(new Vector4(1.0f, 0.5f, 0.5f, 1.0f), "[DEV]"); // Rojo salmón
            ImGui.PopFont();
            ImGui.EndChild();
            ImGui.PopStyleColor();

            if (Core.Instances.OverlayMode == OverlayMode.Normal)
            {
                // --- Verificar si el cursor está en el área de la barra de título ---
                Vector2 mousePos = ImGui.GetMousePos(); // Posición absoluta del mouse
                Vector2 titleBarMin = new Vector2(0, 0); // Esquina superior izquierda de la barra
                Vector2 titleBarMax = new Vector2(windowSize.X, titleBarHeight); // Esquina inferior derecha de la barra

                // Calcular si el mouse está dentro del área de la barra
                bool isMouseOverTitleBar =
                    mousePos.X >= titleBarMin.X && mousePos.X <= titleBarMax.X &&
                    mousePos.Y >= titleBarMin.Y && mousePos.Y <= titleBarMax.Y;

                // Actualizar EnableDrag solo si el cursor está sobre la barra de título
                bool IsFocusOnMainImguiWindow = (Form.ActiveForm == Core.Instances.OverlayWindow);
                Core.Instances.OverlayWindow.EnableDrag = isMouseOverTitleBar && IsFocusOnMainImguiWindow;
            }
        }

        /// <summary>
        /// Renderiza la sección principal con sistema de navegación.
        /// </summary>
        private static void MainSection()
        {
            float titleBarHeight = 50.0f;
            Vector2 windowSize = new Vector2(Gui_Size.X, Gui_Size.Y);

            if (Core.Instances.OverlayMode == OverlayMode.Normal) windowSize = ImGui.GetIO().DisplaySize;

            // --- Sección Principal ---
            ImGui.SetCursorPos(new Vector2(0, titleBarHeight));
            ImGui.BeginChild("MainSection", new Vector2(windowSize.X, windowSize.Y - titleBarHeight), ImGuiWindowFlags.NoScrollbar);

            float navigationWidth = 200.0f;
            float viewsWidth = (windowSize.X - navigationWidth);

            // --- Sección Izquierda: Navegación ---
            NavigationSection(navigationWidth);

            ImGui.SameLine();

            // --- Sección Derecha: Vista Activa ---
            ImGui.BeginChild("ViewSection", new Vector2(viewsWidth - 20, 0));

            var firstCheckedView = views.FirstOrDefault(v => v.Checked);
            if (firstCheckedView != null)
            {
                firstCheckedView.Render();
            }
            else
            {
                ImGui.Text("Vista no encontrada.");
            }

            ImGui.EndChild();

            ImGui.EndChild();
        }


        private static List<IView> views = new List<IView>();

        /// <summary>
        /// Renderiza la sección de navegación (botones) y actualiza la vista activa.
        /// </summary>
        private static void NavigationSection(float width)
        {
            Vector4 navigationBgColor = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            float marginX = 20.0f; // Margen horizontal
            float marginTop = 10.0f; // Margen superior


            ImGui.PushStyleColor(ImGuiCol.ChildBg, navigationBgColor);
            ImGui.BeginChild("NavigationSection", new Vector2(width, 0));
            ImGui.PushFont(Core.Instances.fontManager.GetFont("navigation"));
            // Configurar margen superior e izquierdo
            ImGui.SetCursorPos(new Vector2(marginX, marginTop));

            // Tamaño de los botones
            float buttonWidth = width - 2 * marginX;
            Vector2 buttonSize = new Vector2(buttonWidth, 45);

            foreach (var view in views)
            {
                bool isChecked = view.Checked;
                ImGui.SetCursorPosX(marginX);
                if (ToggleButton("toggle_" + view.ID, view.Icon, view.Text, buttonSize, ref isChecked))
                {
                    view.Checked = isChecked;
                    SetActiveView(view.ID);
                }
            }

            ImGui.PopFont();
            ImGui.EndChild();
            ImGui.PopStyleColor();
        }

        /// <summary>
        /// Establece la vista activa y sincroniza el estado de los botones.
        /// </summary>
        private static void SetActiveView(string ID)
        {
            foreach (var view in views) { if (view.ID != ID) view.Checked = false; }
        }

        /// <summary>
        /// Widget personalizado ToggleButton con imagen, texto y estilo activo/inactivo.
        /// </summary>
        /// <param name="id">Identificador único del botón.</param>
        /// <param name="imageTextureID">La textura de la imagen del ícono.</param>
        /// <param name="label">El texto del botón.</param>
        /// <param name="size">Tamaño del botón.</param>
        /// <param name="isActive">Referencia al estado del botón (ON/OFF).</param>
        private static bool ToggleButton(string id, ImTextureID imageTextureID, string label, Vector2 size, ref bool isActive)
        {
            bool clicked = false;

            // Colores de los estilos
            Vector4 inactiveBgColor = new Vector4(0.153f, 0.153f, 0.200f, 1.000f); // Gris oscuro
            Vector4 activeBgColor = new Vector4(0.439f, 0.698f, 0.675f, 1.000f);   // Celeste
            Vector4 inactiveTextColor = new Vector4(1, 1, 1, 1.0f);        // Blanco
            Vector4 activeTextColor = new Vector4(1, 1, 1, 1.0f);          // Negro
            Vector4 decorationColor = new Vector4(0.153f, 0.153f, 0.200f, 1.000f); // Color oscuro para decoración
            Vector4 decorationColor2 = new Vector4(0.153f, 0.153f, 0.200f, 0.800f);

            // Capturar clics invisibles
            ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, 0.0f); // Sin bordes redondeados
            ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(0, 0));
            if (ImGui.InvisibleButton(id, size))
            {
                isActive = true;
                clicked = true;
            }
            ImGui.PopStyleVar(2);

            // Coordenadas del botón
            Vector2 buttonMin = ImGui.GetItemRectMin();
            Vector2 buttonMax = ImGui.GetItemRectMax();

            if (isActive)
            {
                buttonMax.X += 5.0f;
            }
            else
            {
                buttonMax.X -= 5.0f;
            }

            Vector2 buttonCenter = new Vector2((buttonMin.X + buttonMax.X) / 2, (buttonMin.Y + buttonMax.Y) / 2);

            ImDrawListPtr drawList = ImGui.GetWindowDrawList();

            // Fondo del botón
            drawList.AddRectFilled(buttonMin, buttonMax, ImGui.ColorConvertFloat4ToU32(isActive ? activeBgColor : inactiveBgColor));

            // Decoración si el botón está activo
            if (isActive)
            {
                // 1. Rectángulo como barra decorativa
                float rectHeight = (buttonMax.Y - buttonMin.Y) * 0.6f; // Altura del rectángulo
                float rectWidth = 5.0f; // Ancho pequeño para parecer una barra
                Vector2 rectBottomRight = new Vector2(buttonMax.X, buttonMax.Y);
                Vector2 rectTopLeft = new Vector2(buttonMax.X - rectWidth, buttonMax.Y - rectHeight);

                // Dibujar la barra
                drawList.AddRectFilled(rectTopLeft, rectBottomRight, ImGui.ColorConvertFloat4ToU32(decorationColor));

                // 2. Triángulo ligeramente más ancho que la barra
                Vector2 trianglePoint1 = new Vector2(rectTopLeft.X + 1, rectTopLeft.Y);                   // Esquina superior izquierda del rectángulo
                Vector2 trianglePoint2 = new Vector2(rectTopLeft.X + 1 + rectWidth, rectTopLeft.Y); // Extremo derecho del triángulo
                Vector2 trianglePoint3 = new Vector2(rectTopLeft.X + 1, rectTopLeft.Y - 5);             // Punto inclinado hacia arriba

                drawList.AddTriangleFilled(trianglePoint1, trianglePoint2, trianglePoint3, ImGui.ColorConvertFloat4ToU32(decorationColor));

                // --- Decoración Inferior Derecha ---

                float bottomRectWidth = 28.0f; // Ancho de la barra horizontal
                float bottomRectHeight = 5.0f; // Altura de la barra
                Vector2 bottomRectTopLeft = new Vector2(buttonMax.X - bottomRectWidth, buttonMax.Y - bottomRectHeight);
                Vector2 bottomRectBottomRight = new Vector2(buttonMax.X, buttonMax.Y);

                // Dibujar la barra inferior
                drawList.AddRectFilled(bottomRectTopLeft, bottomRectBottomRight, ImGui.ColorConvertFloat4ToU32(decorationColor));

                // Triángulo inferior derecho
                Vector2 bottomTrianglePoint1 = new Vector2(bottomRectTopLeft.X, bottomRectTopLeft.Y + 1);         // Esquina superior izquierda de la barra
                Vector2 bottomTrianglePoint2 = new Vector2(bottomRectTopLeft.X - 5, bottomRectTopLeft.Y + 1);  // Unos píxeles a la izquierda, misma altura
                Vector2 bottomTrianglePoint3 = new Vector2(bottomRectTopLeft.X, bottomRectBottomRight.Y + 1);     // Esquina inferior izquierda de la barra

                drawList.AddTriangleFilled(bottomTrianglePoint1, bottomTrianglePoint2, bottomTrianglePoint3, ImGui.ColorConvertFloat4ToU32(decorationColor));

                bottomRectWidth = 8.0f;
                bottomRectTopLeft = new Vector2(buttonMax.X - bottomRectWidth, buttonMax.Y - bottomRectHeight);

                int separation = 40;

                Vector2 bottomRectTopLeftb = new Vector2((buttonMax.X - bottomRectWidth) - separation, buttonMax.Y - bottomRectHeight);
                Vector2 bottomRectBottomRightb = new Vector2(buttonMax.X - separation, buttonMax.Y);

                // Dibujar la barra inferior
                drawList.AddRectFilled(bottomRectTopLeftb, bottomRectBottomRightb, ImGui.ColorConvertFloat4ToU32(decorationColor));

                Vector2 bottomTrianglePoint1b = new Vector2(bottomRectTopLeft.X - separation, bottomRectTopLeft.Y + 1);         // Esquina superior izquierda de la barra
                Vector2 bottomTrianglePoint2b = new Vector2((bottomRectTopLeft.X - 5) - separation, bottomRectTopLeft.Y + 1);  // Unos píxeles a la izquierda, misma altura
                Vector2 bottomTrianglePoint3b = new Vector2(bottomRectTopLeft.X - separation, bottomRectBottomRight.Y + 1);     // Esquina inferior izquierda de la barra

                drawList.AddTriangleFilled(bottomTrianglePoint1b, bottomTrianglePoint2b, bottomTrianglePoint3b, ImGui.ColorConvertFloat4ToU32(decorationColor));

                // Triángulo en el lado derecho con la hipotenusa apuntando hacia arriba
                Vector2 bottomTriangleRightPoint1b = new Vector2(bottomRectBottomRightb.X, bottomRectBottomRightb.Y + 1);       // Punto inferior derecho del rectángulo
                Vector2 bottomTriangleRightPoint2b = new Vector2(bottomRectBottomRightb.X + 5, bottomRectBottomRightb.Y + 1);   // Extiende 5px hacia la derecha, misma altura inferior
                Vector2 bottomTriangleRightPoint3b = new Vector2(bottomRectBottomRightb.X, bottomRectTopLeftb.Y + 1);           // Punto superior, creando la punta hacia arriba

                drawList.AddTriangleFilled(bottomTriangleRightPoint1b, bottomTriangleRightPoint2b, bottomTriangleRightPoint3b, ImGui.ColorConvertFloat4ToU32(decorationColor));


                bottomRectWidth = 8.0f;
                bottomRectTopLeft = new Vector2(buttonMax.X - bottomRectWidth, buttonMax.Y - bottomRectHeight);

                separation = 60;

                bottomRectTopLeftb = new Vector2((buttonMax.X - bottomRectWidth) - separation, buttonMax.Y - bottomRectHeight);
                bottomRectBottomRightb = new Vector2(buttonMax.X - separation, buttonMax.Y);

                // Dibujar la barra inferior
                drawList.AddRectFilled(bottomRectTopLeftb, bottomRectBottomRightb, ImGui.ColorConvertFloat4ToU32(decorationColor));

                bottomTrianglePoint1b = new Vector2(bottomRectTopLeft.X - separation, bottomRectTopLeft.Y + 1);         // Esquina superior izquierda de la barra
                bottomTrianglePoint2b = new Vector2((bottomRectTopLeft.X - 5) - separation, bottomRectTopLeft.Y + 1);  // Unos píxeles a la izquierda, misma altura
                bottomTrianglePoint3b = new Vector2(bottomRectTopLeft.X - separation, bottomRectBottomRight.Y + 1);     // Esquina inferior izquierda de la barra

                drawList.AddTriangleFilled(bottomTrianglePoint1b, bottomTrianglePoint2b, bottomTrianglePoint3b, ImGui.ColorConvertFloat4ToU32(decorationColor));

                // Triángulo en el lado derecho con la hipotenusa apuntando hacia arriba
                bottomTriangleRightPoint1b = new Vector2(bottomRectBottomRightb.X, bottomRectBottomRightb.Y + 1);       // Punto inferior derecho del rectángulo
                bottomTriangleRightPoint2b = new Vector2(bottomRectBottomRightb.X + 5, bottomRectBottomRightb.Y + 1);   // Extiende 5px hacia la derecha, misma altura inferior
                bottomTriangleRightPoint3b = new Vector2(bottomRectBottomRightb.X, bottomRectTopLeftb.Y + 1);           // Punto superior, creando la punta hacia arriba

                drawList.AddTriangleFilled(bottomTriangleRightPoint1b, bottomTriangleRightPoint2b, bottomTriangleRightPoint3b, ImGui.ColorConvertFloat4ToU32(decorationColor));

                bottomRectWidth = 8.0f;
                bottomRectTopLeft = new Vector2(buttonMax.X - bottomRectWidth, buttonMax.Y - bottomRectHeight);

                separation = 80;

                bottomRectTopLeftb = new Vector2((buttonMax.X - bottomRectWidth) - separation, buttonMax.Y - bottomRectHeight);
                bottomRectBottomRightb = new Vector2(buttonMax.X - separation, buttonMax.Y);

                // Dibujar la barra inferior
                drawList.AddRectFilled(bottomRectTopLeftb, bottomRectBottomRightb, ImGui.ColorConvertFloat4ToU32(decorationColor));

                bottomTrianglePoint1b = new Vector2(bottomRectTopLeft.X - separation, bottomRectTopLeft.Y + 1);         // Esquina superior izquierda de la barra
                bottomTrianglePoint2b = new Vector2((bottomRectTopLeft.X - 5) - separation, bottomRectTopLeft.Y + 1);  // Unos píxeles a la izquierda, misma altura
                bottomTrianglePoint3b = new Vector2(bottomRectTopLeft.X - separation, bottomRectBottomRight.Y + 1);     // Esquina inferior izquierda de la barra

                drawList.AddTriangleFilled(bottomTrianglePoint1b, bottomTrianglePoint2b, bottomTrianglePoint3b, ImGui.ColorConvertFloat4ToU32(decorationColor));

                // Triángulo en el lado derecho con la hipotenusa apuntando hacia arriba
                bottomTriangleRightPoint1b = new Vector2(bottomRectBottomRightb.X, bottomRectBottomRightb.Y + 1);       // Punto inferior derecho del rectángulo
                bottomTriangleRightPoint2b = new Vector2(bottomRectBottomRightb.X + 5, bottomRectBottomRightb.Y + 1);   // Extiende 5px hacia la derecha, misma altura inferior
                bottomTriangleRightPoint3b = new Vector2(bottomRectBottomRightb.X, bottomRectTopLeftb.Y + 1);           // Punto superior, creando la punta hacia arriba

                drawList.AddTriangleFilled(bottomTriangleRightPoint1b, bottomTriangleRightPoint2b, bottomTriangleRightPoint3b, ImGui.ColorConvertFloat4ToU32(decorationColor));



                float bottomRectWidthc = 89.0f; // Ancho de la barra horizontal
                float bottomRectHeightc = 5.0f; // Altura de la barra
                Vector2 bottomRectTopLeftc = new Vector2((buttonMax.X - bottomRectWidthc) - 7, (buttonMax.Y - bottomRectHeightc) - 7);
                Vector2 bottomRectBottomRightc = new Vector2(buttonMax.X - 7, buttonMax.Y - 7);

                // Dibujar la barra inferior
                drawList.AddRectFilled(bottomRectTopLeftc, bottomRectBottomRightc, ImGui.ColorConvertFloat4ToU32(decorationColor2));

                // Triángulo inferior derecho
                Vector2 bottomTrianglePoint1c = new Vector2(bottomRectTopLeftc.X - 5, bottomRectTopLeftc.Y);         // Esquina superior izquierda de la barra
                Vector2 bottomTrianglePoint2c = new Vector2(bottomRectTopLeftc.X, bottomRectTopLeftc.Y);  // Unos píxeles a la izquierda, misma altura
                Vector2 bottomTrianglePoint3c = new Vector2(bottomRectTopLeftc.X, bottomRectBottomRightc.Y);     // Esquina inferior izquierda de la barra

                drawList.AddTriangleFilled(bottomTrianglePoint1c, bottomTrianglePoint2c, bottomTrianglePoint3c, ImGui.ColorConvertFloat4ToU32(decorationColor2));

            }

            // Imagen del botón (ícono)
            float imageHeight = size.Y * 0.4f;
            Vector2 imageSize = new Vector2(imageHeight, imageHeight);
            Vector2 imagePos = new Vector2(buttonMin.X + 10, buttonCenter.Y - (imageSize.Y / 2));
            drawList.AddImage(imageTextureID, imagePos, new Vector2(imagePos.X + imageSize.X, imagePos.Y + imageSize.Y));

            // Texto centrado verticalmente, a la derecha del ícono
            Vector2 textSize = ImGui.CalcTextSize(label);
            Vector2 textPos = new Vector2(imagePos.X + imageSize.X + 10, buttonCenter.Y - (textSize.Y / 2));
            drawList.AddText(textPos, ImGui.ColorConvertFloat4ToU32(isActive ? activeTextColor : inactiveTextColor), label);

            return clicked;
        }

    }
}
