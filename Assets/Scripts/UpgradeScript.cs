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
    public string name_button;
    public Upgrade Upgrade;
    // Start is called before the first frame update
    void Start()
    {

        gameData = MainScript.gameData;
       // Upgrade = Building.allUpgrades[name_button];
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
            gameData.gameModifiers[Upgrade.name_of_change]++;
            cost = (int)(cost * Upgrade.cost_mult);
            text.text = cost.ToString();
        }
    }
}
