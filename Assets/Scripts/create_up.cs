using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.UI;
using System;

public class create_up : MonoBehaviour
{
    public GameObject Upg;

    public class ItemData
    {
        public string name_of_change;
        public string name;
        public string dis;
        public string image;
        public int cost;
        public float cost_mult;
    }

    // Start is called before the first frame update
    void Start()
    {
        //MakeList(Building.allUpgrades);
    }
    public void MakeList(Dictionary<string, Upgrade> allUpgrades, BuildingObject building)
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        foreach (var upgrade in allUpgrades)
        {
            GameObject newItem = Instantiate(Upg, transform.position, transform.rotation, transform);
            itm_scr itemScript = newItem.GetComponent<itm_scr>();
            if (itemScript != null)
            {
                itemScript.name.text = upgrade.Value.name;
                itemScript.dis.text = upgrade.Value.dis;
                itemScript.pr.text = upgrade.Value.cost.ToString();
                newItem.transform.GetComponentInChildren<UpgradeScript>().Upgrade = upgrade.Value;
                newItem.transform.GetComponentInChildren<UpgradeScript>().Building = building;
                newItem.transform.GetComponentInChildren<UpgradeScript>().item = newItem;
                Image imageComponent = newItem.transform.GetChild(0).GetChild(2).GetComponent<Image>();

                
                if (imageComponent != null)
                {
                    Sprite loadedSprite = Resources.Load<Sprite>("Sprites/" + upgrade.Value.image);
                    if (loadedSprite != null)
                    {
                        imageComponent.sprite = loadedSprite;
                        itemScript.pic = loadedSprite;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
