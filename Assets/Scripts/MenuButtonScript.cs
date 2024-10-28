using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonScript : MonoBehaviour
{
    public Screen buttonType;
    public GameObject mainCamera;
    MainScript mainScript;
    // Start is called before the first frame update
    void Start()
    {
        mainScript = mainCamera.GetComponent<MainScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        mainScript.LoadScrene(buttonType);
    }
}
