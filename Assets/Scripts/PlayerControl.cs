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
    public GameObject weap;
    public float fireSpeed, fireReload;
    public float jumpTemp, jumpTimer;
    public Collider2D coll;
    public Ability one,two;

    //UI stuff
    public GameObject GameUI, InventoryUI, MenuUI, cam;
    public int menuCheck = 0;
    public Vector3 campos = new Vector3(0,0,-16f);
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
            cam.transform.localPosition = campos;
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
            if(weap != null)
            {
                weap.transform.position = Vector3.MoveTowards(weap.transform.position, transform.position, Time.deltaTime * 40);
            }
        }

        
         if (Input.GetMouseButton(0))
         {
            if(one)
            {
                one.Use(weap, coll, 5);
            }
            Debug.Log("Left Click");
        }
        
        

        if (Input.GetMouseButton(1))
        {
            if(two)
            {
                two.Use(weap, coll, 5);
            }
            Debug.Log("Right Click");
        }

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            if(one && two)
            {
                coll.enabled = false;
            }
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
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Mathf.Infinity;
            RaycastHit2D hit = Physics2D.Raycast(ray.origin,  ray.direction * 20,Mathf.Infinity);
            Debug.DrawRay(ray.origin,  ray.direction * 20,Color.blue, 50);
            Debug.Log("Interacted.");
            if(hit)
            {
                Vector3 targetPos = hit.collider.gameObject.transform.position; //Now send message whenever
                Debug.Log ("Interact hit name: " + hit.collider.name);
            
            }
        }
            
    }

    

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public void UpdateAbilities(GameObject w, Ability a, Ability b)
    {
        coll = w.GetComponent<Collider2D>();
        weap = w;
        one = a;
        two = b;
    }
}
