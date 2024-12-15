using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Game Data", order = 51)]
public class GameDataScript : ScriptableObject
{
    // Start is called before the first frame update
    public int score = 0;
    public int mod = 0;
    public int PPS = 0;
    public int Klick = 0;
    public double add_mod = 0;
    public Screen currentScreen = Screen.Grind;
    public int stackSize = 0;
}
public enum Screen
{
    Tasks,
    Grind,
    Map,
    Shop
}