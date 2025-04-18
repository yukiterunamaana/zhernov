using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using System.IO;
using Unity.VisualScripting.FullSerializer;
using System;

public class GameDataScript
{
    public float CurrentAngle;
    // Start is called before the first frame update
    public int score = 300;
    public Level state;
    public static TMP_Text score_people_tmp;
    public int People = 4;

    public int Klick = 0;
    public Screen currentScreen = Screen.Grind;
    [JsonIgnore]
    public int stackSize = 0;
    [JsonIgnore]
    public EditorData editorData;
    [JsonIgnore]
    public Landscape[] landscapes;
    [JsonIgnore]
    public MapObject[] objs;
    [JsonIgnore]
    public Dictionary<string, Building> buildings;
    public Dictionary<string, int> BuildingCount;
    public Dictionary<string, int> gameModifiers = new();
    public static void ToJson(string path, object state)
    {
        JsonSerializer serializer = new JsonSerializer();
        serializer.NullValueHandling = NullValueHandling.Ignore;
        using (StreamWriter sw = new StreamWriter(path))
        using (JsonWriter writer = new JsonTextWriter(sw))
        {
            serializer.Serialize(writer, state);
        }
    }
    public void BuildBuilding(int x, int y, string type)
    {
        state.tiles[x, y].building = new BuildingObject(buildings[type]);
        state.tiles[x, y].buildingCenter = new Vector2Int(x, y);
        var b = buildings[type];
        for (int i = x; i < x + b.width; i++)
        {
            for (int j = y; j < y + b.height; j++)
            {
                if (i == x && j == y)
                {
                    continue;
                }
                state.tiles[i, j].building = state.tiles[x, y].building;
                state.tiles[i, j].buildingCenter = new Vector2Int(x, y);
            }
        }
    }
}
public enum Screen
{
    Tasks,
    Grind,
    Map,
    Shop
}

[System.Serializable]
public class Level
{
    public int width;
    public int height;
    public Tile[,] tiles;
    public Level(int width, int height)
    {
        this.width = width;
        this.height = height;
        tiles = new Tile[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                tiles[i,j] = new Tile(i, j);
            }
        }
    }
}
[System.Serializable]

public class Tile
{
    public int x;
    public int y;
    public string type = "grass";
    public BuildingObject building = null;
    public Vector2Int buildingCenter;
    public ObjectTile obj = null;
    public Tile(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

[System.Serializable]
public class Building
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

    [JsonConstructor]
    public Building (string type, int cost, int max, int? width, int? height, List<string> upgradeList)
    {
        this.type = type;
        this.cost = cost;
        this.max = max;
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
    }
}
[System.Serializable]
public class BuildingObject
{
    public string type;
    public Dictionary<string, Upgrade> upgrades;
    public BuildingObject()
    {

    }
    public BuildingObject(Building building)
    {
        type = building.type;
        upgrades = building.upgrades;
    }
}
[System.Serializable]
public class ObjectTile
{
    public string tag;
    public string icon;
    public ObjectTile(string tag, string icon)
    {
        this.tag = tag;
        this.icon = icon;
    }
}
[System.Serializable]
public class Landscape
{
    public string type;
    public string icon;
}

[System.Serializable]
public class MapObject
{
    public string tag;
    public string icon;
}

public class Upgrade: ICloneable
{
    public string name_of_change;
    public string name;
    public string dis;
    public string image;
    public int cost;
    public float cost_mult;
    public int level = 0;
    public object Clone()
    {
        return this.MemberwiseClone();
    }
}