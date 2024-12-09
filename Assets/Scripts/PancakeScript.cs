using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PancakeScript : MonoBehaviour, IPointerDownHandler
{
    public GameDataScript gameData;
    public GameObject Fall;
    public Canvas Canvas;
    public Image Plate;
    TMP_Text tmp;
    int stackSize = 0;
    //public GameDataScript Mod;
    // Start is called before the first frame update
    void Start()
    {
        tmp = Camera.main.GetComponent<MainScript>().score_tmp;
        tmp.text = "<sprite=0> " + gameData.score.ToString();
 
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnPointerDown(PointerEventData data)
    {
        gameData.score += 1 + gameData.mod;
        tmp.text = "<sprite=0> " + gameData.score.ToString();
        gameData.Klick++;
        Instantiate(Fall, new Vector3(Plate.transform.position.x, Plate.transform.position.y+20 * stackSize, 0), Quaternion.identity, Canvas.transform);
        stackSize++;
    }
}
