using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float timer, range;
    public float tempTimer;
    public GameObject[] objects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tempTimer += Time.deltaTime;

        if(timer <= tempTimer)
        {
            tempTimer = 0;
            Vector3 pos = transform.position;
            pos.x += Random.Range(-range, range);
            pos.y += Random.Range(-range, range);
            SpawnObject(objects[Random.Range(0,objects.GetLength(0))], pos);
            
        }
    }

    void SpawnObject(GameObject g, Vector3 p)
    {
        GameObject s = Instantiate(g, p, Quaternion.identity);
        s.transform.localScale = new Vector3(Random.Range(1f, 1f+ 1f/(range)), Random.Range(1f, 1f+ 1f/(range)), Random.Range(1f, 1f+ 1f/(range)));
        Color RCol = new Color( Random.Range(0f, 1f),  Random.Range(0f, 1f),  Random.Range(0f, 1f));
        s.GetComponent<SpriteRenderer>().color = RCol;
        //health
        //stats
    }
}
