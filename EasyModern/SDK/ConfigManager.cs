using Newtonsoft.Json;
using System.IO;
using System.Numerics;

namespace EasyModern.SDK
{
    public class ConfigManager
    {
        [JsonProperty("UserName")]
        public string UserName { get; set; } = string.Empty;

        [JsonProperty("AccountName")]
        public string AccountName { get; set; } = string.Empty;

        [JsonProperty("License")]
        public string License { get; set; } = string.Empty;

        [JsonProperty("ShowMenu")]
        public bool ShowMenu { get; set; } = false;

        [JsonProperty("PEMutate")]
        public bool PEMutate { get; set; } = false;

        [JsonProperty("AutoSpot")]
        public bool AutoSpot { get; set; } = true;

        [JsonProperty("AntiScreenShot")]
        public bool AntiScreenShot { get; set; } = false;

        [JsonProperty("AntiAFK")]
        public bool AntiAFK { get; set; } = false;

        [JsonProperty("NodeParticles")]
        public bool NodeParticles { get; set; } = false;

        [JsonProperty("Particles_Draw_Mode")]
        public int Particles_Draw_Mode { get; set; } = 0;

        [JsonProperty("RGB_Color")]
        public bool RGB_Color { get; set; } = false;

        [JsonProperty("RGB_Crosshair_Color")]
        public bool RGB_Crosshair_Color { get; set; } = false;

        [JsonProperty("Triggerbot")]
        public bool Triggerbot { get; set; } = false;

        [JsonProperty("Triggerbot_Interval")]
        public int Triggerbot_Interval { get; set; } = 100;

        [JsonProperty("Triggerbot_Delay")]
        public int Triggerbot_Delay { get; set; } = 100;

        [JsonProperty("Crosshair")]
        public bool Crosshair { get; set; } = false;

        [JsonProperty("Crosshair_Style")]
        public int Crosshair_Style { get; set; } = 0;

        [JsonProperty("Crosshair_Scale")]
        public float Crosshair_Scale { get; set; } = 1.0f;

        [JsonProperty("Crosshair_AnimationSpeed")]
        public float Crosshair_AnimationSpeed { get; set; } = 0.001f;

        [JsonProperty("Crosshair_Color")]
        public Vector4 Crosshair_Color { get; set; } = new Vector4(1f, 1f, 1f, 1f);

        //Menu 

        [JsonProperty("InGameEffects")]
        public bool InGameEffects { get; set; } = false;

        [JsonProperty("FPSLimiter")]
        public bool FPSLimiter { get; set; } = false;

        [JsonProperty("FPSLimiter_Value")]
        public int FPSLimiter_Value { get; set; } = 120;

        [JsonProperty("Theme_ID")]
        public int Theme_ID { get; set; } = 0;

        //Fov

        [JsonProperty("FovChanger")]
        public bool FovChanger { get; set; } = false;

        [JsonProperty("FovChanger_Value")]
        public int FovChanger_Value { get; set; } = 120;


        //ESP


        [JsonProperty("ESP")]
        public bool ESP { get; set; } = false;

        [JsonProperty("ESP_OnlyVisible")]
        public bool ESP_OnlyVisible { get; set; } = false;

        [JsonProperty("ESP_Preview")]
        public bool ESP_Preview { get; set; } = false;

        [JsonProperty("Chams")]
        public bool Chams { get; set; } = false;

        [JsonProperty("ESP_Name")]
        public bool ESP_Name { get; set; } = false;

        [JsonProperty("ESP_Distance")]
        public bool ESP_Distance { get; set; } = false;

        [JsonProperty("ESP_Distance_InName")]
        public bool ESP_Distance_InName { get; set; } = false;

        [JsonProperty("ESP_DrawDistance")]
        public int ESP_DrawDistance { get; set; } = 1000;

        [JsonProperty("ESP_Health")]
        public bool ESP_Health { get; set; } = false;

        [JsonProperty("ESP_Box")]
        public bool ESP_Box { get; set; } = false;


        [JsonProperty("ESP_Vehicle")]
        public bool ESP_Vehicle { get; set; } = false;

        [JsonProperty("ESP_VehicleBoxType")]
        public int ESP_VehicleBoxType { get; set; } = 1;

