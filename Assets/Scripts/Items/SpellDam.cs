using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDam : Item
{
    public int SpellDamage;
    public int SpellSpeed;
    public bool equipped;
    public Collider2D WeaponColl;
    public AudioClip[] HitSounds;
    public AudioSource HitSource;
    public enItemType itemType = enItemType.ITEM_WEAPON;
    public bool FriendCheck =true;
    public string TagToCheck;
    public bool shouldDestroyOnCollision = false;


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
            Debug.Log(gameObject.tag + " hit: " + other.gameObject.tag + " will apply damage if tag isnt: " + TagToCheck);
            if (other.gameObject.tag != TagToCheck)
            {
                Debug.Log(gameObject.tag + " hit: " + other.gameObject.tag + " with: " + SpellDamage + " damage");
                other.gameObject.SendMessage(("TakeDamage"), SpellDamage, SendMessageOptions.DontRequireReceiver);
                if (shouldDestroyOnCollision)
                    Destroy(gameObject);
            }
            else
            {

            }
        
    }

}
