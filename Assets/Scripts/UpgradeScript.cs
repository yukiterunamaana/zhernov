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

    // Start is called before the first frame update
    void Start()
    {
        cost = (int)(100 * Mathf.Pow(1.35f, gameData.mod));
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
        switch (name_button)
        {
            case  "hand":
                if (gameData.score >= cost)
                {
                    gameData.score -= cost;
                    gameData.mod += 1;
                    cost = (int)(cost * 1.35);
                    score_tmp.text = "<sprite=0> " + gameData.score.ToString();
                    text.text = cost.ToString();
                }
                break;

            case  "lizard":
                if (gameData.score >= cost)
                {
                    gameData.score -= cost;
                    gameData.PPS++;
                    cost = (int)(cost * 1.35);
                    score_tmp.text = "<sprite=0> " + gameData.score.ToString();
                    text.text = cost.ToString();
                }
                break;

            case "bank":
                if (gameData.score >= cost)
                {
                    gameData.score -= cost;
                    gameData.add_mod += 0.1;
                    cost = (int)(cost * 1.35);
                    score_tmp.text = "<sprite=0> " + gameData.score.ToString();
                    text.text = cost.ToString();
                }
                break;
        }
        
    }
}
