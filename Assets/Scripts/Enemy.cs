﻿using System.Collections;
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
    public GameObject health;
    // an enum of possible behaviours for enemies.
    public enum enEnemyActions { UNKNOWN = 0, ROAM, ATTACK, RETREAT };
    public Queue<enEnemyActions> actionQueue = new Queue<enEnemyActions>();

    private Animator animator;

    private void Start()
    {
        dead = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (dead)
        {
            Die();
            return;
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

    void Die() {
        float nLen = animator.GetCurrentAnimatorStateInfo(0).length;
        animator.SetBool("EnemyAttacking", false);
        animator.SetBool("EnemyFloating", false);
        animator.SetBool("EnemyDead", true);
        animator.speed = 1;
        Destroy(this.gameObject,nLen);
    }

    // Handles next item from Queue. Returns false if action unknown
    bool ConsumeQueue() {
        if (actionQueue.Count == 0)
            return true; // Nothing to process

        animator.speed = 1;
        animator.SetBool("EnemyAttacking",false);
        animator.SetBool("EnemyFloating",false);
        
        enEnemyActions currentAction = actionQueue.Dequeue(); // Get the action to consume from front of queue

        // Set up timer vars (these appeared to be the same for each case)
        timer = Random.Range(1f, 5f);
        tempTimer = timer;

        switch (currentAction) {
            case(enEnemyActions.ROAM):
                StartCoroutine(RandomMove(timer, p));
                Debug.Log(gameObject.name + " is randomly moving.");
                animator.SetBool("EnemyFloating", true);
                break;
            case (enEnemyActions.ATTACK):
                StartCoroutine(AttackMove(timer, target));
                Debug.Log(gameObject.name + " is attacking: " + target.name + ".");
                animator.SetBool("EnemyAttacking", true);
                break;
            default:
                animator.SetBool("EnemyFloating", true);
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
            if (p == null) // The target has been destroyed already
                break;
            if (target == null) // The target has been destroyed already
                break;
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
            if (t == null)
                break;
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

    void OnDestroy() {
        
        if (Random.Range(0,4) == 1)
            Instantiate(health, p.position, p.rotation);
    }
}
