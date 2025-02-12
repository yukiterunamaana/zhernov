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
    public EditorData editorData;

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
    public List<Building> buildings;
    public List<Tile> tiles;
    public Level(int width, int height)
    {
        this.width = width;
        this.height = height;
        tiles = new List<Tile>();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                tiles.Add(new Tile(i - width/2, j - height/2));
            }
        }
        buildings = new List<Building>() { new (0, 0, "mill") };
    }
}
[System.Serializable]

public class Tile
{
    public string type;
    public int x;
    public int y;
    public Tile(int x, int y)
    {
        this.x = x; 
        this.y = y;
        type = "grass";
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

[System.Serializable]
public class Landscape
{
    public string type;
    public string icon;
}

[System.Serializable]
public class LWrapper
{
    public Landscape[] items;
}
