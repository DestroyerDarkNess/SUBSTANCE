using EasyModern.Core.Model;
using EasyModern.UI.Widgets;
using Hexa.NET.ImGui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace EasyModern.UI.Views
{
    public class View3 : IView
    {
        public string ID { get; set; } = "view3";
        public string Text { get; set; } = "Misc";
        //public bool Checked { get; set; } = false;

        private bool _checked = false;

        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; if (!value) ResetOptions = true; }
        }

        public ImTextureID Icon { get; set; }


        public List<FunctionWidget> Widgets = new List<FunctionWidget>();

        public string currentOption = "func.gui";

        HeaderBar headerBar = new HeaderBar
        {
            Size = new Vector2(800, 40),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TitleBg],
            LeftLabelText = "pastowl & substance ~& cd combat/killaura",
            LeftLabelColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            RightLabelText = "-/-",
            LeftLabelIndent = 20.0f,
            RightLabelIndent = 20.0f,
            TextRevealDelay = 0.05f
        };

        FunctionWidget USER_Interface = new FunctionWidget
        {
            ID = "func.gui",
            Title = $"Cheat GUI",
            Description = $"The cheat User Interface",
            Checked = false,
            Size = new Vector2(200, 100),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            BorderPercent = 0.3f,
            Animating = false,
            ShowStatusLabel = false,
            BottomRightIconName = "config_icon",
            IconButtonRounding = 1.0f,
            IconButtonSize = 15.0f,
        };

        FunctionWidget FPS_Limit = new FunctionWidget
        {
            ID = "func.fps_Limit",
            Title = $"FPS Limit",
            Description = $"Limit Cheat FPS, If your PC is bad, this option will help you achieve an optimal performance between the cheat and the game.",
            Checked = Core.Instances.Settings.FPSLimiter,
            Size = new Vector2(200, 100),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            BorderPercent = 0.3f,
            Animating = true,
            BottomRightIconName = "config_icon",
            IconButtonRounding = 1.0f,
            IconButtonSize = 15.0f,
        };

        FunctionWidget Fov_Changer = new FunctionWidget
        {
            ID = "func.fov_changer",
            Title = $"Fov Changer",
            Description = $"Changed your FOV",
            Checked = Core.Instances.Settings.FovChanger,
            Size = new Vector2(200, 100),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            BorderPercent = 0.3f,
            Animating = true,
            BottomRightIconName = "config_icon",
            IconButtonRounding = 1.0f,
            IconButtonSize = 15.0f,
        };

        FunctionWidget antiSS_Cheat = new FunctionWidget
        {
            ID = "func.antiSS",
            Title = $"Anti Screenshot",
            Description = $"Temporarily disables the cheat during PunkBuster screenshot.",
            Checked = Core.Instances.Settings.AntiScreenShot,
            Size = new Vector2(200, 100),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            BorderPercent = 0.3f,
            Animating = true,
            IconButtonVisible = false,
            BottomRightIconName = "config_icon",
            IconButtonRounding = 1.0f,
            IconButtonSize = 15.0f,
        };

        FunctionWidget antiAFK_Cheat = new FunctionWidget
        {
            ID = "func.antiAFK",
            Title = $"Anti AFK",
            Description = $"Anti AFK",
            Checked = Core.Instances.Settings.AntiAFK,
            Size = new Vector2(200, 100),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            BorderPercent = 0.3f,
            Animating = true,
            IconButtonVisible = false,
            BottomRightIconName = "config_icon",
            IconButtonRounding = 1.0f,
            IconButtonSize = 15.0f,
        };

        FunctionWidget OtherFunctions_Changer = new FunctionWidget
        {
            ID = "func.other_function",
            Title = $"Other Functions",
            Description = $"Other Cheat Functions",
            Checked = true,
            Size = new Vector2(200, 100),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            BorderPercent = 0.3f,
            Animating = true,
            BottomRightIconName = "config_icon",
            IconButtonRounding = 1.0f,
            IconButtonSize = 15.0f,
        };

        public View3()
        {

            headerBar.LeftLabelText = "destroyer & substance ~& cd " + this.Text.ToLower() + "/" + this.currentOption.ToLower();

            Widgets.Add(USER_Interface);
            Widgets.Add(OtherFunctions_Changer);
            Widgets.Add(FPS_Limit);
            Widgets.Add(antiSS_Cheat);
            Widgets.Add(antiAFK_Cheat);
            //Widgets.Add(Fov_Changer);


            foreach (var widget in Widgets)
            {
                widget.CheckedChanged += (s, e) =>
                {
                    var senderWidget = s as FunctionWidget;
                    senderWidget.BorderOffset = 0.0f;

                    if (senderWidget.ID == FPS_Limit.ID)
                    {
                        Core.Instances.Settings.FPSLimiter = widget.Checked;
                        UpdateFramerate();
                    }
                    else if (senderWidget.ID == Fov_Changer.ID)
                    {
                        Core.Instances.Settings.FovChanger = widget.Checked;
                    }
                    else if (senderWidget.ID == antiSS_Cheat.ID)
                    {
                        Core.Instances.Settings.AntiScreenShot = widget.Checked;
                    }
                    else if (senderWidget.ID == antiAFK_Cheat.ID)
                    {
                        Core.Instances.Settings.AntiAFK = widget.Checked;
                    }
                };

                widget.ButtonClicked += (s, e) =>
                {
                    if (currentOption == widget.ID)
                        return;

                    currentOption = widget.ID;
                    headerBar.LeftLabelText = "destroyer & substance ~& cd " + this.Text.ToLower() + "/" + this.currentOption.ToLower();
                    headerBar.ResetAnimationTimer();
                };
            }


            Theme_ComboWidget.ComboBoxItems = Core.Instances.Themes.Select(tema => tema.ID.Split('.').Last()).ToArray();
            UpdateFramerate();

        }

        bool ResetOptions = true;

        public void Render()
        {
            Vector2 windowSize = ImGui.GetIO().DisplaySize;

            float leftSectionWidth = 220.0f;
            ImGui.BeginChild("LeftSection", new Vector2(leftSectionWidth, 0));

            float topMargin = 10.0f;
            float bottomMargin = 10.0f;

            ImGui.SetCursorPosY(ImGui.GetCursorPosY() + topMargin);

            foreach (var widget in Widgets)
            {
                widget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg];
                if (widget.Animating)
                {
                    widget.BorderOffset += 0.005f;
                    widget.Animating = !(widget.BorderOffset >= 1.0f);
                }
                widget.Render();
            }

            ImGui.Dummy(new Vector2(0, bottomMargin));

            ImGui.EndChild();

            ImGui.SameLine();
            ImGui.BeginChild("RightSection", new Vector2(windowSize.X - leftSectionWidth, 0));

            float marginX = 15.0f; // Sangría horizontal (igual para ambos lados)
            float marginY = 10.0f; // Sangría vertical

            Vector2 childStartPos = ImGui.GetCursorPos();
            ImGui.SetCursorPos(new Vector2(0, childStartPos.Y + marginY));

            float adjustedWidth = (windowSize.X - leftSectionWidth) - (2 * marginX);
            headerBar.Size = new Vector2(adjustedWidth - 200, headerBar.Size.Y);
            headerBar.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TitleBg];
            headerBar.Render(ImGui.GetIO().DeltaTime);

            USER_Interface.Checked = true;


            ImGui.BeginGroup();
            ImGui.Dummy(new Vector2(0, 1));
            ImGui.BeginChild("RightSectionItems", new Vector2(ImGui.GetWindowSize().X - /*marginX*/ 4.0f, 0));

            if (ResetOptions && this.Checked)
            {
                ResetOptions = false;
                ImGui.SetScrollHereY(0.0f);
            }

            if (currentOption == USER_Interface.ID)
            {
                USERInterface();
            }
            else if (currentOption == FPS_Limit.ID)
            {
                FPSLimit();
            }
            else if (currentOption == Fov_Changer.ID)
            {
                FovChanger();
            }
            else if (currentOption == OtherFunctions_Changer.ID)
            {
                OtherFunctions();
            }

            ImGui.EndChild();
            ImGui.EndGroup();

            ImGui.EndChild();
        }

        #region " USER_Interface "

        ComboBoxWidget Theme_ComboWidget = new ComboBoxWidget
        {
            ID = "Theme_combo_1",
            Title = "Theme",
            Description = "Theme",
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            SelectedIndex = Core.Instances.Settings.Theme_ID,
            Size = new Vector2(200, 90)
        };

        ComboBoxWidget Particles_Render_ComboWidget = new ComboBoxWidget
        {
            ID = "Particles_Render_combo_1",
            Title = "Particles Mode",
            Description = "Sets whether particles are drawn backwards/forwards.",
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            SelectedIndex = Core.Instances.Settings.Particles_Draw_Mode,
            ComboBoxItems = new string[] { "Foreground", "Background", "WindowDraw" },
            Size = new Vector2(200, 90)
        };

        CheckWidget RGB_CheckWidget = new CheckWidget
        {
            ID = "RGB_Check_check",
            Title = "RGB Color",
            Description = "rgb colors to borders",
            Checked = Core.Instances.Settings.RGB_Color,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.RGB_Color ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.RGB_Color ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };

        CheckWidget NodeParticles_CheckWidget = new CheckWidget
        {
            ID = "NodeParticles_check",
            Title = "Node Particles",
            Description = "a system of particles.",
            Checked = Core.Instances.Settings.NodeParticles,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.NodeParticles ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.NodeParticles ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };


        bool ConfigUSER_InterfaceOptions = false;

        private void USERInterface()
        {
            if (!ConfigUSER_InterfaceOptions)
            {
                ConfigUSER_InterfaceOptions = true;

                Theme_ComboWidget.SelectedIndexChanged += USERInterface_Checks;
                RGB_CheckWidget.CheckedChanged += USERInterface_Checks;
                NodeParticles_CheckWidget.CheckedChanged += USERInterface_Checks;
                Particles_Render_ComboWidget.SelectedIndexChanged += USERInterface_Checks;

            }

            Theme_ComboWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; Theme_ComboWidget.Render();
            ImGui.SameLine(210);
            RGB_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; RGB_CheckWidget.Render();
            ImGui.SameLine(420);
            Particles_Render_ComboWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; Particles_Render_ComboWidget.Render();


            NodeParticles_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; NodeParticles_CheckWidget.Render();

        }

        public void USERInterface_Checks(object sender, EventArgs e)
        {

            if (sender is CheckWidget)
            {
                CheckWidget widget = sender as CheckWidget;

                if (widget.Checked)
                {
                    widget.BottomRightIconName = "check";
                    widget.BottomRightIconBgColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Button];
                }
                else
                {
                    widget.BottomRightIconName = "uncheck";
                    widget.BottomRightIconBgColor = ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered];
                }

                if (widget.ID == RGB_CheckWidget.ID)
                {
                    Core.Instances.Settings.RGB_Color = widget.Checked;
                }
                else if (widget.ID == NodeParticles_CheckWidget.ID)
                {
                    Core.Instances.Settings.NodeParticles = widget.Checked;
                }

            }
            else if (sender is ComboBoxWidget)
            {
                ComboBoxWidget widget = sender as ComboBoxWidget;

                if (widget.ID == Theme_ComboWidget.ID)
                {
                    Core.Instances.Settings.Theme_ID = Theme_ComboWidget.SelectedIndex;
                    UpdateTheme();
                    Program.InvalidateGUI();
                }
                else if (widget.ID == Particles_Render_ComboWidget.ID)
                {
                    Core.Instances.Settings.Particles_Draw_Mode = Particles_Render_ComboWidget.SelectedIndex;
                }

            }
            else if (sender is TrackBarWidget)
            {
                TrackBarWidget widget = sender as TrackBarWidget;

            }
        }

        private void UpdateTheme()
        {
            int indexToCheck = Core.Instances.Settings.Theme_ID;

            if (indexToCheck >= 0 && indexToCheck < Core.Instances.Themes.Count)
            {
                Core.Instances.Themes[indexToCheck].Apply();
            }
        }

        #endregion

        #region " FPS_Limit "

        TrackBarWidget FPS_Limit_trackBarWidget = new TrackBarWidget
        {
            ID = "FPS_Limit_trackBarWidget",
            Title = "FPS Limiter Value",
            Description = "120 is the optimal value for a balanced game.",
            Minimum = 15,
            Maximum = 200,
            Value = Core.Instances.Settings.FPSLimiter_Value,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90),
        };



        bool ConfigFPS_LimitOptions = false;

        private void FPSLimit()
        {
            if (!ConfigFPS_LimitOptions)
            {
                ConfigFPS_LimitOptions = true;

                FPS_Limit_trackBarWidget.ValueChanged += FPSLimit_Checks;

            }

            FPS_Limit_trackBarWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; FPS_Limit_trackBarWidget.Render();
        }

        public void FPSLimit_Checks(object sender, EventArgs e)
        {
            if (sender is CheckWidget)
            {
                CheckWidget widget = sender as CheckWidget;

            }
            else if (sender is ComboBoxWidget)
            {
                ComboBoxWidget widget = sender as ComboBoxWidget;


            }
            else if (sender is TrackBarWidget)
            {
                TrackBarWidget widget = sender as TrackBarWidget;
                if (widget.ID == FPS_Limit_trackBarWidget.ID)
                {
                    Core.Instances.Settings.FPSLimiter_Value = (int)widget.Value;
                    UpdateFramerate();
                }
            }
            else if (sender is ColorPickerWidget)
            {
                ColorPickerWidget widget = sender as ColorPickerWidget;

            }
        }

        private void UpdateFramerate()
        {
            Core.Instances.OverlayWindow.FPSlimit = Core.Instances.Settings.FPSLimiter ? Core.Instances.Settings.FPSLimiter_Value : 0;
        }

        #endregion

        #region " FovChanger "

        TrackBarWidget Fov_trackBarWidget = new TrackBarWidget
        {
            ID = "Fov_trackBarWidget",
            Title = "Fov Value",
            Description = "120 is the optimal value.",
            Minimum = 100,
            Maximum = 300,
            Value = Core.Instances.Settings.FovChanger_Value,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90),
        };



        bool ConfigFovOptions = false;

        private void FovChanger()
        {
            if (!ConfigFovOptions)
            {
                ConfigFovOptions = true;

                Fov_trackBarWidget.ValueChanged += FovChanger_Checks;

            }

            Fov_trackBarWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; Fov_trackBarWidget.Render();

        }

        public void FovChanger_Checks(object sender, EventArgs e)
        {
            if (sender is CheckWidget)
            {
                CheckWidget widget = sender as CheckWidget;

            }
            else if (sender is ComboBoxWidget)
            {
                ComboBoxWidget widget = sender as ComboBoxWidget;


            }
            else if (sender is TrackBarWidget)
            {
                TrackBarWidget widget = sender as TrackBarWidget;
                if (widget.ID == Fov_trackBarWidget.ID)
                {
                    Core.Instances.Settings.FovChanger_Value = (int)widget.Value;
                }
            }
            else if (sender is ColorPickerWidget)
            {
                ColorPickerWidget widget = sender as ColorPickerWidget;

            }
        }

        #endregion

        #region " OtherFunctions "

        TextBoxWidget UserNameTextBox = new TextBoxWidget
        {
            ID = "UserName",
            Title = "Local User Name",
            Description = "Change your Username locally. (Enemies will still see your real name)",
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            CurrentText = Core.Instances.Settings.UserName,
            Size = new Vector2(250, 100),
            MaxTextLength = 256
        };


        bool ConfigOtherFunctionsOptions = false;

        private void OtherFunctions()
        {
            if (!ConfigOtherFunctionsOptions)
            {
                ConfigOtherFunctionsOptions = true;

                UserNameTextBox.SelectedIndexChanged += OtherFunctions_Checks;

            }

            UserNameTextBox.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; UserNameTextBox.Render();
        }

        public void OtherFunctions_Checks(object sender, EventArgs e)
        {
            if (sender is CheckWidget)
            {
                CheckWidget widget = sender as CheckWidget;

            }
            else if (sender is ComboBoxWidget)
            {
                ComboBoxWidget widget = sender as ComboBoxWidget;


            }
            else if (sender is TrackBarWidget)
            {
                TrackBarWidget widget = sender as TrackBarWidget;

            }
            else if (sender is ColorPickerWidget)
            {
                ColorPickerWidget widget = sender as ColorPickerWidget;

            }
            else if (sender is TextBoxWidget)
            {
                TextBoxWidget widget = sender as TextBoxWidget;

                if (widget.ID == UserNameTextBox.ID)
                {
                    Core.Instances.Settings.UserName = widget.CurrentText;
                }
            }
        }

        #endregion
    }
}
