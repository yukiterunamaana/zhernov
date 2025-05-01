using System;
using UnityEngine;

public class PancakeModel
{
    public GameDataScript gameData;
    public float CurrentAngle = 0f;
    public int stackSize = 0;

    float timer = 0f;
    public float lastClick = 1f;
    public float speed = 0f;
    public readonly float RoutateMod = 10;
    public readonly int limitedHeight = 15;
    float MaxSpeed = 50f;
    public float FallSpeed = 0f;


    public Action<float> OnPancakeFall;
    public Action<float> OnStackFull;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PancakeModel(GameDataScript gameData)
    {
        this.gameData = gameData;
    }
    // Update is called once per frame
    public void Update()
    {
        timer += Time.deltaTime;
        lastClick += Time.deltaTime;
        if (timer >= 0.25f)
        {
            timer = 0f;
            speed -= (float)(5f - gameData.gameModifiers["brakeing"] * 0.1); /** lastClick * lastClick * lastClick*/;
            if (speed < 0)
                speed = 0;
        }
        CurrentAngle += speed * Time.deltaTime * RoutateMod;
        if (CurrentAngle >= 360)
        {
            CurrentAngle = 0f + CurrentAngle / 360;
            gameData.resources["pancakes"] += 1 + gameData.gameModifiers["mod"];
            FallSpeed = 1.8f * limitedHeight / (1.8f * limitedHeight - stackSize);
            OnPancakeFall.Invoke(FallSpeed);
            stackSize++;
            if (stackSize >= limitedHeight)
            {
                stackSize = 0;
                OnStackFull.Invoke(FallSpeed);
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
        MaxSpeed = 50f + gameData.gameModifiers["limit"] * 10;
        if (speed > MaxSpeed)
            speed = MaxSpeed;
    }

}
