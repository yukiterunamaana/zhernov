using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EditorData", menuName = "Editor Data", order = 52)]
public class EditorData : ScriptableObject
{
    // Start is called before the first frame update
    public int width = 20;
    public int height = 20;
}
