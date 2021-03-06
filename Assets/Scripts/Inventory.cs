﻿using System.Collections;
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

    void EquipItem(GameObject i, Item.enItemType type)
    {
        if(type == Item.enItemType.ITEM_WEAPON)
        {
            wea = i.GetComponent<Weapon>();
            wea.equipped = true;
            wea.inInv = true;
        }
        else if (type == Item.enItemType.ITEM_ARMOUR)
        {
            arm = i.GetComponent<Armour>();
            SpriteRenderer currentRender = arm.GetComponent<SpriteRenderer>();
            if (currentRender != null) {
                currentRender.enabled = false;
            }
            
            arm.equipped = true;
            arm.inInv = true;
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
        i.transform.localPosition = new Vector3(0,0,0);
        Item currentItem = i.GetComponent<Item>();
        if(!currentItem)
            return;
        EquipItem(i, currentItem.GetItemType());

        UpdateAllStats();
    }

    void DropItem(int pos)
    {
        if(inv[pos] != null)
        {
            //checktype
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