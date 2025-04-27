using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using System.IO;
using System;
using Model;
using UnityEngine.UI;

public class GameDataScript
{
    public float CurrentAngle;
    // Start is called before the first frame update
    public Level state;
    public static TMP_Text score_people_tmp;

    public int Klick = 0;
    public Screen currentScreen = Screen.Grind;
    [JsonIgnore]
    public int stackSize = 0;
    [JsonIgnore]
    public EditorData editorData;
    public Dictionary<string, int> BuildingCount;
    public Dictionary<string, int> gameModifiers = new();
    public Dictionary<string, int> resources = new();

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
        state.tiles[x, y].building = new BuildingObject(MainScript.ConfigManager.Buildings[type]);
        state.tiles[x, y].buildingCenter = new Vector2Int(x, y);
        var b = MainScript.ConfigManager.Buildings[type];
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
    public List<string> icons;
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
public class BuildingObject
{
    public string type;
    public Dictionary<string, Upgrade> upgrades;
    public string[][] icons;
    public int workers;
    public int level = 0;
    public BuildingObject()
    {

    }
    public BuildingObject(BuildingConfig building)
    {
        type = building.type;
        upgrades = building.upgrades;
        workers = building.workers;
        icons = building.icons;
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

public class Upgrade: ICloneable
{
    public string id;
    public string modifier;
    public string name;
    public string dis;
    public string image;
    public int cost;
    public float cost_mult;
    public int level = 0;
    public int? building_level;
    public object Clone()
    {
        return this.MemberwiseClone();
    }
}

public class Modifier
{
    public string name;
    public int initial;
}