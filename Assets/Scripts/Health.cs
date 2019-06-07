using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public Inventory inventory;
    public Enemy d;
    private bool dead = false;

    public Text healthUI;

    public AudioSource DamageTaken;
    public AudioClip[] HitTaken;

    void Start()
    {
        DamageTaken = GetComponent<AudioSource>();
        inventory = gameObject.GetComponent<Inventory>();
    }

    void TakeDamage(int dmg)
    {
        if (inventory)
            dmg -= inventory.totalDefence;
       
        if(DamageTaken != null && HitTaken.Length > 0)
        {
            DamageTaken.clip = HitTaken[Random.Range(0,HitTaken.Length-1)];
            DamageTaken.Play();
        }

        health -= dmg;
        if (health > maxHealth)
            health = maxHealth;
        if (healthUI != null)
        {
            healthUI.text = "Health: " + health;
        }
        
        if(health <= 0)
        {
            dead = true;
            Debug.Log(gameObject.name + " has died after taking " + dmg + " damage.");
            if (transform.parent != null && transform.parent.tag != null)
            {
                if (transform.parent.tag == "Player")
                {

                }
                else if (transform.parent.tag == "Enemy")
                {
                    d.dead = true;


                }
            }

            Destroy(gameObject); //effects here plz //Send death message to shaders to delete the sprite or somethin cool
        }
    }


}
