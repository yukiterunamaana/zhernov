using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LandscapeButtonScript : MonoBehaviour, IPointerDownHandler
{
    public EditorData EditorData;
    public string type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (EditorData.brush == type)
        {
            EditorData.brush = "none";
        }
        else
        {
            EditorData.brush = type;
        }
    }
}
