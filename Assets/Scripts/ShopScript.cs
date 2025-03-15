using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    Image ShopItem;
    public Canvas Canvas;
    public GameDataScript GameData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameData = MainScript.gameData;
        ShopItem = Resources.Load<Image>("Prefabs/ShopBuilding");
        foreach (var b in GameData.buildings.Values)
        {
            var obj = Instantiate(ShopItem, new Vector3(0, 0, 0), Quaternion.identity, transform).GetComponent<BuildingScript>();
            obj.canvas = Canvas;
            obj.Sprite = Resources.Load<Sprite>("Sprites/" + b.type);
            obj.type = b.type;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
