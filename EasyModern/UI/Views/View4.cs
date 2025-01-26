using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Views
{
    public class View4 : IView
    {
        public string ID { get; set; } = "view4";
        public string Text { get; set; } = "About";
        public bool Checked { get; set; } = false;
        public ImTextureID Icon { get; set; }
        public void Render()
        {
            Vector2 windowSize = ImGui.GetIO().DisplaySize;

            float leftSectionWidth = 220.0f;
            ImGui.BeginChild("LeftSection", new Vector2(leftSectionWidth, 0));

            float topMargin = 10.0f;
            float bottomMargin = 10.0f;

            ImGui.SetCursorPosY(ImGui.GetCursorPosY() + topMargin);

            ImTextureID app_loco = Core.Instances.ImageManager.GetImage("app_logo");
            if (!app_loco.IsNull)
            {
                Vector2 imageSize = new Vector2(200, 200);
                ImGui.Image(app_loco, imageSize);
            }

            ImGui.Dummy(new Vector2(0, bottomMargin));

            ImGui.EndChild();

            ImGui.SameLine();
            ImGui.BeginChild("RightSection", new Vector2(windowSize.X - leftSectionWidth, 0));

            float marginY = 10.0f; // Sangría vertical

            Vector2 childStartPos = ImGui.GetCursorPos();
            ImGui.SetCursorPos(new Vector2(0, childStartPos.Y + marginY));


            ImGui.Text("Created By Destroyer | Discord: Destroyer#8328");
            ImGui.Separator();
            ImGui.Separator();
            ImGui.Text("Github https://github.com/DestroyerDarkNess");
            ImGui.Separator();
            ImGui.Separator();
            ImGui.Text("Copyright ©  2024 - All rights reserved.");
            ImGui.EndChild();
        }
    }

}
