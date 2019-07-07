using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearOnTrigger : MonoBehaviour
{
    public GameObject target;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("entered");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("exited");
        }
    }
}
