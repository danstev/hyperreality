using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    private bool dead = false;

    void TakeDamage(int dmg)
    {
        health -= dmg;

        if(health <= 0)
        {
            dead = true;
            Debug.Log(gameObject.name + " has died after taking " + dmg + " damage."); 
            Destroy(gameObject); //effects here plz
        }
    }


}
