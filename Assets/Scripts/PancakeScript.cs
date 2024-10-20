using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PancakeScript : MonoBehaviour
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
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                gameData.score += 1;
                tmp.text = "Блины: " + gameData.score;
            }
        }
    }
}
