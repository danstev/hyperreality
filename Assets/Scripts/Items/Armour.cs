using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armour : Item
{
    public int defense; //deal wth this later;
    public GameObject effect;
    public bool equipped;

    public void equip()
    {
        //Doesnt get to here? fix later, its just cosmetic at the moment
        Debug.Log("Armour equipped");
        if (!equipped)
        {
            effect.SetActive(true);
            GetComponent<SpriteRenderer>().enabled = false;
            equipped = true;
        }     
    }

    public void unequip()
    {
        if(equipped)
        {
            effect.SetActive(false); 
            GetComponent<SpriteRenderer>().enabled= true;
            equipped = false;
        }
    }

}