        [JsonProperty("ESP_Orientation")]
        public int ESP_Orientation { get; set; } = 1;

        [JsonProperty("ESP_Position")]
        public int ESP_Position { get; set; } = 1;

        [JsonProperty("ESP_Enemy")]
        public bool ESP_Enemy { get; set; } = false;

        [JsonProperty("ESP_Bone")]
        public bool ESP_Bone { get; set; } = false;

        [JsonProperty("ESP_BoneDistance")]
        public int ESP_BoneDistance { get; set; } = 1000;

        [JsonProperty("ESP_Line")]
        public bool ESP_Line { get; set; } = false;

        [JsonProperty("ESP_BoxType")]
        public int ESP_BoxType { get; set; } = 0;

        [JsonProperty("ESP_Team_Color")]
        public Vector4 ESP_Team_Color { get; set; } = new Vector4(0.25f, 0.60f, 0.78f, 1.0f);

        [JsonProperty("ESP_Team_Vehicle_Color")]
        public Vector4 ESP_Team_Vehicle_Color { get; set; } = new Vector4(0.25f, 0.60f, 0.78f, 1.0f);

        [JsonProperty("ESP_Team_Skeleton_Color")]
        public Vector4 ESP_Team_Skeleton_Color { get; set; } = new Vector4(0.25f, 0.60f, 0.78f, 0.5f);

        [JsonProperty("ESP_Enemy_Color")]
        public Vector4 ESP_Enemy_Color { get; set; } = new Vector4(1.0f, 0.0f, 0.0f, 0.78f);

        [JsonProperty("ESP_Enemy_Vehicle_Color")]
        public Vector4 ESP_Enemy_Vehicle_Color { get; set; } = new Vector4(1.0f, 0.5f, 0.28f, 0.78f);

        [JsonProperty("ESP_Enemy_Visible_Color")]
        public Vector4 ESP_Enemy_Visible_Color { get; set; } = new Vector4(1.0f, 1.0f, 0.0f, 0.86f);

        [JsonProperty("ESP_Enemy_Skeleton_Color")]
        public Vector4 ESP_Enemy_Skeleton_Color { get; set; } = new Vector4(1.0f, 0.0f, 0.0f, 0.5f);

        [JsonProperty("ESP_Enemy_Line_Color")]
        public Vector4 ESP_Enemy_Line_Color { get; set; } = new Vector4(1.0f, 0.0f, 0.5f, 1.0f);


        //Aimbot
        [JsonProperty("AIM")]
        public bool AIM { get; set; } = false;

        [JsonProperty("AIM_Distance")]
        public int AIM_Distance { get; set; } = 1000;

        [JsonProperty("AIM_ExcludeHead")]
        public bool AIM_ExcludeHead { get; set; } = false;

        [JsonProperty("AIM_SmoothFactor")]
        public int AIM_SmoothFactor { get; set; } = 0;

        [JsonProperty("AIM_OneShot")]
        public bool AIM_OneShot { get; set; } = false;

        [JsonProperty("AIM_Visible_Check")]
        public bool AIM_Visible_Check { get; set; } = false;

        [JsonProperty("AIM_Humanizer")]
        public bool AIM_Humanizer { get; set; } = false;

        [JsonProperty("AIM_Silent")]
        public bool AIM_Silent { get; set; } = false;

        [JsonProperty("AIM_AimAtAll")]
        public bool AIM_AimAtAll { get; set; } = false;

        [JsonProperty("AIM_StickTarget")]
        public bool AIM_StickTarget { get; set; } = false;

        [JsonProperty("AIM_Location")]
        public int AIM_Location { get; set; } = 0;

        [JsonProperty("AIM_Fov")]
        public int AIM_Fov { get; set; } = 5;

        [JsonProperty("AIM_Draw_Fov")]
        public bool AIM_Draw_Fov { get; set; } = false;

        [JsonProperty("AIM_Fov_Color")]
        public Vector4 AIM_Fov_Color { get; set; } = new Vector4(1f, 1f, 1f, 1f);

        [JsonProperty("AIM_Draw_TargetLine")]
        public bool AIM_Draw_TargetLine { get; set; } = false;

