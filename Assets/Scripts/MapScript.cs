using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System;


public class MapScript : MonoBehaviour
{
    public Canvas canvas;
    public GameDataScript data;
    public bool isEditor = false;
    void Start()
    {
        data = MainScript.gameData;
        string LevelString;
        string saveFile = Application.persistentDataPath + "/gamedata.json";
        if (isEditor)
        {
            LevelString = Resources.Load<TextAsset>("DefaultLevel").text;
            LoadLevel(JsonConvert.DeserializeObject<Level>(LevelString));
        }
        else
        {
            LoadLevel(data.state);
        }
    }

    public void LoadLevel(Level level)
    {
        data.state = level;
        Image Tile = Resources.Load<Image>("Prefabs/Tile");
        Dictionary<string, Sprite> sprites = new();
        foreach (var l in data.landscapes) {
            sprites.Add(l.type, Resources.Load<Sprite>("Sprites/"+l.icon));
        }
        foreach (var l in data.objs)
        {
            sprites.Add(l.icon, Resources.Load<Sprite>("Sprites/" + l.icon));
        }
        foreach (var l in data.buildings.Values)
        {
            sprites.Add(l.type, Resources.Load<Sprite>("Sprites/" + l.type));
        }
        Image Building = Resources.Load<Image>("Prefabs/Building");
        for (int i=0; i<data.state.width; i++)
        {
            for (int j=0; j<data.state.height; j++)
            {
                var t = data.state.tiles[i, j];
                var Cell = Instantiate(Tile, new Vector3(i, j, 0), Quaternion.identity, canvas.transform);
                Cell.sprite = sprites[t.type];
                if (isEditor)
                {
                    var scr = Cell.AddComponent<EditorTileScript>();
                    scr.GameData = data;
                }
                if (t.building is not null)
                {
                    Image millBuilding = Instantiate(Tile, new Vector3(i, j, 0), Quaternion.identity, canvas.transform);
                    millBuilding.sprite = sprites[t.building.type];
                }
                if (t.obj is not null)
                {
                    Image MapObject = Instantiate(Tile, new Vector3(i, j, 0), Quaternion.identity, canvas.transform);
                    MapObject.sprite = sprites[t.obj.icon];
                }
            }
        }
    }
    public void SaveLevel()
    {
        //string save = JsonUtility.ToJson(data.state);
        GameDataScript.ToJson(Directory.GetCurrentDirectory() + "/Assets/Resources/DefaultLevel.json", data.state);
        //File.WriteAllText(Directory.GetCurrentDirectory() + "/Assets/Resources/DefaultLevel.json", save);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public static void CheckMap(Level state)
    {
        if (state.width==0 || state.height == 0)
        {
            throw new ArgumentException("Incorrect map");
        }
    }
}
