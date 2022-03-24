using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    [SerializeField] private Transform player;
    [SerializeField] private Transform firePoint;

    [SerializeField] private float agroRange;
    [SerializeField] private float shootRange;
    [SerializeField] private float moveSpeed;

    private Rigidbody2D rb;
    private Animator anim;

    private float fireRate;
    private float nextFire;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        fireRate = 1f;
        nextFire = Time.time;
    }

    private void Update()
    {
        
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if(distToPlayer < shootRange)
        {
            Flip();
            anim.SetBool("isAttack", true);
            //CheckIfTimeToFire();
            StopChasingPlayer();
        }
        else if (distToPlayer < agroRange)
        {
            anim.SetBool("isAttack", false);
            ChasePlayer();
        }
        else
        {
            anim.SetBool("isAttack", false);
            StopChasingPlayer();
        }
    }

    void CheckIfTimeToFire()
    {
        if(Time.time > nextFire)
        {
            Instantiate(bullet, firePoint.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }

    void Attack()
    {
        Instantiate(bullet, firePoint.position, Quaternion.identity);
        AudioManager.instance.audioPlay("EnemyShoot");
    }

    void Flip()
    {
        if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (transform.position.x > player.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        else if (transform.position.x > player.position.x)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
    }

    void StopChasingPlayer()
    {
        rb.velocity = new Vector2(0, 0);
    }
}
