using EasyModern.Core.Model;
using Hexa.NET.ImGui;
using System.Numerics;

namespace EasyModern.UI.Themes
{
    internal class GruvboxDayNightTheme : ITheme
    {
        public string ID { get; set; } = "theme.gruvbox-daynight";

        // Duración de un ciclo día-noche (en segundos)
        private const float CYCLE_DURATION = 20f;

        // Acumulador de tiempo estático
        private static float dayNightTimer = 0.0f;

        // Arrays que guardan los colores de Gruvbox Light y Gruvbox Dark
        // (ÍNDICE = (int)ImGuiCol.*, valor = Vector4(R,G,B,A)).
        private static readonly Vector4[] s_gruvboxLightColors = new Vector4[(int)ImGuiCol.Count];
        private static readonly Vector4[] s_gruvboxDarkColors = new Vector4[(int)ImGuiCol.Count];

        // Para asegurarnos de inicializar solo una vez, usamos un constructor estático
        static GruvboxDayNightTheme()
        {
            InitializeGruvboxLight();
            InitializeGruvboxDark();
        }

        public bool Apply()
        {
            var style = ImGui.GetStyle();
            var io = ImGui.GetIO();

            // Incrementamos el tiempo
            dayNightTimer += io.DeltaTime;
            // Factor en [0..1]
            float t = (dayNightTimer % CYCLE_DURATION) / CYCLE_DURATION;

            // Interpolamos cada color en la paleta
            for (int i = 0; i < (int)ImGuiCol.Count; i++)
            {
                Vector4 lightColor = s_gruvboxLightColors[i];
                Vector4 darkColor = s_gruvboxDarkColors[i];
                style.Colors[i] = Lerp(lightColor, darkColor, t);
            }

            // Opcional: Ajustes de estilo (bordes, rounding, etc.) 
            style.WindowRounding = 6.0f;
            style.FrameRounding = 4.0f;
            style.ScrollbarRounding = 6.0f;
            style.GrabRounding = 4.0f;
            style.TabRounding = 4.0f;

            style.WindowPadding = new Vector2(10.0f, 10.0f);
            style.FramePadding = new Vector2(5.0f, 3.0f);
            style.ItemSpacing = new Vector2(8.0f, 5.0f);
            style.ItemInnerSpacing = new Vector2(6.0f, 4.0f);

            style.Alpha = 1.0f;
            style.DisabledAlpha = 0.60f;

            return true;
        }

        // Interpolación lineal entre dos Vector4
        private static Vector4 Lerp(Vector4 a, Vector4 b, float t)
        {
            return new Vector4(
                a.X + (b.X - a.X) * t,
                a.Y + (b.Y - a.Y) * t,
                a.Z + (b.Z - a.Z) * t,
                a.W + (b.W - a.W) * t
            );
        }

        // ------------------------------
        // Carga de Gruvbox Light
        // ------------------------------
        private static void InitializeGruvboxLight()
        {
            // Ejemplo tomado (y reducido) de "Gruvbox Light"
            // Ajusta estos valores según tu propia implementación
            s_gruvboxLightColors[(int)ImGuiCol.Text] = new Vector4(0.235f, 0.219f, 0.214f, 1.00f); // #3c3836
            s_gruvboxLightColors[(int)ImGuiCol.TextDisabled] = new Vector4(0.31f, 0.28f, 0.27f, 1.00f);
            s_gruvboxLightColors[(int)ImGuiCol.WindowBg] = new Vector4(0.984f, 0.945f, 0.78f, 1.00f); // #fbf1c7
            s_gruvboxLightColors[(int)ImGuiCol.ChildBg] = new Vector4(0.984f, 0.945f, 0.78f, 1.00f);
            s_gruvboxLightColors[(int)ImGuiCol.PopupBg] = new Vector4(0.92f, 0.86f, 0.70f, 0.95f);
            if (!Core.Instances.Settings.RGB_Color) s_gruvboxLightColors[(int)ImGuiCol.Border] = new Vector4(0.84f, 0.77f, 0.63f, 1.00f);
            s_gruvboxLightColors[(int)ImGuiCol.FrameBg] = new Vector4(0.92f, 0.86f, 0.70f, 1.00f);
            // ... Repite para TODOS los ImGuiCol.* relevantes
            // (Header, Button, etc.)

            // Para los que no definamos aquí, quedarán en (0,0,0,0), 
            // así que es preferible definir todos para mayor consistencia.
        }

        // ------------------------------
        // Carga de Gruvbox Dark
        // ------------------------------
        private static void InitializeGruvboxDark()
        {
            // Ejemplo tomado (y reducido) de "Gruvbox Dark"
            s_gruvboxDarkColors[(int)ImGuiCol.Text] = new Vector4(0.92f, 0.86f, 0.70f, 1.00f); // #ebdbb2
            s_gruvboxDarkColors[(int)ImGuiCol.TextDisabled] = new Vector4(0.74f, 0.68f, 0.57f, 1.00f);
            s_gruvboxDarkColors[(int)ImGuiCol.WindowBg] = new Vector4(0.16f, 0.16f, 0.16f, 1.00f); // #282828
            s_gruvboxDarkColors[(int)ImGuiCol.ChildBg] = new Vector4(0.16f, 0.16f, 0.16f, 1.00f);
            s_gruvboxDarkColors[(int)ImGuiCol.PopupBg] = new Vector4(0.235f, 0.219f, 0.214f, 0.95f);
            if (!Core.Instances.Settings.RGB_Color) s_gruvboxDarkColors[(int)ImGuiCol.Border] = new Vector4(0.314f, 0.289f, 0.277f, 1.00f);
            s_gruvboxDarkColors[(int)ImGuiCol.FrameBg] = new Vector4(0.235f, 0.219f, 0.214f, 1.00f);
            // ... Lo mismo, repitiendo para todos los campos que quieras cubrir.
        }
    }
}
