using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class MapEditorScript : MonoBehaviour
{
    public GameObject shop;
    public EditorData data;
    public GameDataScript gameData;
    private MapScript map;
    // Start is called before the first frame update
    void Start()
    {
        gameData = MainScript.gameData;
        map = GetComponent<MapScript>();
        Image LandscapeButton = Resources.Load<Image>("Prefabs/EditorLandscape");
        foreach (var l in gameData.landscapes)
        {
            Image I = Instantiate(LandscapeButton, new Vector3(0, 0, 0),
            Quaternion.identity, shop.transform);
            I.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + l.icon);
            I.GetComponent<LandscapeButtonScript>().type = l.type;
            I.GetComponent<LandscapeButtonScript>().brushType = "landscape";
        }
        foreach (var l in gameData.objs)
        {
            Image I = Instantiate(LandscapeButton, new Vector3(0, 0, 0),
            Quaternion.identity, shop.transform);
            I.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + l.icon);
            I.GetComponent<LandscapeButtonScript>().type = l.icon;
            I.GetComponent<LandscapeButtonScript>().brushType = "object";
        }
        foreach (var l in gameData.buildings.Values)
        {
            Image I = Instantiate(LandscapeButton, new Vector3(0, 0, 0),
            Quaternion.identity, shop.transform);
            I.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + l.type);
            I.GetComponent<LandscapeButtonScript>().type = l.type;
            I.GetComponent<LandscapeButtonScript>().brushType = "building";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateLevel()
    {
        map.LoadLevel(new Level(data.width, data.height));
    }
}