        [JsonProperty("AIM_TargetColor")]
        public Vector4 AIM_TargetColor { get; set; } = new Vector4(1.000f, 0.000f, 0.914f, 1.000f);

        [JsonProperty("AIM_Type")]
        public int AIM_Type { get; set; } = 0;

        [JsonProperty("AIM_Driver_First")]
        public bool AIM_Driver_First { get; set; } = false;

        [JsonProperty("AIM_AutoAim")]
        public bool AIM_AutoAim { get; set; } = false;

        [JsonProperty("AIM_Vehicle")]
        public bool AIM_Vehicle { get; set; } = false;

        [JsonProperty("Draw_Info")]
        public bool Draw_Info { get; set; } = false;

        [JsonProperty("OneHitKill")]
        public bool OneHitKill { get; set; } = false;

        [JsonProperty("NoGravity")]
        public bool NoGravity { get; set; } = false;

        [JsonProperty("RateOfFire")]
        public bool RateOfFire { get; set; } = false;

        [JsonProperty("FireRate")]
        public float FireRate { get; set; } = 0.0f;

        [JsonProperty("Teleport")]
        public bool Teleport { get; set; } = false;

        [JsonProperty("RCS")]
        public bool RCS { get; set; } = false;

        [JsonProperty("NoSpread")]
        public bool NoSpread { get; set; } = false;

        [JsonProperty("NoBreath")]
        public bool NoBreath { get; set; } = false;

        [JsonProperty("Radar")]
        public bool Radar { get; set; } = false;

        [JsonProperty("Radar_Scale")]
        public int Radar_Scale { get; set; } = 150;

        [JsonProperty("Overheat")]
        public bool Overheat { get; set; } = false;

        [JsonProperty("Overheat_DrawText")]
        public bool Overheat_DrawText { get; set; } = false;

        [JsonProperty("Overheat_DrawBackground")]
        public bool Overheat_DrawBackground { get; set; } = false;

        [JsonProperty("Overheat_Color")]
        public Vector4 Overheat_Color { get; set; } = new Vector4(0.071f, 0.071f, 0.071f, 1.000f);

        [JsonProperty("Overheat_ForeColor")]
        public Vector4 Overheat_ForeColor { get; set; } = new Vector4(1.000f, 1.000f, 1.000f, 1.000f);

        [JsonProperty("VehicleSpeed")]
        public bool VehicleSpeed { get; set; } = false;

        [JsonProperty("Spectators")]
        public bool Spectators { get; set; } = false;

        [JsonProperty("RedPlayers")]
        public bool RedPlayers { get; set; } = false;

        [JsonProperty("RedPlayers_Alert")]
        public bool RedPlayers_Alert { get; set; } = false;

        [JsonProperty("Draw_Health")]
        public bool Draw_Health { get; set; } = false;

        [JsonProperty("Health_Color")]
        public Vector4 Health_Color { get; set; } = new Vector4(1.000f, 1.000f, 1.000f, 1.000f);

        [JsonProperty("Draw_FPS")]
        public bool Draw_FPS { get; set; } = false;

        [JsonProperty("Bullet_Control")]
        public bool Bullet_Control { get; set; } = false;

        [JsonProperty("VehicleBulletsPershell")]
        public int VehicleBulletsPershell { get; set; } = 0;

        [JsonProperty("VehicleBulletsPerShot")]
        public int VehicleBulletsPerShot { get; set; } = 0;

        [JsonProperty("BulletsPerShell")]
        public int BulletsPerShell { get; set; } = 0;

        [JsonProperty("BulletsPerShot")]
        public int BulletsPerShot { get; set; } = 0;

        public static void SaveConfig(ConfigManager config, string CONFIG_FILE = "config.json")
        {
            string json = JsonConvert.SerializeObject(config);
            File.WriteAllText(CONFIG_FILE, json);
        }

        public static ConfigManager LoadConfig(string CONFIG_FILE = "config.json")
        {
            if (File.Exists(CONFIG_FILE))
            {
                string json = File.ReadAllText(CONFIG_FILE);
                return (ConfigManager)JsonConvert.DeserializeObject(json, typeof(ConfigManager));
            }
            else
            {
                return new ConfigManager();
            }
        }

    }
}
