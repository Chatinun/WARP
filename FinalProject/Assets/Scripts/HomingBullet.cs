using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public Transform target;

    public float speed = 5f;
    public float rotateSpeed = 200f;

    public int damage = 10;

    public int maxDamage;
    public int minDamage;

    public Vector3 LauchOffset;

    private Rigidbody2D rb;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") != null)
        {
            target = FindClosestEnemy().transform;
        }
        else if(GameObject.FindGameObjectWithTag("FinalBoss") != null)
        {
            target = FindClosestBoss().transform;
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        StartCoroutine(destroyBullet());
        transform.Translate(LauchOffset);
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") != null)
        {
            target = FindClosestEnemy().transform;
        }
        else if (GameObject.FindGameObjectWithTag("FinalBoss") != null)
        {
            target = FindClosestBoss().transform;
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

    }

    private void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;
    }

    IEnumerator destroyBullet()
    {
        yield return new WaitForSeconds(5);
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(Random.Range(minDamage, maxDamage));
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
        Miniboss1 miniboss1 = collision.GetComponent<Miniboss1>();
        if (miniboss1 != null)
        {
            miniboss1.TakeDamage(Random.Range(minDamage, maxDamage));
            Destroy(gameObject);
        }

        Miniboss2 miniboss2 = collision.GetComponent<Miniboss2>();
        if (miniboss2 != null)
        {
            miniboss2.TakeDamage(Random.Range(minDamage, maxDamage));
            Destroy(gameObject);
        }

        Miniboss3 miniboss3 = collision.GetComponent<Miniboss3>();
        if (miniboss3 != null)
        {
            miniboss3.TakeDamage(Random.Range(minDamage, maxDamage));
            Destroy(gameObject);
        }

        FinalBossLeftHand finalbosslh = collision.GetComponent<FinalBossLeftHand>();
        if (finalbosslh != null)
        {
            finalbosslh.TakeDamage(Random.Range(minDamage, maxDamage));
            Destroy(gameObject);
        }

        FinalBossRightHand finalbossrh = collision.GetComponent<FinalBossRightHand>();
        if (finalbossrh != null)
        {
            finalbossrh.TakeDamage(Random.Range(minDamage, maxDamage));
            Destroy(gameObject);
        }

        FinalBossHead finalbosshead = collision.GetComponent<FinalBossHead>();
        if (finalbosshead != null)
        {
            finalbosshead.TakeDamage(Random.Range(minDamage, maxDamage));
            Destroy(gameObject);
        }

    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        Debug.Log(closest);
        return closest;
    }

    public GameObject FindClosestBoss()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("FinalBoss");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        Debug.Log(closest);
        return closest;
    }
}
