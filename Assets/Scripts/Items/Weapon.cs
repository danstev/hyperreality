using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public int WeaponDamage;
    public int WeaponSpeed;
    public bool equipped;
    public Collider2D WeaponColl;
    public AudioClip[] HitSounds;
    public AudioSource HitSource;

    public bool FriendCheck =true;
    public string NameToCheck;
    public bool shouldDestroyOnCollision = false;

    public Ability one, two;

    // Start is called before the first frame update
    void Start()
    {
        WeaponColl = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(equipped)
        {
            if(HitSource != null)
            {
                HitSource.clip = HitSounds[Random.Range(0,HitSounds.Length)];
                HitSource.Play();
            }
            Debug.Log(gameObject.name + " hit: " + other.gameObject.name + " will apply damage if last name isnt: " + NameToCheck);
            if (other.gameObject.name != NameToCheck)
            {
                Debug.Log(gameObject.name + " hit: " + other.gameObject.name + " with: " + WeaponDamage + " damage");
                other.gameObject.SendMessage(("TakeDamage"), WeaponDamage, SendMessageOptions.DontRequireReceiver);
                if (shouldDestroyOnCollision)
                    Destroy(gameObject);
            }
            else
            {

            }
        }
        else
        {
            if(equipable && !inInv)
            {
                Debug.Log(other.transform.name + " has interacted with " + transform.name + ".");
                other.gameObject.SendMessage(("AddItem"), gameObject, SendMessageOptions.DontRequireReceiver);
            }
            else
            {

            }
        }
        
    }

    public void equip()
    {
        if (!equipped)
        {
            equipped = true;
        }     
    }
}
