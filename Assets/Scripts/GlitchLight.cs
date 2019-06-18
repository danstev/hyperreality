using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchLight : MonoBehaviour
{
    public int max = 60;
    private int count = 0;
    private int target;
    private bool s = true;
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
            
        }
        else
        {
            g.SetActive(true);
            s= true;
        }
        
        target = Random.Range(0,max);
        count = 0;
    }


}
