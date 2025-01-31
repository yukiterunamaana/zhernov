using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "GameData", menuName = "Game Data", order = 51)]
public class GameDataScript : ScriptableObject
{
    // Start is called before the first frame update
    public TMP_Text score_tmp;
    private int score;
    public Level state;
    public string saveFile;
    public int Score {
        get
        {
            return score;
        }
        set
        {
            score = value;
            score_tmp.text = score.ToString() + " <sprite=0>";

        }
    }
    public int mod = 0;
    public int PPS = 0;
    public int Klick = 0;
    public double add_mod = 0;
    public Screen currentScreen = Screen.Grind;
    public int stackSize = 0;

    public void OnEnable()
    {
        saveFile = Application.persistentDataPath + "/gamedata.json";
    }
}
public enum Screen
{
    Tasks,
    Grind,
    Map,
    Shop
}

public class MapTile
{
    public int x { get; set; }
    public int y { get; set; }
    public TileType type { get; set; }
}

public enum TileType
{
    Grass
}

[System.Serializable]
public class Level
{
    public int width;
    public int height;
    public List<Building> buildings;
    public static Level CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Level>(jsonString);
    }
}
[System.Serializable]
public class Building
{
    public int x;
    public int y;
    public string type;

    public Building (int x, int y, string type)
    {
        this.x = x;
        this.y = y;
        this.type = type;
    }
}