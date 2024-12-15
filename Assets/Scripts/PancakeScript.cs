using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PancakeScript : MonoBehaviour, IPointerDownHandler
{
    public GameDataScript gameData;
    GameObject Fall;
    GameObject Stack;
    GameObject StackObject;
    List<GameObject> OldStacks = new List<GameObject>();
    Transform Plate;
    public Canvas Canvas;
    TMP_Text tmp;
    public Image Tablecloth;
    float speed = 0f;
    private float timer = 0f;
    float lastClick = 1f;
    int stackHeight = 0;
    private int limitedHeight = 15;
    //public GameDataScript Mod;
    // Start is called before the first frame update
    void Start()
    {
        tmp = Camera.main.GetComponent<MainScript>().score_tmp;
        tmp.text = "<sprite=0> " + gameData.score.ToString();
        Fall = Resources.Load<GameObject>("Prefabs/Fall");
        Stack = Resources.Load<GameObject>("Prefabs/Stack");
        StackObject = Instantiate(Stack, new Vector3(UnityEngine.Device.Screen.width / 2, 0, 0),
            Quaternion.identity, Canvas.transform);
        Plate = StackObject.transform.GetChild(0);
        stackHeight = gameData.stackSize;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        lastClick += Time.deltaTime;
        if (timer >= 0.25f)
        {
            timer = 0f;
            speed -= 0.5f * lastClick * lastClick;
            if (speed < 0)
                speed = 0;
        }
        transform.RotateAround(transform.position, Vector3.back, speed * Time.deltaTime);
        if (stackHeight >= limitedHeight)
        {
            foreach (GameObject OldStack in OldStacks)
            {
                OldStack.GetComponent<Animator>().SetBool("Move", true);
                StartCoroutine(WaitAndDelete(OldStack));
            }
            Tablecloth.GetComponent<Animator>().SetBool("Started", true);
            stackHeight = 0;
        }
    }
    public void OnPointerDown(PointerEventData data)
    {
        gameData.score += 1 + gameData.mod;
        tmp.text = "<sprite=0> " + gameData.score.ToString();
        gameData.Klick++;
        Instantiate(Fall, new Vector3(Plate.position.x, Plate.position.y + 20 * gameData.stackSize, 0), Quaternion.identity, StackObject.transform);
        speed += 4f;
        lastClick = 1f;
        if (speed > 90)
            speed = 90;
        gameData.stackSize++;
        if (gameData.stackSize == limitedHeight)
        {
            OldStacks.Add(StackObject);
            StackObject = Instantiate(Stack, new Vector3(UnityEngine.Device.Screen.width / 2, 0, 0),
             Quaternion.identity, Canvas.transform);
            StackObject.transform.SetAsFirstSibling();
            gameData.stackSize = 0;
            Plate = StackObject.transform.GetChild(0);
        }
        StartCoroutine(WaitAndCount());
    }
    public IEnumerator WaitAndCount()
    {
        yield return new WaitForSeconds(1);
        stackHeight++;
    }

    public IEnumerator WaitAndDelete(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        OldStacks.Remove(obj);
        if (OldStacks.Count == 0)
        {
            Tablecloth.GetComponent<Animator>().SetBool("Started", false);
        }
        Destroy(obj);
    }

    public void OnDisable()
    {
        foreach (GameObject OldStack in OldStacks)
        {
            Destroy(OldStack);
        }
        Tablecloth.GetComponent<Animator>().SetBool("Started", false);
        OldStacks.Clear();
    }
}
