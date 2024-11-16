using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class Upgreeat : MonoBehaviour
{
    public GameDataScript gameData;
    public GameObject ShopScreen;
    public GameObject TextPan;
    public GameObject TextCost;
    public TMP_Text tmp;
    public TMP_Text text;
    int cost = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TextCost.text = (100 * gameData.mod).ToString();
    }
    public void something()
    {
        if (gameData.score >= cost)
        {
            gameData.score -= cost;
            gameData.mod += 1;
            cost = (int)(cost*1.35);
            tmp.text = "Блины: " + gameData.score;
            text.text = cost.ToString();
        }

    }
}
