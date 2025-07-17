using UnityEngine;
using ReformEnt;

public class TileMapSystem
{

    int width;
    int firstDimention = 0;
    int height;
    int secondDimention = 1;

    bool isHorizontalGrid;
    Vector3 tileOriginPosition = Vector3.zero;
    int[,] tiles;
    TextMesh[,] debugArray;
    float tileSize;

    public TileMapSystem(Transform parentGridObject, string tileGameObjectName, int width, int height, float tileSize, bool horizontalGrid, Vector3 originPosition){

        
        this.width = width;
        this.height = height;
        this.tileSize = tileSize;
        this.isHorizontalGrid = horizontalGrid;
        this.tileOriginPosition = originPosition;

        tiles = new int[width, height];
        debugArray = new TextMesh[width, height];

        for (int x = 0; x < tiles.GetLength(firstDimention); x++) 
        {
            for (int y = 0; y < tiles.GetLength(secondDimention); y++)
            {
                if (isHorizontalGrid)
                {
                    debugArray[x,y] = Utilities.UI.CreateWorldText(tileGameObjectName, tiles[x, y].ToString(), parentGridObject, GetWorldPositionForHorizontalTile(x, y) + new Vector3(tileSize,tileSize) * .5f, 8, Color.white, TextAnchor.MiddleCenter, TextAlignment.Center);
                    Debug.DrawLine(GetWorldPositionForHorizontalTile(x, y), GetWorldPositionForHorizontalTile(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPositionForHorizontalTile(x, y), GetWorldPositionForHorizontalTile(x + 1, y), Color.white, 100f);
                }
                else
                {
                    Utilities.UI.CreateWorldText(tileGameObjectName, tiles[x, y].ToString(), parentGridObject, GetWorldPositionForVerticalTile(x, y) + new Vector3(tileSize,tileSize) * .5f, 8, Color.white, TextAnchor.MiddleCenter, TextAlignment.Center);
                    Debug.DrawLine(GetWorldPositionForVerticalTile(x, y), GetWorldPositionForVerticalTile(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPositionForVerticalTile(x, y), GetWorldPositionForVerticalTile(x + 1, y), Color.white, 100f);
                }

            }
        }

        if (isHorizontalGrid)
        {
            Debug.DrawLine(GetWorldPositionForHorizontalTile(0, height), GetWorldPositionForHorizontalTile(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPositionForHorizontalTile(width, 0), GetWorldPositionForHorizontalTile(width, height), Color.white, 100f);
        }
        else 
        {
            Debug.DrawLine(GetWorldPositionForVerticalTile(0, height), GetWorldPositionForVerticalTile(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPositionForVerticalTile(width, 0), GetWorldPositionForVerticalTile(width, height), Color.white, 100f);
        }
    }

    private Vector3 GetWorldPositionForVerticalTile(int x, int y)
    {
        return new Vector3(x, y) * tileSize + tileOriginPosition;
    }
    private Vector3 GetWorldPositionForHorizontalTile(int x, int y) 
    {
        return new Vector3(x, 0, y) * tileSize + tileOriginPosition;
    }
    private void GetTileFromVerticalGrid(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - tileOriginPosition).x / tileSize);
        y = Mathf.FloorToInt((worldPosition - tileOriginPosition).y / tileSize);
    }
    private void GetTileFromHorizontalGrid(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - tileOriginPosition).x / tileSize);
        y = Mathf.FloorToInt((worldPosition - tileOriginPosition).z / tileSize);
    }
    public int GetTileValue(int x, int y)
    {

        if ((x >= 0 && y >= 0) && (x < width && y < height))
        {
            return tiles[x, y];
        }

        //tile not found
        return -1;
    }
    public int GetTileValue(Vector3 worldPosition)
    {
        int x, y;
        if (isHorizontalGrid)
        {
            GetTileFromHorizontalGrid(worldPosition, out x, out y);
            return GetTileValue(x, y);
        }
        else
        {
            GetTileFromVerticalGrid(worldPosition, out x, out y);
            return GetTileValue(x, y);
        }
    }

    public void SetTileValue(int x, int y, int value)
    {
        if ((x >= 0 && y >= 0) && (x < width && y < height))
        {
            tiles[x, y] = value;
            debugArray[x,y].text = tiles[x, y].ToString();
        }
    }

    public void SetTileValue(Vector3 worldPosition, int value) 
    {
        int x, y;
        if (isHorizontalGrid)
        {
            GetTileFromHorizontalGrid(worldPosition, out x, out y);
            SetTileValue(x, y, value);
        }
        else 
        {
            GetTileFromVerticalGrid(worldPosition, out x, out y);
            SetTileValue(x, y, value);
        }
    }
}
