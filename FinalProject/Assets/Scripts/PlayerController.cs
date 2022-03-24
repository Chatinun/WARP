using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    [Header("Object References")]
    public Rigidbody2D rb;
    public BoxCollider2D col;
    public SpriteRenderer sr;
    public Animator anim;

    [Header("Basic Movement")]
    public float speed;
    public float jumpForce;
    public bool isInvincible;
    public GameObject deadParticle;

    public bool doubleJump;
    private float moveInput;
    public bool isShooting;
    public bool isDead;
    private bool spawnedDeadParticle;
    private bool facingRight = true;
    private bool isMoving;
    public bool isJumping;
    private bool isCrouch;

    [Header("Grounded")]
    public float checkRadius;
    public Transform groundCheck;
    public LayerMask layerGround;
    public bool isGrounded;

    [Header("Dash")]
    public float dashPower;
    public float dashTime;
    private bool isDashing;
    private bool canDash;
    public float delayBetweenDash;
    private float tempDelayBetweenDash;

    [Header("Ghost")]
    [SerializeField] private GameObject playerGhost;

    public static PlayerController instance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        instance = this;
    }

    private void Start()
    {
        isDead = false;
    }

    void Update()
    {
        if (!isDead)
        {
            //Check if is ground
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, layerGround);
            if (!isGrounded)
            {
                isJumping = true;
            }
            else
            {
                isJumping = false;
            }
            anim.SetBool("isJumping", isJumping);

            //Check is shooting
            if (Input.GetKey(KeyCode.Z))
            {
                isShooting = true;
            }
            else
            {
                isShooting = false;
            }

            anim.SetBool("isShooting", isShooting);

            if(GetComponentInChildren<Light2D>() != null)
            {
                if (doubleJump)
                {
                    GetComponentInChildren<Light2D>().enabled = true;
                }
                else
                {
                    GetComponentInChildren<Light2D>().enabled = false;
                }
            }
            
            //Jump
            if (Input.GetButtonDown("Jump"))
            {
                if (isGrounded)
                {
                    rb.velocity = Vector2.up * jumpForce;
                    anim.SetTrigger("takeOf");
                    AudioManager.instance.audioPlay("Jump");
                }
                else if(doubleJump)
                {
                    rb.velocity = Vector2.up * jumpForce;
                    anim.SetTrigger("takeOf");
                    doubleJump = false;
                    AudioManager.instance.audioPlay("Jump");
                }
                
            }

            //Move Input
            moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

            if (moveInput == 0)
            {
                anim.SetBool("isMoving", false);
            }
            else
            {
                anim.SetBool("isMoving", true);
            }

            //Flip
            if (!facingRight && moveInput > 0)
            {
                Flip();
            }
            else if (facingRight && moveInput < 0)
            {
                Flip();
            }



            //Dash
            if (Input.GetKeyDown(KeyCode.LeftShift) && moveInput != 0)
            {
                if (!isDashing && StaminaBar.instance.currentStamina >= 50)
                {
                    StaminaBar.instance.UseStamina(50);
                    AudioManager.instance.audioPlay("Dash");
                    StartCoroutine(Dash());
                }
            }

            //Ghost
            if (isDashing)
            {
                GameObject GhostBaby = Instantiate(playerGhost, transform.position, transform.rotation);
            }

            //Look up
            if (Weapon.instance.lastHitKey == KeyCode.UpArrow)
            {
                anim.SetBool("lookUp", true);
            }
            else
            {
                anim.SetBool("lookUp", false);
            }

            if(transform.position.y < -5.5f)
            {
                HealthController.instance.DamageHealth(HealthController.instance.maxPlayerHealth);
                StartCoroutine(FlashAfterDamage());
                ScreenShakeController.instance.StartShake(0.2f, 0.02f);
                AudioManager.instance.audioPlay("Hurt");
            }

        }

    }

    void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        speed *= dashPower;
        

        yield return new WaitForSeconds(dashTime);

        speed = 5;
        isDashing = false;
    }

    public IEnumerator Die()
    {
        isDead = true;
        col.enabled = false;
        anim.SetTrigger("isDead");
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(0.8f);
        sr.enabled = false;
        if (!spawnedDeadParticle)
        {
            AudioManager.instance.audioPlay("Dead");
            Instantiate(deadParticle, transform.position, transform.rotation);
            spawnedDeadParticle = true;
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
        if(collider.CompareTag("Enemy") && !isInvincible)
        {
            HealthController.instance.DamageHealth(1);
            StartCoroutine(FlashAfterDamage());
            ScreenShakeController.instance.StartShake(0.2f, 0.02f);
            AudioManager.instance.audioPlay("Hurt");
        }
        if (collider.CompareTag("Obstacle") && !isInvincible)
        {
            HealthController.instance.DamageHealth(1);
            StartCoroutine(FlashAfterDamage());
            ScreenShakeController.instance.StartShake(0.2f, 0.02f);
            AudioManager.instance.audioPlay("Hurt");
        }
        if (collider.CompareTag("Goal"))
        {
            LevelLoader.instance.LoadNextLevel();
        }
        if (collider.CompareTag("Miniboss1"))
        {
            Miniboss1.instance.bossTriggerStart = true;
        }
        if (collider.CompareTag("Miniboss2"))
        {
            Miniboss2.instance.bossTriggerStart = true;
        }
        if (collider.CompareTag("Miniboss3"))
        {
            Miniboss3.instance.bossTriggerStart = true;
        }
    }

    public IEnumerator FlashAfterDamage()
    {
        float flashDelay = 0.08f;
        isInvincible = true;
        for (int i = 0; i < 10; i++)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(flashDelay);
            sr.enabled = true;
            yield return new WaitForSeconds(flashDelay);
        }
        isInvincible = false;
    }

}
