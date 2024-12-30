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
        public bool Checked { get; set; } = false;
        public ImTextureID Icon { get; set; }

        public List<FunctionWidget> Widgets = new List<FunctionWidget>();

        public string currentOption = "func.aimbot";

        HeaderBar headerBar = new HeaderBar
        {
            Size = new Vector2(800, 40),
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            LeftLabelText = "pastowl & substance ~& cd combat/killaura",
            LeftLabelColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
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
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
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
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            Size = new Vector2(190, 80),
        };

        ColorPickerWidget colorPickerWidget = new ColorPickerWidget
        {
            Title = "Selector de Color",
            Description = "Elige un color para personalizar.",
            EnableAlpha = true,
            SelectedColor = new Vector4(0.5f, 0.8f, 0.2f, 1.0f),
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            Size = new Vector2(200, 80),
        };


        FunctionWidget Aimbot_Cheat = new FunctionWidget
        {
            ID = "func.aimbot",
            Title = $"Aimbot",
            Description = $"Automatically assists the player in aiming and firing at their targets.",
            Checked = Core.Instances.Settings.AIM,
            Size = new Vector2(200, 100),
            BackgroundColor = new Vector4(0.153f, 0.153f, 0.200f, 1.000f),
            BorderPercent = 0.3f,
            Animating = true,
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
            BackgroundColor = new Vector4(0.153f, 0.153f, 0.200f, 1.000f),
            BorderPercent = 0.3f,
            Animating = true,
            BottomRightIconName = "config_icon",
            IconButtonRounding = 1.0f,
            IconButtonSize = 15.0f,
        };

        public View1()
        {
            headerBar.LeftLabelText = "destroyer & substance ~& cd " + this.Text.ToLower() + "/" + this.currentOption.ToLower();

            Widgets.Add(Aimbot_Cheat);
            Widgets.Add(Triggerbot_Cheat);

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
                widget.BorderOffset += 0.005f;
                widget.Animating = !(widget.BorderOffset >= 1.0f);
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

            headerBar.Render(ImGui.GetIO().DeltaTime);

            if (currentOption == Aimbot_Cheat.ID)
            {
                Aimbot();
            }
            else if (currentOption == Triggerbot_Cheat.ID)
            {
                Trigger();
            }

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
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            TitleColor = new Vector4(1.0f, 0.9f, 0.3f, 1.0f),
            DescriptionColor = new Vector4(0.8f, 0.8f, 0.8f, 1.0f),
            BorderColor = new Vector4(0.0f, 1.0f, 0.5f, 1.0f),
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.AIM_Visible_Check ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.AIM_Visible_Check ? new Vector4(0.439f, 0.698f, 0.675f, 1.000f) : new Vector4(1.000f, 0.490f, 0.592f, 1.000f),
            BorderPercent = 1f
        };

        CheckWidget AIM_AimAtAll_CheckWidget = new CheckWidget
        {
            ID = "AIM_AimAtAll_Check_check",
            Title = "AIM At All",
            Description = "Aimbot for everyone, including the same team.",
            Checked = Core.Instances.Settings.AIM_AimAtAll,
            Size = new Vector2(200, 90),
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            TitleColor = new Vector4(1.0f, 0.9f, 0.3f, 1.0f),
            DescriptionColor = new Vector4(0.8f, 0.8f, 0.8f, 1.0f),
            BorderColor = new Vector4(0.0f, 1.0f, 0.5f, 1.0f),
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.AIM_AimAtAll ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.AIM_AimAtAll ? new Vector4(0.439f, 0.698f, 0.675f, 1.000f) : new Vector4(1.000f, 0.490f, 0.592f, 1.000f),
            BorderPercent = 1f
        };

        CheckWidget AIM_StickTarget_CheckWidget = new CheckWidget
        {
            ID = "AIM_StickTarget_Check_check",
            Title = "Stick Target",
            Description = "Maintain its lock on a specific target, even if the target temporarily moves out.",
            Checked = Core.Instances.Settings.AIM_StickTarget,
            Size = new Vector2(200, 90),
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            TitleColor = new Vector4(1.0f, 0.9f, 0.3f, 1.0f),
            DescriptionColor = new Vector4(0.8f, 0.8f, 0.8f, 1.0f),
            BorderColor = new Vector4(0.0f, 1.0f, 0.5f, 1.0f),
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.AIM_StickTarget ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.AIM_StickTarget ? new Vector4(0.439f, 0.698f, 0.675f, 1.000f) : new Vector4(1.000f, 0.490f, 0.592f, 1.000f),
            BorderPercent = 1f
        };

        ComboBoxWidget AIM_Location_ComboWidget = new ComboBoxWidget
        {
            ID = "AIM_Location_combo_1",
            Title = "AIM Target",
            Description = "Determines where the aimbot should target on the enemy player's model.",
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
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
            Value = Core.Instances.Settings.AIM_Fov,
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            Size = new Vector2(200, 90),
        };

        ComboBoxWidget AIM_Type_ComboWidget = new ComboBoxWidget
        {
            ID = "AIM_Type_combo_1",
            Title = "AIM Type",
            Description = "Determines where the aimbot should target on the enemy player's model.",
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
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
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            TitleColor = new Vector4(1.0f, 0.9f, 0.3f, 1.0f),
            DescriptionColor = new Vector4(0.8f, 0.8f, 0.8f, 1.0f),
            BorderColor = new Vector4(0.0f, 1.0f, 0.5f, 1.0f),
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.AIM_Driver_First ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.AIM_Driver_First ? new Vector4(0.439f, 0.698f, 0.675f, 1.000f) : new Vector4(1.000f, 0.490f, 0.592f, 1.000f),
            BorderPercent = 1f
        };

        CheckWidget AIM_AutoAim_First_CheckWidget = new CheckWidget
        {
            ID = "AIM_AutoAim_First_Check_check",
            Title = "Auto Aim",
            Description = "The aimbot is always on auto and will not activate when aiming.",
            Checked = Core.Instances.Settings.AIM_AutoAim,
            Size = new Vector2(200, 90),
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            TitleColor = new Vector4(1.0f, 0.9f, 0.3f, 1.0f),
            DescriptionColor = new Vector4(0.8f, 0.8f, 0.8f, 1.0f),
            BorderColor = new Vector4(0.0f, 1.0f, 0.5f, 1.0f),
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.AIM_AutoAim ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.AIM_AutoAim ? new Vector4(0.439f, 0.698f, 0.675f, 1.000f) : new Vector4(1.000f, 0.490f, 0.592f, 1.000f),
            BorderPercent = 1f
        };

        CheckWidget AIM_Vehicle_First_CheckWidget = new CheckWidget
        {
            ID = "AIM_Vehicle_First_Check_check",
            Title = "Vehicle Aim",
            Description = "auto-targeting for vehicles.",
            Checked = Core.Instances.Settings.AIM_Vehicle,
            Size = new Vector2(200, 90),
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            TitleColor = new Vector4(1.0f, 0.9f, 0.3f, 1.0f),
            DescriptionColor = new Vector4(0.8f, 0.8f, 0.8f, 1.0f),
            BorderColor = new Vector4(0.0f, 1.0f, 0.5f, 1.0f),
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.AIM_Vehicle ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.AIM_Vehicle ? new Vector4(0.439f, 0.698f, 0.675f, 1.000f) : new Vector4(1.000f, 0.490f, 0.592f, 1.000f),
            BorderPercent = 1f
        };

        CheckWidget AIM_Draw_Fov_CheckWidget = new CheckWidget
        {
            ID = "AIM_Draw_Fov_check",
            Title = "Draw Fov",
            Description = "Draw Fov.",
            Checked = Core.Instances.Settings.AIM_Draw_Fov,
            Size = new Vector2(200, 90),
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            TitleColor = new Vector4(1.0f, 0.9f, 0.3f, 1.0f),
            DescriptionColor = new Vector4(0.8f, 0.8f, 0.8f, 1.0f),
            BorderColor = new Vector4(0.0f, 1.0f, 0.5f, 1.0f),
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.AIM_Draw_Fov ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.AIM_Draw_Fov ? new Vector4(0.439f, 0.698f, 0.675f, 1.000f) : new Vector4(1.000f, 0.490f, 0.592f, 1.000f),
            BorderPercent = 1f
        };

        CheckWidget AIM_Draw_TargetLine_CheckWidget = new CheckWidget
        {
            ID = "AIM_Draw_TargetLine_check",
            Title = "Target Line",
            Description = "Draw Target Line",
            Checked = Core.Instances.Settings.AIM_Draw_TargetLine,
            Size = new Vector2(200, 90),
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            TitleColor = new Vector4(1.0f, 0.9f, 0.3f, 1.0f),
            DescriptionColor = new Vector4(0.8f, 0.8f, 0.8f, 1.0f),
            BorderColor = new Vector4(0.0f, 1.0f, 0.5f, 1.0f),
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.AIM_Draw_TargetLine ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.AIM_Draw_TargetLine ? new Vector4(0.439f, 0.698f, 0.675f, 1.000f) : new Vector4(1.000f, 0.490f, 0.592f, 1.000f),
            BorderPercent = 1f
        };

        ColorPickerWidget AIM_Fov_Color_ColorPickerWidget = new ColorPickerWidget
        {
            ID = "AIM_Fov_Color_ColorPickerWidget",
            Title = "Fov Color",
            Description = "Fov Color",
            EnableAlpha = true,
            SelectedColor = Core.Instances.Settings.AIM_Fov_Color,
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            Size = new Vector2(200, 90),
        };

        ColorPickerWidget AIM_TargetColor_ColorPickerWidget = new ColorPickerWidget
        {
            ID = "AIM_TargetColor_ColorPickerWidget",
            Title = "Target Color",
            Description = "Target Line Color",
            EnableAlpha = true,
            SelectedColor = Core.Instances.Settings.AIM_TargetColor,
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            Size = new Vector2(200, 90),
        };

        bool ConfigOptions = false;

        private void Aimbot()
        {
            if (!ConfigOptions)
            {
                ConfigOptions = true;

                AIM_Visible_CheckWidget.CheckedChanged += Aim_Checks;
                AIM_AimAtAll_CheckWidget.CheckedChanged += Aim_Checks;
                AIM_StickTarget_CheckWidget.CheckedChanged += Aim_Checks;
                AIM_Driver_First_CheckWidget.CheckedChanged += Aim_Checks;
                AIM_AutoAim_First_CheckWidget.CheckedChanged += Aim_Checks;
                AIM_Vehicle_First_CheckWidget.CheckedChanged += Aim_Checks;

                AIM_Location_ComboWidget.SelectedIndexChanged += Aim_Checks;
                AIM_Type_ComboWidget.SelectedIndexChanged += Aim_Checks;
                AIM_Fov_trackBarWidget.ValueChanged += Aim_Checks;
                AIM_Draw_Fov_CheckWidget.CheckedChanged += Aim_Checks;
                AIM_Draw_TargetLine_CheckWidget.CheckedChanged += Aim_Checks;

                AIM_Fov_Color_ColorPickerWidget.ColorChanged += Aim_Checks;
                AIM_TargetColor_ColorPickerWidget.ColorChanged += Aim_Checks;
            }

            AIM_Visible_CheckWidget.Render();
            ImGui.SameLine(210);
            AIM_AimAtAll_CheckWidget.Render();
            ImGui.SameLine(420);
            AIM_StickTarget_CheckWidget.Render();

            AIM_Location_ComboWidget.Render();
            ImGui.SameLine(210);
            AIM_Type_ComboWidget.Render();
            ImGui.SameLine(420);
            AIM_Fov_trackBarWidget.Render();

            AIM_Driver_First_CheckWidget.Render();
            ImGui.SameLine(210);
            AIM_AutoAim_First_CheckWidget.Render();
            ImGui.SameLine(420);
            AIM_Vehicle_First_CheckWidget.Render();

            AIM_Draw_Fov_CheckWidget.Render();
            ImGui.SameLine(210);
            AIM_Draw_TargetLine_CheckWidget.Render();
            ImGui.SameLine(420);
            AIM_Fov_Color_ColorPickerWidget.Render();

            AIM_TargetColor_ColorPickerWidget.Render();

        }

        public void Aim_Checks(object sender, EventArgs e)
        {
            if (sender is CheckWidget)
            {

                CheckWidget widget = sender as CheckWidget;
                if (widget.Checked)
                {
                    widget.BottomRightIconName = "check";
                    widget.BottomRightIconBgColor = new Vector4(0.439f, 0.698f, 0.675f, 1.000f);
                }
                else
                {
                    widget.BottomRightIconName = "uncheck";
                    widget.BottomRightIconBgColor = new Vector4(1.000f, 0.490f, 0.592f, 1.000f);
                }

                if (widget.ID == AIM_Visible_CheckWidget.ID)
                {
                    Core.Instances.Settings.AIM_Visible_Check = widget.Checked;
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
            Value = Core.Instances.Settings.Triggerbot_Interval,
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            Size = new Vector2(200, 90),
        };


        TrackBarWidget Triggerbot_Delay_trackBarWidget = new TrackBarWidget
        {
            ID = "Triggerbot_Delay_trackBarWidget",
            Title = "Fire Delay",
            Description = "Firing Delay interval in milliseconds",
            Minimum = 0,
            Maximum = 1000,
            Value = Core.Instances.Settings.Triggerbot_Delay,
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
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

            Triggerbot_Interval_trackBarWidget.Render();
            ImGui.SameLine(210);
            Triggerbot_Delay_trackBarWidget.Render();
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
                    Core.Instances.Settings.Triggerbot_Interval = widget.Value;
                }
                else if (widget.ID == Triggerbot_Delay_trackBarWidget.ID)
                {
                    Core.Instances.Settings.Triggerbot_Delay = widget.Value;
                }
            }
        }


        #endregion



    }


}
