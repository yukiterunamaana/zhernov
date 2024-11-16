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
        if (gameData.score >= 100)
        {
            gameData.score -= 100;
            gameData.mod += 1;
            Update();
        }

    }
}
