using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideDamage : MonoBehaviour
{
    /*
    DEPRECATED, USE A WEAPON INSTEAD WITH A CORRECT ABILITY
     */
    public int damage;
    public bool FriendCheck =true;
    public string NameToCheck;
    public bool shouldDestroyOnCollision = false;

    public AudioClip[] HitSounds;
    public AudioSource HitSource;

    void OnStart()
    {
        HitSource = GetComponent<AudioSource>();
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
            // The logging below is incorrect, as health also applies the defence modifier in the inventory
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
