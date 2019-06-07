using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public int WeaponDamage;
    public int WeaponSpeed;

    public Collider2D WeaponColl;
    public AudioClip[] HitSounds;
    public AudioSource HitSource;

    public bool FriendCheck =true;
    public string NameToCheck;

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
        if(HitSource != null)
        {
            HitSource.clip = HitSounds[Random.Range(0,HitSounds.Length)];
            HitSource.Play();
        }
        Debug.Log(gameObject.name + " hit: " + other.gameObject.name + " will apply damage if last name isnt: " + NameToCheck);
        if (other.gameObject.name != NameToCheck)
        {
            Debug.Log(gameObject.name + " hit: " + other.gameObject.name + " with: " + damage + " damage");
            other.gameObject.SendMessage(("TakeDamage"), damage, SendMessageOptions.DontRequireReceiver);
            if (shouldDestroyOnCollision)
                Destroy(gameObject);
        }
        else
        {

        }
        
    }
}
