using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //speeds
    public float speed = 3f;
    public float movementSpeed = 10f;
    public float weaponSpeedMod = 8.0f;
    public Rigidbody2D p;

    //Timers and weapon
    public GameObject arrow;
    public float fireSpeed, fireReload;
    public float jumpTemp, jumpTimer;
    public Collider2D coll;

    //UI stuff
    public GameObject GameUI, InventoryUI, MenuUI, cam;
    public int menuCheck = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void SwitchUI()
    {

         menuCheck = 0;
     
         GameUI.SetActive(true);
         InventoryUI.SetActive(false);
         MenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            cam.transform.localPosition = new Vector3(0,0,-18);
        }

        if (Input.GetKeyDown("i"))
        {
            //Inventory

            if (menuCheck == 0)
            {
                GameUI.SetActive(false);
                InventoryUI.SetActive(true);
                MenuUI.SetActive(false);
                menuCheck = 1;
            }
            else
            {
                SwitchUI();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Menu
            if(menuCheck == 0)
            {
                //off off on
                GameUI.SetActive(false);
                InventoryUI.SetActive(false);
                MenuUI.SetActive(true);
                menuCheck = 2;
            }
            else
            {
                SwitchUI();
            }
        }

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

            coll.enabled = true;

            float hi = (Input.mousePosition.x / Screen.width) - 0.5f;
            float vi = (Input.mousePosition.y / Screen.height) - 0.5f;

            Vector3 tempVecti = new Vector3(hi, vi, 0);
            tempVecti = tempVecti.normalized * movementSpeed * Time.deltaTime;
            
            arrow.transform.localPosition += tempVecti;

        }

        if (Input.GetMouseButtonUp(0))
        {
            coll.enabled = false;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 tempVect = new Vector3(h, v, 0);
        //tempVect = tempVect.normalized * movementSpeed * Time.deltaTime;

        p.MovePosition(new Vector2((p.transform.position.x + tempVect.x * speed * Time.deltaTime),(p.transform.position.y + tempVect.y * speed * Time.deltaTime)));

        if (Input.GetButtonUp("Jump") && jumpTemp <= 0)
        {
            tempVect = tempVect.normalized * movementSpeed * Time.deltaTime;
            GetComponent<Rigidbody2D>().velocity = tempVect * 250.0f;
            //p.position += tempVect;
            jumpTemp = jumpTimer;
        }
        else if(jumpTemp >= 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
            jumpTemp -= Time.deltaTime;
        }
    }
}
