using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System;
using Model;
using UnityEngine.EventSystems;


public class MapScript : MonoBehaviour, IPointerDownHandler
{
    public Canvas canvas;
    public Canvas upgradesShop;
    public GameDataScript data;
    public GameObject ShopList;
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
        for (int i=0; i<data.state.width; i++)
        {
            for (int j=0; j<data.state.height; j++)
            {
                var t = data.state.tiles[i, j];
                t.icons = MainScript.ConfigManager.Landscapes[t.type].icons;
                var Cell = Instantiate(Tile, new Vector3(i, j, 0), Quaternion.identity, canvas.transform);
                Cell.rectTransform.SetAsFirstSibling();
                TileScript script = Cell.AddComponent<TileScript>();
                script.Create(Tile, data.state.tiles[i, j], canvas.transform);
                if (isEditor)
                {
                    var scr = Cell.AddComponent<EditorTileScript>();
                    scr.GameData = data;
                }
                if (t.building is not null)
                {
                    foreach (var u in MainScript.ConfigManager.Buildings[t.building.type].upgrades.Keys)
                    {
                        if (!t.building.upgrades.ContainsKey(u))
                        {
                            t.building.upgrades.Add(u, (Upgrade)BuildingConfig.allUpgrades[u].Clone());
                        }
                    }
                    if (t.buildingCenter.x != i || t.buildingCenter.y != j)
                    {
                        t.building = data.state.tiles[t.buildingCenter.x, t.buildingCenter.y].building;
                    }
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
    public void OnPointerDown(PointerEventData eventData)
    {
        int x = Mathf.FloorToInt(eventData.pointerCurrentRaycast.worldPosition.x);
        int y = Mathf.FloorToInt(eventData.pointerCurrentRaycast.worldPosition.y);
        if (data.state.tiles[x, y].building is not null && data.state.tiles[x, y].building.upgrades.Count > 0)
        {
            ShopList.GetComponent<create_up>().MakeList(data.state.tiles[x, y].building.upgrades);
            upgradesShop.gameObject.SetActive(true);
        } else
        {
            upgradesShop.gameObject.SetActive(false);
        }
    }
}
