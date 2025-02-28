using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
            if (GameData.editorData.brushType == "landscape")
            {
                GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + GameData.editorData.brush);
                GameData.state.tiles[GetComponent<TileScript>().index].type = GameData.editorData.brush;
            }
            else if (GameData.editorData.brushType == "object")
            {
                var obj = Instantiate(this, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, transform);
                obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + GameData.editorData.brush);
                Regex regex = new Regex(@"\d");
                GameData.state.objs.Add(new ObjectTile((int)transform.position.x, (int)transform.position.y, regex.Replace(GameData.editorData.brush, ""), GameData.editorData.brush));
            }
        }
    }
}
