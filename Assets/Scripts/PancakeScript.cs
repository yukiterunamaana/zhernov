using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PancakeScript : MonoBehaviour, IPointerDownHandler
{
    public TMP_Text tmp;
    public GameDataScript gameData;
    //public GameDataScript Mod;
    // Start is called before the first frame update
    void Start()
    {
        tmp.text = "Блины: " + gameData.score;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnPointerDown(PointerEventData data)
    {
        gameData.score += 1 + gameData.mod;
        tmp.text = "Блины: " + gameData.score;
    }
}
