using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class PancakeView : MonoBehaviour, IPointerDownHandler
    {
        public PancakeScript Model => MainScript.PancakeModel;
        GameObject Fall;
        GameObject Stack;
        GameObject OldStack;
        GameObject StackObject;
        Transform Plate;
        public Canvas Canvas;
        public RawImage Tablecloth;

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

        void Update()
        {
            transform.RotateAround(transform.position, Vector3.back, Model.speed * Time.deltaTime * Model.RoutateMod);
        }

        public void OnPancakeFall(int pancakesCount)
        {
            var FallObject = Instantiate(Fall,
                new Vector3(Plate.position.x, Plate.position.y + 20 * Model.stackSize, 0),
                Quaternion.identity, StackObject.transform);
            var fallSpeed = 1.5f * Model.limitedHeight / (1.5f * Model.limitedHeight - Model.stackSize);
            FallObject.transform.GetChild(0).GetComponent<Animator>().SetFloat("Speed", fallSpeed);
            FallObject.transform.GetChild(1).GetComponent<Animator>().SetFloat("Speed", fallSpeed);
            if (OldStack != null)
                StartCoroutine(WaitAndCount(OldStack, fallSpeed, Model.stackSize));
        }

        public void OnStackFull()
        {
            OldStack = StackObject;
            StackObject = Instantiate(Stack, new Vector3(UnityEngine.Device.Screen.width / 2, 0, 0),
                Quaternion.identity, Canvas.transform);
            StackObject.transform.SetAsFirstSibling();
            Plate = StackObject.transform.GetChild(0);
        }

        public void OnPointerDown(PointerEventData data)
        {
            Model.HandleClick();
        }

        public IEnumerator WaitAndCount(GameObject obj, float fallSpeed, int stackHeight)
        {
            yield return new WaitForSeconds(1 / fallSpeed);
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
                Tablecloth.GetComponent<TableclothScript>().started = false;

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
}