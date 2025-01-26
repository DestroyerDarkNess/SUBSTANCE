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
        //public bool Checked { get; set; } = false;

        private bool _checked = false;

        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; if (!value) ResetOptions = true; }
        }

        public ImTextureID Icon { get; set; }


        public List<FunctionWidget> Widgets = new List<FunctionWidget>();

        public string currentOption = "func.esp";

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

        FunctionWidget ESP_Cheat = new FunctionWidget
        {
            ID = "func.esp",
            Title = $"ESP",
            Description = $"Extra Sensory Perception.",
            Checked = Core.Instances.Settings.ESP,
            Size = new Vector2(200, 100),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            BorderPercent = 0.3f,
            Animating = true,
            BottomRightIconName = "config_icon",
            IconButtonRounding = 1.0f,
            IconButtonSize = 15.0f,
        };

        FunctionWidget Chams_Cheat = new FunctionWidget
        {
            ID = "func.chams",
            Title = $"Chams",
            Description = $"Enable Model Chams",
            Checked = Core.Instances.Settings.Chams,
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

        FunctionWidget Crosshair_Cheat = new FunctionWidget
        {
            ID = "func.crosshair",
            Title = $"Crosshair",
            Description = $"Visual Indicator on the player's screen that represents the point of aim for their weapon.",
            Checked = Core.Instances.Settings.Crosshair,
            Size = new Vector2(200, 100),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
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
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
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
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
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
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
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
            Widgets.Add(Chams_Cheat);
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
                    else if (senderWidget.ID == Chams_Cheat.ID)
                    {
                        Core.Instances.Settings.Chams = widget.Checked;
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


            ImGui.BeginGroup();
            ImGui.Dummy(new Vector2(0, 1));
            ImGui.BeginChild("RightSectionItems", new Vector2(ImGui.GetWindowSize().X - /*marginX*/ 4.0f, 0));

            if (ResetOptions && this.Checked)
            {
                ResetOptions = false;
                ImGui.SetScrollHereY(0.0f);
            }

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
            ImGui.EndGroup();

            ImGui.EndChild();
        }

        #region " ESP "

        CheckWidget ESP_Preview_CheckWidget = new CheckWidget
        {
            ID = "ESP_Preview_check",
            Title = "Preview",
            Description = "Show ESP Preview Window",
            Checked = Core.Instances.Settings.ESP_Preview,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.ESP_Preview ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.ESP_Preview ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };

        CheckWidget ESP_OnlyVisible_CheckWidget = new CheckWidget
        {
            ID = "ESP_OnlyVisible_check",
            Title = "Only Visible",
            Description = "ESP only works on Visible players (at the request of a streamer)",
            Checked = Core.Instances.Settings.ESP_OnlyVisible,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.ESP_OnlyVisible ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.ESP_OnlyVisible ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };

        CheckWidget ESP_Name_CheckWidget = new CheckWidget
        {
            ID = "ESP_Name_Check_check",
            Title = "Name",
            Description = "Displays the name of the players.",
            Checked = Core.Instances.Settings.ESP_Name,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.ESP_Name ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.ESP_Name ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };

        CheckWidget ESP_Distance_CheckWidget = new CheckWidget
        {
            ID = "ESP_Distance_Check_check",
            Title = "Distance",
            Description = "Displays the distance of the players.",
            Checked = Core.Instances.Settings.ESP_Distance,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.ESP_Distance ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.ESP_Distance ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };

        CheckWidget ESP_Distance_InName_CheckWidget = new CheckWidget
        {
            ID = "ESP_Distance_InName_Check_check",
            Title = "Distance In Name",
            Description = "Displays the distance in the name of the players.",
            Checked = Core.Instances.Settings.ESP_Distance_InName,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.ESP_Distance_InName ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.ESP_Distance_InName ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };

        CheckWidget ESP_Health_CheckWidget = new CheckWidget
        {
            ID = "ESP_Health_Check_check",
            Title = "Health",
            Description = "Displays the health of the players.",
            Checked = Core.Instances.Settings.ESP_Health,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.ESP_Health ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.ESP_Health ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };

        CheckWidget ESP_Box_CheckWidget = new CheckWidget
        {
            ID = "ESP_Box_Check_check",
            Title = "Box",
            Description = "Draw ESP Box",
            Checked = Core.Instances.Settings.ESP_Box,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.ESP_Box ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.ESP_Box ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };

        CheckWidget ESP_Vehicle_CheckWidget = new CheckWidget
        {
            ID = "ESP_Vehicle_Check_check",
            Title = "Vehicle",
            Description = "Draw Vehicle ESP",
            Checked = Core.Instances.Settings.ESP_Vehicle,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.ESP_Vehicle ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.ESP_Vehicle ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };

        CheckWidget ESP_Enemy_CheckWidget = new CheckWidget
        {
            ID = "ESP_Enemy_Check_check",
            Title = "Only Enemy",
            Description = "Draw Enemy ESP",
            Checked = Core.Instances.Settings.ESP_Enemy,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.ESP_Enemy ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.ESP_Enemy ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };

        CheckWidget ESP_Bone_CheckWidget = new CheckWidget
        {
            ID = "ESP_Bone_Check_check",
            Title = "Bone",
            Description = "Draw Bone ESP",
            Checked = Core.Instances.Settings.ESP_Bone,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.ESP_Bone ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.ESP_Bone ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };

        CheckWidget ESP_Line_CheckWidget = new CheckWidget
        {
            ID = "ESP_Line_Check_check",
            Title = "Line",
            Description = "Draw Line ESP",
            Checked = Core.Instances.Settings.ESP_Line,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.ESP_Line ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.ESP_Line ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };


        ComboBoxWidget ESP_BoxType_ComboWidget = new ComboBoxWidget
        {
            ID = "ESP_BoxType_combo_1",
            Title = "Box Type",
            Description = "Visual representation of the information provided about other players.",
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            ComboBoxItems = new string[] { "2D", "2D Target", "3D" },
            SelectedIndex = Core.Instances.Settings.ESP_BoxType,
            Size = new Vector2(200, 90)
        };


        ComboBoxWidget ESP_VehicleBoxType_ComboWidget = new ComboBoxWidget
        {
            ID = "ESP_VehicleBoxType_combo_1",
            Title = "Vehicle Box Type",
            Description = "Visual representation of the information provided about other Vehicle.",
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            ComboBoxItems = new string[] { "2D", "3D" },
            SelectedIndex = Core.Instances.Settings.ESP_VehicleBoxType,
            Size = new Vector2(200, 90)
        };

        ComboBoxWidget ESP_Orientation_ComboWidget = new ComboBoxWidget
        {
            ID = "ESP_Orientation_combo_1",
            Title = "Health Orientation",
            Description = "Orientation of ESP Health Bar.",
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            ComboBoxItems = new string[] { "Horizontal", "Vertical" },
            SelectedIndex = Core.Instances.Settings.ESP_Orientation,
            Size = new Vector2(200, 90)
        };


        ComboBoxWidget ESP_Position_ComboWidget = new ComboBoxWidget
        {
            ID = "ESP_Position_combo_1",
            Title = "Health Position",
            Description = "Orientation of ESP Health Bar.",
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            ComboBoxItems = new string[] { "Top", "Bottom", "Left", "Right" },
            SelectedIndex = Core.Instances.Settings.ESP_Position,
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
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
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
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90),
        };

        // ---------------------------------------------

        ColorPickerWidget EspTeam_Color_ColorPickerWidget = new ColorPickerWidget
        {
            ID = "ESP_Team_Color_ColorPickerWidget",
            Title = "Team Color",
            Description = "ESP Team Color",
            EnableAlpha = true,
            SelectedColor = Core.Instances.Settings.ESP_Team_Color,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90),
        };

        ColorPickerWidget ESP_Team_Vehicle_Color_ColorPickerWidget = new ColorPickerWidget
        {
            ID = "ESP_Team_Vehicle_Color_ColorPickerWidget",
            Title = "Team Vehicle Color",
            Description = "ESP Team Vehicle Color",
            EnableAlpha = true,
            SelectedColor = Core.Instances.Settings.ESP_Team_Vehicle_Color,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90),
        };

        ColorPickerWidget ESP_Team_Skeleton_Color_ColorPickerWidget = new ColorPickerWidget
        {
            ID = "ESP_Team_Skeleton_Color_ColorPickerWidget",
            Title = "Team Skeleton Color",
            Description = "ESP Team Skeleton Color",
            EnableAlpha = true,
            SelectedColor = Core.Instances.Settings.ESP_Team_Skeleton_Color,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90),
        };

        ColorPickerWidget ESP_Enemy_Color_ColorPickerWidget = new ColorPickerWidget
        {
            ID = "ESP_Enemy_Color_ColorPickerWidget",
            Title = "Enemy Color",
            Description = "ESP Enemy Color",
            EnableAlpha = true,
            SelectedColor = Core.Instances.Settings.ESP_Enemy_Color,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90),
        };

        ColorPickerWidget ESP_Enemy_Vehicle_Color_ColorPickerWidget = new ColorPickerWidget
        {
            ID = "ESP_Enemy_Vehicle_Color_ColorPickerWidget",
            Title = "Enemy Vehicle Color",
            Description = "ESP Enemy Vehicle Color",
            EnableAlpha = true,
            SelectedColor = Core.Instances.Settings.ESP_Enemy_Vehicle_Color,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90),
        };

        ColorPickerWidget ESP_Enemy_Visible_Color_ColorPickerWidget = new ColorPickerWidget
        {
            ID = "ESP_Enemy_Visible_Color_ColorPickerWidget",
            Title = "Enemy Visible Color",
            Description = "ESP Enemy Visible Color",
            EnableAlpha = true,
            SelectedColor = Core.Instances.Settings.ESP_Enemy_Visible_Color,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90),
        };

        ColorPickerWidget ESP_Enemy_Skeleton_Color_ColorPickerWidget = new ColorPickerWidget
        {
            ID = "ESP_Enemy_Skeleton_Color_ColorPickerWidget",
            Title = "Enemy Skeleton Color",
            Description = "ESP Enemy Skeleton Color",
            EnableAlpha = true,
            SelectedColor = Core.Instances.Settings.ESP_Enemy_Skeleton_Color,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90),
        };

        ColorPickerWidget ESP_Enemy_Line_Color_ColorPickerWidget = new ColorPickerWidget
        {
            ID = "ESP_Enemy_Line_Color_ColorPickerWidget",
            Title = "Enemy Line Color",
            Description = "ESP Enemy Line Color",
            EnableAlpha = true,
            SelectedColor = Core.Instances.Settings.ESP_Enemy_Line_Color,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90),
        };

        bool ConfigESPOptions = false;

        private void ESP()
        {
            if (!ConfigESPOptions)
            {
                ConfigESPOptions = true;

                ESP_Preview_CheckWidget.CheckedChanged += ESP_Checks;
                ESP_OnlyVisible_CheckWidget.CheckedChanged += ESP_Checks;
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
                ESP_VehicleBoxType_ComboWidget.SelectedIndexChanged += ESP_Checks;
                ESP_Orientation_ComboWidget.SelectedIndexChanged += ESP_Checks;
                ESP_Position_ComboWidget.SelectedIndexChanged += ESP_Checks;
                ESP_DrawDistance_trackBarWidget.ValueChanged += ESP_Checks;
                ESP_BoneDistance_trackBarWidget.ValueChanged += ESP_Checks;

                //Colores
                EspTeam_Color_ColorPickerWidget.ColorChanged += ESP_Checks;
                ESP_Team_Vehicle_Color_ColorPickerWidget.ColorChanged += ESP_Checks;
                ESP_Team_Skeleton_Color_ColorPickerWidget.ColorChanged += ESP_Checks;

                ESP_Enemy_Color_ColorPickerWidget.ColorChanged += ESP_Checks;
                ESP_Enemy_Vehicle_Color_ColorPickerWidget.ColorChanged += ESP_Checks;
                ESP_Enemy_Visible_Color_ColorPickerWidget.ColorChanged += ESP_Checks;
                ESP_Enemy_Skeleton_Color_ColorPickerWidget.ColorChanged += ESP_Checks;
                ESP_Enemy_Line_Color_ColorPickerWidget.ColorChanged += ESP_Checks;
            }

            ESP_Preview_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; ESP_Preview_CheckWidget.Render();
            ImGui.SameLine(210);
            ESP_OnlyVisible_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; ESP_OnlyVisible_CheckWidget.Render();
            ImGui.SameLine(420);
            ESP_Name_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; ESP_Name_CheckWidget.Render();

            ESP_Distance_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; ESP_Distance_CheckWidget.Render();
            ImGui.SameLine(210);
            ESP_Distance_InName_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; ESP_Distance_InName_CheckWidget.Render();
            ImGui.SameLine(420);
            ESP_Health_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; ESP_Health_CheckWidget.Render();

            ESP_Box_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; ESP_Box_CheckWidget.Render();
            ImGui.SameLine(210);
            ESP_Vehicle_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; ESP_Vehicle_CheckWidget.Render();
            ImGui.SameLine(420);
            ESP_Enemy_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; ESP_Enemy_CheckWidget.Render();

            ESP_Bone_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; ESP_Bone_CheckWidget.Render();
            ImGui.SameLine(210);
            ESP_Line_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; ESP_Line_CheckWidget.Render();
            ImGui.SameLine(420);
            ESP_BoxType_ComboWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; ESP_BoxType_ComboWidget.Render();

            ESP_VehicleBoxType_ComboWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; ESP_VehicleBoxType_ComboWidget.Render();
            ImGui.SameLine(210);
            ESP_Orientation_ComboWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; ESP_Orientation_ComboWidget.Render();
            ImGui.SameLine(420);
            ESP_Position_ComboWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; ESP_Position_ComboWidget.Render();

            ESP_DrawDistance_trackBarWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; ESP_DrawDistance_trackBarWidget.Render();
            ImGui.SameLine(210);
            ESP_BoneDistance_trackBarWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; ESP_BoneDistance_trackBarWidget.Render();
            ImGui.SameLine(420);
            EspTeam_Color_ColorPickerWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; EspTeam_Color_ColorPickerWidget.Render();

            ESP_Team_Vehicle_Color_ColorPickerWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; ESP_Team_Vehicle_Color_ColorPickerWidget.Render();
            ImGui.SameLine(210);
            ESP_Team_Skeleton_Color_ColorPickerWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; ESP_Team_Skeleton_Color_ColorPickerWidget.Render();
            ImGui.SameLine(420);
            ESP_Enemy_Color_ColorPickerWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; ESP_Enemy_Color_ColorPickerWidget.Render();

            ESP_Enemy_Vehicle_Color_ColorPickerWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; ESP_Enemy_Vehicle_Color_ColorPickerWidget.Render();
            ImGui.SameLine(210);
            ESP_Enemy_Visible_Color_ColorPickerWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; ESP_Enemy_Visible_Color_ColorPickerWidget.Render();
            ImGui.SameLine(420);
            ESP_Enemy_Skeleton_Color_ColorPickerWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; ESP_Enemy_Skeleton_Color_ColorPickerWidget.Render();

            //ESP_Enemy_Line_Color_ColorPickerWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; ESP_Enemy_Line_Color_ColorPickerWidget.Render();
        }

        public void ESP_Checks(object sender, EventArgs e)
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

                if (widget.ID == ESP_Preview_CheckWidget.ID)
                {
                    Core.Instances.Settings.ESP_Preview = widget.Checked;
                }
                else if (widget.ID == ESP_OnlyVisible_CheckWidget.ID)
                {
                    Core.Instances.Settings.ESP_OnlyVisible = widget.Checked;
                }
                else if (widget.ID == ESP_Name_CheckWidget.ID)
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
                else if (widget.ID == ESP_VehicleBoxType_ComboWidget.ID)
                {
                    Core.Instances.Settings.ESP_VehicleBoxType = widget.SelectedIndex;
                }
                else if (widget.ID == ESP_Orientation_ComboWidget.ID)
                {
                    Core.Instances.Settings.ESP_Orientation = widget.SelectedIndex;
                }
                else if (widget.ID == ESP_Position_ComboWidget.ID)
                {
                    Core.Instances.Settings.ESP_Position = widget.SelectedIndex;
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
            else if (sender is ColorPickerWidget)
            {
                ColorPickerWidget widget = sender as ColorPickerWidget;
                if (widget.ID == EspTeam_Color_ColorPickerWidget.ID)
                {
                    Core.Instances.Settings.ESP_Team_Color = widget.SelectedColor;
                }
                else if (widget.ID == ESP_Team_Vehicle_Color_ColorPickerWidget.ID)
                {
                    Core.Instances.Settings.ESP_Team_Vehicle_Color = widget.SelectedColor;
                }
                else if (widget.ID == ESP_Team_Skeleton_Color_ColorPickerWidget.ID)
                {
                    Core.Instances.Settings.ESP_Team_Skeleton_Color = widget.SelectedColor;
                }
                else if (widget.ID == ESP_Enemy_Color_ColorPickerWidget.ID)
                {
                    Core.Instances.Settings.ESP_Enemy_Color = widget.SelectedColor;
                }
                else if (widget.ID == ESP_Enemy_Vehicle_Color_ColorPickerWidget.ID)
                {
                    Core.Instances.Settings.ESP_Enemy_Vehicle_Color = widget.SelectedColor;
                }
                else if (widget.ID == ESP_Enemy_Visible_Color_ColorPickerWidget.ID)
                {
                    Core.Instances.Settings.ESP_Enemy_Visible_Color = widget.SelectedColor;
                }
                else if (widget.ID == ESP_Enemy_Skeleton_Color_ColorPickerWidget.ID)
                {
                    Core.Instances.Settings.ESP_Enemy_Skeleton_Color = widget.SelectedColor;
                }
                else if (widget.ID == ESP_Enemy_Line_Color_ColorPickerWidget.ID)
                {
                    Core.Instances.Settings.ESP_Enemy_Line_Color = widget.SelectedColor;
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
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            ComboBoxItems = new string[] { "Cross", "Box", "Triangle", "Linear", "Hexagram", "Text", "Point", "Sharingan" },
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
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90)
        };

        ColorPickerWidget Crosshair_Color_ColorPickerWidget = new ColorPickerWidget
        {
            ID = "Crosshair_Color_ColorPickerWidget",
            Title = "Color",
            Description = "Crosshair Color",
            EnableAlpha = true,
            SelectedColor = Core.Instances.Settings.Crosshair_Color,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90)
        };

        CheckWidget Crosshair_RGB_CheckWidget = new CheckWidget
        {
            ID = "Crosshair_RGB_check",
            Title = "RGB Color",
            Description = "rgb colors to Crosshair",
            Checked = Core.Instances.Settings.RGB_Crosshair_Color,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = false,
            BottomRightIconName = Core.Instances.Settings.RGB_Crosshair_Color ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.RGB_Crosshair_Color ? ImGui.GetStyle().Colors[(int)ImGuiCol.Button] : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered],
            BorderPercent = 1f
        };

        TrackBarWidget Crosshair_AnimationSpeed_trackBarWidget = new TrackBarWidget
        {
            ID = "AnimationSpeed_trackBarWidget",
            Title = "Animation Speed",
            Description = "Adjusts the animation speed for animated crosshairs",
            Minimum = 0.000f,
            Maximum = 1.000f,
            Value = Core.Instances.Settings.Crosshair_AnimationSpeed,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90)
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
                Crosshair_RGB_CheckWidget.CheckedChanged += Crosshair_Checks;
                Crosshair_AnimationSpeed_trackBarWidget.ValueChanged += Crosshair_Checks;
            }

            Crosshair_Style_ComboWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; Crosshair_Style_ComboWidget.Render();
            ImGui.SameLine(210);
            Crosshair_Scale_trackBarWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; Crosshair_Scale_trackBarWidget.Render();
            ImGui.SameLine(420);
            Crosshair_Color_ColorPickerWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; Crosshair_Color_ColorPickerWidget.Render();

            Crosshair_RGB_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; Crosshair_RGB_CheckWidget.Render();
            ImGui.SameLine(210);
            Crosshair_AnimationSpeed_trackBarWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; Crosshair_AnimationSpeed_trackBarWidget.Render();

        }

        public void Crosshair_Checks(object sender, EventArgs e)
        {
            if (sender is CheckWidget)
            {
                CheckWidget widget = sender as CheckWidget;

                if (widget.ID == Crosshair_RGB_CheckWidget.ID)
                {
                    Core.Instances.Settings.RGB_Crosshair_Color = widget.Checked;
                }
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
                else if (widget.ID == Crosshair_AnimationSpeed_trackBarWidget.ID)
                {
                    Core.Instances.Settings.Crosshair_AnimationSpeed = widget.Value;
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
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
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

            Radar_Scale_trackBarWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; Radar_Scale_trackBarWidget.Render();
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
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
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
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
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
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            Size = new Vector2(200, 90),
        };

        ColorPickerWidget Overheat_ForeColor_ColorPickerWidget = new ColorPickerWidget
        {
            ID = "Overheat_ForeColor_ColorPickerWidget",
            Title = "Fore Color",
            Description = "Overheat Fore Color",
            EnableAlpha = true,
            SelectedColor = Core.Instances.Settings.Overheat_ForeColor,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
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
            Overheat_DrawBackground.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; Overheat_DrawBackground.Render();
            ImGui.SameLine(210);
            Overheat_DrawText.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; Overheat_DrawText.Render();
            ImGui.SameLine(420);
            Overheat_Color_ColorPickerWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; Overheat_Color_ColorPickerWidget.Render();
            Overheat_ForeColor_ColorPickerWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; Overheat_ForeColor_ColorPickerWidget.Render();
        }

        public void Overheat_Checks(object sender, EventArgs e)
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

        CheckWidget VehicleSpeed_CheckWidget = new CheckWidget
        {
            ID = "VehicleSpeed_Check_check",
            Title = "Vehicle Speed",
            Description = "Show VehicleSpeed HUD",
            Checked = Core.Instances.Settings.VehicleSpeed,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.VehicleSpeed ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.VehicleSpeed ? new Vector4(0.439f, 0.698f, 0.675f, 1.000f) : new Vector4(1.000f, 0.490f, 0.592f, 1.000f),
            BorderPercent = 1f
        };

        CheckWidget Spectators_CheckWidget = new CheckWidget
        {
            ID = "Spectators_check",
            Title = "Spectators",
            Description = "Displays warning about spectators",
            Checked = Core.Instances.Settings.Spectators,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.Spectators ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.Spectators ? new Vector4(0.439f, 0.698f, 0.675f, 1.000f) : new Vector4(1.000f, 0.490f, 0.592f, 1.000f),
            BorderPercent = 1f
        };

        CheckWidget Draw_Health_CheckWidget = new CheckWidget
        {
            ID = "Draw_Health_check",
            Title = "Draw Health",
            Description = "Draw Health Bar",
            Checked = Core.Instances.Settings.Draw_Health,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.Draw_Health ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.Draw_Health ? new Vector4(0.439f, 0.698f, 0.675f, 1.000f) : new Vector4(1.000f, 0.490f, 0.592f, 1.000f),
            BorderPercent = 1f
        };

        ColorPickerWidget Health_ColorPickerWidget = new ColorPickerWidget
        {
            Title = "Health Color",
            Description = "Health Color",
            EnableAlpha = true,
            Size = new Vector2(200, 90),
            SelectedColor = Core.Instances.Settings.Health_Color,
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
        };

        CheckWidget RedPlayers_CheckWidget = new CheckWidget
        {
            ID = "RedPlayers_check",
            Title = "RedPlayers",
            Description = "Displays List about potential cheaters.",
            Checked = Core.Instances.Settings.RedPlayers,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.RedPlayers ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.RedPlayers ? new Vector4(0.439f, 0.698f, 0.675f, 1.000f) : new Vector4(1.000f, 0.490f, 0.592f, 1.000f),
            BorderPercent = 1f
        };

        CheckWidget RedPlayers_Alert_CheckWidget = new CheckWidget
        {
            ID = "RedPlayers_Alert_check",
            Title = "RedPlayers Alert",
            Description = "Displays Alert about potential cheaters.",
            Checked = Core.Instances.Settings.RedPlayers_Alert,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.RedPlayers_Alert ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.RedPlayers_Alert ? new Vector4(0.439f, 0.698f, 0.675f, 1.000f) : new Vector4(1.000f, 0.490f, 0.592f, 1.000f),
            BorderPercent = 1f
        };

        CheckWidget Draw_FPS_CheckWidget = new CheckWidget
        {
            ID = "Draw_FPS_check",
            Title = "Draw FPS",
            Description = "Draw Overlay Framerate",
            Checked = Core.Instances.Settings.Draw_FPS,
            Size = new Vector2(200, 90),
            BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg],
            TitleColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Text],
            DescriptionColor = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled],
            BorderColor = ImGui.GetStyle().Colors[(int)ImGuiCol.Border],
            IconButtonVisible = true,
            BottomRightIconName = Core.Instances.Settings.Draw_FPS ? "check" : "uncheck",
            BottomRightIconBgColor = Core.Instances.Settings.Draw_FPS ? new Vector4(0.439f, 0.698f, 0.675f, 1.000f) : new Vector4(1.000f, 0.490f, 0.592f, 1.000f),
            BorderPercent = 1f
        };

        bool ConfigInfoOptions = false;

        private void Info()
        {
            if (!ConfigInfoOptions)
            {
                ConfigInfoOptions = true;

                VehicleSpeed_CheckWidget.CheckedChanged += Info_Checks;
                Spectators_CheckWidget.CheckedChanged += Info_Checks;
                Draw_Health_CheckWidget.CheckedChanged += Info_Checks;
                Health_ColorPickerWidget.ColorChanged += Info_Checks;
                RedPlayers_CheckWidget.CheckedChanged += Info_Checks;
                RedPlayers_Alert_CheckWidget.CheckedChanged += Info_Checks;
                Draw_FPS_CheckWidget.CheckedChanged += Info_Checks;
            }

            VehicleSpeed_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; VehicleSpeed_CheckWidget.Render();
            ImGui.SameLine(210);
            Spectators_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; Spectators_CheckWidget.Render();
            ImGui.SameLine(420);
            Draw_Health_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; Draw_Health_CheckWidget.Render();

            Health_ColorPickerWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; Health_ColorPickerWidget.Render();
            ImGui.SameLine(210);
            RedPlayers_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; RedPlayers_CheckWidget.Render();
            ImGui.SameLine(420);
            RedPlayers_Alert_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; RedPlayers_Alert_CheckWidget.Render();

            Draw_FPS_CheckWidget.BackgroundColor = ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg]; Draw_FPS_CheckWidget.Render();
        }

        public void Info_Checks(object sender, EventArgs e)
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

                if (widget.ID == VehicleSpeed_CheckWidget.ID)
                {
                    Core.Instances.Settings.VehicleSpeed = widget.Checked;
                }
                else if (widget.ID == Spectators_CheckWidget.ID)
                {
                    Core.Instances.Settings.Spectators = widget.Checked;
                }
                else if (widget.ID == Draw_Health_CheckWidget.ID)
                {
                    Core.Instances.Settings.Draw_Health = widget.Checked;
                }
                else if (widget.ID == RedPlayers_CheckWidget.ID)
                {
                    Core.Instances.Settings.RedPlayers = widget.Checked;
                }
                else if (widget.ID == RedPlayers_Alert_CheckWidget.ID)
                {
                    Core.Instances.Settings.RedPlayers_Alert = widget.Checked;
                }
                else if (widget.ID == Draw_FPS_CheckWidget.ID)
                {
                    Core.Instances.Settings.Draw_FPS = widget.Checked;
                }
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

                if (widget.ID == Health_ColorPickerWidget.ID)
                {
                    Core.Instances.Settings.Health_Color = widget.SelectedColor;
                }
            }
        }


        #endregion

    }

}
