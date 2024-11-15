using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler
{
    public int radius;
    Texture2D texture;
    bool isMoving = false;
    int green = 255;
    int greenDirection = -1;
    // Start is called before the first frame update
    void Start()
    {
        texture = (Texture2D)GetComponent<RawImage>().texture;
        for (int i = 0; i < 600; ++i)
        {
            for (int j = 0; j < 600; ++j)
            {
                texture.SetPixel(i, j, new Color(0,0,0,1));
            }
        }
        texture.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Paint(PointerEventData data)
    {
        for (int i = -radius; i <= radius; ++i)
        {
            for (int j = -radius; j <= radius; ++j)
            {
                if (i * i + j * j <= radius * radius)
                    texture.SetPixel((int)(data.position.x - transform.position.x) + 300 + i,
                        (int)(data.position.y - transform.position.y) + 300 + j, new Color(0, (float)green/255, 1, 1));
            }
        }
        texture.Apply();
    }

    public void OnPointerDown(PointerEventData data)
    {
        Paint(data);
        isMoving = true;
    }
    public void OnPointerMove(PointerEventData data)
    {
        if (isMoving) 
        {
            green += greenDirection * 5;
            print(green);
            if (green == 0 || green == 255)
            {
                greenDirection *= -1;
            }
            Paint(data); 
        }
    }
    public void OnPointerUp(PointerEventData data)
    {
        isMoving = false;
        for (int i = 0; i < 600; ++i)
        {
            for (int j = 0; j < 600; ++j)
            {
                texture.SetPixel(i, j, new Color(0, 0, 0, 1));
            }
        }
        green = 255;
        greenDirection = -1;
        texture.Apply();
    }
}
