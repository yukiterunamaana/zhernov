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
        GameData = MainScript.gameData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData) 
    { 
        if (GameData.editorData.brush != "none")
        {
            int x = (int)transform.position.x;
            int y = (int)transform.position.y;
            var tyle = GameData.state.tiles[x, y];
            if (GameData.editorData.brushType == "landscape")
            {
                GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + GameData.editorData.brush);
                tyle.type = GameData.editorData.brush;
            }
            else if (GameData.editorData.brushType == "object")
            {
                var obj = Instantiate(this, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, transform);
                obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + GameData.editorData.brush);
                Regex regex = new Regex(@"\d");
                tyle.obj = new ObjectTile(regex.Replace(GameData.editorData.brush, ""), GameData.editorData.brush);
            }
            else if (GameData.editorData.brushType == "building")
            {
                var building = Instantiate(this, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, transform);
                building.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + GameData.editorData.brush);
                var btype = MainScript.ConfigManager.Buildings[GameData.editorData.brush];
                building.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(btype.width, btype.height);
                GameData.BuildBuilding(x, y, GameData.editorData.brush);
            }
        }
    }
}
