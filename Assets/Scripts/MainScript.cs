using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;

public class MainScript : MonoBehaviour
{
    public GameDataScript gameData;
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

    // Start is called before the first frame update
    void Start()
    {
        LoadScrene(gameData.currentScreen);
        gameData.score = 300;
        gameData.mod = 0;
        gameData.PPS = 0;
        gameData.Klick = 0;
        gameData.add_mod = 0;
        gameData.currentScreen = Screen.Map;
        gameData.stackSize = 0;
}

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // Увеличиваем таймер
        if (timer >= 1f) // Если прошла 1 секунда
        {
            if ((gameData.PPS > 0) & (gameData.currentScreen == Screen.Grind))
                for (int i = 1; i <= gameData.PPS; i++)
                {
                    System.Random rnd = new System.Random();
                    int x_cor = rnd.Next(-1, 3);
                    instance = Instantiate(total_fall,
                    new Vector3(x_cor, total_fall.transform.position.y, 0), Quaternion.identity, canvas.transform);
                    StartCoroutine(CheckAnimationEnd(instance));
                }
            timer = 0f; // Сбрасываем таймер
            gameData.score += 1 * gameData.PPS;
            score_tmp.text = gameData.score.ToString() + " <sprite=0>";
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
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