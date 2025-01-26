using EasyModern.Core.Model;
using EasyModern.UI.Widgets;
using Hexa.NET.ImGui;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace EasyModern.UI.Views
{
    public class View1 : IView
    {

        public string ID { get; set; } = "view1";
        public string Text { get; set; } = "Combat";
        //public bool Checked { get; set; } = false;

        private bool _checked = false;

        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; if (!value) ResetOptions = true; }
        }

        public ImTextureID Icon { get; set; }

        public List<FunctionWidget> Widgets = new List<FunctionWidget>();

        public string currentOption = "func.aimbot";

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

        ComboBoxWidget comboWidget = new ComboBoxWidget
        {
            ID = "combo_1",
            Title = "Select Option",
            Description = "Este widget permite seleccionar una opción.",
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            ComboBoxItems = new string[] { "Primero", "Segundo", "Tercero" },
            SelectedIndex = 0,
            Size = new Vector2(190, 80)
        };

        TrackBarWidget trackBarWidget = new TrackBarWidget
        {
            Title = "Volumen",
            Description = "Ajusta el nivel de volumen.",
            Minimum = 0,
            Maximum = 100,
            Value = 50,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(190, 80),
        };

        ColorPickerWidget colorPickerWidget = new ColorPickerWidget
        {
            Title = "Selector de Color",
            Description = "Elige un color para personalizar.",
            EnableAlpha = true,
            SelectedColor = ImGui.GetStyle().Colors[(int)ImGuiCol.PlotLines],
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 80),
        };

        FunctionWidget Aimbot_Cheat = new FunctionWidget
        {
            ID = "func.aimbot",
            Title = $"Aimbot",
            Description = $"Automatically assists the player in aiming and firing at their targets.",
            Checked = Core.Instances.Settings.AIM,
            Size = new Vector2(200, 100),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            BorderPercent = 0.3f,
            Animating = true,
            BottomRightIconName = "config_icon",
            IconButtonRounding = 1.0f,
            IconButtonSize = 15.0f,
        };


        FunctionWidget RCS_Cheat = new FunctionWidget
        {
            ID = "func.RCS",
            Title = $"No Recoil",
            Description = $"No weapon Recoil [May Cause Ban | Use With Caution]",
            Checked = Core.Instances.Settings.RCS,
            Size = new Vector2(200, 100),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            BorderColor = new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
            BorderPercent = 0.3f,
            Animating = true,
            IconButtonVisible = false,
            BottomRightIconName = "config_icon",
            IconButtonRounding = 1.0f,
            IconButtonSize = 15.0f,
        };

        FunctionWidget NoSpread_Cheat = new FunctionWidget
        {
            ID = "func.NoSpread",
            Title = $"No Spread",
            Description = $"No weapon Spread [May Cause Ban | Use With Caution]",
            Checked = Core.Instances.Settings.NoSpread,
            Size = new Vector2(200, 100),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            BorderColor = new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
            BorderPercent = 0.3f,
            Animating = true,
            IconButtonVisible = false,
            BottomRightIconName = "config_icon",
            IconButtonRounding = 1.0f,
            IconButtonSize = 15.0f,
        };

        FunctionWidget NoGravity_Cheat = new FunctionWidget
        {
            ID = "func.NoGravity",
            Title = $"No Gravity",
            Description = $"No Bullet Gravity [May Cause Ban | Use With Caution]",
            Checked = Core.Instances.Settings.NoGravity,
            Size = new Vector2(200, 100),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            BorderColor = new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
            BorderPercent = 0.3f,
            Animating = true,
            IconButtonVisible = false,
            BottomRightIconName = "config_icon",
            IconButtonRounding = 1.0f,
            IconButtonSize = 15.0f,
        };

        FunctionWidget Triggerbot_Cheat = new FunctionWidget
        {
            ID = "func.triggerbot",
            Title = $"Triggerbot",
            Description = $"Automatically firing at their targets.",
            Checked = Core.Instances.Settings.Triggerbot,
            Size = new Vector2(200, 100),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            BorderPercent = 0.3f,
            Animating = true,
            BottomRightIconName = "config_icon",
            IconButtonRounding = 1.0f,
            IconButtonSize = 15.0f,
        };

        FunctionWidget FireRate_Cheat = new FunctionWidget
        {
            ID = "func.RateOfFire",
            Title = $"Rate Of Fire",
            Description = $"Adjust Rounds per minute (RPM) [May Cause Ban | Use With Caution]",
            Checked = Core.Instances.Settings.RateOfFire,
            Size = new Vector2(200, 100),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            BorderColor = new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
            BorderPercent = 0.3f,
            Animating = true,
            BottomRightIconName = "config_icon",
            IconButtonRounding = 1.0f,
            IconButtonSize = 15.0f,
        };

        FunctionWidget Bullet_Control_Cheat = new FunctionWidget
        {
            ID = "func.Bullet_Control",
            Title = $"Bullet Control",
            Description = $"Bullets Control [May Cause Ban | Use With Caution]",
            Checked = Core.Instances.Settings.Bullet_Control,
            Size = new Vector2(200, 100),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            BorderColor = new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
            BorderPercent = 0.3f,
            Animating = true,
            BottomRightIconName = "config_icon",
            IconButtonRounding = 1.0f,
            IconButtonSize = 15.0f,
        };

        FunctionWidget Autospot_Cheat = new FunctionWidget
        {
            ID = "func.Autospot",
            Title = $"Autospot",
            Description = $"Automatically warn your teammates of enemies on the map.",
            Checked = Core.Instances.Settings.AutoSpot,
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

        FunctionWidget NoBreath_Cheat = new FunctionWidget
        {
            ID = "func.NoBreath",
            Title = $"No Breath",
            Description = $"You don't get tired. [May Cause Ban | Use With Caution]",
            Checked = Core.Instances.Settings.NoBreath,
            Size = new Vector2(200, 100),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            BorderColor = new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
            BorderPercent = 0.3f,
            Animating = true,
            IconButtonVisible = false,
            BottomRightIconName = "config_icon",
            IconButtonRounding = 1.0f,
            IconButtonSize = 15.0f,
        };


        FunctionWidget OneHitKill_Cheat = new FunctionWidget
        {
            ID = "func.OneHitKill",
            Title = $"007",
            Description = $"One Hit One Kill",
            Checked = Core.Instances.Settings.OneHitKill,
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

        public View1()
        {
            headerBar.LeftLabelText = "destroyer & substance ~& cd " + this.Text.ToLower() + "/" + this.currentOption.ToLower();

            Widgets.Add(Aimbot_Cheat);
            Widgets.Add(Triggerbot_Cheat);
            Widgets.Add(FireRate_Cheat);
            Widgets.Add(Bullet_Control_Cheat);
            Widgets.Add(RCS_Cheat);
            Widgets.Add(NoBreath_Cheat);
            Widgets.Add(NoSpread_Cheat);
            Widgets.Add(NoGravity_Cheat);
            Widgets.Add(Autospot_Cheat);
            //Widgets.Add(OneHitKill_Cheat);

            foreach (var widget in Widgets)
            {
                widget.CheckedChanged += (s, e) =>
                {
                    var senderWidget = s as FunctionWidget;
                    senderWidget.BorderOffset = 0.0f;

                    if (senderWidget.ID == Aimbot_Cheat.ID)
                    {
                        Core.Instances.Settings.AIM = widget.Checked;
                    }
                    else if (senderWidget.ID == Triggerbot_Cheat.ID)
                    {
                        Core.Instances.Settings.Triggerbot = widget.Checked;
                    }
                    else if (senderWidget.ID == FireRate_Cheat.ID)
                    {
                        Core.Instances.Settings.RateOfFire = widget.Checked;
                    }
                    else if (senderWidget.ID == RCS_Cheat.ID)
                    {
                        Core.Instances.Settings.RCS = widget.Checked;
                    }
                    else if (senderWidget.ID == NoSpread_Cheat.ID)
                    {
                        Core.Instances.Settings.NoSpread = widget.Checked;
                    }
                    else if (senderWidget.ID == NoGravity_Cheat.ID)
                    {
                        Core.Instances.Settings.NoGravity = widget.Checked;
                    }
                    else if (senderWidget.ID == Autospot_Cheat.ID)
                    {
                        Core.Instances.Settings.AutoSpot = widget.Checked;
                    }
                    else if (senderWidget.ID == NoBreath_Cheat.ID)
                    {
                        Core.Instances.Settings.NoBreath = widget.Checked;
                    }
                    else if (senderWidget.ID == OneHitKill_Cheat.ID)
                    {
                        //Core.Instances.Settings.OneHitKill = widget.Checked;
                    }
                    else if (senderWidget.ID == Bullet_Control_Cheat.ID)
                    {
                        Core.Instances.Settings.Bullet_Control = widget.Checked;
                    }

                    //UpdateColors(senderWidget);
                };

                widget.ButtonClicked += (s, e) =>
                {
                    // UpdateColors(s as FunctionWidget);

                    if (currentOption == widget.ID)
                        return;

                    currentOption = widget.ID;
                    headerBar.LeftLabelText = "destroyer & substance ~& cd " + this.Text.ToLower() + "/" + this.currentOption.ToLower();
                    headerBar.ResetAnimationTimer();
                };
            }



            comboWidget.SelectedIndexChanged += (sender, args) =>
            {
                Console.WriteLine($"Índice seleccionado: {comboWidget.SelectedIndex}");
            };

            trackBarWidget.ValueChanged += (s, e) =>
            {
                Console.WriteLine($"Nuevo valor: {trackBarWidget.Value}");
            };

            colorPickerWidget.ColorChanged += (s, e) =>
            {
                Console.WriteLine($"Nuevo color seleccionado: {colorPickerWidget.SelectedColor}");
            };
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
                    widget.BorderOffset += 1.0f;
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

            ImGui.BeginGroup();
            ImGui.Dummy(new Vector2(0, 1));
            ImGui.BeginChild("RightSectionItems", new Vector2(ImGui.GetWindowSize().X - /*marginX*/ 4.0f, 0));

            if (ResetOptions && this.Checked)
            {
                ResetOptions = false;
                ImGui.SetScrollHereY(0.0f);
            }

            if (currentOption == Aimbot_Cheat.ID)
            {
                Aimbot();
            }
            else if (currentOption == Triggerbot_Cheat.ID)
            {
                Trigger();
            }
            else if (currentOption == FireRate_Cheat.ID)
            {
                RateOfFire();
            }
            else if (currentOption == Bullet_Control_Cheat.ID)
            {
                BulletControl();
            }

            ImGui.EndChild();
            ImGui.EndGroup();

            ImGui.EndChild();
        }


        private void UpdateColors(FunctionWidget widget)
        {
            if (widget.BottomRightIconBgColor != new Vector4(1.0f, 1.0f, 1.0f, 1.0f))
            {
                widget.BottomRightIconBgColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            }
            else
            {
                widget.BottomRightIconBgColor = widget.Checked ? widget.OnColor : widget.OffColor;
            }

        }


        #region " Aimbot "

        CheckWidget AIM_Visible_CheckWidget = new CheckWidget
        {
            ID = "AIM_Visible_Check_check",
            Title = "AIM Visible",
            Description = "Determines if the target player is currently visible to the player using the aimbot",
            Checked = Core.Instances.Settings.AIM_Visible_Check,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.AIM_Visible_Check ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.AIM_Visible_Check ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };


        CheckWidget AIM_Silent_CheckWidget = new CheckWidget
        {
            ID = "AIM_Silent_check",
            Title = "Silent AIM",
            Description = "Redirects the bullet towards the enemy target. (It may freeze the game.)",
            Checked = Core.Instances.Settings.AIM_Silent,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.AIM_Silent ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.AIM_Silent ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };

        CheckWidget AIM_AimAtAll_CheckWidget = new CheckWidget
        {
            ID = "AIM_AimAtAll_Check_check",
            Title = "AIM At All",
            Description = "Aimbot for everyone, including the same team.",
            Checked = Core.Instances.Settings.AIM_AimAtAll,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.AIM_AimAtAll ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.AIM_AimAtAll ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };

        CheckWidget AIM_StickTarget_CheckWidget = new CheckWidget
        {
            ID = "AIM_StickTarget_Check_check",
            Title = "Stick Target",
            Description = "Maintain its lock on a specific target, even if the target temporarily moves out.",
            Checked = Core.Instances.Settings.AIM_StickTarget,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.AIM_StickTarget ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.AIM_StickTarget ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };

        ComboBoxWidget AIM_Location_ComboWidget = new ComboBoxWidget
        {
            ID = "AIM_Location_combo_1",
            Title = "AIM Target",
            Description = "Determines where the aimbot should target on the enemy player's model.",
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            ComboBoxItems = new string[] { "BONE_HEAD", "BONE_NECK", "BONE_SPINE2", "BONE_SPINE1", "BONE_PSEUDO_SPINE" },
            SelectedIndex = Core.Instances.Settings.AIM_Location,
            Size = new Vector2(200, 90)
        };

        TrackBarWidget AIM_Fov_trackBarWidget = new TrackBarWidget
        {
            ID = "AIM_Fov_trackBarWidget",
            Title = "FOV",
            Description = "Field of View within which the aimbot will actively target enemies.",
            Minimum = 0,
            Maximum = 10,
            FloatValue = false,
            Value = Core.Instances.Settings.AIM_Fov,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90),
        };

        ComboBoxWidget AIM_Type_ComboWidget = new ComboBoxWidget
        {
            ID = "AIM_Type_combo_1",
            Title = "AIM Type",
            Description = "Determines where the aimbot should target on the enemy player's model.",
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            ComboBoxItems = new string[] { "Auto", "FOV", "DISTANCE" },
            SelectedIndex = Core.Instances.Settings.AIM_Type,
            Size = new Vector2(200, 90)
        };

        CheckWidget AIM_Driver_First_CheckWidget = new CheckWidget
        {
            ID = "AIM_Driver_First_Check_check",
            Title = "Driver First",
            Description = "Gives preference to targeting enemy players that are driving vehicles.",
            Checked = Core.Instances.Settings.AIM_Driver_First,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.AIM_Driver_First ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.AIM_Driver_First ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };

        CheckWidget AIM_AutoAim_First_CheckWidget = new CheckWidget
        {
            ID = "AIM_AutoAim_First_Check_check",
            Title = "Auto Aim",
            Description = "The aimbot is always on auto and will not activate when aiming.",
            Checked = Core.Instances.Settings.AIM_AutoAim,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.AIM_AutoAim ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.AIM_AutoAim ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };

        CheckWidget AIM_Vehicle_First_CheckWidget = new CheckWidget
        {
            ID = "AIM_Vehicle_First_Check_check",
            Title = "Vehicle Aim",
            Description = "auto-targeting for vehicles.",
            Checked = Core.Instances.Settings.AIM_Vehicle,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.AIM_Vehicle ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.AIM_Vehicle ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };

        CheckWidget AIM_Draw_Fov_CheckWidget = new CheckWidget
        {
            ID = "AIM_Draw_Fov_check",
            Title = "Draw Fov",
            Description = "Draw Fov.",
            Checked = Core.Instances.Settings.AIM_Draw_Fov,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.AIM_Draw_Fov ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.AIM_Draw_Fov ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };


        CheckWidget AIM_Draw_TargetLine_CheckWidget = new CheckWidget
        {
            ID = "AIM_Draw_TargetLine_check",
            Title = "Target Line",
            Description = "Draw Target Line",
            Checked = Core.Instances.Settings.AIM_Draw_TargetLine,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.AIM_Draw_TargetLine ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.AIM_Draw_TargetLine ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };

        CheckWidget AIM_Humanizer_CheckWidget = new CheckWidget
        {
            ID = "AIM_Humanizer_check",
            Title = "Humanizer",
            Description = "Humanize the aimbot to make it look more human.",
            Checked = Core.Instances.Settings.AIM_Humanizer,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.AIM_Humanizer ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.AIM_Humanizer ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };

        CheckWidget AIM_OneShot_CheckWidget = new CheckWidget
        {
            ID = "AIM_OneShot_check",
            Title = "OneShot",
            Description = "Aim once at the enemy, the rest is up to you.",
            Checked = Core.Instances.Settings.AIM_OneShot,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.AIM_OneShot ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.AIM_OneShot ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };

        CheckWidget AIM_ExcludeHead_CheckWidget = new CheckWidget
        {
            ID = "AIM_ExcludeHead_check",
            Title = "Exclude Head",
            Description = "Exclude the head as a target. This helps to humanize your aimbot more.",
            Checked = Core.Instances.Settings.AIM_ExcludeHead,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.AIM_ExcludeHead ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.AIM_ExcludeHead ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };

        ColorPickerWidget AIM_Fov_Color_ColorPickerWidget = new ColorPickerWidget
        {
            ID = "AIM_Fov_Color_ColorPickerWidget",
            Title = "Fov Color",
            Description = "Fov Color",
            EnableAlpha = true,
            SelectedColor = Core.Instances.Settings.AIM_Fov_Color,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90),
        };

        TrackBarWidget AIM_Distance_trackBarWidget = new TrackBarWidget
        {
            ID = "AIM_Distance_trackBarWidget",
            Title = "AIM Distance",
            Description = "Minimum working distance of the Aimbot",
            Minimum = 5,
            Maximum = 2000,
            FloatValue = false,
            Value = Core.Instances.Settings.AIM_Distance,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90),
        };

        TrackBarWidget AIM_SmoothFactor_trackBarWidget = new TrackBarWidget
        {
            ID = "AIM_SmoothFactor_trackBarWidget",
            Title = "Smooth Factor",
            Description = "Smoothing for aiming",
            Minimum = 0,
            Maximum = 50,
            FloatValue = false,
            Value = Core.Instances.Settings.AIM_SmoothFactor,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90),
        };

        ColorPickerWidget AIM_TargetColor_ColorPickerWidget = new ColorPickerWidget
        {
            ID = "AIM_TargetColor_ColorPickerWidget",
            Title = "Target Color",
            Description = "Target Line Color",
            EnableAlpha = true,
            SelectedColor = Core.Instances.Settings.AIM_TargetColor,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90),
        };

        bool ConfigOptions = false;

        private void Aimbot()
        {
            if (!ConfigOptions)
            {
                ConfigOptions = true;

                AIM_Visible_CheckWidget.CheckedChanged += Aim_Checks;
                AIM_Silent_CheckWidget.CheckedChanged += Aim_Checks;
                AIM_AimAtAll_CheckWidget.CheckedChanged += Aim_Checks;
                AIM_StickTarget_CheckWidget.CheckedChanged += Aim_Checks;
                AIM_Driver_First_CheckWidget.CheckedChanged += Aim_Checks;
                AIM_AutoAim_First_CheckWidget.CheckedChanged += Aim_Checks;
                AIM_Vehicle_First_CheckWidget.CheckedChanged += Aim_Checks;

                AIM_Location_ComboWidget.SelectedIndexChanged += Aim_Checks;
                AIM_Type_ComboWidget.SelectedIndexChanged += Aim_Checks;
                AIM_Fov_trackBarWidget.ValueChanged += Aim_Checks;
                AIM_Distance_trackBarWidget.ValueChanged += Aim_Checks;
                AIM_SmoothFactor_trackBarWidget.ValueChanged += Aim_Checks;
                AIM_Draw_Fov_CheckWidget.CheckedChanged += Aim_Checks;
                AIM_Draw_TargetLine_CheckWidget.CheckedChanged += Aim_Checks;

                AIM_Humanizer_CheckWidget.CheckedChanged += Aim_Checks;
                AIM_OneShot_CheckWidget.CheckedChanged += Aim_Checks;
                AIM_ExcludeHead_CheckWidget.CheckedChanged += Aim_Checks;
                AIM_Fov_Color_ColorPickerWidget.ColorChanged += Aim_Checks;
                AIM_TargetColor_ColorPickerWidget.ColorChanged += Aim_Checks;
            }

            AIM_Visible_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; AIM_Visible_CheckWidget.Render();
            ImGui.SameLine(210);
            AIM_Silent_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; AIM_Silent_CheckWidget.Render();
            ImGui.SameLine(420);
            AIM_AimAtAll_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; AIM_AimAtAll_CheckWidget.Render();

            AIM_StickTarget_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; AIM_StickTarget_CheckWidget.Render();
            ImGui.SameLine(210);
            AIM_Location_ComboWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; AIM_Location_ComboWidget.Render();
            ImGui.SameLine(420);
            AIM_Type_ComboWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; AIM_Type_ComboWidget.Render();

            AIM_Fov_trackBarWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; AIM_Fov_trackBarWidget.Render();
            ImGui.SameLine(210);
            AIM_Driver_First_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; AIM_Driver_First_CheckWidget.Render();
            ImGui.SameLine(420);
            AIM_AutoAim_First_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; AIM_AutoAim_First_CheckWidget.Render();

            AIM_Vehicle_First_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; AIM_Vehicle_First_CheckWidget.Render();
            ImGui.SameLine(210);
            AIM_Draw_Fov_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; AIM_Draw_Fov_CheckWidget.Render();
            ImGui.SameLine(420);
            AIM_Draw_TargetLine_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; AIM_Draw_TargetLine_CheckWidget.Render();

            AIM_Humanizer_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; AIM_Humanizer_CheckWidget.Render();
            ImGui.SameLine(210);
            AIM_OneShot_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; AIM_OneShot_CheckWidget.Render();
            ImGui.SameLine(420);
            AIM_ExcludeHead_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; AIM_ExcludeHead_CheckWidget.Render();

            AIM_Distance_trackBarWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; AIM_Distance_trackBarWidget.Render();
            ImGui.SameLine(210);
            AIM_SmoothFactor_trackBarWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; AIM_SmoothFactor_trackBarWidget.Render();
            ImGui.SameLine(420);
            AIM_Fov_Color_ColorPickerWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; AIM_Fov_Color_ColorPickerWidget.Render();

            AIM_TargetColor_ColorPickerWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; AIM_TargetColor_ColorPickerWidget.Render();

        }

        public void Aim_Checks(object sender, EventArgs e)
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

                if (widget.ID == AIM_Visible_CheckWidget.ID)
                {
                    Core.Instances.Settings.AIM_Visible_Check = widget.Checked;
                }
                else if (widget.ID == AIM_Silent_CheckWidget.ID)
                {
                    Core.Instances.Settings.AIM_Silent = widget.Checked;
                }
                else if (widget.ID == AIM_AimAtAll_CheckWidget.ID)
                {
                    Core.Instances.Settings.AIM_AimAtAll = widget.Checked;
                }
                else if (widget.ID == AIM_StickTarget_CheckWidget.ID)
                {
                    Core.Instances.Settings.AIM_StickTarget = widget.Checked;
                }
                else if (widget.ID == AIM_Driver_First_CheckWidget.ID)
                {
                    Core.Instances.Settings.AIM_Driver_First = widget.Checked;
                }
                else if (widget.ID == AIM_AutoAim_First_CheckWidget.ID)
                {
                    Core.Instances.Settings.AIM_AutoAim = widget.Checked;
                }
                else if (widget.ID == AIM_Vehicle_First_CheckWidget.ID)
                {
                    Core.Instances.Settings.AIM_Vehicle = widget.Checked;
                }
                else if (widget.ID == AIM_Draw_Fov_CheckWidget.ID)
                {
                    Core.Instances.Settings.AIM_Draw_Fov = widget.Checked;
                }
                else if (widget.ID == AIM_Draw_TargetLine_CheckWidget.ID)
                {
                    Core.Instances.Settings.AIM_Draw_TargetLine = widget.Checked;
                }
                else if (widget.ID == AIM_Humanizer_CheckWidget.ID)
                {
                    if (AIM_Humanizer_CheckWidget.Checked)
                    {
                        AIM_AutoAim_First_CheckWidget.Checked = false;
                        Core.Instances.Settings.AIM_AutoAim = false;
                    }
                    Core.Instances.Settings.AIM_Humanizer = widget.Checked;
                }
                else if (widget.ID == AIM_OneShot_CheckWidget.ID)
                {
                    if (AIM_Humanizer_CheckWidget.Checked)
                    {
                        AIM_AutoAim_First_CheckWidget.Checked = false;
                        Core.Instances.Settings.AIM_AutoAim = false;
                    }
                    Core.Instances.Settings.AIM_OneShot = widget.Checked;
                }
                else if (widget.ID == AIM_ExcludeHead_CheckWidget.ID)
                {
                    Core.Instances.Settings.AIM_ExcludeHead = widget.Checked;
                }
            }
            else if (sender is ComboBoxWidget)
            {
                ComboBoxWidget widget = sender as ComboBoxWidget;

                if (widget.ID == AIM_Location_ComboWidget.ID)
                {
                    Core.Instances.Settings.AIM_Location = widget.SelectedIndex;
                }
                else if (widget.ID == AIM_Type_ComboWidget.ID)
                {
                    Core.Instances.Settings.AIM_Type = widget.SelectedIndex;
                }
            }
            else if (sender is TrackBarWidget)
            {
                TrackBarWidget widget = sender as TrackBarWidget;

                if (widget.ID == AIM_Fov_trackBarWidget.ID)
                {
                    Core.Instances.Settings.AIM_Fov = (int)widget.Value;
                }
                else if (widget.ID == AIM_Distance_trackBarWidget.ID)
                {
                    Core.Instances.Settings.AIM_Distance = (int)widget.Value;
                }
                else if (widget.ID == AIM_SmoothFactor_trackBarWidget.ID)
                {
                    Core.Instances.Settings.AIM_SmoothFactor = (int)widget.Value;
                }
            }
            else if (sender is ColorPickerWidget)
            {
                ColorPickerWidget widget = sender as ColorPickerWidget;
                if (widget.ID == AIM_Fov_Color_ColorPickerWidget.ID)
                {
                    Core.Instances.Settings.AIM_Fov_Color = widget.SelectedColor;
                }
                else if (widget.ID == AIM_TargetColor_ColorPickerWidget.ID)
                {
                    Core.Instances.Settings.AIM_TargetColor = widget.SelectedColor;
                }
            }
        }

        #endregion

        #region " Trigger "

        TrackBarWidget Triggerbot_Interval_trackBarWidget = new TrackBarWidget
        {
            ID = "Triggerbot_Interval_trackBarWidget",
            Title = "Fire Interval",
            Description = "Firing duration in milliseconds",
            Minimum = 0,
            Maximum = 1000,
            FloatValue = false,
            Value = Core.Instances.Settings.Triggerbot_Interval,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90),
        };


        TrackBarWidget Triggerbot_Delay_trackBarWidget = new TrackBarWidget
        {
            ID = "Triggerbot_Delay_trackBarWidget",
            Title = "Fire Delay",
            Description = "Firing Delay interval in milliseconds",
            Minimum = 0,
            Maximum = 1000,
            FloatValue = false,
            Value = Core.Instances.Settings.Triggerbot_Delay,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90),
        };


        bool ConfigTriggerOptions = false;

        private void Trigger()
        {
            if (!ConfigTriggerOptions)
            {
                ConfigTriggerOptions = true;

                Triggerbot_Interval_trackBarWidget.ValueChanged += Trigger_Checks;
                Triggerbot_Delay_trackBarWidget.ValueChanged += Trigger_Checks;
            }

            Triggerbot_Interval_trackBarWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; Triggerbot_Interval_trackBarWidget.Render();
            ImGui.SameLine(210);
            Triggerbot_Delay_trackBarWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; Triggerbot_Delay_trackBarWidget.Render();
        }

        public void Trigger_Checks(object sender, EventArgs e)
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

                if (widget.ID == Triggerbot_Interval_trackBarWidget.ID)
                {
                    Core.Instances.Settings.Triggerbot_Interval = (int)widget.Value;
                }
                else if (widget.ID == Triggerbot_Delay_trackBarWidget.ID)
                {
                    Core.Instances.Settings.Triggerbot_Delay = (int)widget.Value;
                }
            }
        }


        #endregion

        #region " FireRate "

        TrackBarWidget FireRate_Interval_trackBarWidget = new TrackBarWidget
        {
            ID = "FireRate_Interval_trackBarWidget",
            Title = "Fire Interval",
            Description = "Firing duration in milliseconds",
            Minimum = 1000,
            Maximum = 5000,
            FloatValue = false,
            Value = Core.Instances.Settings.FireRate,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90),
        };


        bool ConfigRateOfFireOptions = false;

        private void RateOfFire()
        {
            if (!ConfigRateOfFireOptions)
            {
                ConfigRateOfFireOptions = true;

                FireRate_Interval_trackBarWidget.ValueChanged += ConfigRateOfFireOptions_Checks;
            }

            FireRate_Interval_trackBarWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; FireRate_Interval_trackBarWidget.Render();
        }

        public void ConfigRateOfFireOptions_Checks(object sender, EventArgs e)
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

                if (widget.ID == FireRate_Interval_trackBarWidget.ID)
                {
                    Core.Instances.Settings.FireRate = widget.Value;
                }
            }
        }


        #endregion

        #region " BulletControl "

        TrackBarWidget BulletsPerShell_trackBarWidget = new TrackBarWidget
        {
            ID = "BulletsPerShell_trackBarWidget",
            Title = "Per Shell",
            Description = "Bullet Firing Config",
            Minimum = 0,
            Maximum = 100,
            FloatValue = false,
            Value = (int)Core.Instances.Settings.BulletsPerShell,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90),
        };

        TrackBarWidget BulletsPerShot_trackBarWidget = new TrackBarWidget
        {
            ID = "BulletsPerShot_trackBarWidget",
            Title = "Per Shot",
            Description = "Bullet Firing Config",
            Minimum = 0,
            Maximum = 100,
            FloatValue = false,
            Value = (int)Core.Instances.Settings.BulletsPerShot,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90),
        };

        TrackBarWidget VehicleBulletsPershell_trackBarWidget = new TrackBarWidget
        {
            ID = "VehicleBulletsPershell_trackBarWidget",
            Title = "Veh Per shell",
            Description = "Vehicle Bullet Firing Config",
            Minimum = 0,
            Maximum = 100,
            FloatValue = false,
            Value = (int)Core.Instances.Settings.VehicleBulletsPershell,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90),
        };

        TrackBarWidget VehicleBulletsPerShot_trackBarWidget = new TrackBarWidget
        {
            ID = "VehicleBulletsPerShot_trackBarWidget",
            Title = "Vehicle Per Shot",
            Description = "Vehicle Bullets Per Shot",
            Minimum = 0,
            Maximum = 100,
            FloatValue = false,
            Value = (int)Core.Instances.Settings.VehicleBulletsPerShot,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90),
        };


        bool ConfigBulletControlOptions = false;

        private void BulletControl()
        {
            if (!ConfigBulletControlOptions)
            {
                ConfigBulletControlOptions = true;

                BulletsPerShell_trackBarWidget.ValueChanged += ConfigBulletControlOptions_Checks;
                BulletsPerShot_trackBarWidget.ValueChanged += ConfigBulletControlOptions_Checks;
                VehicleBulletsPershell_trackBarWidget.ValueChanged += ConfigBulletControlOptions_Checks;
                VehicleBulletsPerShot_trackBarWidget.ValueChanged += ConfigBulletControlOptions_Checks;
            }

            BulletsPerShell_trackBarWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; BulletsPerShell_trackBarWidget.Render();
            ImGui.SameLine(210);
            BulletsPerShot_trackBarWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; BulletsPerShot_trackBarWidget.Render();
            ImGui.SameLine(420);
            VehicleBulletsPershell_trackBarWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; VehicleBulletsPershell_trackBarWidget.Render();

            VehicleBulletsPerShot_trackBarWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; VehicleBulletsPerShot_trackBarWidget.Render();
        }

        public void ConfigBulletControlOptions_Checks(object sender, EventArgs e)
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

                if (widget.ID == BulletsPerShell_trackBarWidget.ID)
                {
                    Core.Instances.Settings.BulletsPerShell = (int)widget.Value;
                }
                else if (widget.ID == BulletsPerShot_trackBarWidget.ID)
                {
                    Core.Instances.Settings.BulletsPerShot = (int)widget.Value;
                }
                else if (widget.ID == VehicleBulletsPershell_trackBarWidget.ID)
                {
                    Core.Instances.Settings.VehicleBulletsPershell = (int)widget.Value;
                }
                else if (widget.ID == VehicleBulletsPershell_trackBarWidget.ID)
                {
                    Core.Instances.Settings.VehicleBulletsPershell = (int)widget.Value;
                }
                else if (widget.ID == VehicleBulletsPerShot_trackBarWidget.ID)
                {
                    Core.Instances.Settings.VehicleBulletsPerShot = (int)widget.Value;
                }
            }
        }


        #endregion

    }


}
