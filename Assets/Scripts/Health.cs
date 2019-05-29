using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public Enemy d;
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

            if(transform.parent.tag == "Player")
            {

            }
            else if(transform.parent.tag == "Enemy")
            {
                d.dead = true;
                

            }

            Destroy(gameObject); //effects here plz //Send death message to shaders to delete the sprite or somethin cool
        }
    }


}
