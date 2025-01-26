using SharpDX.Direct3D9;
using System;
namespace EasyModern.Core.Model
{
    public abstract class EffectBase
    {
        protected Device device; // Dispositivo DirectX 9
        protected Effect effect; // Efecto cargado
        protected string techniqueName; // Técnica que usará el shader
        protected string shaderPath; // Ruta del shader

        public EffectBase(Device device, string shaderPath, string techniqueName)
        {
            this.device = device;
            this.shaderPath = shaderPath;
            this.techniqueName = techniqueName;
            LoadEffect();
        }

        protected void LoadEffect()
        {
            Console.WriteLine(Environment.NewLine);
            try
            {
                effect = Effect.FromFile(device, shaderPath, ShaderFlags.Debug);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write($"[COMPILED] ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"Shader");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($" '{shaderPath}'");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception ex)
            {

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"[ERROR] ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"Compiling Shader");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($" '{shaderPath}':");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{Environment.NewLine}         {ex.Message} {Environment.NewLine} ");
                Console.ForegroundColor = ConsoleColor.White;
            }

        }

        // Método abstracto que debe ser implementado por cada efecto
        public abstract void Apply(Action renderContent = null);


        public abstract bool IsValid();

        // Limpiar recursos
        public void Dispose()
        {
            effect?.Dispose();
        }
    }
}
