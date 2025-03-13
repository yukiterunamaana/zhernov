using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;


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
        LoadLevel(JsonConvert.DeserializeObject<Level>(LevelString));
    }

    public void LoadLevel(Level level)
    {
        data.state = level;
        Image Tile = Resources.Load<Image>("Prefabs/Tile");
        Sprite mill = Resources.Load<Sprite>("Sprites/mill");
        Landscape[] landscapes = JsonConvert.DeserializeObject<Landscape[]>(Resources.Load<TextAsset>("Landscapes").text);
        MapObject[] objs = JsonConvert.DeserializeObject<MapObject[]>(Resources.Load<TextAsset>("Objects").text);
        Dictionary<string, Sprite> landSprites = new();
        foreach (var l in landscapes) {
            landSprites.Add(l.type, Resources.Load<Sprite>("Sprites/"+l.icon));
        }
        foreach (var l in objs)
        {
            landSprites.Add(l.icon, Resources.Load<Sprite>("Sprites/" + l.icon));
        }
        Image Building = Resources.Load<Image>("Prefabs/Building");
        for (int i=0; i<data.state.width; i++)
        {
            for (int j=0; j<data.state.height; j++)
            {
                var t = data.state.tiles[i, j];
                var Cell = Instantiate(Tile, new Vector3(i, j, 0), Quaternion.identity, canvas.transform);
                Cell.sprite = landSprites[t.type];
                if (isEditor)
                {
                    var scr = Cell.AddComponent<EditorTileScript>();
                    scr.GameData = data;
                }
                if (t.building is not null)
                {
                    Image millBuilding = Instantiate(Tile, new Vector3(i, j, 0), Quaternion.identity, canvas.transform);
                    millBuilding.sprite = mill;
                }
                if (t.obj is not null)
                {
                    Image MapObject = Instantiate(Tile, new Vector3(i, j, 0), Quaternion.identity, canvas.transform);
                    MapObject.sprite = landSprites[t.obj.icon];
                }
            }
        }
    }
    public void SaveLevel()
    {
        //string save = JsonUtility.ToJson(data.state);
        JsonSerializer serializer = new JsonSerializer();
        serializer.NullValueHandling = NullValueHandling.Ignore;
        using (StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + "/Assets/Resources/DefaultLevel.json"))
        using (JsonWriter writer = new JsonTextWriter(sw))
        {
            serializer.Serialize(writer, data.state);
        }
        //File.WriteAllText(Directory.GetCurrentDirectory() + "/Assets/Resources/DefaultLevel.json", save);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
