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

public class MainScript : MonoBehaviour
{
    public static GameDataScript gameData;
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
    public TMP_Text score_tmp;
    public EditorData editorData;

    // Start is called before the first frame update
    private void Awake()
    {
        GameDataScript.score_tmp = score_tmp;
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
        gameData.landscapes = JsonConvert.DeserializeObject<Landscape[]>(Resources.Load<TextAsset>("Landscapes").text);
        gameData.objs = JsonConvert.DeserializeObject<MapObject[]>(Resources.Load<TextAsset>("Objects").text);
        Upgrade[] ups = JsonConvert.DeserializeObject<Upgrade[]>(Resources.Load<TextAsset>("Upgrades").text);
        Building.allUpgrades = new();
        foreach (var u in ups)
        {
            Building.allUpgrades.Add(u.name_of_change, u);
        }
        Building[] buildings = JsonConvert.DeserializeObject<Building[]>(Resources.Load<TextAsset>("Buildings").text);
        gameData.buildings = new();
        foreach (var b in buildings)
        {
            gameData.buildings.Add(b.type, b);
        }
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
        timer += Time.deltaTime; // Увеличиваем таймер
        if (timer >= 1f) // Если прошла 1 секунда
        {
            if ((Building.allUpgrades["PPS"].level > 0) & (gameData.currentScreen == Screen.Grind))
                for (int i = 1; i <= Building.allUpgrades["PPS"].level; i++)
                {
                    System.Random rnd = new System.Random();
                    int x_cor = rnd.Next(-1, 3);
                    instance = Instantiate(total_fall,
                    new Vector3(x_cor, total_fall.transform.position.y, 0), Quaternion.identity, canvas.transform);
                    StartCoroutine(CheckAnimationEnd(instance));
                }
            timer = 0f; // Сбрасываем таймер
            gameData.Score += 1 * Building.allUpgrades["PPS"].level;
        }

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
            // Ждем, пока анимация завершится
            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                yield return null; // Ждем один кадр
            }

            // Удаляем объект после завершения анимации
            Destroy(instance);
        }
        else
        {
            Debug.LogError("У объекта нет компонента Animator.");
            Destroy(instance); // Удаляем объект, если Animator отсутствует
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