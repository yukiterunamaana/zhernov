using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using static Unity.Burst.Intrinsics.X86.Avx;

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
        if (gameData.Score <= 75)
        {
            
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (gameData.Score < 75)
        {
            eventData.pointerDrag = null;
            return;
        }
        Image Tile = Resources.Load<Image>("Prefabs/Building");
        Sprite mill = Resources.Load<Sprite>("Sprites/mill");
        millBuilding = Instantiate(Tile, new Vector3(0, 0, 0), Quaternion.identity, canvas.transform);
        millBuilding.sprite = mill;
        millBuilding.rectTransform.sizeDelta = new Vector2(2, 2);
        millBuilding.color = new Color(0, 0, 0, 0.5f);
    }
    public void OnDrag(PointerEventData eventData) {
        x = Mathf.FloorToInt(eventData.pointerCurrentRaycast.worldPosition.x);
        y = Mathf.FloorToInt(eventData.pointerCurrentRaycast.worldPosition.y);
        millBuilding.transform.position = new Vector3(x, y, 0);
    }
    public void OnEndDrag(PointerEventData eventData) {
        millBuilding.color = new Color(0, 0, 0, 1f);
        if (millBuilding.GetComponent<BuildingObjectScript>().CollidingCount==0)
        {
            millBuilding.GetComponent<BuildingObjectScript>().IsBuilt = true;
            gameData.Score -= 75;
            gameData.state.buildings.Add(new Building(x, y, "mill"));
            string save = JsonUtility.ToJson(gameData.state);
            File.WriteAllText(gameData.saveFile, save);
        }
        else
        {
            Destroy(millBuilding.gameObject);
        }
    }
}
