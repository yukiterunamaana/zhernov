using System;
using UnityEngine;

public class PancakeScript
{
    public GameDataScript gameData;
    public float CurrentAngle;
    public int stackSize = 0;
    public float speed = 0f;
    public float maxspeed = 50f;
    public float timer = 0f;
    public float lastClick = 1f;
    public int limitedHeight = 15;
    public float RoutateMod = 10;

    public Action<int> OnPancakeFall;
    public Action OnStackFull;

    public PancakeScript(GameDataScript data)
    {
        gameData = data;
    }

    public void Update()
    {
        timer += Time.deltaTime;
        lastClick += Time.deltaTime;
        if (timer >= 0.25f)
        {
            timer = 0f;
            speed -= (float)(5f - gameData.gameModifiers["brakeing"] * 0.1); /** lastClick * lastClick * lastClick*/
            if (speed < 0)
                speed = 0;
        }

        CurrentAngle += speed * Time.deltaTime * RoutateMod;
        
        if (CurrentAngle >= 360) 
        {
            CurrentAngle %= 360;
            var pancakesCount = 1 + gameData.gameModifiers["mod"];
            gameData.resources["pancakes"] += pancakesCount;
            OnPancakeFall.Invoke(pancakesCount);
            stackSize++;
            if (stackSize >= limitedHeight)
            {
                stackSize = 0;
                OnStackFull.Invoke();
            }
        }
    }
    public void HandleClick() 
    {
        gameData.Klick++;
        if (gameData.Klick == 100)
        {
            gameData.resources["pancakes"] += (int)(gameData.gameModifiers["add_mod"] * 0.01 * (float)gameData.resources["pancakes"]); //it coud be fixed
            gameData.Klick = 0;
        }
        speed += (float)(10f + (10 * gameData.gameModifiers["accelerator"]));
        lastClick = 1f;
        maxspeed = 50f + gameData.gameModifiers["limit"] * 10;
        if (speed > maxspeed)
            speed = maxspeed;
    }
}
