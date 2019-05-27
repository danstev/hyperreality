using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideDamage : MonoBehaviour
{
    public int damage;

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other);
        Debug.Log(gameObject.name + " hit: " + other.gameObject.name);
        other.gameObject.SendMessage(("TakeDamage"), damage, SendMessageOptions.DontRequireReceiver);
        //Destroy(gameObject);
    }
}
