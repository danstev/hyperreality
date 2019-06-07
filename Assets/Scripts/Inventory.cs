using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public GameObject[] inv;
    public Armour arm;
    // Start is called before the first frame update
    void Start()
    {
        inv = new GameObject[10];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //drop all, drop 1,

    void AddItem(GameObject i)
    {
        Debug.Log("AddItemTriggered");
        Debug.Log(i);
        int count = 0;
        while(inv[count] != null)
        {
            count++;
        }
        inv[count] = i;
        i.transform.parent = gameObject.transform;
        //i.SetActive(false);
        i.transform.localPosition = new Vector3(0,0,0);
        arm = i.GetComponent<Armour>();
        Debug.Log(arm);
        arm.equip();
    }

    void DropItem(int pos)
    {
        if(inv[pos] != null)
        {
            inv[pos].transform.parent = null;
            inv[pos].GetComponent<Item>().enabled = true;//inv[pos].SetActive(true);
            inv[pos] = null;
        }
    }
}
