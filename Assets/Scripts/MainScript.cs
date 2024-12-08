using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using static Unity.Burst.Intrinsics.X86.Avx;
using static System.Net.Mime.MediaTypeNames;

public class MainScript : MonoBehaviour
{
    public GameDataScript gameData;
    public GameObject TasksCanvas;
    public GameObject GrindCanvas;
    public GameObject MapCanvas;
    public GameObject ShopCanvas;
    public Button TasksButton;
    public Button GrindButton;
    public Button MapButton;
    public Button ShopButton;
    public GameDataScript GD;
    private float timer = 0f;
    public TMP_Text score_main;
    public TMP_Text score_shop;

    // Start is called before the first frame update
    void Start()
    {
        LoadScrene(gameData.currentScreen);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // Увеличиваем таймер

        if (timer >= 1f) // Если прошла 1 секунда
        {
            timer = 0f; // Сбрасываем таймер
            gameData.score += 1 * gameData.PPS;
            score_main.text = "Блины: " + gameData.score;
            score_shop.text = "Блины: " + gameData.score;
        }
        if (gameData.Klick == 100)
        {
            gameData.score += (int)(gameData.add_mod * (float)gameData.score);
            gameData.Klick = 0;
            score_main.text = "Блины: " + gameData.score;
            score_shop.text = "Блины: " + gameData.score;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call<bool>("moveTaskToBack", true);
        }

    }

    private void OnValidate()
    {
        if (TasksCanvas.activeSelf)
        {
            TasksButton.gameObject.SetActive(false);
            GrindButton.gameObject.SetActive(true);
            MapButton.gameObject.SetActive(true);
            ShopButton.gameObject.SetActive(true);
        }
        else if (GrindCanvas.activeSelf)
        {
            TasksButton.gameObject.SetActive(true);
            GrindButton.gameObject.SetActive(false);
            MapButton.gameObject.SetActive(true);
            ShopButton.gameObject.SetActive(true);
        }
        else if (MapCanvas.activeSelf)
        {
            TasksButton.gameObject.SetActive(true);
            GrindButton.gameObject.SetActive(true);
            MapButton.gameObject.SetActive(false);
            ShopButton.gameObject.SetActive(true);
        }
        else if (ShopCanvas.activeSelf)
        {
            TasksButton.gameObject.SetActive(true);
            GrindButton.gameObject.SetActive(true);
            MapButton.gameObject.SetActive(true);
            ShopButton.gameObject.SetActive(false);
        }
    }

    public void LoadScrene(Screen screen)
    {
        TasksButton.gameObject.SetActive(true);
        GrindButton.gameObject.SetActive(true);
        MapButton.gameObject.SetActive(true);
        ShopButton.gameObject.SetActive(true);
        TasksCanvas.SetActive(false);
        GrindCanvas.SetActive(false);
        MapCanvas.SetActive(false);
        ShopCanvas.SetActive(false);
        switch (screen) {
            case Screen.Tasks:
                TasksButton.gameObject.SetActive(false);
                TasksCanvas.SetActive(true);
                break;
            case Screen.Grind:
                GrindButton.gameObject.SetActive(false);
                GrindCanvas.SetActive(true);
                GrindCanvas.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Блины: " + gameData.score;
                break;
            case Screen.Map:
                MapButton.gameObject.SetActive(false);
                MapCanvas.SetActive(true);
                break;
            case Screen.Shop:
                ShopButton.gameObject.SetActive(false);
                ShopCanvas.SetActive(true);
                ShopCanvas.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text= "Блины: " + gameData.score;
                break;
        }
        gameData.currentScreen = screen;
    }
}