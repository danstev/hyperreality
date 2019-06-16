using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool state = false;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("Close", 0, 0.25f);
    }

    void Interact()
    {
        if(state == false)
        {
            anim.Play("Door", 0, 0.25f);
            state = true;
        }
        else
        {
            anim.Play("Close", 0, 0.25f);
            state = false;
        }
    }
}
