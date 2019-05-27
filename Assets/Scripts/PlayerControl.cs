using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 3f;
    public float movementSpeed = 10f;
    public float weaponSpeedMod = 8.0f;


    public GameObject arrow;
    public float fireSpeed, fireReload;
    public float jumpTemp, jumpTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        fireReload -= Time.deltaTime;
        

        if (!Input.GetMouseButton(0))
        {
            float t = fireSpeed/ fireReload;
            Vector3 n = Vector3.MoveTowards(arrow.transform.localPosition, new Vector3(-0.7f, 0.5f, 0.0f), t);
            arrow.transform.localPosition -= n;
        }

         if (Input.GetMouseButton(0) && fireReload <=0)
        {
            //fireReload = fireSpeed;
            //Vector2 shootDir = new Vector2((Input.mousePosition.x/Screen.width) - 0.5f, (Input.mousePosition.y / Screen.height) - 0.5f);
            //shootDir.Normalize();
            //Vector3 pos = transform.position;
            //pos.x += shootDir.x; pos.y += shootDir.y;
            //pos.Normalize();
            //arrow.transform.position += pos * Time.deltaTime * weaponSpeedMod;
            /*
            GameObject g = Instantiate(arrow, transform.position, Quaternion.identity);
            fireReload = fireSpeed;
            g.GetComponent<Rigidbody2D>().velocity = shootDir * 15.0f;
            Destroy(g,5.0f);
            */


            float hi = (Input.mousePosition.x / Screen.width) - 0.5f;
            float vi = (Input.mousePosition.y / Screen.height) - 0.5f;

            Vector3 tempVecti = new Vector3(hi, vi, 0);
            tempVecti = tempVecti.normalized * movementSpeed * Time.deltaTime;
            
            arrow.transform.localPosition += tempVecti;

        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 tempVect = new Vector3(h, v, 0);
        tempVect = tempVect.normalized * movementSpeed * Time.deltaTime;

        transform.position += tempVect;

        if (Input.GetButtonUp("Jump") && jumpTemp <= 0)
        {
            tempVect = tempVect.normalized * movementSpeed * Time.deltaTime;
            GetComponent<Rigidbody2D>().velocity = tempVect * 40.0f;
            transform.position += tempVect;
            jumpTemp = jumpTimer;
        }
        else if(jumpTemp >= 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
            jumpTemp -= Time.deltaTime;
        }
    }
}
