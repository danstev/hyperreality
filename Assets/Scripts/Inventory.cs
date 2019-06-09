using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] inv;
    public Armour arm;
    public Vector3 ArmPos = new Vector3(0,0,0);
    public Weapon wea;
    public Vector3 WeaPos = new Vector3(0.5f,0.125f,0);
    private int itemSlots = 10;

    // Player Item Stats
    public int totalDefence = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        inv = new GameObject[itemSlots];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //drop all, drop 1,

    void EquipItem(GameObject i, int type)
    {
        if(type == 1)
        {
            wea = i.GetComponent<Weapon>();
            wea.equipped = true;
            wea.inInv = true;
        }
    }

    void AddItem(GameObject i)
    {
        int count = 0;
        while(inv[count] != null)
        {
            count++;
        }
        inv[count] = i;
        i.transform.parent = gameObject.transform;
        //i.SetActive(false);
        i.transform.localPosition = new Vector3(0,0,0);
        EquipItem(i, 1);
        //arm = i.GetComponent<Armour>();
       // Debug.Log(arm);
       // arm.equip();
        UpdateAllStats();
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

    void UpdateAllStats() {
        GetComponent<PlayerControl>().UpdateAbilities(wea.gameObject, wea.one, wea.two);
        //Will redo this

        /*
        int currentDefence = 0;
        for (int i = 0; i < itemSlots; i++)
        {
            if (inv[i] == null)
                continue;
            GameObject current = inv[i];
            Armour currentArmour = current.GetComponent<Armour>();
            if (currentArmour)
                currentDefence += currentArmour.defense;
        }
        totalDefence = currentDefence;
        */
    }
}