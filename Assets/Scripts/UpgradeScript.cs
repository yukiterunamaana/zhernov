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
    TMP_Text score_tmp;
    public TMP_Text text;
    public int cost = 100;
    public string name_button;
    public Upgrade Upgrade;
    // Start is called before the first frame update
    void Start()
    {

        gameData = MainScript.gameData;
        Upgrade = Building.allUpgrades[name_button];
        cost = (int)(Upgrade.cost * Mathf.Pow(Upgrade.cost_mult, Upgrade.level));
        text.text = cost.ToString();
        score_tmp = Camera.main.GetComponent<MainScript>().score_tmp;
    }

    // Update is called once per frame
    void Update()
    {
        //TextCost.text = (100 * gameData.mod).ToString();
    }
    public void OnPointerDown(PointerEventData data)
    {
        if (gameData.Score >= cost)
        {
            gameData.Score -= cost;
            Upgrade.level++;
            cost = (int)(cost * Upgrade.cost_mult);
            text.text = cost.ToString();
        }
        /*switch (name_button)
        {
            case "mod":
                if (gameData.Score >= cost)
                {
                    gameData.Score -= cost;
                    gameData.mod += 1;
                    cost = (int)(cost * 1.35);
                    text.text = cost.ToString();
                }
                break;

            case  "lizard":
                if (gameData.Score >= cost)
                {
                    gameData.Score -= cost;
                    gameData.PPS++;
                    cost = (int)(cost * 1.35);
                    text.text = cost.ToString();
                }
                break;

            case "bank":
                if (gameData.Score >= cost)
                {
                    gameData.Score -= cost;
                    gameData.add_mod += 0.1;
                    cost = (int)(cost * 1.35);
                    text.text = cost.ToString();
                }
                break;
        }*/

    }
}
