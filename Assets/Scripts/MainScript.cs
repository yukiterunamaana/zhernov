using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;
using Newtonsoft.Json;
using System.IO;
using Config;

public class MainScript : MonoBehaviour
{
    public static GameDataScript gameData;
    public static ConfigManager ConfigManager => _configManager;
    public GameObject total_fall;
    public GameObject TasksCanvas;
    public GameObject GrindCanvas;
    public GameObject MapCanvas;
    public GameObject ShopCanvas;
    public GameObject instance;
    public Button TasksButton;
    public Button GrindButton;
    public Button MapButton;
    public Button ShopButton;
    public Canvas canvas;
    private float timer = 0f;
    private float timerfood = 0f;
    public EditorData editorData;

    private static ConfigManager _configManager;

    // Start is called before the first frame update
    private void Awake()
    {
        Debug.developerConsoleVisible = false;
        _configManager = new ConfigManager();
        _configManager.Init();
        
        try
        {
            gameData = JsonConvert.DeserializeObject<GameDataScript>(File.ReadAllText(Application.persistentDataPath + "/gamedata.json"));
            MapScript.CheckMap(gameData.state);
        }
        catch
        {
            gameData = new GameDataScript();
            gameData.state = JsonConvert.DeserializeObject<Level>(Resources.Load<TextAsset>("DefaultLevel").text);
        }
        gameData.editorData = editorData;
        gameData.resources = new Dictionary<string, int>(_configManager.GameResources);
        gameData.gameModifiers = new Dictionary<string, int>(_configManager.GameModifiers);
    }
    void Start()
    {
        if (editorData == null)
        {
            LoadScrene(gameData.currentScreen);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // Óâåëè÷èâàåì òàéìåð
        if (timer >= 1f) // Åñëè ïðîøëà 1 ñåêóíäà
        {
            timer = 0f; // Ñáðàñûâàåì òàéìåð
            gameData.resources["pancakes"] += 1 * gameData.gameModifiers["PPS"];
        }
        if (timerfood == 4000)
        {
            gameData.resources["pancakes"] -= gameData.resources["workers"] * 2;
            timerfood = 0f;
        }
        timerfood += 1f;

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GameDataScript.ToJson(Application.persistentDataPath + "/gamedata.json", gameData.state);
            AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call<bool>("moveTaskToBack", true);
        }


    }
    private IEnumerator CheckAnimationEnd(GameObject instance)
    {
        Animator animator = instance.GetComponent<Animator>();
        if (animator != null)
        {
            // Æäåì, ïîêà àíèìàöèÿ çàâåðøèòñÿ
            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                yield return null; // Æäåì îäèí êàäð
            }

            // Óäàëÿåì îáúåêò ïîñëå çàâåðøåíèÿ àíèìàöèè
            Destroy(instance);
        }
        else
        {
            Debug.LogError("Ó îáúåêòà íåò êîìïîíåíòà Animator.");
            Destroy(instance); // Óäàëÿåì îáúåêò, åñëè Animator îòñóòñòâóåò
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
                break;
            case Screen.Map:
                MapButton.gameObject.SetActive(false);
                MapCanvas.SetActive(true);
                break;
            case Screen.Shop:
                ShopButton.gameObject.SetActive(false);
                ShopCanvas.SetActive(true);
                break;
        }
        gameData.currentScreen = screen;
    }
}