using EasyModern.Core.Model;
using EasyModern.UI.Widgets;
using Hexa.NET.ImGui;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace EasyModern.UI.Views
{
    public class View2 : IView
    {
        public string ID { get; set; } = "view2";
        public string Text { get; set; } = "Render";
        public bool Checked { get; set; } = false;
        public ImTextureID Icon { get; set; }


        public List<FunctionWidget> Widgets = new List<FunctionWidget>();

        public string currentOption = "func.esp";

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

        FunctionWidget ESP_Cheat = new FunctionWidget
        {
            ID = "func.esp",
            Title = $"ESP",
            Description = $"Extra Sensory Perception.",
            Checked = Core.Instances.Settings.ESP,
            Size = new Vector2(200, 100),
            BackgroundColor = new Vector4(0.153f, 0.153f, 0.200f, 1.000f),
            BorderPercent = 0.3f,
            Animating = true,
            BottomRightIconName = "config_icon",
            IconButtonRounding = 1.0f,
            IconButtonSize = 15.0f,
        };

        FunctionWidget Crosshair_Cheat = new FunctionWidget
        {
            ID = "func.crosshair",
            Title = $"Crosshair",
            Description = $"Visual Indicator on the player's screen that represents the point of aim for their weapon.",
            Checked = Core.Instances.Settings.Crosshair,
            Size = new Vector2(200, 100),
            BackgroundColor = new Vector4(0.153f, 0.153f, 0.200f, 1.000f),
            BorderPercent = 0.3f,
            Animating = true,
            BottomRightIconName = "config_icon",
            IconButtonRounding = 1.0f,
            IconButtonSize = 15.0f,
        };

        FunctionWidget Radar_Cheat = new FunctionWidget
        {
            ID = "func.radar",
            Title = $"Radar",
            Description = $"Visual Indicator on the player's screen that represents the point of aim for their weapon.",
            Checked = Core.Instances.Settings.Radar,
            Size = new Vector2(200, 100),
            BackgroundColor = new Vector4(0.153f, 0.153f, 0.200f, 1.000f),
            BorderPercent = 0.3f,
            Animating = true,
            BottomRightIconName = "config_icon",
            IconButtonRounding = 1.0f,
            IconButtonSize = 15.0f,
        };

        FunctionWidget Overheat_Cheat = new FunctionWidget
        {
            ID = "func.overheat",
            Title = $"Overheat",
            Description = $"Visual Indicator for vehicle their weapon.",
            Checked = Core.Instances.Settings.Overheat,
            Size = new Vector2(200, 100),
            BackgroundColor = new Vector4(0.153f, 0.153f, 0.200f, 1.000f),
            BorderPercent = 0.3f,
            Animating = true,
            BottomRightIconName = "config_icon",
            IconButtonRounding = 1.0f,
            IconButtonSize = 15.0f,
        };

        FunctionWidget Info_Cheat = new FunctionWidget
        {
            ID = "func.info",
            Title = $"Info",
            Description = $"Draw information about cheats and settings.",
            Checked = Core.Instances.Settings.Draw_Info,
            Size = new Vector2(200, 100),
            BackgroundColor = new Vector4(0.153f, 0.153f, 0.200f, 1.000f),
            BorderPercent = 0.3f,
            Animating = true,
            BottomRightIconName = "config_icon",
            IconButtonRounding = 1.0f,
            IconButtonSize = 15.0f,
        };

        public View2()
        {

            headerBar.LeftLabelText = "destroyer & substance ~& cd " + this.Text.ToLower() + "/" + this.currentOption.ToLower();

            Widgets.Add(ESP_Cheat);
            Widgets.Add(Crosshair_Cheat);
            Widgets.Add(Radar_Cheat);
            Widgets.Add(Overheat_Cheat);
            Widgets.Add(Info_Cheat);

            foreach (var widget in Widgets)
            {
                widget.CheckedChanged += (s, e) =>
                {
                    var senderWidget = s as FunctionWidget;
                    senderWidget.BorderOffset = 0.0f;

                    if (senderWidget.ID == ESP_Cheat.ID)
                    {
                        Core.Instances.Settings.ESP = widget.Checked;
                    }
                    else if (senderWidget.ID == Crosshair_Cheat.ID)
                    {
                        Core.Instances.Settings.Crosshair = widget.Checked;
                    }
                    else if (senderWidget.ID == Radar_Cheat.ID)
                    {
                        Core.Instances.Settings.Radar = widget.Checked;
                    }
                    else if (senderWidget.ID == Overheat_Cheat.ID)
                    {
                        Core.Instances.Settings.Overheat = widget.Checked;
                    }
                    else if (senderWidget.ID == Info_Cheat.ID)
                    {
                        Core.Instances.Settings.Draw_Info = widget.Checked;
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

            if (currentOption == ESP_Cheat.ID)
            {
                ESP();
            }
            else if (currentOption == Crosshair_Cheat.ID)
            {
                Crosshair();
            }
            else if (currentOption == Radar_Cheat.ID)
            {
                Radar();
            }
            else if (currentOption == Overheat_Cheat.ID)
            {
                Overheat();
            }
            else if (currentOption == Info_Cheat.ID)
            {
                Info();
            }

            ImGui.EndChild();
        }

        #region " ESP "

        CheckWidget ESP_Name_CheckWidget = new CheckWidget
        {
            ID = "ESP_Name_Check_check",
            Title = "Name",
            Description = "Displays the name of the players.",
            Checked = Core.Instances.Settings.ESP_Name,
            Size = new Vector2(200, 90),
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            TitleColor = new Vector4(1.0f, 0.9f, 0.3f, 1.0f),
            DescriptionColor = new Vector4(0.8f, 0.8f, 0.8f, 1.0f),
            BorderColor = new Vector4(0.0f, 1.0f, 0.5f, 1.0f),
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.ESP_Name ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.ESP_Name ? new Vector4(0.439f, 0.698f, 0.675f, 1.000f) : new Vector4(1.000f, 0.490f, 0.592f, 1.000f),
            BorderPercent = 1f
        };

        CheckWidget ESP_Distance_CheckWidget = new CheckWidget
        {
            ID = "ESP_Distance_Check_check",
            Title = "Distance",
            Description = "Displays the distance of the players.",
            Checked = Core.Instances.Settings.ESP_Distance,
            Size = new Vector2(200, 90),
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            TitleColor = new Vector4(1.0f, 0.9f, 0.3f, 1.0f),
            DescriptionColor = new Vector4(0.8f, 0.8f, 0.8f, 1.0f),
            BorderColor = new Vector4(0.0f, 1.0f, 0.5f, 1.0f),
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.ESP_Distance ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.ESP_Distance ? new Vector4(0.439f, 0.698f, 0.675f, 1.000f) : new Vector4(1.000f, 0.490f, 0.592f, 1.000f),
            BorderPercent = 1f
        };

        CheckWidget ESP_Distance_InName_CheckWidget = new CheckWidget
        {
            ID = "ESP_Distance_InName_Check_check",
            Title = "Distance In Name",
            Description = "Displays the distance in the name of the players.",
            Checked = Core.Instances.Settings.ESP_Distance_InName,
            Size = new Vector2(200, 90),
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            TitleColor = new Vector4(1.0f, 0.9f, 0.3f, 1.0f),
            DescriptionColor = new Vector4(0.8f, 0.8f, 0.8f, 1.0f),
            BorderColor = new Vector4(0.0f, 1.0f, 0.5f, 1.0f),
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.ESP_Distance_InName ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.ESP_Distance_InName ? new Vector4(0.439f, 0.698f, 0.675f, 1.000f) : new Vector4(1.000f, 0.490f, 0.592f, 1.000f),
            BorderPercent = 1f
        };

        CheckWidget ESP_Health_CheckWidget = new CheckWidget
        {
            ID = "ESP_Health_Check_check",
            Title = "Health",
            Description = "Displays the health of the players.",
            Checked = Core.Instances.Settings.ESP_Health,
            Size = new Vector2(200, 90),
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            TitleColor = new Vector4(1.0f, 0.9f, 0.3f, 1.0f),
            DescriptionColor = new Vector4(0.8f, 0.8f, 0.8f, 1.0f),
            BorderColor = new Vector4(0.0f, 1.0f, 0.5f, 1.0f),
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.ESP_Health ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.ESP_Health ? new Vector4(0.439f, 0.698f, 0.675f, 1.000f) : new Vector4(1.000f, 0.490f, 0.592f, 1.000f),
            BorderPercent = 1f
        };

        CheckWidget ESP_Box_CheckWidget = new CheckWidget
        {
            ID = "ESP_Box_Check_check",
            Title = "Box",
            Description = "Draw ESP Box",
            Checked = Core.Instances.Settings.ESP_Box,
            Size = new Vector2(200, 90),
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            TitleColor = new Vector4(1.0f, 0.9f, 0.3f, 1.0f),
            DescriptionColor = new Vector4(0.8f, 0.8f, 0.8f, 1.0f),
            BorderColor = new Vector4(0.0f, 1.0f, 0.5f, 1.0f),
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.ESP_Box ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.ESP_Box ? new Vector4(0.439f, 0.698f, 0.675f, 1.000f) : new Vector4(1.000f, 0.490f, 0.592f, 1.000f),
            BorderPercent = 1f
        };

        CheckWidget ESP_Vehicle_CheckWidget = new CheckWidget
        {
            ID = "ESP_Vehicle_Check_check",
            Title = "Vehicle",
            Description = "Draw Vehicle ESP",
            Checked = Core.Instances.Settings.ESP_Vehicle,
            Size = new Vector2(200, 90),
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            TitleColor = new Vector4(1.0f, 0.9f, 0.3f, 1.0f),
            DescriptionColor = new Vector4(0.8f, 0.8f, 0.8f, 1.0f),
            BorderColor = new Vector4(0.0f, 1.0f, 0.5f, 1.0f),
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.ESP_Vehicle ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.ESP_Vehicle ? new Vector4(0.439f, 0.698f, 0.675f, 1.000f) : new Vector4(1.000f, 0.490f, 0.592f, 1.000f),
            BorderPercent = 1f
        };

        CheckWidget ESP_Enemy_CheckWidget = new CheckWidget
        {
            ID = "ESP_Enemy_Check_check",
            Title = "Only Enemy",
            Description = "Draw Enemy ESP",
            Checked = Core.Instances.Settings.ESP_Enemy,
            Size = new Vector2(200, 90),
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            TitleColor = new Vector4(1.0f, 0.9f, 0.3f, 1.0f),
            DescriptionColor = new Vector4(0.8f, 0.8f, 0.8f, 1.0f),
            BorderColor = new Vector4(0.0f, 1.0f, 0.5f, 1.0f),
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.ESP_Enemy ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.ESP_Enemy ? new Vector4(0.439f, 0.698f, 0.675f, 1.000f) : new Vector4(1.000f, 0.490f, 0.592f, 1.000f),
            BorderPercent = 1f
        };

        CheckWidget ESP_Bone_CheckWidget = new CheckWidget
        {
            ID = "ESP_Bone_Check_check",
            Title = "Bone",
            Description = "Draw Bone ESP",
            Checked = Core.Instances.Settings.ESP_Bone,
            Size = new Vector2(200, 90),
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            TitleColor = new Vector4(1.0f, 0.9f, 0.3f, 1.0f),
            DescriptionColor = new Vector4(0.8f, 0.8f, 0.8f, 1.0f),
            BorderColor = new Vector4(0.0f, 1.0f, 0.5f, 1.0f),
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.ESP_Bone ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.ESP_Bone ? new Vector4(0.439f, 0.698f, 0.675f, 1.000f) : new Vector4(1.000f, 0.490f, 0.592f, 1.000f),
            BorderPercent = 1f
        };

        CheckWidget ESP_Line_CheckWidget = new CheckWidget
        {
            ID = "ESP_Line_Check_check",
            Title = "Line",
            Description = "Draw Line ESP",
            Checked = Core.Instances.Settings.ESP_Line,
            Size = new Vector2(200, 90),
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            TitleColor = new Vector4(1.0f, 0.9f, 0.3f, 1.0f),
            DescriptionColor = new Vector4(0.8f, 0.8f, 0.8f, 1.0f),
            BorderColor = new Vector4(0.0f, 1.0f, 0.5f, 1.0f),
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.ESP_Line ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.ESP_Line ? new Vector4(0.439f, 0.698f, 0.675f, 1.000f) : new Vector4(1.000f, 0.490f, 0.592f, 1.000f),
            BorderPercent = 1f
        };

        ComboBoxWidget ESP_BoxType_ComboWidget = new ComboBoxWidget
        {
            ID = "ESP_BoxType_combo_1",
            Title = "Box Type",
            Description = "Visual representation of the information provided about other players.",
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            ComboBoxItems = new string[] { "2D", "2D Target", "3D" },
            SelectedIndex = Core.Instances.Settings.ESP_BoxType,
            Size = new Vector2(200, 90)
        };

        TrackBarWidget ESP_DrawDistance_trackBarWidget = new TrackBarWidget
        {
            ID = "ESP_DrawDistance_trackBarWidget",
            Title = "Draw Distance",
            Description = "Limit the players that will be drawn by distance.",
            Minimum = 100,
            Maximum = 4000,
            Value = Core.Instances.Settings.ESP_DrawDistance,
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            Size = new Vector2(200, 90),
        };

        TrackBarWidget ESP_BoneDistance_trackBarWidget = new TrackBarWidget
        {
            ID = "ESP_BoneDistance_trackBarWidget",
            Title = "Bone Draw Distance",
            Description = "Limit the Bones that will be drawn by distance.",
            Minimum = 100,
            Maximum = 2000,
            Value = Core.Instances.Settings.ESP_BoneDistance,
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            Size = new Vector2(200, 90),
        };

        bool ConfigESPOptions = false;

        private void ESP()
        {
            if (!ConfigESPOptions)
            {
                ConfigESPOptions = true;

                ESP_Name_CheckWidget.CheckedChanged += ESP_Checks;
                ESP_Distance_CheckWidget.CheckedChanged += ESP_Checks;
                ESP_Distance_InName_CheckWidget.CheckedChanged += ESP_Checks;
                ESP_Health_CheckWidget.CheckedChanged += ESP_Checks;
                ESP_Box_CheckWidget.CheckedChanged += ESP_Checks;
                ESP_Vehicle_CheckWidget.CheckedChanged += ESP_Checks;
                ESP_Enemy_CheckWidget.CheckedChanged += ESP_Checks;
                ESP_Bone_CheckWidget.CheckedChanged += ESP_Checks;
                ESP_Line_CheckWidget.CheckedChanged += ESP_Checks;

                ESP_BoxType_ComboWidget.SelectedIndexChanged += ESP_Checks;
                ESP_DrawDistance_trackBarWidget.ValueChanged += ESP_Checks;
                ESP_BoneDistance_trackBarWidget.ValueChanged += ESP_Checks;
            }

            ESP_Name_CheckWidget.Render();
            ImGui.SameLine(210);
            ESP_Distance_CheckWidget.Render();
            ImGui.SameLine(420);
            ESP_Distance_InName_CheckWidget.Render();

            ESP_Health_CheckWidget.Render();
            ImGui.SameLine(210);
            ESP_Box_CheckWidget.Render();
            ImGui.SameLine(420);
            ESP_Vehicle_CheckWidget.Render();


            ESP_Enemy_CheckWidget.Render();
            ImGui.SameLine(210);
            ESP_Bone_CheckWidget.Render();
            ImGui.SameLine(420);
            ESP_Line_CheckWidget.Render();

            ESP_BoxType_ComboWidget.Render();
            ImGui.SameLine(210);
            ESP_DrawDistance_trackBarWidget.Render();
            ImGui.SameLine(420);
            ESP_BoneDistance_trackBarWidget.Render();
        }

        public void ESP_Checks(object sender, EventArgs e)
        {
            if (sender is CheckWidget)
            {
                CheckWidget widget = sender as CheckWidget;

                if (widget.ID == ESP_Name_CheckWidget.ID)
                {
                    Core.Instances.Settings.ESP_Name = widget.Checked;
                }
                else if (widget.ID == ESP_Distance_CheckWidget.ID)
                {
                    Core.Instances.Settings.ESP_Distance = widget.Checked;
                }
                else if (widget.ID == ESP_Distance_InName_CheckWidget.ID)
                {
                    Core.Instances.Settings.ESP_Distance_InName = widget.Checked;
                }
                else if (widget.ID == ESP_Health_CheckWidget.ID)
                {
                    Core.Instances.Settings.ESP_Health = widget.Checked;
                }
                else if (widget.ID == ESP_Box_CheckWidget.ID)
                {
                    Core.Instances.Settings.ESP_Box = widget.Checked;
                }
                else if (widget.ID == ESP_Vehicle_CheckWidget.ID)
                {
                    Core.Instances.Settings.ESP_Vehicle = widget.Checked;
                }
                else if (widget.ID == ESP_Enemy_CheckWidget.ID)
                {
                    Core.Instances.Settings.ESP_Enemy = widget.Checked;
                }
                else if (widget.ID == ESP_Bone_CheckWidget.ID)
                {
                    Core.Instances.Settings.ESP_Bone = widget.Checked;
                }
                else if (widget.ID == ESP_Line_CheckWidget.ID)
                {
                    Core.Instances.Settings.ESP_Line = widget.Checked;
                }
            }
            else if (sender is ComboBoxWidget)
            {
                ComboBoxWidget widget = sender as ComboBoxWidget;

                if (widget.ID == ESP_BoxType_ComboWidget.ID)
                {
                    Core.Instances.Settings.ESP_BoxType = widget.SelectedIndex;
                }
            }
            else if (sender is TrackBarWidget)
            {
                TrackBarWidget widget = sender as TrackBarWidget;
                if (widget.ID == ESP_DrawDistance_trackBarWidget.ID)
                {
                    Core.Instances.Settings.ESP_DrawDistance = (int)widget.Value;
                }
                else if (widget.ID == ESP_BoneDistance_trackBarWidget.ID)
                {
                    Core.Instances.Settings.ESP_BoneDistance = (int)widget.Value;
                }
            }
        }


        #endregion

        #region " Crosshair "

        ComboBoxWidget Crosshair_Style_ComboWidget = new ComboBoxWidget
        {
            ID = "Crosshair_Style_combo_1",
            Title = "Style",
            Description = "Crosshair Style",
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            ComboBoxItems = new string[] { "Cross", "Box", "Triangle", "Linear", "Hexagram", "Text", "Point" },
            SelectedIndex = Core.Instances.Settings.Crosshair_Style,
            Size = new Vector2(200, 90)
        };

        TrackBarWidget Crosshair_Scale_trackBarWidget = new TrackBarWidget
        {
            ID = "Crosshair_Scale_trackBarWidget",
            Title = "Scale",
            Description = "Field of View within which the aimbot will actively target enemies.",
            Minimum = 0,
            Maximum = 10,
            Value = Core.Instances.Settings.Crosshair_Scale,
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            Size = new Vector2(200, 90),
        };

        ColorPickerWidget Crosshair_Color_ColorPickerWidget = new ColorPickerWidget
        {
            ID = "Crosshair_Color_ColorPickerWidget",
            Title = "Color",
            Description = "Crosshair Color",
            EnableAlpha = true,
            SelectedColor = Core.Instances.Settings.Crosshair_Color,
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            Size = new Vector2(200, 90),
        };


        bool ConfigCrosshairOptions = false;

        private void Crosshair()
        {
            if (!ConfigCrosshairOptions)
            {
                ConfigCrosshairOptions = true;

                Crosshair_Style_ComboWidget.SelectedIndexChanged += Crosshair_Checks;
                Crosshair_Scale_trackBarWidget.ValueChanged += Crosshair_Checks;
                Crosshair_Color_ColorPickerWidget.ColorChanged += Crosshair_Checks;
            }

            Crosshair_Style_ComboWidget.Render();
            ImGui.SameLine(210);
            Crosshair_Scale_trackBarWidget.Render();
            ImGui.SameLine(420);
            Crosshair_Color_ColorPickerWidget.Render();

        }

        public void Crosshair_Checks(object sender, EventArgs e)
        {
            if (sender is CheckWidget)
            {
                CheckWidget widget = sender as CheckWidget;

            }
            else if (sender is ComboBoxWidget)
            {
                ComboBoxWidget widget = sender as ComboBoxWidget;

                if (widget.ID == Crosshair_Style_ComboWidget.ID)
                {
                    Core.Instances.Settings.Crosshair_Style = widget.SelectedIndex;
                }
            }
            else if (sender is TrackBarWidget)
            {
                TrackBarWidget widget = sender as TrackBarWidget;
                if (widget.ID == Crosshair_Scale_trackBarWidget.ID)
                {
                    Core.Instances.Settings.Crosshair_Scale = widget.Value;
                }
            }
            else if (sender is ColorPickerWidget)
            {
                ColorPickerWidget widget = sender as ColorPickerWidget;
                if (widget.ID == Crosshair_Color_ColorPickerWidget.ID)
                {
                    Core.Instances.Settings.Crosshair_Color = widget.SelectedColor;
                }
            }
        }


        #endregion

        #region " Radar "

        TrackBarWidget Radar_Scale_trackBarWidget = new TrackBarWidget
        {
            ID = "Crosshair_Scale_trackBarWidget",
            Title = "Scale",
            Description = "Field of View within which the aimbot will actively target enemies.",
            Minimum = 100,
            Maximum = 300,
            Value = Core.Instances.Settings.Radar_Scale,
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            Size = new Vector2(200, 90),
        };



        bool ConfigRadarOptions = false;

        private void Radar()
        {
            if (!ConfigRadarOptions)
            {
                ConfigRadarOptions = true;

                Radar_Scale_trackBarWidget.ValueChanged += Radar_Checks;

            }

            Radar_Scale_trackBarWidget.Render();
        }

        public void Radar_Checks(object sender, EventArgs e)
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
                if (widget.ID == Radar_Scale_trackBarWidget.ID)
                {
                    Core.Instances.Settings.Radar_Scale = (int)widget.Value;
                }
            }
            else if (sender is ColorPickerWidget)
            {
                ColorPickerWidget widget = sender as ColorPickerWidget;

            }
        }


        #endregion

        #region " Overheat "

        CheckWidget Overheat_DrawBackground = new CheckWidget
        {
            ID = "Overheat_DrawBackground_Check_check",
            Title = "Draw Background",
            Description = "Draw Overheat Background",
            Checked = Core.Instances.Settings.Overheat_DrawBackground,
            Size = new Vector2(200, 90),
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            TitleColor = new Vector4(1.0f, 0.9f, 0.3f, 1.0f),
            DescriptionColor = new Vector4(0.8f, 0.8f, 0.8f, 1.0f),
            BorderColor = new Vector4(0.0f, 1.0f, 0.5f, 1.0f),
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.Overheat_DrawBackground ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.Overheat_DrawBackground ? new Vector4(0.439f, 0.698f, 0.675f, 1.000f) : new Vector4(1.000f, 0.490f, 0.592f, 1.000f),
            BorderPercent = 1f
        };


        CheckWidget Overheat_DrawText = new CheckWidget
        {
            ID = "Overheat_DrawText_Check_check",
            Title = "Draw Text",
            Description = "Draw Overheat Text",
            Checked = Core.Instances.Settings.Overheat_DrawText,
            Size = new Vector2(200, 90),
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            TitleColor = new Vector4(1.0f, 0.9f, 0.3f, 1.0f),
            DescriptionColor = new Vector4(0.8f, 0.8f, 0.8f, 1.0f),
            BorderColor = new Vector4(0.0f, 1.0f, 0.5f, 1.0f),
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.Overheat_DrawText ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.Overheat_DrawText ? new Vector4(0.439f, 0.698f, 0.675f, 1.000f) : new Vector4(1.000f, 0.490f, 0.592f, 1.000f),
            BorderPercent = 1f
        };

        ColorPickerWidget Overheat_Color_ColorPickerWidget = new ColorPickerWidget
        {
            ID = "Overheat_Color_ColorPickerWidget",
            Title = "Background Color",
            Description = "Overheat Background Color",
            EnableAlpha = true,
            SelectedColor = Core.Instances.Settings.Overheat_Color,
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            Size = new Vector2(200, 90),
        };

        ColorPickerWidget Overheat_ForeColor_ColorPickerWidget = new ColorPickerWidget
        {
            ID = "Overheat_ForeColor_ColorPickerWidget",
            Title = "Fore Color",
            Description = "Overheat Fore Color",
            EnableAlpha = true,
            SelectedColor = Core.Instances.Settings.Overheat_ForeColor,
            BackgroundColor = new Vector4(0.043f, 0.047f, 0.059f, 1.000f),
            Size = new Vector2(200, 90),
        };


        bool ConfigOverheatOptions = false;

        private void Overheat()
        {
            if (!ConfigOverheatOptions)
            {
                ConfigOverheatOptions = true;

                Overheat_DrawBackground.CheckedChanged += Overheat_Checks;
                Overheat_DrawText.CheckedChanged += Overheat_Checks;
                Overheat_Color_ColorPickerWidget.ColorChanged += Overheat_Checks;
                Overheat_ForeColor_ColorPickerWidget.ColorChanged += Overheat_Checks;
            }
            Overheat_DrawBackground.Render();
            ImGui.SameLine(210);
            Overheat_DrawText.Render();
            ImGui.SameLine(420);
            Overheat_Color_ColorPickerWidget.Render();
            Overheat_ForeColor_ColorPickerWidget.Render();
        }

        public void Overheat_Checks(object sender, EventArgs e)
        {
            if (sender is CheckWidget)
            {
                CheckWidget widget = sender as CheckWidget;
                if (widget.ID == Overheat_DrawBackground.ID)
                {
                    Core.Instances.Settings.Overheat_DrawBackground = widget.Checked;
                }
                else if (widget.ID == Overheat_DrawText.ID)
                {
                    Core.Instances.Settings.Overheat_DrawText = widget.Checked;
                }
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
                if (widget.ID == Overheat_Color_ColorPickerWidget.ID)
                {
                    Core.Instances.Settings.Overheat_Color = widget.SelectedColor;
                }
                else if (widget.ID == Overheat_ForeColor_ColorPickerWidget.ID)
                {
                    Core.Instances.Settings.Overheat_ForeColor = widget.SelectedColor;
                }
            }
        }


        #endregion

        #region " Info "


        bool ConfigInfoOptions = false;

        private void Info()
        {
            if (!ConfigInfoOptions)
            {
                ConfigInfoOptions = true;

            }

        }

        public void Info_Checks(object sender, EventArgs e)
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
                if (widget.ID == Radar_Scale_trackBarWidget.ID)
                {
                    Core.Instances.Settings.Radar_Scale = (int)widget.Value;
                }
            }
            else if (sender is ColorPickerWidget)
            {
                ColorPickerWidget widget = sender as ColorPickerWidget;

            }
        }


        #endregion

    }

}
