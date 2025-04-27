using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class UpgradeScript : MonoBehaviour, IPointerDownHandler
{
    public GameDataScript gameData;
    public TMP_Text text;
    public int cost = 100;
    public Upgrade Upgrade;
    public BuildingObject Building;
    public GameObject item;
    // Start is called before the first frame update
    void Start()
    {

        gameData = MainScript.gameData;
        if (Upgrade.building_level is not null)
        {
            if (Building.level != Upgrade.building_level - 1)
            {
                item.SetActive(false);
            }
            else
            {
                cost = Upgrade.cost;
            }
        }
        cost = (int)(Upgrade.cost * Mathf.Pow(Upgrade.cost_mult, Upgrade.level));
        text.text = cost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //TextCost.text = (100 * gameData.mod).ToString();
    }
    public void OnPointerDown(PointerEventData data)
    {
        if (gameData.resources["pancakes"] >= cost)
        {
            gameData.resources["pancakes"] -= cost;
            Upgrade.level++;
            if (Upgrade.building_level is not null)
            {
                Building.level = (int)Upgrade.building_level;
                item.SetActive(false);
            }
            else
            {
                gameData.gameModifiers[Upgrade.modifier]++;
                cost = (int)(cost * Upgrade.cost_mult);
            }
            text.text = cost.ToString();
        }
    }
}
