using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    private int damage;
    public int maxDamage;
    public int minDamage;

    public Vector3 launchOffset;
    public Vector3 direction;

    public GameObject destroyParticle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        damage = Random.Range(minDamage, maxDamage);
        rb.velocity = direction * speed;
        if (WeaponManager.instance.weaponID == 4)
        {
            //Vector3 direction = transform.right + Vector3.up;
            Vector3 direction = transform.right + new Vector3(0, 0.5f, 0);
            rb.AddForce(direction * speed, ForceMode2D.Impulse);
            StartCoroutine(destroyBullet());
        }
        transform.Translate(launchOffset);
      
    }
    IEnumerator destroyBullet()
    {
        yield return new WaitForSeconds(5);
        if (gameObject != null)
        {
            Instantiate(destroyParticle, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProcessCollision(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessCollision(collision.gameObject);
    }
    void ProcessCollision(GameObject collider)
    {
        Enemy enemy = collider.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(Random.Range(minDamage,maxDamage));
            Destroy(gameObject);
        }

        if (collider.CompareTag("Ground"))
        {
            if (WeaponManager.instance.weaponID != 4)
            {
                Instantiate(destroyParticle, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

        Miniboss1 miniboss1 = collider.GetComponent<Miniboss1>();
        if (miniboss1 != null)
        {
            miniboss1.TakeDamage(Random.Range(minDamage, maxDamage));
            Destroy(gameObject);
        }

        Miniboss2 miniboss2 = collider.GetComponent<Miniboss2>();
        if (miniboss2 != null)
        {
            miniboss2.TakeDamage(Random.Range(minDamage, maxDamage));
            Destroy(gameObject);
        }

        Miniboss3 miniboss3 = collider.GetComponent<Miniboss3>();
        if (miniboss3 != null)
        {
            miniboss3.TakeDamage(Random.Range(minDamage, maxDamage));
            Destroy(gameObject);
        }

        FinalBossLeftHand finalbosslh = collider.GetComponent<FinalBossLeftHand>();
        if (finalbosslh != null)
        {
            finalbosslh.TakeDamage(Random.Range(minDamage, maxDamage));
            Destroy(gameObject);
        }

        FinalBossRightHand finalbossrh = collider.GetComponent<FinalBossRightHand>();
        if (finalbossrh != null)
        {
            finalbossrh.TakeDamage(Random.Range(minDamage, maxDamage));
            Destroy(gameObject);
        }

        FinalBossHead finalbosshead = collider.GetComponent<FinalBossHead>();
        if (finalbosshead != null)
        {
            finalbosshead.TakeDamage(Random.Range(minDamage, maxDamage));
            Destroy(gameObject);
        }
        
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
