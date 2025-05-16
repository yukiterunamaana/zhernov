using System.Collections.Generic;
using UnityEngine;

public class Create_Panel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        
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
