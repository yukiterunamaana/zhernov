using System.Collections.Generic;
using Model;
using Newtonsoft.Json;
using UnityEngine;

namespace Config
{
    public class ConfigManager
    {
        public LandscapeConfig[] Landscapes;
        public MapObjectConfig[] Objs;
        public Dictionary<string, BuildingConfig> Buildings;
        public Dictionary<string, int> GameModifiers = new();
        public Dictionary<string, int> GameResources = new();

        private const string ConfigDir = "Configs";
        
        public void Init()
        {
            Landscapes = JsonConvert.DeserializeObject<LandscapeConfig[]>(Resources.Load<TextAsset>($"{ConfigDir}/Landscapes").text);
            Objs = JsonConvert.DeserializeObject<MapObjectConfig[]>(Resources.Load<TextAsset>($"{ConfigDir}/Objects").text);
            var ups = JsonConvert.DeserializeObject<Upgrade[]>(Resources.Load<TextAsset>($"{ConfigDir}/Upgrades").text);
            BuildingConfig.allUpgrades = new();
            foreach (var u in ups)
            {
                BuildingConfig.allUpgrades.Add(u.name_of_change, u);
                if (!GameModifiers.ContainsKey(u.name_of_change))
                {
                    GameModifiers.Add(u.name_of_change, 0);
                }
            }
            GameResources = JsonConvert.DeserializeObject<Dictionary<string, int>>(Resources.Load<TextAsset>($"{ConfigDir}/Resources").text);
            var buildings = JsonConvert.DeserializeObject<BuildingConfig[]>(Resources.Load<TextAsset>($"{ConfigDir}/Buildings").text);
            Buildings = new();
            foreach (var b in buildings)
            {
                Buildings.Add(b.type, b);
            }
        }
    }
}