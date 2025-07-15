using UnityEngine;
using ReformEnt;

public class TileMapSystem
{

    int width;
    int firstDimention = 0;
    int height;
    int secondDimention = 1;

    bool isHorizontalGrid;
   
    int[,] tiles;
    float tileSize;

    public TileMapSystem(Transform parentGridObject, string tileGameObjectName, int width, int height, float tileSize, bool horizontalGrid){

        
        this.width = width;
        this.height = height;
        this.tileSize = tileSize;
        this.isHorizontalGrid = horizontalGrid;

        tiles = new int[width, height];

        for (int x = 0; x < tiles.GetLength(firstDimention); x++) 
        {
            for (int y = 0; y < tiles.GetLength(secondDimention); y++)
            {
                if (isHorizontalGrid)
                {
                    Utilities.UI.CreateWorldText(tileGameObjectName, tiles[x, y].ToString(), parentGridObject, GetWorldPositionForHorizontalGrid(x, y) + new Vector3(tileSize,tileSize) * .5f, 8, Color.white, TextAnchor.MiddleCenter, TextAlignment.Center);
                    Debug.DrawLine(GetWorldPositionForHorizontalGrid(x, y), GetWorldPositionForHorizontalGrid(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPositionForHorizontalGrid(x, y), GetWorldPositionForHorizontalGrid(x + 1, y), Color.white, 100f);
                }
                else
                {
                    Utilities.UI.CreateWorldText(tileGameObjectName, tiles[x, y].ToString(), parentGridObject, GetWorldPositionForVerticalGrid(x, y) + new Vector3(tileSize,tileSize) * .5f, 8, Color.white, TextAnchor.MiddleCenter, TextAlignment.Center);
                    Debug.DrawLine(GetWorldPositionForVerticalGrid(x, y), GetWorldPositionForVerticalGrid(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPositionForVerticalGrid(x, y), GetWorldPositionForVerticalGrid(x + 1, y), Color.white, 100f);
                }

            }
        }

        if (isHorizontalGrid)
        {
            Debug.DrawLine(GetWorldPositionForHorizontalGrid(0, height), GetWorldPositionForHorizontalGrid(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPositionForHorizontalGrid(width, 0), GetWorldPositionForHorizontalGrid(width, height), Color.white, 100f);
        }
        else 
        {
            Debug.DrawLine(GetWorldPositionForVerticalGrid(0, height), GetWorldPositionForVerticalGrid(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPositionForVerticalGrid(width, 0), GetWorldPositionForVerticalGrid(width, height), Color.white, 100f);
        }
    }

    private Vector3 GetWorldPositionForVerticalGrid(int x, int y)
    {
        return new Vector3(x, y) * tileSize;
    }
    private Vector3 GetWorldPositionForHorizontalGrid(int x, int y) 
    {
        return new Vector3(x, 0, y) * tileSize;
    }
    private void GetXYForVerticalGrid(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPosition.x / tileSize);
        y = Mathf.FloorToInt(worldPosition.y / tileSize);
    }
    private void GetXYForHorizontalGrid(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPosition.x / tileSize);
        y = Mathf.FloorToInt(worldPosition.z / tileSize);
    }
    public void SetTileValue(int x, int y, int value)
    {
        if ((x > 0 && y > 0) && (x < width && y < height))
        {
            tiles[x, y] = value;
        }
    }

    public void SetTileValue(Vector3 worldPosition, int value) 
    {
        int x, y;
        if (isHorizontalGrid)
        {
            GetXYForHorizontalGrid(worldPosition, out x, out y);
            SetTileValue(x, y, value);
        }
        else 
        {
            GetXYForVerticalGrid(worldPosition, out x, out y);
            SetTileValue(x, y, value);
        }
    }
}
