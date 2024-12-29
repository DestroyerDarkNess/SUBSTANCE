using Hexa.NET.ImGui;

namespace EasyModern.Core.Model
{
    /// <summary>
    /// Interfaz base para las vistas.
    /// </summary>
    public interface IView
    {
        string ID { get; set; }
        string Text { get; set; }
        bool Checked { get; set; }
        ImTextureID Icon { get; set; }
        void Render(); // Método para renderizar la vista
    }
}
