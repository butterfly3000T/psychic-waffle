using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ReadTilesMap : MonoBehaviour
{
    private BaseTile[,] tileMap;

    private int left, right, down, up = 0;

    private List<Transform> tiles = new List<Transform>();

    public static Action<BaseTile[,]> OnMapRead;

    private BaseTile[,] BaseTiles => tileMap;


    private void Awake()
    {
        foreach(Transform childTile in transform)
        {
            tiles.Add(childTile);
        }

        foreach(Transform tile in tiles)
        {
            CheckPosition(tile.position);
        }

        int width = (-left + right);
        int height = (-down + up);
        tileMap = new BaseTile[width, height];

        Debug.Log($"left: {left}; right: {right}; down: {down}; up: {up}; ");


        foreach (Transform tile in tiles)
        {
            int x = (int)((tile.position.x - left - 0.5));
            int z = (int)((tile.position.z - up + 0.5) * (-1));

            tileMap[x, z] = tile.GetComponent<BaseTile>();
            tileMap[x, z].Init(x, z);
        }

        GetComponent<TurnController>().InitMap(tileMap);
    }

    private void CheckPosition(Vector3 tilePosition)
    {
        if (tilePosition.x < left)
        {
            left = (int)(tilePosition.x - 0.5);
        }
        if(tilePosition.x > right)
        {
           right = (int)(tilePosition.x + 0.5);
        }
        if (tilePosition.z < down)
        {
            down = (int)(tilePosition.z - 0.5);
        }
        if(tilePosition.z > up)
        {
            up = (int)(tilePosition.z + 0.5);
        }
    }
}
