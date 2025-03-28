using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using static Unity.Burst.Intrinsics.X86.Avx;
using Newtonsoft.Json;

public class BuildingScript : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // Start is called before the first frame update
    Image building;
    public Canvas canvas;
    public GameDataScript gameData;
    Image Tile;
    public Sprite Sprite;
    public string type;
    Building b;
    int x;
    int y;
    void Start()
    {
        gameData = MainScript.gameData;
        Tile = Resources.Load<Image>("Prefabs/Tile");
        transform.GetChild(0).GetComponent<Image>().sprite = Sprite;
        b = gameData.buildings[type];
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (gameData.Score < b.cost)
        {
            eventData.pointerDrag = null;
            return;
        }
        building = Instantiate(Tile, new Vector3(0, 0, 0), Quaternion.identity, canvas.transform);
        building.sprite = Sprite;
        building.rectTransform.sizeDelta = new Vector2(b.width, b.height);
        building.color = new Color(1, 1, 1, 0.5f);
    }
    public void OnDrag(PointerEventData eventData) {
        x = Mathf.FloorToInt(eventData.pointerCurrentRaycast.worldPosition.x);
        y = Mathf.FloorToInt(eventData.pointerCurrentRaycast.worldPosition.y);
        building.transform.position = new Vector3(x, y, 0);
        if (IsColliding())
        {
            building.color = new Color(0, 0, 0, 0.0f);
        }
        else
        {
            building.color = new Color(1, 1, 1, 0.25f);
        }
    }
    bool IsColliding()
    {
        for (int i = x; i < x + b.width; i++)
        {
            for (int j = y; j < y + b.height; j++)
            {
                var tile = gameData.state.tiles[i, j];
                if (tile.type == "water" || tile.obj is not null || tile.building is not null)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void OnEndDrag(PointerEventData eventData) {
        building.color = new Color(1, 1, 1, 1f);
        var tile = gameData.state.tiles[x, y];
        if (!IsColliding())
        {
            gameData.Score -= b.cost;
            gameData.BuildBuilding(x, y, type);
            GameDataScript.ToJson(Application.persistentDataPath + "/gamedata.json", gameData);
        }
        else
        {
            Destroy(building.gameObject);
        }
    }
}
