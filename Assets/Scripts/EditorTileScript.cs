using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EditorTileScript : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    public GameDataScript GameData;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData) 
    { 
        if (GameData.editorData.brush != "none")
        {
            GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + GameData.editorData.brush);
            GameData.state.tiles[GetComponent<TileScript>().index].type = GameData.editorData.brush;
        }
    }
}
