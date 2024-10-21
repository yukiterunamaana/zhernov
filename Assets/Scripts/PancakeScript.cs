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
    private Touch touch;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnPointerDown(PointerEventData data)
    {
        gameData.score += 1;
        tmp.text = "Блины: " + gameData.score;
    }
}
