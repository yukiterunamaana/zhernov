using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapScript : MonoBehaviour
{
    public Canvas canvas;
    public GameDataScript data;
    void Start()
    {
        int width = data.width;
        int height = data.height;
        Image Tile = Resources.Load<Image>("Prefabs/Tile");
        Sprite mill = Resources.Load<Sprite>("Sprites/mill");
        for (int i = -width/2; i<width/2; i++)
        {
            for (int j = -height/2; j<height/2; j++)
            {
                Instantiate(Tile, new Vector3(i, j, 0), Quaternion.identity, canvas.transform);
            }
        }
        Image Building = Resources.Load<Image>("Prefabs/Building");
        Image millBuilding = Instantiate(Building, new Vector3(0, 0, 0), Quaternion.identity, canvas.transform);
        millBuilding.GetComponent<BuildingObjectScript>().IsBuilt = true;
        millBuilding.sprite = mill;
        millBuilding.rectTransform.sizeDelta = new Vector2(2,2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
