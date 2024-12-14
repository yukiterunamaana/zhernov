using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class PancakeScript : MonoBehaviour, IPointerDownHandler
{
    public GameDataScript gameData;
    GameObject Fall;
    GameObject Stack;
    GameObject StackObject;
    Transform Plate;
    public Canvas Canvas;
    public Canvas Front;
    TMP_Text tmp;
    int stackSize = 0;
    int stackHeight = 0;
    float speed = 0f;
    private float timer = 0f;
    float lastClick = 1f;
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
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        lastClick+= Time.deltaTime;
        if (timer >= 0.25f)
        {
            timer = 0f;
            speed -= 0.5f*lastClick*lastClick;
            if (speed < 0)
                speed = 0;
        }
            transform.RotateAround(transform.position, Vector3.back, speed * Time.deltaTime);
        if (stackHeight == 15)
        {
            StackObject.GetComponent<Animator>().SetBool("Move", true);
            stackHeight = 0;
            stackSize = 0;
            StackObject.transform.SetParent(Front.transform);
            StartCoroutine(WaitAndDelete(StackObject));
            StackObject = Instantiate(Stack, new Vector3(UnityEngine.Device.Screen.width / 2, 0, 0),
                Quaternion.identity, Canvas.transform);
            Plate = StackObject.transform.GetChild(0);
        }
    }
    public void OnPointerDown(PointerEventData data)
    {
        gameData.score += 1 + gameData.mod;
        tmp.text = "<sprite=0> " + gameData.score.ToString();
        gameData.Klick++;
        Instantiate(Fall, new Vector3(Plate.position.x, Plate.position.y+20 * stackSize, 0), Quaternion.identity, StackObject.transform);
        speed += 4f;
        lastClick = 1f;
        if (speed == 60)
            speed = 60;
        stackSize++;
        StartCoroutine(WaitAndCount());
    }
    private IEnumerator WaitAndCount()
    {
        yield return new WaitForSeconds(1);
        stackHeight++;
    }

    private IEnumerator WaitAndDelete(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        Destroy(obj);
    }
}
