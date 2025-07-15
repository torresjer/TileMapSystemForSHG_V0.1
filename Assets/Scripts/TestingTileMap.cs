using UnityEngine;

public class TestingTileMap : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] float tileSize;
    [SerializeField] bool isHorizontalGrid = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TileMapSystem thisTileMap = new TileMapSystem(this.transform, "TestingTile", width, height, tileSize, isHorizontalGrid);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
