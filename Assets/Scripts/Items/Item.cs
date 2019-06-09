using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName; 
    public bool equipable; //True is weapon/armour, so send it to inv system, false is just run the use();
    public bool inInv = false;
    public enum enItemType {ITEM_UNKNOWN = 0, ITEM_ARMOUR, ITEM_WEAPON}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use(GameObject other)
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(equipable)
        {
            Debug.Log(other.transform.name + " has interacted with " + transform.name + ".");
            other.gameObject.SendMessage(("AddItem"), gameObject, SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            Use(other.gameObject);
        }
        
    }

    public virtual enItemType GetItemType() {
        return enItemType.ITEM_UNKNOWN;
    }
}
