using EasyModern.Core.Model;
using Hexa.NET.ImGui;

namespace EasyModern.UI.Views
{
    public class View3 : IView
    {
        public string ID { get; set; } = "view3";
        public string Text { get; set; } = "Misc";
        public bool Checked { get; set; } = false;
        public ImTextureID Icon { get; set; }
        public void Render()
        {
            ImGui.Text("Esta es la Vista 3");
            ImGui.Separator();
            for (int i = 0; i < 10; i++)
            {
                ImGui.Text($"Elemento de la Vista 3 - #{i + 1}");
            }
        }
    }
}
