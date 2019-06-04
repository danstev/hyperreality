using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideDamage : MonoBehaviour
{
    public int damage;
    public bool FriendCheck =true;
    public string NameToCheck;
    public bool shouldDestroyOnCollision = false;

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(gameObject.name + " hit: " + other.gameObject.name + " will apply damage if last name isnt: " + NameToCheck);
        if (other.gameObject.name != NameToCheck)
        {
            Debug.Log(gameObject.name + " hit: " + other.gameObject.name + " with: " + damage + " damage");
            other.gameObject.SendMessage(("TakeDamage"), damage, SendMessageOptions.DontRequireReceiver);
        }
        else
        {

        }
        if (shouldDestroyOnCollision)
            Destroy(gameObject);
    }
}
