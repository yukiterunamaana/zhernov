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
    GameObject OldStack;
    GameObject StackObject;
    Transform Plate;
    public Canvas Canvas;
    public RawImage Tablecloth;
    float speed = 0f;
    float maxspeed = 50f;
    private float timer = 0f;
    float lastClick = 1f;
    private int limitedHeight = 15;
    //public GameDataScript Mod;
    // Start is called before the first frame update
    void Start()
    {
        Fall = Resources.Load<GameObject>("Prefabs/Fall");
        Stack = Resources.Load<GameObject>("Prefabs/Stack");
        StackObject = Instantiate(Stack, new Vector3(UnityEngine.Device.Screen.width / 2, 0, 0),
            Quaternion.identity, Canvas.transform);
        Plate = StackObject.transform.GetChild(0);
    }
    // Update is called once per frame
    void Update()
    {
         /*gameData.cur_klick += 1;
        if (timer >= 0.25f)
        {
            timer = 0f;
            speed -= 1f * lastClick * lastClick * lastClick;
            if (speed < 0)
                speed = 0;
        }
        transform.RotateAround(transform.position, Vector3.back, speed * Time.deltaTime * 5);*/
       timer += Time.deltaTime;
        lastClick += Time.deltaTime;
        if (timer >= 0.25f)
        {
            timer = 0f;
            speed -= 1f * lastClick * lastClick * lastClick;
            if (speed < 0)
                speed = 0;
        }
        transform.RotateAround(transform.position, Vector3.back, speed * Time.deltaTime * 5);

    }
    public void OnPointerDown(PointerEventData data)
    {
        gameData.Score += 1 + gameData.mod;
        gameData.Klick++;
        if (gameData.Klick == 100)
        {
            gameData.Score += (int)(gameData.add_mod * (float)gameData.Score);
            gameData.Klick = 0;
        }
        GameObject FallObject=Instantiate(Fall, 
            new Vector3(Plate.position.x, Plate.position.y + 20 * gameData.stackSize, 0), 
            Quaternion.identity, StackObject.transform);
        float fallSpeed = 1.5f*limitedHeight / (1.5f*limitedHeight - gameData.stackSize);
        FallObject.transform.GetChild(0).GetComponent<Animator>().SetFloat("Speed", fallSpeed);
        FallObject.transform.GetChild(1).GetComponent<Animator>().SetFloat("Speed", fallSpeed);
        speed += 10f;
        lastClick = 1f;
        if (speed > maxspeed)
            speed = maxspeed;
        gameData.stackSize++;
        if (gameData.stackSize == limitedHeight)
        {
            OldStack = StackObject;
            StackObject = Instantiate(Stack, new Vector3(UnityEngine.Device.Screen.width / 2, 0, 0),
             Quaternion.identity, Canvas.transform);
            StackObject.transform.SetAsFirstSibling();
            gameData.stackSize = 0;
            Plate = StackObject.transform.GetChild(0);
        }
        StartCoroutine(WaitAndCount(OldStack, fallSpeed, gameData.stackSize));
    }
    public IEnumerator WaitAndCount(GameObject obj, float fallSpeed, int stackHeight)
    {
        yield return new WaitForSeconds(1/ fallSpeed);
        if (stackHeight == 0)
        {
            obj.GetComponent<Animator>().SetBool("Move", true);
            StartCoroutine(WaitAndDelete(obj));
            Tablecloth.GetComponent<TableclothScript>().started = true;
        }
    }

    public IEnumerator WaitAndDelete(GameObject obj)
    {
        yield return new WaitForSeconds(1);     
        if (OldStack == obj)
        {
            Tablecloth.GetComponent<TableclothScript>().started = false;
        }
        Destroy(obj);
    }

    public void OnDisable()
    {
        Destroy(OldStack);
        Tablecloth.GetComponent<TableclothScript>().started = false;
    }
}
