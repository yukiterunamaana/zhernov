using System.Collections.Generic;
using Model;
using Newtonsoft.Json;
using UnityEngine;

namespace Config
{
    public class ConfigManager
    {
        public Dictionary<string, LandscapeConfig> Landscapes = new();
        public MapObjectConfig[] Objs;
        public Dictionary<string, BuildingConfig> Buildings;
        public Dictionary<string, int> GameModifiers = new();
        public Dictionary<string, int> GameResources = new();
        public Dictionary<string, Sprite> Sprites;

        private const string ConfigDir = "Configs";
        
        public void Init()
        {
            Landscapes = new();
            var Ls = JsonConvert.DeserializeObject<LandscapeConfig[]>(Resources.Load<TextAsset>($"{ConfigDir}/Landscapes").text);
            foreach (var l in Ls)
            {
                Landscapes.Add(l.type, l);
            }
            Objs = JsonConvert.DeserializeObject<MapObjectConfig[]>(Resources.Load<TextAsset>($"{ConfigDir}/Objects").text);
            var ups = JsonConvert.DeserializeObject<Upgrade[]>(Resources.Load<TextAsset>($"{ConfigDir}/Upgrades").text);
            BuildingConfig.allUpgrades = new();
            foreach (var u in ups)
            {
                BuildingConfig.allUpgrades.Add(u.id, u);
            }
            var gms = JsonConvert.DeserializeObject<Modifier[]>(Resources.Load<TextAsset>($"{ConfigDir}/Modifiers").text);
            foreach (var m in gms)
            {
                GameModifiers.Add(m.name, m.initial);
            }
            GameResources = JsonConvert.DeserializeObject<Dictionary<string, int>>(Resources.Load<TextAsset>($"{ConfigDir}/Resources").text);
            var buildings = JsonConvert.DeserializeObject<BuildingConfig[]>(Resources.Load<TextAsset>($"{ConfigDir}/Buildings").text);
            Buildings = new();
            foreach (var b in buildings)
            {
                Buildings.Add(b.type, b);
            }
            Sprites = new();
            foreach (var l in Landscapes.Values)
            {
                foreach (var i in l.icons)
                {
                    Sprites.Add(i, Resources.Load<Sprite>("Sprites/" + i));
                }
            }
            foreach (var l in Objs)
            {
                Sprites.Add(l.icon, Resources.Load<Sprite>("Sprites/" + l.icon));
            }
            foreach (var l in Buildings.Values)
            {
                foreach (var i in l.icons)
                {
                    foreach (var j in i)
                    {
                        Sprites.Add(j, Resources.Load<Sprite>("Sprites/" + j));
                    }
                }
            }
        }
    }
}