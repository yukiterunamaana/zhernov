using UnityEngine;
using UnityEngine.UI;

public class TileScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is create
    Image tileObject;
    public Tile Tile;
    float timer;
    GameDataScript data;
    int workers = 0;
    float animationTimer;
    float animationBuildingTimer;
    int currentTact=0;
    int currentBuildTact = 0;

    void Start()
    {
        data = MainScript.gameData;
    }

    // Update is called once per frame
    void Update()
    {
        if (animationTimer > 1 / (float)Tile.icons.Count)
        {
            currentTact = (currentTact + 1) % Tile.icons.Count;
            animationTimer = 0f;
        }
        animationTimer += Time.deltaTime;
        GetComponent<Image>().sprite = MainScript.ConfigManager.Sprites[Tile.icons[currentTact]];
        if (Tile.building is not null && Tile.buildingCenter.x == Tile.x && Tile.buildingCenter.y == Tile.y)
        {
            if (animationBuildingTimer > 1 / (float)Tile.building.icons[Tile.building.level].Length)
            {
                currentBuildTact = (currentBuildTact + 1) % Tile.building.icons[Tile.building.level].Length;
                animationBuildingTimer = 0f;
            }
            animationBuildingTimer += Time.deltaTime;
            animationTimer += Time.deltaTime;
            tileObject.sprite = MainScript.ConfigManager.Sprites[Tile.building.icons[Tile.building.level][currentBuildTact]];
            var b = MainScript.ConfigManager.Buildings[Tile.building.type];
            tileObject.rectTransform.sizeDelta = new Vector2(b.width, b.height);
            tileObject.color = Color.white;
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                if (Tile.building.type == "sawmill" && workers == Tile.building.workers)
                {
                    data.resources["wood"] += 2;
                }
                if (Tile.building.workers > workers && data.resources["workers"] > 0)
                {
                    workers++;
                    data.resources["workers"]--;
                }
                timer = 0f;
            }
        }
        else if (Tile.obj is not null)
        {
            tileObject.sprite = MainScript.ConfigManager.Sprites[Tile.obj.icon];
            tileObject.color = Color.white;
        }
        else
        {
            tileObject.color = new Color(0, 0, 0, 0);
        }
    }

    public void Create (Image image, Tile tile, Transform parent)
    {
        this.Tile = tile;
        tileObject = Instantiate(image, new Vector3(Tile.x, Tile.y, 0), Quaternion.identity, parent);
    }
}
