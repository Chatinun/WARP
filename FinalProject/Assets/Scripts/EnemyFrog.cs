using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrog : MonoBehaviour
{
    public bool facingRight = true;
    public LayerMask whatIsGround;

    public bool isGrounded = false;
    public bool isFalling = false;
    public bool isJumping = false;

    public float jumpForceX = 2f;
    public float jumpForceY = 4f;

    public float lastYPos = 0;

    public enum Animations 
    {
        Idle = 0,
        Jumping = 1,
        //Falling = 2,
    }

    public Animations currentAnim;

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Animator anim;

    public float idleTIme = 2f;
    public float currentIdleTime = 0;
    public bool isIdle = true;

    private void Awake()
    {
        lastYPos = transform.position.y;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        //sr.flipX = facingRight;
    }

    private void FixedUpdate()
    {
        if (isIdle)
        {
            currentIdleTime += Time.deltaTime;
            if(currentIdleTime >= idleTIme)
            {
                currentIdleTime = 0;
                
                Jump();
            }
        }

        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f), new Vector2(transform.position.x + 0.5f, transform.position.y + 0.51f), whatIsGround);

        //We have just fallen onto the ground, set idle to true
        if(isGrounded && isFalling)
        {
            isFalling = false;
            isJumping = false;
            isIdle = true;
            facingRight = !facingRight;
            sr.flipX = facingRight;
            ChangeAnimation(Animations.Idle);
        }
        //We are going up, and are not grounded, set isJumping to true
        else if(transform.position.y > lastYPos && !isGrounded && !isIdle)
        {
            isJumping = true;
            isFalling = false;
            ChangeAnimation(Animations.Jumping);
        }
        //We are going down, and are not grounded, set isFalling to true
        else if(transform.position.y < lastYPos && !isGrounded && !isIdle)
        {
            isJumping = false;
            isFalling = true;
            //ChangeAnimation(Animations.Falling);
        }

        lastYPos = transform.position.y;
    }

    void Jump()
    {
        isJumping = true;
        isIdle = false;
        int direction = 0;
        if (!facingRight)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        rb.velocity = new Vector2(jumpForceX * direction, jumpForceY);
    }

    void ChangeAnimation(Animations newAnim)
    {
        if(currentAnim != newAnim)
        {
            currentAnim = newAnim;
            anim.SetInteger("state", (int)newAnim);
        }
    }
}
