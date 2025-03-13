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
                tiles[i,j] = new Tile();
            }
        }
    }
}
[System.Serializable]

public class Tile
{
    public string type = "grass";
    public Building building = null;
    public ObjectTile obj = null;
}

[System.Serializable]
public class Building
{
    public string type;

    public Building (string type)
    {
        this.type = type;
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