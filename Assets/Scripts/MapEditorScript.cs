using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEditorScript : MonoBehaviour
{
    public GameObject shop;
    public EditorData data;
    private MapScript map;
    // Start is called before the first frame update
    void Start()
    {
        map = GetComponent<MapScript>();
        Landscape[] landscapes = LoadAsset<LWrapper>("Landscapes").items;
        Image LandscapeButton = Resources.Load<Image>("Prefabs/EditorLandscape");
        foreach (var l in landscapes)
        {
            Image I = Instantiate(LandscapeButton, new Vector3(0, 0, 0),
            Quaternion.identity, shop.transform);
            I.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + l.icon);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public T LoadAsset<T>(string path)
    {
        string asset = Resources.Load<TextAsset>(path).text;
        return JsonUtility.FromJson<T>(asset);
    }

    public void CreateLevel()
    {
        map.LoadLevel(new Level(data.width, data.height));
    }
}
