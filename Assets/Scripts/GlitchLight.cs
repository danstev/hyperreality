using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchLight : MonoBehaviour
{
    public int max = 60;
    private int count = 0;
    private int target;
    private bool s = true;
    public bool Increase = false;
    public GameObject g;

    void Start()
    {
        target = Random.Range(0,max);
    }

    void FixedUpdate()
    {
        count++;

        if(count >= target)
        {
            Switch();
        }
    }

    void Switch()
    {
        if(s)
        {
            g.SetActive(false);
            s= false;

            if(Increase)
            {
                target = Random.Range(0,(max * 10));
            }
            else
            {
                target = Random.Range(0,max);
            }
            
        }
        else
        {
            g.SetActive(true);
            s= true;

            if(Increase)
            {
                target = Random.Range(0,max);
            }
            else
            {
                target = Random.Range(0,max);
            }
        }
        
        
        count = 0;
    }


}
