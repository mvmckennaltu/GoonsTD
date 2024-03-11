using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;

public class TowerPlacement : MonoBehaviour
{
    public static int playerMoney = 100;
    public TextMeshProUGUI moneyText;
    public Tilemap placeableTilemap;
    private bool isPlacingOffenseTower = false;
    private bool isPlacingHealTower = false;
    public GameObject offenseTowerPrefab;
    public GameObject healTowerPrefab;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlacingOffenseTower && Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickedTilePos = placeableTilemap.WorldToCell(mouseWorldPos);

            if (CanPlaceTower(clickedTilePos))
            {
                Vector3 towerPosition = placeableTilemap.GetCellCenterWorld(clickedTilePos);
                

                Vector3 tileSize = placeableTilemap.cellSize;
                Vector3 towerSize = offenseTowerPrefab.GetComponent<SpriteRenderer>().bounds.size;
                Vector3 adjustedPosition = towerPosition + new Vector3(tileSize.x / 2 - towerSize.x / 2, tileSize.y / 2 - towerSize.y / 2, 0);

                Instantiate(offenseTowerPrefab, adjustedPosition, Quaternion.identity);
                playerMoney -= 10; // Deduct money only if the tower is successfully placed
            }

            isPlacingOffenseTower = false;
        }

        if (isPlacingHealTower && Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickedTilePos = placeableTilemap.WorldToCell(mouseWorldPos);

            if (CanPlaceTower(clickedTilePos))
            {
                Vector3 towerPosition = placeableTilemap.GetCellCenterWorld(clickedTilePos);
                

                Vector3 tileSize = placeableTilemap.cellSize;
                Vector3 towerSize = healTowerPrefab.GetComponent<SpriteRenderer>().bounds.size;
                Vector3 adjustedPosition = towerPosition + new Vector3(tileSize.x / 2 - towerSize.x / 2, tileSize.y / 2 - towerSize.y / 2, 0);

                Instantiate(healTowerPrefab, adjustedPosition, Quaternion.identity);
                playerMoney -= 10; // Deduct money only if the tower is successfully placed
            }

            isPlacingHealTower = false;
        }

        if (moneyText != null)
        {
            moneyText.text = playerMoney.ToString();
        }
    }

    private bool CanPlaceTower(Vector3Int tilePos)
    {
        // Check if the clicked tile position is valid for tower placement
        return placeableTilemap.HasTile(tilePos) && IsTileEmpty(tilePos);
    }
    public void StartPlacingOffenseTower()
    {
        isPlacingOffenseTower = true;
        playerMoney = playerMoney - 10;
    }
    public void StartPlacingHealTower()
    {
        isPlacingHealTower = true;
        playerMoney = playerMoney - 10;
    }

    private bool IsTileEmpty(Vector3Int tilePos)
    {
        // Implement logic to check if the tile is empty (no tower placed)
        // For example, check if there's no tower at the clicked position
        return true;
    }
}
