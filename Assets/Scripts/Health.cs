using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    private bool dead = false;

    public Text healthUI;

    void TakeDamage(int dmg)
    {
        if(healthUI != null)
        {
            healthUI.text = "Health: " + health;
        }
        health -= dmg;

        if(health <= 0)
        {
            dead = true;
            Debug.Log(gameObject.name + " has died after taking " + dmg + " damage."); 
            Destroy(gameObject); //effects here plz
        }
    }


}
