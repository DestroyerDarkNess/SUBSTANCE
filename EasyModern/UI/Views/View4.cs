using EasyModern.Core.Model;
using Hexa.NET.ImGui;

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
            ImGui.Text("Esta es la Vista 4");
            ImGui.Separator();
            for (int i = 0; i < 10; i++)
            {
                ImGui.Text($"Elemento de la Vista 4 - #{i + 1}");
            }
        }
    }

}
