using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance;

    public Tilemap tilemap;
    public List<Vector3> pathNodes;

    public void SetInit()
    {
        Instance = this;
    }
    
    public void Activate()
    {
        pathNodes = GetTileInfoList();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //GetTileInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Vector3> GetTileInfoList()
    {
        Tilemap pathTilemap = tilemap; // ∞Ê∑Œ ≈∏¿œ∏ 
        BoundsInt bounds = pathTilemap.cellBounds;
        TileBase[] allTiles = pathTilemap.GetTilesBlock(bounds);
        List<Vector3> pathNodes = new List<Vector3>();

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Vector3Int tilePos = new Vector3Int(x + bounds.xMin, y + bounds.yMin, 0);
                    pathNodes.Add(tilePos);
                    Debug.Log($"Tile found at {tilePos}");
                }
            }
        }

        return pathNodes;
    }
}
