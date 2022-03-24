using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxCollider;

    public GameObject particle;

    public float distance;
    bool isFalling = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Physics2D.queriesStartInColliders = false;
        if (!isFalling)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance);

            Debug.DrawRay(transform.position, Vector2.down * distance, Color.red);

            if(hit.transform != null)
            {
                if(hit.transform.tag == "Player")
                {
                    rb.gravityScale = 3;
                    isFalling = true;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(), boxCollider);
            Destroy(gameObject);
        }
        else
        {
            rb.gravityScale = 0;
            boxCollider.enabled = false;
        }
        AudioManager.instance.audioPlay("FallingSpike");
        Instantiate(particle, transform.position, transform.rotation);
    }
}
