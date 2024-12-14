using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationScript : MonoBehaviour
{
    Sprite SourceImage;
    public GameDataScript gameData;
    void Start()
    {
        SourceImage = GetComponent<Image>().sprite;
    }
    void OnDisable()
    {
        Destroy(GetComponent<Animator>());
        GetComponent<Image>().sprite = SourceImage;
    }
}
