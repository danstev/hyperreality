using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorMAtt : MonoBehaviour
{
    int[,] map;

    public int height = 20;
    public int width = 20;

    void Start()
    {
        map = INITBlankMap(map, height, width);

        //MakeRoom(map, 5,5,5,5,1);
        MakeRoom(map, 10, 3, 10, 3, 2);

        //Smooth(map, 5, 1);
        //Replace(map, 2, 3);

        Debug.Log(map);
        DebugMap();
    }

    int [,] INITBlankMap(int[,] m, int x, int y)
    {
        m = new int[x,y];
        FillMap(m, 0);
        return m;
    }

    void FillMap(int[,] m, int f)
    {
        for(int x = 0; x < height; x++)
        {
            for(int y = 0; y < width; y++)
            {
                m[x, y] = f;
            }
        }
    }

    void Replace(int[,] m, int f, int r)
    {
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                if (m[x, y] == r)
                {
                    m[x, y] = f;
                }
            }
        }
    }

    void MakeRoom(int[,] map, int x1, int x2, int y1, int y2, int f)
    {
        if((x1 + x2) >= height)
        {
            return;
        }

        if ((y1 + y2) >= width)
        {
            return;
        }

        for (int x = x1; x <  x2; x++)
        {
            for (int y = y1; y < y2; y++)
            {
                map[x, y] = f;
            }
        }

    }


    void Smooth(int [,] map, int t, int f)
    {
        for (int i = 0; i < t; i++)
        {


            for (int x = 1; x < height - 1; x++)
            {
                for (int y = 1; y < width - 1; y++)
                {
                    int count = 0;
                    if (map[x + 1, y] != f)
                    {
                        count++;
                    }

                    if (map[x - 1, y] != f)
                    {
                        count++;
                    }

                    if (map[x, y - 1] != f)
                    {
                        count++;
                    }

                    if (map[x, y + 1] != f)
                    {
                        count++;
                    }

                    if (count > 2)
                    {
                        map[x, y] = f;
                    }
                }
            }
        }
    }


    void DebugMap()
    {
        for(int x = 0; x < height; x++)
        {
            string s = "";

            for (int y = 0; y< width; y++)
            {
                s += map[x, y];
            }
            Debug.Log(s);
        }
    }
}
