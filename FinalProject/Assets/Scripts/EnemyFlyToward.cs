using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyToward : MonoBehaviour
{
    public Transform player;
    private bool chasing = false;
    private bool attack;

    public float flySpeed = 5f;
    public float attackSpeed = 5f;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }
    private void OnBecameVisible()
    {
        chasing = true;
    }
    private void Update()
    {
        if (chasing)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), flySpeed*Time.deltaTime);
            Flip();
        }

        float dist = Mathf.Abs(transform.position.x - player.position.x);
        if (dist <= 0.1)
        {
            chasing = false;
            attack = true;
        }
        if (attack)
        {
            anim.SetTrigger("Attack");
            transform.Translate(0, -attackSpeed * Time.deltaTime, 0);
        }
        
    }

    void Flip()
    {
        if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (transform.position.x > player.position.x)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            GetComponent<Enemy>().Die();
        }
        if(collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
        }
    }
}
