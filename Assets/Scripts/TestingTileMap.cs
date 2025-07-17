using UnityEngine;

public class TestingTileMap : MonoBehaviour
{
    [SerializeField] Vector3 tileMapOrigin = Vector3.zero;
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] float tileSize;
    [SerializeField] bool isHorizontalGrid = true;

    [SerializeField] int testValue = 56;
    TileMapSystem thisTileMap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         thisTileMap = new TileMapSystem(this.transform, "TestingTile", width, height, tileSize, isHorizontalGrid, tileMapOrigin);
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManagerForSHGV01.Instance.GetisLeftClickPressed())
            thisTileMap.SetTileValue(InputManagerForSHGV01.Instance.GetMouseWorldPosition(), testValue);

        if (InputManagerForSHGV01.Instance.GetisRightClickPressed())
            Debug.Log(thisTileMap.GetTileValue(InputManagerForSHGV01.Instance.GetMouseWorldPosition()));
    }
}
