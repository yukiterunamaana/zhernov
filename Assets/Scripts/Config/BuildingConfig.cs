using System.Collections.Generic;
using Newtonsoft.Json;

namespace Model
{
    [System.Serializable]
    public class BuildingConfig
    {
        public string type;
        public int cost;
        public int max;
        public int width = 1;
        public int height = 1;
        [JsonProperty("upgrades")]
        public List<string> upgradeList;
        [JsonIgnore]
        public Dictionary<string, Upgrade> upgrades;
        public static Dictionary<string, Upgrade> allUpgrades;
        public int workers;
        public string[] icons;

        [JsonConstructor]
        public BuildingConfig (string type, int cost, int max, int? width, int? height, List<string> upgradeList, int workers, string[] icons)
        {
            this.type = type;
            this.cost = cost;
            this.max = max;
            this.workers = workers;
            if (width is not null) this.width = (int)width;
            if (height is not null) this.height = (int)height;
            this.upgrades = new();
            if (upgradeList is not null)
            {
                foreach (var u in upgradeList)
                {
                    this.upgrades.Add(u, allUpgrades[u]);
                }
            }
            this.icons = icons;
        }
    }
}