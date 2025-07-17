using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Vector2 xMinMaxBoundsForLevel;
    [SerializeField] Vector2 yMinMaxBoundsForLevel;
    [SerializeField] Vector2 zMinMaxBoundsForLevel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetXMinMaxBoundsForLevel() { return xMinMaxBoundsForLevel; }
    public Vector2 GetYMinMaxBoundsForLevel() { return yMinMaxBoundsForLevel; }
    public Vector2 GetZMinMaxBoundsForLevel() { return zMinMaxBoundsForLevel; }
}
