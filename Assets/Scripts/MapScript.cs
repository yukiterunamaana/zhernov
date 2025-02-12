using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MapScript : MonoBehaviour
{
    public Canvas canvas;
    public GameDataScript data;
    public bool isEditor = false;
    void Start()
    {
        string LevelString;
        string saveFile = Application.persistentDataPath + "/gamedata.json";
        if (File.Exists(saveFile) && !isEditor)
        {
            LevelString = File.ReadAllText(saveFile);
        }
        else
        {
            LevelString = Resources.Load<TextAsset>("DefaultLevel").text;
        }
        LoadLevel(JsonUtility.FromJson<Level>(LevelString));
    }

    public void LoadLevel(Level level)
    {
        data.state = level;
        Image Tile = Resources.Load<Image>("Prefabs/Tile");
        Sprite mill = Resources.Load<Sprite>("Sprites/mill");
        Landscape[] landscapes = JsonUtility.FromJson<LWrapper>(Resources.Load<TextAsset>("Landscapes").text).items;
        Dictionary<string, Sprite> landSprites = new();
        foreach (var l in landscapes) {
            landSprites.Add(l.type, Resources.Load<Sprite>("Sprites/"+l.icon));
        }
        int index = 0;
        foreach (var t in data.state.tiles)
        {
            var Cell = Instantiate(Tile, new Vector3(t.x, t.y, 0), Quaternion.identity, canvas.transform);
            Cell.sprite = landSprites[t.type];
            Cell.tag = t.type;
            Cell.GetComponent<TileScript>().x = t.x;
            Cell.GetComponent<TileScript>().y = t.y;
            Cell.GetComponent<TileScript>().index = index;
            if (isEditor)
            {
                var scr = Cell.AddComponent<EditorTileScript>();
                scr.GameData = data;
            }
            index++;
        }
        Image Building = Resources.Load<Image>("Prefabs/Building");
        foreach (var b in data.state.buildings)
        {
            Image millBuilding = Instantiate(Building, new Vector3(b.x, b.y, 0), Quaternion.identity, canvas.transform);
            millBuilding.GetComponent<BuildingObjectScript>().IsBuilt = true;
            millBuilding.sprite = mill;
            millBuilding.rectTransform.sizeDelta = new Vector2(2, 2);
        }
    }
    public void SaveLevel()
    {
        string save = JsonUtility.ToJson(data.state);
        File.WriteAllText(Directory.GetCurrentDirectory() + "/Assets/Resources/DefaultLevel.json", save);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
