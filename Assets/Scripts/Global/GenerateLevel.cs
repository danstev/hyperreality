using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject BackGround1, BackGround2;
    public GameObject Wall1;

    Map m;

    // Update is called once per frame
    void Update()
    {

    }

    //Matrix mappin

    void Start()
    {
        Map map = new Map(20, 20, 20);
        map.GenerateLevel1(20, 20, 20);
        m = map;
        Render();
    }

    class Map
    {
        public int x1, x2, y1, y2, z1, z2;
        public float[,,] level;

        public Map(int x, int y, int z)
        {
            level = new float[x, y, z];
            x1 = 0; y1 = 0; z1 = 0;
            x2 = x; y2 = y; z2 = z;
        }

        public float[,,] GenerateLevel1(int x, int y, int z)
        {
            level = MatFill(level, 0, x, 0, y, 0, z, 0.0f);
            level = MatFill(level, 2, x -2, 2, y -2, 2, z -2, 1.0f);
            //level = MatFill(level, 3, x - 3, 3, y - 3, 3, z - 3, 2.0f);
            level = MatFill(level, 3, x - 3, 3, y - 3, 3, z - 3, 0.0f);
            level = RandomFill(level, 3, x - 3, 3, y - 3, 3, z - 3, 2.0f);
            level = Smooth(level, 5, x - 5, 5, y - 5, 5, z - 5, 3.0f, 5);
            level = MatFill(level, 3, x - 3, 3, y - 3, 3, z - 3, 0.0f);
            return level;
        }

        private float[,,] RandomFill(float[,,] ToBeFilled, int x1, int x2, int y1, int y2, int z1, int z2, float val)
        {
            for (int x = x1; x < x2; x++)
            {
                for (int y = y1; y < y2; y++)
                {
                    for (int z = z1; z < z2; z++)
                    {
                        if(Random.Range(0.0f,1.0f) > 0.90f)
                        {
                            ToBeFilled[x, y, z] = val;
                        }
                        
                    }
                }
            }

            return ToBeFilled;
        }

        private float[,,] MatFill(float[,,] ToBeFilled, int x1, int x2, int y1, int y2, int z1, int z2, float val)
        {
            for (int x = x1; x < x2; x++)
            {
                for (int y = y1; y < y2; y++)
                {
                    for (int z = z1; z < z2; z++)
                    {
                        ToBeFilled[x, y, z] = val;
                    }
                }
            }

            return ToBeFilled;
        }

        private float[,,] Smooth(float[,,] ToBeFilled, int x1, int x2, int y1, int y2, int z1, int z2, float val, int repeats)
        {
            for (int i = 0; i < repeats; i++)
            {
                
                for (int x = x1; x < x2; x++)
                {
                    for (int y = y1; y < y2; y++)
                    {
                        for (int z = z1; z < z2; z++)
                        {
                            int score = SmoothCheck(x, y, z);
                            
                            if (score > 5)
                            {
                                ToBeFilled[x, y, z] = val;
                                Debug.Log(val);
                            }
                        }
                        }
                    }
                
            }

            return ToBeFilled;
        }

        int SmoothCheck(int x, int y, int z)
        {
            int average = 0;
            if (x == 0 || level[x-1,y,z] != 0.0f)
            {
                average++;
            }
            if (x == x2 || level[x+1, y, z] != 0.0f)
            {
                average++;
            }
            if (y == 0 || level[x, y-1, z] != 0.0f)
            {
                average++;
            }
            if (y == y2 || level[x, y+1, z] != 0.0f)
            {
                average++;
            }
            if (z == 0 || level[x, y, z-1] != 0.0f)
            {
                average++;
            }
            if (z == z2 || level[x, y, z+1] != 0.0f)
            {
                average++;
            }
            return average;
        }
    }

    

    void Render()
    {
        if (m.level == null)
        {
            //error wtf u playin at

        }
        for (int x = m.x1; x < m.x2; x++)
        {
            for (int y = m.y1; y < m.y2; y++)
            {
                for (int z = m.z1; z < m.z2; z++)
                {
                    {
                        if (m.level[x, y, z] == 0.0f)
                        {
                            
                        }
                        else if (m.level[x, y, z] == 1.0f)
                        {
                            GameObject w = Instantiate(Wall1, new Vector3(x, y, z), Quaternion.identity) as GameObject;
                        }
                        else if (m.level[x, y, z] == 2.0f)
                        {
                            GameObject bg = Instantiate(BackGround1, new Vector3(x, y, z), Quaternion.identity) as GameObject;
                        }
                        else if (m.level[x, y, z] == 3.0f)
                        {
                            GameObject bg2 = Instantiate(BackGround2, new Vector3(x, y, z), Quaternion.identity) as GameObject;
                        }
                    }
                }
            }

        }
    }

}