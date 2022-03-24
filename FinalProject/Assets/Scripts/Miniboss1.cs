using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Miniboss1 : MonoBehaviour
{
    public int health;
    public int damage;

    public bool facingRight = true;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public GameObject bossActivator;
    public GameObject[] invisibleWall;
    public GameObject deadParticle;

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
    public Animator anim;
    public Slider healthBar;
    public GameObject bossHealthBar;
    public SpriteRenderer sr;

    public float idleTime = 2f;
    public float currentIdleTime = 0;
    public bool isIdle = true;

    public bool bossTriggerStart = false;
    private bool froggySpawn1 = false;
    private bool froggySpawn2 = false;
    private bool froggySpawn3 = false;
    private bool froggySpawn4 = false;
    private bool froggySpawn5 = false;
    public GameObject[] froggy;
    public Transform[] spawner;

    public static Miniboss1 instance;

    private void Awake()
    {
        instance = this;
        lastYPos = transform.position.y;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        bossHealthBar.SetActive(false);
    }
    void Update()
    {
        healthBar.value = health;
    }

    private void FixedUpdate()
    {
        if (bossTriggerStart)
        {
            bossActivator.GetComponent<BoxCollider2D>().enabled = false;
            bossHealthBar.SetActive(true);
            invisibleWall[0].GetComponent<BoxCollider2D>().enabled = true;
            invisibleWall[1].GetComponent<BoxCollider2D>().enabled = true;
            CameraFollow.instance.minValue.x = 21.46f;
            CameraFollow.instance.maxValue.x = 21.46f;
            if (isIdle)
            {
                currentIdleTime += Time.deltaTime;
                if (currentIdleTime >= idleTime)
                {
                    currentIdleTime = 0;
                    Jump();
                }
            }

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f, whatIsGround);

            //We have just fallen onto the ground, set idle to true
            if (isGrounded && isFalling)
            {
                ScreenShakeController.instance.StartShake(0.5f, 0.02f);
                AudioManager.instance.audioPlay("FrogHitGround");
                isFalling = false;
                isJumping = false;
                isIdle = true;
                facingRight = !facingRight;
                sr.flipX = facingRight;
                ChangeAnimation(Animations.Idle);
            }
            //We are going up, and are not grounded, set isJumping to true
            else if (transform.position.y > lastYPos && !isGrounded && !isIdle)
            {
                isJumping = true;
                isFalling = false;
                ChangeAnimation(Animations.Jumping);
            }
            //We are going down, and are not grounded, set isFalling to true
            else if (transform.position.y < lastYPos && !isGrounded && !isIdle)
            {
                isJumping = false;
                isFalling = true;
                //ChangeAnimation(Animations.Falling);
            }

            lastYPos = transform.position.y;
        }
        
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
        if (currentAnim != newAnim)
        {
            currentAnim = newAnim;
            anim.SetInteger("state", (int)newAnim);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(BlinkDamage());


        if (health <= 0)
        {
            Die();
        } else if (health <= 50) 
        {
            if (!froggySpawn5)
            {
                StartCoroutine(SpawnFroggy(12));
            }
            froggySpawn5 = true;
        }
        else if (health <= 100)
        {
            if (!froggySpawn4)
            {
                StartCoroutine(SpawnFroggy(9));
            }
            froggySpawn4 = true;
            froggySpawn5 = false;
            idleTime = 0.2f;
        }
        else if (health <= 150)
        {
            if (!froggySpawn3)
            {
                StartCoroutine(SpawnFroggy(6));
            }
            froggySpawn3 = true;
            froggySpawn4 = false;
        }
        else if (health <= 225)
        {
            if (!froggySpawn2)
            {
                StartCoroutine(SpawnFroggy(4));
            }
            froggySpawn2 = true;
            froggySpawn3 = false;
            idleTime = 1;
        }
        else if (health <= 325)
        {
            if (!froggySpawn1)
            {
                StartCoroutine(SpawnFroggy(2));
            }
            froggySpawn1 = true;
            froggySpawn2 = false;
        }
    }

    IEnumerator BlinkDamage()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        sr.color = Color.white;
    }

    IEnumerator SpawnFroggy(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            int num = Random.Range(0, 2);
            Debug.Log(num);
            Instantiate(froggy[num], spawner[num].position, spawner[num].rotation);
            yield return new WaitForSeconds(0.5f);
        }
    }
    void Die()
    {
        Instantiate(deadParticle, transform.position, transform.rotation);
        bossTriggerStart = false;
        sr.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        bossHealthBar.SetActive(false);
        Time.timeScale = 0.25f;
        ScreenShakeController.instance.StartShake(0.5f, 0.03f);
        AudioManager.instance.audioPlay("Dead");
        StartCoroutine(Die2());
    }

    IEnumerator Die2()
    {
        yield return new WaitForSeconds(0.75f);
        Time.timeScale = 1f;
        CameraFollow.instance.minValue.x = 0f;
        CameraFollow.instance.maxValue.x = 25f;
        invisibleWall[0].GetComponent<BoxCollider2D>().enabled = false;
        invisibleWall[1].GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject);
    }

}
