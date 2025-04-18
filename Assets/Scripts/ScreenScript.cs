using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScreenScript : MonoBehaviour, IDragHandler
{
    public GameDataScript data;
    // Start is called before the first frame update
    void Start()
    {
        data = MainScript.gameData;
        Camera.main.transform.position = new Vector3(data.state.width / 2, data.state.height / 2, -10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        float x = Camera.main.transform.position.x + eventData.delta.x / 100;
        float y = Camera.main.transform.position.y + eventData.delta.y / 100;
        float rX = x + 0.5f + Camera.main.orthographicSize * Camera.main.aspect;
        float rY = y + 0.5f + Camera.main.orthographicSize;
        float lX = x - 0.5f - Camera.main.orthographicSize * Camera.main.aspect;
        float lY = y - 0.5f - Camera.main.orthographicSize;
        if (lX > 0 && rX < data.state.width && 
            lY > 0 && rY < data.state.height)
        {
            Camera.main.transform.position = new Vector3(x, y, -10);
        }
    }
}
