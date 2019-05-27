using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target = null;
    private float tempTimer;
    public float speed, reach, timer, bulletSpeed;
    public int damage;
    public GameObject enemy, bullet;

    private void Start()
    {
        enemy = GetComponentInChildren<Health>().gameObject;
    }

    void Update()
    {

        tempTimer -= Time.deltaTime;

        if (target == null && tempTimer <= 0)
        {
            timer = Random.Range(1f, 5f);
            tempTimer = timer;
            StartCoroutine(RandomMove(timer, transform));
            Debug.Log(gameObject.name + " is randomly moving.");
        }
        else if (target != null && tempTimer <= 0)
        {
            timer = Random.Range(1f, 5f);
            tempTimer = timer;
            StartCoroutine(AttackMove(timer, target));
            Debug.Log(gameObject.name + " is attacking: " + target.name + ".");
        }

    }

    IEnumerator AttackMove(float time, Transform target)
    {
        float start = Time.time;
        transform.LookAt(new Vector3(target.position.x, target.position.y + 1.75f, target.position.z), Vector3.up);

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
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
            yield return new WaitForEndOfFrame();
        }
    }

    void Attack()
    {
        if (target == null)
        {

        }
        else// (reach > Vector3.Distance(transform.position, target.transform.position))
        {
            //target.transform.SendMessage(("TakeDamage"), damage, SendMessageOptions.DontRequireReceiver);
            GameObject firedBullet = Instantiate(bullet, transform.position + transform.forward * 1, transform.rotation);
            Rigidbody bulletRigidbody = firedBullet.GetComponent<Rigidbody>();
            bulletRigidbody.velocity = transform.forward * bulletSpeed;
            Destroy(firedBullet.gameObject, 2.0f);
        }
    }

    IEnumerator RandomMove(float time, Transform t)
    {
        float start = Time.time;
        Vector3 euler = t.eulerAngles;
        euler.x = 0; euler.z = 0;
        euler.y = Random.Range(0f, 360f);
        t.eulerAngles = euler;
        while (Time.time <= start + time)
        {
            t.position = t.position + (t.transform.forward * Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (target == null && other.tag == "Player")
        {
            Debug.Log(other.name + " has entered aggro range.");
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(other.name + " has left aggro range.");
            target = null;
        }
    }
}
