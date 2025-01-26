using EasyImGui;
using EasyImGui.Core;
using EasyModern.Core;
using EasyModern.Core.Model;
using EasyModern.Core.Utils;
using EasyModern.UI.Particles;
using EasyModern.UI.Themes;
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


            Console.WriteLine(Environment.NewLine);
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;

            //if (Core.Instances.Settings.PEMutate)
            //{
            //    Core.Instances.Settings.PEMutate = false;
            //    SDK.ConfigManager.SaveConfig(Core.Instances.Settings);
            //    //Core.Utils.PEMutator.CheckHash(Application.ExecutablePath);
            //}
            //else
            //{
            //    Core.Instances.Settings.PEMutate = true;
            //    SDK.ConfigManager.SaveConfig(Core.Instances.Settings);
            //    if (Core.Utils.PEMutator.MutatePE())
            //        Environment.Exit(0);
            //}

            //bool InProcess = false;

            //Console.WriteLine("Process Execution Mode:");
            //Console.WriteLine("1) Normal.");
            //Console.WriteLine("2) Process Embed.");
            //char sInput = Console.ReadKey().KeyChar;
            //Console.WriteLine("");
            //switch (sInput)
            //{
            //    case '1':
            //        InProcess = false;
            //        break;
            //    case '2':
            //        InProcess = true;
            //        break;
            //    default:
            //        break;
            //}

            //if (InProcess)
            //{

            //    //Process process =   Process.GetProcessesByName("bf4").FirstOrDefault(); 
            //    //VNX.RemoteControl Control = new VNX.RemoteControl(process);

            //    Process process;

            //    VNX.RemoteControl Control = new VNX.RemoteControl("calc", out process);

            //    bool Compatible = Control.IsCompatibleProcess();

            //    Console.WriteLine(process.Is64Bits() ? "x64 Detected" : "x86 Detected");
            //    Console.WriteLine($"The process {(Compatible ? "is" : "isn't")} compatible with this build.");

            //    Control.WaitInitialize();

            //    if (Compatible)
            //    {
            //        InjecInProc(Control, process);
            //        return;
            //    }
            //    else
            //    {
            //        Console.WriteLine("Impossible to Inject, Continuing externally.  Press any key to continue...");
            //    }

            //    Console.ReadKey();

            //}
            //else
            //{
            StartOverlay();
            //}

        }

        public static bool LoadInto = false;
        public static void InjecInProc(RemoteControl Control, Process Process)
        {
            Control.LockEntryPoint();

            string CurrentAssembly = Assembly.GetExecutingAssembly().Location;

            int Ret = Control.CLRInvoke(CurrentAssembly, Process.GetCurrentProcess().Id.ToString()); // Control.CLRInvoke(CurrentAssembly, typeof(Program).FullName, "EntryPoint", Process.GetCurrentProcess().Id.ToString());

            Control.UnlockEntryPoint();

            Environment.Exit(0);
        }


        public static int EntryPoint(string Arg)
        {
            Process.GetProcessById(int.Parse(Arg))?.Kill();
            Core.WinApis.AllocConsole();
            StartOverlay();

            return int.MaxValue;
        }

        public static Vector2 Gui_Size = new Vector2(1090, 550);

        public static void StartOverlay()
        {
            WindowFinder finder = new WindowFinder();

            finder.OnProcReady += (sender, status, processId) =>
            {
                OverlayMode GameOverlayMode = OverlayMode.InGameEmbed;

                if (status)
                {
                    Console.WriteLine("Select Overlay Mode (Press a key):");
                    Console.WriteLine("1) In-Game Embed (Run the Overlay In-Game)");
                    Console.WriteLine("2) In-Game (Run the Overlay Over the game)");
                    char sInput = Console.ReadKey().KeyChar;
                    Console.WriteLine("");
                    switch (sInput)
                    {
                        case '1':
                            GameOverlayMode = OverlayMode.InGameEmbed;
                            break;
                        case '2':
                            GameOverlayMode = OverlayMode.InGame;
                            break;
                        default:
                            break;
                    }
                }

                Core.Instances.OverlayMode = status ? GameOverlayMode : OverlayMode.Normal;

                // Initialize the Overlay window with desired properties
                Core.Instances.OverlayWindow = new Overlay() { ResizableBorders = true, ShowInTaskbar = (Core.Instances.OverlayMode == OverlayMode.Normal), TopMost = false };
                Core.Instances.OverlayWindow.Text = "";
                Core.Instances.OverlayWindow.Icon = Core.Utils.Helper.ConvertToIco(Properties.Resources.badge, 16);
                Core.Instances.OverlayWindow.TransparencyKey = Color.Black;
                Core.Instances.OverlayWindow.BackColor = Color.Black;
                Core.Instances.OverlayWindow.ClearColor = new SharpDX.Color(0, 0, 0, 0);
                Core.Instances.OverlayWindow.AdditionalExStyle = 0x80000 | 0x00000080; // WS_EX_LAYERED + WS_EX_TOOLWINDOW 

                if (Core.Instances.OverlayMode != OverlayMode.Normal)
                {
                    Core.Instances.OverlayWindow.AdditionalExStyle |= 0x08000000; // WS_EX_NOACTIVATE 
                }

                Core.Instances.OverlayWindow.PresentParams = new SharpDX.Direct3D9.PresentParameters
                {
                    Windowed = true,
                    SwapEffect = SharpDX.Direct3D9.SwapEffect.Discard,
                    BackBufferFormat = SharpDX.Direct3D9.Format.A8R8G8B8,
                    PresentationInterval = SharpDX.Direct3D9.PresentInterval.Immediate,
                    BackBufferWidth = (int)Gui_Size.X,
                    BackBufferHeight = (int)Gui_Size.Y,
                    EnableAutoDepthStencil = true,
                    AutoDepthStencilFormat = SharpDX.Direct3D9.Format.D16,
                };

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
        private static void LoadThemes()
        {
            Core.Instances.Theme = new Themer.ThemerApplier(Core.Instances.OverlayWindow.Handle);

            Core.Instances.Themes = new List<ITheme>();
            Core.Instances.Themes.Add(new AdobeInspired());
            Core.Instances.Themes.Add(new BattlefieldTheme());
            Core.Instances.Themes.Add(new BHTheme());
            Core.Instances.Themes.Add(new BlueTheme());
            Core.Instances.Themes.Add(new BootstrapDark());
            Core.Instances.Themes.Add(new BreathingTheme());
            Core.Instances.Themes.Add(new Cherry());
            Core.Instances.Themes.Add(new CleanDark());
            Core.Instances.Themes.Add(new Codz());
            Core.Instances.Themes.Add(new Comfy());
            Core.Instances.Themes.Add(new CommunistRed());
            Core.Instances.Themes.Add(new CustomRedTheme());
            Core.Instances.Themes.Add(new Cyberpunk());
            Core.Instances.Themes.Add(new DarkDefault());
            Core.Instances.Themes.Add(new DarkGreen());
            Core.Instances.Themes.Add(new DarkRuda());
            Core.Instances.Themes.Add(new Darky());
            Core.Instances.Themes.Add(new DayNightTheme());
            Core.Instances.Themes.Add(new DeepDark());
            Core.Instances.Themes.Add(new DefaultTheme());
            Core.Instances.Themes.Add(new DesortDarkBlueTheme());
            Core.Instances.Themes.Add(new Discord());
            Core.Instances.Themes.Add(new Dracula());
            Core.Instances.Themes.Add(new DUCKRED());
            Core.Instances.Themes.Add(new Everforest());
            Core.Instances.Themes.Add(new FluentTheme());
            Core.Instances.Themes.Add(new FutureDark());
            Core.Instances.Themes.Add(new Glass());
            Core.Instances.Themes.Add(new Gold());
            Core.Instances.Themes.Add(new GrayStyleTheme());
            Core.Instances.Themes.Add(new GruvboxDark());
            Core.Instances.Themes.Add(new GruvboxDayNightTheme());
            Core.Instances.Themes.Add(new GruvboxLight());
            Core.Instances.Themes.Add(new HackerTheme());
            Core.Instances.Themes.Add(new HazyDark());
            Core.Instances.Themes.Add(new HighContrast());
            Core.Instances.Themes.Add(new Light());
            Core.Instances.Themes.Add(new LightClean());
            Core.Instances.Themes.Add(new MaterialDesign());
            Core.Instances.Themes.Add(new Modern());
            Core.Instances.Themes.Add(new Moonlight());
            Core.Instances.Themes.Add(new Nord());
            Core.Instances.Themes.Add(new Photoshop());
            Core.Instances.Themes.Add(new Purple());
            Core.Instances.Themes.Add(new QuickMinimal());
            Core.Instances.Themes.Add(new RainbowTheme());
            Core.Instances.Themes.Add(new RayTeak());
            Core.Instances.Themes.Add(new RayTeakTransparent());
            Core.Instances.Themes.Add(new RedDark());
            Core.Instances.Themes.Add(new RedDarkTheme());
            Core.Instances.Themes.Add(new Rest());
            Core.Instances.Themes.Add(new Retro80sNeonTheme());
            Core.Instances.Themes.Add(new ShivaAnimatedTheme());
            Core.Instances.Themes.Add(new SonicRiders());
            Core.Instances.Themes.Add(new Tuke());
            Core.Instances.Themes.Add(new Unreal());
            Core.Instances.Themes.Add(new Valve());
            Core.Instances.Themes.Add(new Windark());

            int indexToCheck = Core.Instances.Settings.Theme_ID;

            if (indexToCheck >= 0 && indexToCheck < Core.Instances.Themes.Count)
            {
                Core.Instances.Themes[indexToCheck].Apply();
            }

        }

        private static void DisabledINI()
        {
            try
            {
                unsafe
                {
                    ImGuiIOPtr io = ImGui.GetIO();
                    byte* IniFile = null;
                    io.IniFilename = IniFile;
                }
            }
            catch { }
        }

        private static bool OnConfigContex()
        {
            DisabledINI();

            LoadMode();

            Hexa.NET.ImGui.Backends.Win32.ImGuiImplWin32.EnableAlphaCompositing(Core.Instances.OverlayWindow.Handle);
            Core.WinApis.EnableTransparency(Core.Instances.OverlayWindow.Handle, 255); // Transparencia total

            LoadThemes();

            // Initialize InputImguiEmu for handling input
            Core.Instances.InputImguiEmu = new Core.Input.InputImguiEmu(Core.Instances.OverlayWindow.ImguiManager.IO);

            Core.Instances.InputImguiEmu.AddEvent(Keys.Insert, () =>
            {
                Core.Instances.Settings.ShowMenu = !Core.Instances.Settings.ShowMenu;
                if (Core.Instances.Settings.ShowMenu)
                {
                    Core.Instances.Theme.Apply(Themer.Themes.Acrylic);
                }
                else
                {
                    SDK.ConfigManager.SaveConfig(Core.Instances.Settings);
                    Core.Instances.Theme.Apply(Themer.Themes.None);
                }

            });

            Core.Instances.InputImguiEmu.AddEvent(Keys.Escape, () =>
            {
                if (Core.Instances.Settings.ShowMenu)
                {
                    Core.Instances.Settings.ShowMenu = false;
                    SDK.ConfigManager.SaveConfig(Core.Instances.Settings);
                    Core.Instances.Theme.Apply(Themer.Themes.None);
                }
            });

            Core.Instances.InputImguiEmu.AddEvent(Keys.F3, () =>
            {
                SDK.ConfigManager.SaveConfig(Core.Instances.Settings);
                Environment.Exit(0);
            });

            Core.Instances.InputImguiEmu.AddEvent(Keys.F1, () =>
            {
                Core.Instances.Settings.InGameEffects = !Core.Instances.Settings.InGameEffects;

                if (Core.Instances.TextureEffectManager != null)
                {
                    if (Core.Instances.Settings.InGameEffects)
                    {
                        Core.Instances.TextureEffectManager.Start();
                    }
                    else
                    {
                        Core.Instances.TextureEffectManager.Stop();
                    }
                }

                SDK.ConfigManager.SaveConfig(Core.Instances.Settings);
            });

            Core.Instances.InputImguiEmu.AddEvent(Keys.F2, () =>
            {
                Core.WinApis.SetWindowDisplayAffinity(Core.Instances.OverlayWindow.Handle, Core.WinApis.WDA_NONE);
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
                        string fileName = $"SS_EasyModern_BF4_{timestamp}.png";

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
            Core.Instances.ImageManager.AddImage("app_logo", Properties.Resources.badge);

            if (Core.Instances.Settings.ShowMenu == false) Core.Instances.Settings.ShowMenu = true;
            Core.Instances.OverlayWindow.Interactive(Core.Instances.OverlayMode == OverlayMode.Normal);

            views.Add(new View1() { Checked = true, Icon = Core.Instances.ImageManager.GetImage("aim_icon") });
            views.Add(new View2() { Icon = Core.Instances.ImageManager.GetImage("atom_icon") });
            views.Add(new View3() { Icon = Core.Instances.ImageManager.GetImage("atom_icon") });
            views.Add(new View4() { Icon = Core.Instances.ImageManager.GetImage("atom_icon") });


            //Core.Instances.TextureDrawing = new Core.Drawing.TextureDrawing(Core.Instances.OverlayWindow.D3DDevice);

            //UltraFastParallelFakeHDREffect hdrEffect = new UltraFastParallelFakeHDREffect(intensity: 1.2f);

            //Battlefield4Effect battlefieldEffect = new Battlefield4Effect(Core.Instances.OverlayWindow.Width, Core.Instances.OverlayWindow.Height)
            //{
            //    BulletSpeed = 50f,       // Velocidad de las balas
            //    BulletLength = 50f,     // Longitud de los tubos luminosos
            //    BulletThickness = 5f,   // Grosor de los tubos luminosos
            //    MaxBullets = 100           // Máximo número de balas
            //};

            //Func<Bitmap, Bitmap> BitmapEffect = (Bitmap bmp) =>
            //{
            //    if (Core.Instances.OverlayMode != OverlayMode.Normal && Core.Instances.Settings.ShowMenu)
            //    {
            //        Core.Effects.ImageEffect.ApplyBattlefield4Effect(bmp, Color.FromArgb(255, 255, 165, 0), 1.0f, 50);
            //        battlefieldEffect.ApplyEffect(bmp);
            //    }
            //    else
            //    {
            //        hdrEffect.ApplyEffect(bmp);
            //    }

            //    return bmp;
            //};

            //Core.Instances.TextureEffectManager = new TextureEffectManager(BitmapEffect);
            //Core.Instances.TextureEffectManager.Start();

            if (Core.Instances.OverlayMode != OverlayMode.Normal)
            {
                Core.WinApis.SetWindowDisplayAffinity(Core.Instances.OverlayWindow.Handle, Core.WinApis.WDA_EXCLUDEFROMCAPTURE);
                Console.Clear();
            }
            else
            {
                Core.Instances.OverlayWindow.Focus();
            }


            if (Core.Instances.Settings.ShowMenu)
            {
                Core.Instances.Theme.Apply(Themer.Themes.Acrylic);
            }


            return true;
        }

        public static bool OnChangeTheme = false;

        public static void InvalidateGUI()
        {
            OnChangeTheme = true;
            Core.Instances.OverlayWindow.ImguiManager.Render -= Render;

            SetChildWindow = false;
            UseCustomImguiCursor = true;
            //SizeLocationSet = false;
            views.Clear();
            views.Add(new View1() { Icon = Core.Instances.ImageManager.GetImage("aim_icon") });
            views.Add(new View2() { Icon = Core.Instances.ImageManager.GetImage("atom_icon") });
            views.Add(new View3() { Checked = true, Icon = Core.Instances.ImageManager.GetImage("atom_icon") });
            views.Add(new View4() { Icon = Core.Instances.ImageManager.GetImage("atom_icon") });

            Core.Instances.OverlayWindow.ImguiManager.Render += Render;
            OnChangeTheme = false;
        }

        private static bool SetChildWindow = false;
        private static bool UseCustomImguiCursor = true;
        private static bool SizeLocationSet = false;

        private static string _username = "Destroyer"; // Core.Instances.Settings.AccountName;

        private static bool Render()
        {

            try
            {

                if (OnChangeTheme) return false;

                //if (Core.Instances.TextureEffectManager != null)
                //{
                //    SharpDX.Direct3D9.Texture currentTexture = Core.Instances.TextureEffectManager.Texture;
                //    if (Core.Instances.TextureDrawing != null && currentTexture != null)
                //    {
                //        Core.Instances.TextureDrawing.DrawTexture(
                //            currentTexture,
                //            new Rectangle(0, 0, Core.Instances.OverlayWindow.Width, Core.Instances.OverlayWindow.Height),
                //            1.0f
                //        );

                //    }
                //}

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
                }

                if (Core.Instances.InputImguiEmu != null)
                {
                    Core.Instances.InputImguiEmu.Enabled = (Core.Instances.OverlayMode == OverlayMode.InGame);
                    Core.Instances.InputImguiEmu.UpdateKeyboardState();
                }

                if (Core.Instances.Settings.RGB_Color || Core.Instances.Settings.RGB_Crosshair_Color)
                {
                    Core.Instances.RGBColors.Update(ImGui.GetIO().DeltaTime);
                }

                if (Core.Instances.Settings.ShowMenu)
                {

                    int indexToCheck = Core.Instances.Settings.Theme_ID;

                    if (indexToCheck >= 0 && indexToCheck < Core.Instances.Themes.Count)
                    {
                        Core.Instances.Themes[indexToCheck].Apply();
                    }

                    Vector2 windowSize = new Vector2(Gui_Size.X, Gui_Size.Y);

                    if (Core.Instances.OverlayMode == OverlayMode.Normal) windowSize = ImGui.GetIO().DisplaySize;

                    // --- Background (sección global) ---
                    if (!SizeLocationSet)
                    {
                        Vector2 Pos = new Vector2(0, 0);
                        if (Core.Instances.OverlayMode != OverlayMode.Normal)
                        {
                            Pos = new Vector2(
                                                    (Core.Instances.OverlayWindow.Width / 2) - ((int)Gui_Size.X / 2),
                                                    (Core.Instances.OverlayWindow.Height / 2 - ((int)Gui_Size.Y / 2))
                                                );
                            SizeLocationSet = true;
                        }

                        ImGui.SetNextWindowPos(Pos);
                        ImGui.SetNextWindowSize(windowSize);
                    }

                    ImGui.Begin("Background", ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar);

                    TitleBar();
                    MainSection();

                    RenderParticles();

                    ImGui.End();

                }

                Core.Instances.OverlayWindow.Interactive(Core.Instances.Settings.ShowMenu);

                if (Core.Instances.OverlayMode != OverlayMode.Normal)
                {
                    if (Core.Instances.Cheat == null)
                    {
                        Core.Instances.Cheat = new SDK.Cheat();
                    }
                    else
                    {
                        bool result = Core.Instances.Cheat.Update();
                    }

                    if (Core.Instances.Settings.Draw_FPS)
                        RenderFPSOverlay();
                }


                return true;

            }
            catch { return false; }


        }


        //private static ParticleSystemMatrix matrixEffect = new ParticleSystemMatrix(1920, 1080);
        //private static ParticleSystemChristmas christmasEffect = new ParticleSystemChristmas(1920, 1080);
        private static ParticleSystemNodes nodeEffect = new ParticleSystemNodes(1920, 1080);
        //private static FireParticleSystem fireParticleSystem = new FireParticleSystem();
        //private static StormParticleSystem stormParticleSystem = new StormParticleSystem();
        //private static GalaxyParticleSystem galaxyParticleSystem = new GalaxyParticleSystem(new Vector2(1920, 1080));
        //private static ExplosionParticleSystem explosionSystem = new ExplosionParticleSystem(new Vector2(1920, 1080));
        private static OscillatingParticles particles = new OscillatingParticles();



        private static void RenderParticles()
        {
            float deltaTime = ImGui.GetIO().DeltaTime;


            if (Core.Instances.Settings.RGB_Color)
            {
                //RGBColors.Alpha = 0.5f;

                ImGui.GetStyle().Colors[(int)ImGuiCol.Border] = Core.Instances.RGBColors.CurrentColor;
            }

            ImDrawListPtr DrawList = new ImDrawListPtr();

            if (Core.Instances.Settings.Particles_Draw_Mode == 0)
            {
                DrawList = ImGuiP.GetForegroundDrawList();
            }
            else if (Core.Instances.Settings.Particles_Draw_Mode == 1)
            {
                DrawList = ImGui.GetBackgroundDrawList();
            }
            else if (Core.Instances.Settings.Particles_Draw_Mode == 2)
            {
                DrawList = ImGui.GetWindowDrawList();
            }

            if (Core.Instances.Settings.NodeParticles && !DrawList.IsNull)
            {
                nodeEffect.Update(deltaTime, Core.Instances.OverlayWindow.Width, Core.Instances.OverlayWindow.Height);
                nodeEffect.Render(DrawList, Core.Instances.OverlayWindow.Width, Core.Instances.OverlayWindow.Height);
            }

            //particles.Color = RGBColors.CurrentColor;
            //particles.UpdateParticles(DrawList, new Vector2(Core.Instances.OverlayWindow.Width, Core.Instances.OverlayWindow.Height), 0.5f);

            //christmasEffect.Update(ImGui.GetIO().DeltaTime, Core.Instances.OverlayWindow.Width, Core.Instances.OverlayWindow.Height);
            //christmasEffect.Render(ForegroundDrawList, Core.Instances.OverlayWindow.Width, Core.Instances.OverlayWindow.Height);

            //Vector2 emitterPosition = new Vector2(ImGui.GetIO().MousePos.X, ImGui.GetIO().MousePos.Y);

            //fireParticleSystem.Update(ImGui.GetIO().DeltaTime, emitterPosition); 
            //fireParticleSystem.Render(ForegroundDrawList);

            //Vector2 screenSize = ImGui.GetIO().DisplaySize;
            //stormParticleSystem.Update(deltaTime, screenSize);
            //stormParticleSystem.Render(ForegroundDrawList, screenSize);

            //Vector2 center = new Vector2(ImGui.GetIO().DisplaySize.X / 2, ImGui.GetIO().DisplaySize.Y / 2);

            //galaxyParticleSystem.Update(deltaTime, center);
            //galaxyParticleSystem.Render(ForegroundDrawList);

            //Vector2 dynamicCenter = new Vector2(ImGui.GetIO().MousePos.X, ImGui.GetIO().MousePos.Y);
            //explosionSystem.Center = dynamicCenter;
            //if (Core.Instances.InputImguiEmu.IsKeyDown(Keys.LButton))
            //{
            //    explosionSystem.TriggerExplosion(dynamicCenter);
            //}
            //explosionSystem.Update(deltaTime);
            //explosionSystem.Render(ImGui.GetBackgroundDrawList());


        }

        private static void RenderFPSOverlay()
        {

            float fps = ImGui.GetIO().Framerate;
            //if (Core.Instances.Settings.InGameEffects) fps = Core.Instances.TextureEffectManager.FPS;

            // Centrar el texto en el overlay
            string TextOverlay = $"Overlay FPS: {fps:F1}";

            Vector2 textSize = ImGui.CalcTextSize(TextOverlay);
            float overlayWidth = textSize.X + 20;
            float overlayHeight = 30.0f;

            Vector2 overlayPosition = new Vector2(Core.Instances.OverlayWindow.Width / 2 - overlayWidth / 2, 10); // Posición del overlay
            float rounding = 15.0f; // Bordes redondeados



            // Determinar el color según los FPS (verde -> amarillo -> rojo)
            Vector4 fpsColor;
            if (fps >= 60.0f)
            {
                fpsColor = new Vector4(0.0f, 1.0f, 0.0f, 1.0f); // Verde
            }
            else if (fps >= 30.0f)
            {
                fpsColor = new Vector4(1.0f, 1.0f, 0.0f, 1.0f); // Amarillo
            }
            else
            {
                fpsColor = new Vector4(1.0f, 0.0f, 0.0f, 1.0f); // Rojo
            }

            var drawList = ImGui.GetBackgroundDrawList();

            Vector2 overlayMin = overlayPosition;
            Vector2 overlayMax = overlayPosition + new Vector2(overlayWidth, overlayHeight);
            drawList.AddRectFilled(overlayMin, overlayMax, ImGui.ColorConvertFloat4ToU32(new Vector4(0.071f, 0.071f, 0.090f, 0.500f)), rounding);
            drawList.AddRect(overlayMin, overlayMax, ImGui.ColorConvertFloat4ToU32(new Vector4(1.0f, 1.0f, 1.0f, 1.0f)), rounding, 1f); // Borde blanco de 1 píxel

            // --- Renderizar el contenido del overlay ---
            ImGui.SetNextWindowPos(overlayPosition);
            ImGui.SetNextWindowSize(new Vector2(overlayWidth, overlayHeight));
            ImGui.Begin("FPS Overlay", ImGuiWindowFlags.NoDecoration | ImGuiWindowFlags.NoBackground | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoScrollbar);


            Vector2 textPos = new Vector2((overlayWidth - textSize.X) / 2, (overlayHeight - textSize.Y) / 2);
            ImGui.SetCursorPos(textPos);
            ImGui.TextColored(fpsColor, TextOverlay);

            ImGui.End();
        }





        /// <summary>
        /// Renderiza la barra del título y habilita el drag solo cuando el cursor está sobre ella.
        /// </summary>
        private static void TitleBar()
        {
            //Vector4 titleBarColor = new Vector4(0.043f, 0.047f, 0.059f, 1.0f); // #0B0C0F


            float titleBarHeight = 50.0f;
            Vector2 windowSize = new Vector2(Gui_Size.X, Gui_Size.Y);

            if (Core.Instances.OverlayMode == OverlayMode.Normal) windowSize = ImGui.GetIO().DisplaySize;

            // --- Barra del Título ---
            ImGui.SetCursorPos(new Vector2(0, 0)); // Coloca la posición inicial
            ImGui.PushStyleColor(ImGuiCol.ChildBg, ImGui.GetStyle().Colors[(int)ImGuiCol.TitleBg]); // Color de la barra
            ImGui.BeginChild("TitleBar", new Vector2(windowSize.X, titleBarHeight), ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.NoScrollbar);

            // Centramos el texto verticalmente
            float textHeight = ImGui.CalcTextSize("EasyModern 1.0.4").Y;
            float verticalOffset = (titleBarHeight - textHeight) * 0.5f;

            ImGui.SetCursorPos(new Vector2(15, verticalOffset)); // Sangría de 15 píxeles
                                                                 //ImGui.PushFont(ImGui.GetFont()); 
            ImGui.PushFont(Core.Instances.fontManager.GetFont("title"));
            ImGui.TextColored(ImGui.GetStyle().Colors[(int)ImGuiCol.Text], "EasyModern 1.0.4"); // Blanco
            ImGui.PopFont();


            ImGui.PushFont(Core.Instances.fontManager.GetFont("title_2"));
            ImGui.SameLine(windowSize.X - 200); // Posición derecha
            ImGui.SetCursorPosY(verticalOffset);
            ImGui.TextColored(ImGui.GetStyle().Colors[(int)ImGuiCol.Text], _username);
            ImGui.SameLine();
            ImGui.TextColored(new Vector4(1.0f, 0.5f, 0.5f, 1.0f), $"Days: 5");
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
            ImGui.BeginChild("MainSection", new Vector2(windowSize.X, windowSize.Y - titleBarHeight), ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.NoScrollbar);

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
            float marginX = 20.0f; // Margen horizontal
            float marginTop = 10.0f; // Margen superior


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
            //Vector4 inactiveBgColor = new Vector4(0.153f, 0.153f, 0.200f, 1.000f); // Gris oscuro
            //Vector4 activeBgColor = new Vector4(0.439f, 0.698f, 0.675f, 1.000f);   // Celeste
            //Vector4 inactiveTextColor = new Vector4(1, 1, 1, 1.0f);        // Blanco
            //Vector4 activeTextColor = new Vector4(1, 1, 1, 1.0f);          // Negro
            //Vector4 decorationColor = new Vector4(0.153f, 0.153f, 0.200f, 1.000f); // Color oscuro para decoración
            //Vector4 decorationColor2 = new Vector4(0.153f, 0.153f, 0.200f, 0.800f);

            Vector4 inactiveBgColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Button];
            Vector4 activeBgColor = ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonActive];
            Vector4 inactiveTextColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text];
            Vector4 activeTextColor = ImGui.GetStyle().Colors[(int)ImGuiCol.CheckMark];
            Vector4 decorationColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border];
            Vector4 decorationColor2 = ImGui.GetStyle().Colors[(int)ImGuiCol.BorderShadow];


            // Capturar clics invisibles
            //ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, 0.0f); // Sin bordes redondeados
            //ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(0, 0));
            if (ImGui.InvisibleButton(id, size))
            {
                isActive = true;
                clicked = true;
            }
            //ImGui.PopStyleVar(2);

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
