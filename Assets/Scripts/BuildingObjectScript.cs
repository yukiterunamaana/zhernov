using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingObjectScript : MonoBehaviour
{
    public bool IsBuilt = false;
    public int CollidingCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("building") ||other.CompareTag("water") || other.CompareTag("tree"))
        {
            CollidingCount++;
            if (!IsBuilt)
            {
                GetComponent<Image>().color = new Color(0, 0, 0, 0.0f);
            }
        }

    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("building") || other.CompareTag("water") || other.CompareTag("tree"))
        {
            CollidingCount--;
            if (!IsBuilt && CollidingCount == 0)
            {
                GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
            }
        }
    }
}
