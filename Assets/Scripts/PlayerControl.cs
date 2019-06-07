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
    public float timer;

    //Timers and weapon
    public GameObject arrow;
    public float fireSpeed, fireReload;
    public float jumpTemp, jumpTimer;
    public Collider2D coll;

    //UI stuff
    public GameObject GameUI, InventoryUI, MenuUI, cam;
    public int menuCheck = 0;

    // Animation
    enum animDirection { ANIM_UNKNOWN = 0, ANIM_UP, ANIM_DOWN, ANIM_LEFT, ANIM_RIGHT };
    Animator animator;
    SpriteRenderer render;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
    }

    void SwitchUI()
    {

         menuCheck = 0;
     
         GameUI.SetActive(true);
         InventoryUI.SetActive(false);
         MenuUI.SetActive(false);
    }

    animDirection GetAnimationDirection(float h, float v) {
        if (h > 0)
            return animDirection.ANIM_RIGHT;
        if (h < 0)
            return animDirection.ANIM_LEFT;
        if (v > 0)
            return animDirection.ANIM_UP;
        if (v < 0)
            return animDirection.ANIM_DOWN;
        return animDirection.ANIM_UNKNOWN;
    }

    void HandleAnimation(float h, float v) {
        // Set sane animator properties
        animator.speed = 1;
        animator.SetBool("MoveX", false);
        animator.SetBool("MoveDown", false);
        animator.SetBool("MoveUp", false);
        animator.SetBool("Idle", false);
        render.flipX = false;

        switch (GetAnimationDirection(h, v)) {
            case (animDirection.ANIM_RIGHT):
                animator.SetBool("MoveX", true);
                render.flipX = true;
                break;
            case (animDirection.ANIM_LEFT):
                animator.SetBool("MoveX", true);
                render.flipX = false;
                break;
            case (animDirection.ANIM_DOWN):
                animator.SetBool("MoveDown", true);
                break;
            case (animDirection.ANIM_UP):
                animator.SetBool("MoveUp", true);
                break;
            default:
                animator.SetBool("Idle", true);
                break;
        }

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
        timer += Time.deltaTime;
        

        if (!Input.GetMouseButton(0))
        {

            arrow.transform.position = Vector3.MoveTowards(arrow.transform.position, transform.position, Time.deltaTime * 40);
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

            Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
            Vector2 mouseOnScreen = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
            arrow.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 45));
        }

        if (Input.GetMouseButton(1) && fireReload <= 0)
        {
            coll.enabled = true;

            //POS
            arrow.transform.position = new Vector3(transform.position.x + (Mathf.Cos(timer * 15) * 2) ,transform.position.y + (Mathf.Sin(timer * 15) * 2) , 0);
        }

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            coll.enabled = false;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        HandleAnimation(h, v);
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

        if (Input.GetKeyDown("e"))
        {
            /*  out of bounds, do not touch
            Debug.Log("Interact");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, 20)){
                Instantiate(arrow);
            }
            */
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
