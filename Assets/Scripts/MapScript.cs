using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MapScript : MonoBehaviour
{
    public Canvas canvas;
    public GameDataScript data;
    void Start()
    {
        
        string LevelString;
        if (File.Exists(data.saveFile))
        {
            LevelString = File.ReadAllText(data.saveFile);
        }
        else
        {
            LevelString = Resources.Load<TextAsset>("DefaultLevel").text;
        }
        data.state = Level.CreateFromJSON(LevelString);
        Image Tile = Resources.Load<Image>("Prefabs/Tile");
        Sprite mill = Resources.Load<Sprite>("Sprites/mill");
        for (int i = -data.state.width / 2; i < data.state.width / 2; i++)
        {
            for (int j = -data.state.height / 2; j < data.state.height / 2; j++)
            {
                Instantiate(Tile, new Vector3(i, j, 0), Quaternion.identity, canvas.transform);
            }
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
