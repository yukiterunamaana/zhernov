using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class UpgradeScript : MonoBehaviour, IPointerDownHandler
{
    public GameDataScript gameData;
    public TMP_Text tmp;
    public TMP_Text text;
    int cost = 100;

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
