using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
public class UpgradeScript : MonoBehaviour, IPointerDownHandler
{
    public GameDataScript gameData;
    public TMP_Text tmp;
    public TMP_Text text;
    public int cost = 100;
    public string name_button;

    // Start is called before the first frame update
    void Start()
    {
        cost = (int)(100 * Mathf.Pow(1.35f, gameData.mod));
        text.text = cost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //TextCost.text = (100 * gameData.mod).ToString();
    }
    public void OnPointerDown(PointerEventData data)
    {
        switch (name_button)
        {
            case  "hand":
                if (gameData.score >= cost)
                {
                    gameData.score -= cost;
                    gameData.mod += 1;
                    cost = (int)(cost * 1.35);
                    tmp.text = "Блины: " + gameData.score;
                    text.text = cost.ToString();
                }
                break;

            case  "lizard":
                if (gameData.score >= cost)
                {
                    gameData.score -= cost;
                    gameData.PPS++;
                    cost = (int)(cost * 1.35);
                    tmp.text = "Блины: " + gameData.score;
                    text.text = cost.ToString();
                }
                break;

            case "bank":
                if (gameData.score >= cost)
                {
                    gameData.score -= cost;
                    gameData.add_mod = 0.1;
                    cost = (int)(cost * 1.35);
                    tmp.text = "Блины: " + gameData.score;
                    text.text = cost.ToString();
                }
                break;
        }
        
    }
}
