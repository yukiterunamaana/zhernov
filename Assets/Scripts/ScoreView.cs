using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    GameDataScript gameData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameData = MainScript.gameData;
    }

    // Update is called once per frame
    void Update()
    {
        string text = string.Empty;
        text += gameData.resources["workers"].ToString() + "/";
        text += gameData.resources["people"].ToString() + " <sprite=0>";
        text += gameData.resources["pancakes"].ToString() + "<sprite=1>";
        GetComponent<TMP_Text>().text = text;
    }
}
