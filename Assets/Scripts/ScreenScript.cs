using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScreenScript : MonoBehaviour, IDragHandler
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        float x = Camera.main.transform.position.x + eventData.delta.x / 100;
        float y = Camera.main.transform.position.y + eventData.delta.y / 100;
        Camera.main.transform.position = new Vector3(x, y, -10);
    }
}
