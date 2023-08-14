using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] Tilemap startingGround;
    [SerializeField] string groundTile;

    [SerializeField] TileBase oilTile;
    [SerializeField] TileBase desertTile;
    [SerializeField] int numberOfTilesToInstantiate = 5;
    [SerializeField] Vector2Int areaSize;
    private Vector2Int offset;
    Vector3Int centerTilePosition;

    private List<GameObject> spawnedTiles = new List<GameObject>();

        // Setup Ground
    void Awake()
    {
        centerTilePosition = startingGround.WorldToCell(transform.position); // Find Center
        ClearGround();

        Instantiate(startingGround, centerTilePosition, Quaternion.identity, GameObject.Find("Grid").transform);
        SetTileset(startingGround, desertTile);

            // Randomly Add Oiled Tiles
    }

        // find approprite offset
     private Vector2Int GetRandomOffsetWithinArea() 
    {
        int halfAreaWidth = Mathf.FloorToInt(areaSize.x / 2f);
        int halfAreaHeight = Mathf.FloorToInt(areaSize.y / 2f);

        int x = Random.Range(-halfAreaWidth, halfAreaWidth + 1);
        int y = Random.Range(-halfAreaHeight, halfAreaHeight + 1);

        return new Vector2Int(x, y);
    }
        
        // Set starting ground tileset
      public void SetTileset(Tilemap tilemap, TileBase tileBase)
    {
        tilemap.ClearAllTiles();

        Vector3Int[] positions = new Vector3Int[areaSize.x * areaSize.y];
        TileBase[] tileArray = new TileBase[positions.Length];

        for (int index = 0; index < positions.Length; index++)
        {
            positions[index] = new Vector3Int(index % areaSize.x, index / areaSize.y, 0);
            tileArray[index] = index % 2 == 0 ? desertTile : oilTile;
        }

        tilemap.SetTiles(positions, tileArray);  
    }

        // Destory All Ground Tiles
    void ClearGround()
    {
        GameObject[] groundTiles = GameObject.FindGameObjectsWithTag(groundTile);
        foreach(GameObject ground in groundTiles)
        {
            Destroy(ground.gameObject);
        }
    }
}






  
