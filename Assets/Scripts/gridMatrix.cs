using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridMatrix : MonoBehaviour
{
    public static int score = 0, hs = PlayerPrefs.GetInt("highscore", hs);
    public static int w = 7, b = 7, h = 20;
    public static Transform[, ,] grid = new Transform[w, h, b];

    public static Vector3 roundVec3(Vector3 v)
    {
        return new Vector3(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
    }

    public static bool insideBorder (Vector3 pos)
    {
        print(pos.x);
        print(pos.y);
        print(pos.z);

        return ((int)pos.x >= 0 &&
                (int)pos.x < w &&
                (int)pos.z >= 0 &&
                (int)pos.z < b &&
                (int)pos.y >= 0);
    }

    public static void deleteLayer(int y)
    {
        for (int x=0;x < w;x++)
        {
            for(int z=0;z < b;z++)
            {
                Destroy(grid[x, y, z].gameObject);
                grid[x, y, z] = null;
            }
        }
    }

    public static void decreaseLayer(int y)
    {
        for (int x=0; x < w; ++x)
        {
            for(int z=0; z<b;z++)
            {
                if (grid[x,y,z] != null)
                {
                    grid[x, y - 1, z] = grid[x, y, z];
                    grid[x, y, z] = null;
                    grid[x, y - 1, z].position += new Vector3(0, -1, 0);
                }
            }
        }
    }

    public static void decreaseLayerAbove (int y)
    {
        for (int i = y; i < h; ++i)
            decreaseLayer(i);
    }

    public static bool isLayerFull(int y)
    {
        for(int x=0;x < w;x++)
        {
            for(int z=0;z < b; z++)
            {
                if (grid[x, y, z] == null)
                    return false;
            }
        }
        return true;
    }

    public static void deleteFullLayer()
    {
        for(int y=0;y<h;y++)
        {
            if(isLayerFull(y))
            {
                deleteLayer(y);
                score = score + 250;
                decreaseLayerAbove(y + 1);
                --y;
            }
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
