using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpin : Ability
{

    public void Use(GameObject i, Collider2D c, int dam)
    {
        c.enabled = true;

            //POS
            i.transform.position = new Vector3(transform.position.x + (Mathf.Cos(TimerTemp * 15) * 2) ,transform.position.y + (Mathf.Sin(TimerTemp * 15) * 2) , 0);
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
