using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BuildingObjectScript : MonoBehaviour
{
    public BuildingObject building;
    float timer;
    GameDataScript data;
    int workers = 0;
    void Start()
    {
        data = MainScript.gameData;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer>=1f && workers == building.workers)
        {
            if (building.type=="sawmill")
            {
                data.resources["wood"] += 2;
            }
            timer = 0f;
        }
        if (building.workers > workers && data.resources["workers"] > 0)
        {
            workers++;
            data.resources["workers"]--;
        }
    }
}
