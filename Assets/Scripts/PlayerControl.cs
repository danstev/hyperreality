using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 3f;
    public float movementSpeed = 10f;

    public GameObject arrow;
    public float fireSpeed, fireReload;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        fireReload -= Time.deltaTime;

        if(Input.GetMouseButtonDown(0) && fireReload <=0)
        {
            Vector2 shootDir = new Vector2((Input.mousePosition.x/Screen.width) - 0.5f, (Input.mousePosition.y / Screen.height) - 0.5f);
            //shootDir.x -= 0.5f;
            //shootDir.y -= 0.5f;
            Debug.Log(shootDir);
            shootDir.Normalize();
            Debug.Log(shootDir);


            Vector3 pos = transform.position;
            pos.x += shootDir.x; pos.y += shootDir.y;
            Debug.Log(pos);
            GameObject g = Instantiate(arrow, transform.position, Quaternion.identity);
            fireReload = fireSpeed;
            g.GetComponent<Rigidbody2D>().velocity = shootDir * 15.0f;
            Destroy(g,5.0f);

        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 tempVect = new Vector3(h, v, 0);
        tempVect = tempVect.normalized * movementSpeed * Time.deltaTime;

        transform.position += tempVect;
    }
}
