using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    private int def;
    public Enemy d;
    private bool dead = false;

    public Text healthUI;

    public AudioSource DamageTaken;
    public AudioClip[] HitTaken;

    public GameObject popuptext;

    void Start()
    {
        DamageTaken = GetComponent<AudioSource>();
        def = GetComponent<Statistics>().totalDefense;
    }

    void TakeDamage(int dmg)
    {
        dmg -= def;

        Vector3 offset = new Vector3(0,1f,0.0f);
        GameObject g = Instantiate(popuptext, transform.position, Quaternion.identity);
        TextMesh t = g.GetComponent<TextMesh>();
        t.text = dmg.ToString();
        Destroy(g, 1.0f);

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
