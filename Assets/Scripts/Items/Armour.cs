using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armour : Item
{
    public int defense; //deal wth this later;
    public GameObject effect;
    public bool equipped;

    void Equip()
    {
        //Doesnt get to here? fix later, its just cosmetic at the moment
        Debug.Log("hi");
        if(!equipped)
        {
            
            effect.SetActive(true);
            GetComponent<SpriteRenderer>().enabled = false;
            equipped = true;
        }
        
    }

    void Unequip()
    {
        if(equipped)
        {
            effect.SetActive(false); 
            GetComponent<SpriteRenderer>().enabled= true;
            equipped = false;
        }
    }

}
