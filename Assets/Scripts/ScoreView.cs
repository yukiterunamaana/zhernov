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
        GetComponent<TMP_Text>().text = gameData.score.ToString() + " <sprite=0>";
    }
}
