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
    Image millBuilding;
    public Canvas canvas;
    public GameDataScript gameData;
    int x;
    int y;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (gameData.Score < 75)
        {
            eventData.pointerDrag = null;
            return;
        }
        Image Tile = Resources.Load<Image>("Prefabs/Tile");
        Sprite mill = Resources.Load<Sprite>("Sprites/mill");
        millBuilding = Instantiate(Tile, new Vector3(0, 0, 0), Quaternion.identity, canvas.transform);
        millBuilding.sprite = mill;
        millBuilding.color = new Color(1, 1, 1, 0.5f);
    }
    public void OnDrag(PointerEventData eventData) {
        x = Mathf.FloorToInt(eventData.pointerCurrentRaycast.worldPosition.x);
        y = Mathf.FloorToInt(eventData.pointerCurrentRaycast.worldPosition.y);
        millBuilding.transform.position = new Vector3(x, y, 0);
        var tile = gameData.state.tiles[x, y];
        if (tile.type == "water" || tile.obj is not null || tile.building is not null)
        {
            millBuilding.color = new Color(0, 0, 0, 0.0f);
        }
        else
        {
            millBuilding.color = new Color(0, 0, 0, 0.5f);
        }
    }
    public void OnEndDrag(PointerEventData eventData) {
        millBuilding.color = new Color(1, 1, 1, 1f);
        var tile = gameData.state.tiles[x, y];
        if (tile.type!="water" && tile.obj is null && tile.building is null)
        {
            gameData.Score -= 75;
            gameData.state.tiles[x,y].building = new Building("mill");
        }
        else
        {
            Destroy(millBuilding.gameObject);
        }
    }
}
