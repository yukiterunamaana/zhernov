using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PancakeView : MonoBehaviour, IPointerDownHandler
{
    public PancakeModel Model => MainScript.PancakeModel;
    GameObject Fall;
    GameObject Stack;
    GameObject OldStack;
    GameObject StackObject;
    Transform Plate;
    public Canvas Canvas;
    public RawImage Tablecloth;
    //public GameDataScript Mod;
    // Start is called before the first frame update
    void Start()
    {
        Fall = Resources.Load<GameObject>("Prefabs/Fall");
        Stack = Resources.Load<GameObject>("Prefabs/Stack");
        StackObject = Instantiate(Stack, new Vector3(UnityEngine.Device.Screen.width / 2, 0, 0),
            Quaternion.identity, Canvas.transform);
        Plate = StackObject.transform.GetChild(0);
        Model.OnPancakeFall += OnPancakeFall;
        Model.OnStackFull += OnStackFull;
    }
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.back, Model.speed * Time.deltaTime * Model.RoutateMod);
    }
    public void OnPancakeFall(float FallSpeed)
    {
        GameObject FallObject = Instantiate(Fall,
            new Vector3(Plate.position.x, Plate.position.y + 20 *Model.stackSize, 0),
            Quaternion.identity, StackObject.transform);
        FallObject.transform.GetChild(0).GetComponent<Animator>().SetFloat("Speed", FallSpeed);
        FallObject.transform.GetChild(1).GetComponent<Animator>().SetFloat("Speed", FallSpeed);
    }

    public void OnStackFull(float FallSpeed)
    {
        OldStack = StackObject;
        StackObject = Instantiate(Stack, new Vector3(UnityEngine.Device.Screen.width / 2, 0, 0),
         Quaternion.identity, Canvas.transform);
        StackObject.transform.SetAsFirstSibling();
        Plate = StackObject.transform.GetChild(0);
        StartCoroutine(WaitAndCount(OldStack, FallSpeed));
    }
    public void OnPointerDown(PointerEventData data) 
    {
        Model.HandleClick();
    }
    public IEnumerator WaitAndCount(GameObject obj, float fallSpeed)
    {
        yield return new WaitForSeconds(1/ fallSpeed);
        obj.GetComponent<Animator>().SetBool("Move", true);
        StartCoroutine(WaitAndDelete(obj));
        Tablecloth.GetComponent<TableclothScript>().started = true;
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

    public void OnDestroy()
    {
        Model.OnPancakeFall -= OnPancakeFall;
        Model.OnStackFull -= OnStackFull;
    }
}
