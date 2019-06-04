using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform p;
    public Transform target = null;

    public bool dead;
    private float tempTimer;
    public float speed, reach, timer, bulletSpeed;
    public int damage;
    public GameObject bullet;
    // an enum of possible behaviours for enemies.
    public enum enEnemyActions { UNKNOWN = 0, ROAM, ATTACK, RETREAT };
    public Queue<enEnemyActions> actionQueue = new Queue<enEnemyActions>();

    private void Start()
    {
        dead = false;
    }

    void Update()
    {
        if(dead)
        {
            Destroy(this.gameObject);
        }

        tempTimer -= Time.deltaTime;
        
        // Decide next action for queue
        if (target == null && tempTimer <= 0)
            actionQueue.Enqueue(enEnemyActions.ROAM);
        else if (target != null && tempTimer <= 0)
            actionQueue.Enqueue(enEnemyActions.ATTACK);

        if (!ConsumeQueue())
            Debug.Log("No Action Provided for that event");

    }

    // Handles next item from Queue. Returns false if action unknown
    bool ConsumeQueue() {
        if (actionQueue.Count == 0)
            return true; // Nothing to process

        enEnemyActions currentAction = actionQueue.Dequeue(); // Get the action to consume from front of queue

        // Set up timer vars (these appeared to be the same for each case)
        timer = Random.Range(1f, 5f);
        tempTimer = timer;

        switch (currentAction) {
            case(enEnemyActions.ROAM):
                StartCoroutine(RandomMove(timer, p));
                Debug.Log(gameObject.name + " is randomly moving.");
                break;
            case (enEnemyActions.ATTACK):
                StartCoroutine(AttackMove(timer, target));
                Debug.Log(gameObject.name + " is attacking: " + target.name + ".");
                break;
            default:
                return false;
        }
        return true;
    }

    IEnumerator AttackMove(float time, Transform target)
    {
        float start = Time.time;
        //transform.LookAt(new Vector3(target.position.x, target.position.y + 1.75f, target.position.z), Vector3.up);

        float attackTimer = 1f;

        while (Time.time <= start + time)
        {

            if (attackTimer <= 0)
            {
                Attack();
                attackTimer = 1f;
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }

            float step = speed * Time.deltaTime;
            p.position = Vector3.MoveTowards(p.position, target.transform.position, step);
            yield return new WaitForEndOfFrame();
        }
    }

    void Attack()
    {
        if (target == null)
        {

        }
        else if(reach > Vector3.Distance(p.position, target.transform.position))
        {
            Vector2 shootDir = new Vector2(target.position.x - p.position.x, target.position.y - p.position.y).normalized;
            GameObject firedBullet = Instantiate(bullet, p.position + transform.forward * 1, Quaternion.identity);
            firedBullet.GetComponent<Rigidbody2D>().velocity = shootDir * 10.0f;

            Destroy(firedBullet.gameObject, 2.0f);
        }
    }

    IEnumerator RandomMove(float time, Transform t)
    {
        float start = Time.time;
        float x = Random.Range(-0.5f, 0.5f);
        float y = Random.Range(-0.5f, 0.5f);
        Vector3 towards = new Vector3(x,y,0);

        while (Time.time <= start + time)
        {
            t.position = t.position + ((towards * Time.deltaTime) * speed);
            yield return new WaitForEndOfFrame();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (target == null && other.gameObject.tag == "Player")
        {
            Debug.Log(other.name + " has entered aggro range.");
            target = other.transform;
            tempTimer = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(other.name + " has left aggro range.");
            target = null;
        }
    }
}
