using Newtonsoft.Json;
using System.IO;
using System.Numerics;

namespace EasyModern.SDK
{
    public class ConfigManager
    {

        [JsonProperty("ShowMenu")]
        public bool ShowMenu { get; set; } = false;

        [JsonProperty("Triggerbot")]
        public bool Triggerbot { get; set; } = false;

        [JsonProperty("Triggerbot_Interval")]
        public float Triggerbot_Interval { get; set; } = 100f;

        [JsonProperty("Triggerbot_Delay")]
        public float Triggerbot_Delay { get; set; } = 100f;

        [JsonProperty("Crosshair")]
        public bool Crosshair { get; set; } = false;

        [JsonProperty("Crosshair_Style")]
        public int Crosshair_Style { get; set; } = 0;

        [JsonProperty("Crosshair_Scale")]
        public float Crosshair_Scale { get; set; } = 1.0f;

        [JsonProperty("Crosshair_Color")]
        public Vector4 Crosshair_Color { get; set; } = new Vector4(1f, 1f, 1f, 1f);

        //ESP


        [JsonProperty("ESP")]
        public bool ESP { get; set; } = false;

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
        public int ESP_VehicleBoxType { get; set; } = 0;

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


        //Aimbot
        [JsonProperty("AIM")]
        public bool AIM { get; set; } = false;

        [JsonProperty("AIM_Visible_Check")]
        public bool AIM_Visible_Check { get; set; } = false;

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
        public int FireRate { get; set; } = 0;

        [JsonProperty("Teleport")]
        public bool Teleport { get; set; } = false;

        [JsonProperty("RCS")]
        public bool RCS { get; set; } = false;

        [JsonProperty("NoSpread")]
        public int NoSpread { get; set; } = 0;

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

        [JsonProperty("JetSpeed")]
        public bool JetSpeed { get; set; } = false;

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
