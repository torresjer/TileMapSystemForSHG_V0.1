using UnityEngine;

public class TileMapSystem
{
    int width;
    int height;
    int[,] tiles;
    float cellScale;

    public TileMapSystem(int width, int height){

        this.width = width;
        this.height = height;

        tiles = new int[width, height];
    }

}
