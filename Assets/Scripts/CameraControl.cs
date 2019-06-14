using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    float speed = 3f;
    private Vector3 campos = new Vector3(0,15,-10f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localPosition = campos;
        /* 
        Vector3 to = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -16f);
        to.Normalize();
        //to = to * 3;
        to.z = -16f;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, to, speed * Time.deltaTime);
        */
    }
}
