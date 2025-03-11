using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableclothScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool started;
    private RawImage img;
    void Start()
    {
        img = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            img.uvRect = new Rect((img.uvRect.x + Time.deltaTime)%0.5f, 0, 2, 1);
        }
    }
}
