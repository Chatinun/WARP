using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpikeBoss : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    public GameObject particle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(), boxCollider);
            Destroy(gameObject);
        }
        else
        {
            rb.gravityScale = 0;
            Destroy(gameObject);
            boxCollider.enabled = false;
        }
        AudioManager.instance.audioPlay("FallingSpike");
        Instantiate(particle, transform.position, transform.rotation);
    }
}
