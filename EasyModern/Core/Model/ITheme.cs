namespace EasyModern.Core.Model
{
    /// <summary>
    /// Interfaz base para las vistas.
    /// </summary>
    public interface ITheme
    {
        string ID { get; set; }
        bool Apply();
    }
}
