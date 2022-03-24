using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Miniboss3 : MonoBehaviour
{
    #region Public Variables
    public GameObject target;
    public Transform firePoint;
    #endregion

    #region Private Variables
    public bool bossTriggerStart;
    #endregion

    public float health;
    public float moveSpeed = 2.5f;
    public float attackRange = 3f;

    public GameObject bossActivator;
    public GameObject[] invisibleWall;
    public Slider healthBar;
    public GameObject bossHealthBar;

    public GameObject slashPrefab;
    public float slashSpeed;

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Animator anim;

    public Vector2 oldPlayerPos;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public bool isGrounded;

    public GameObject fallingSpikePrefab;

    private float timeBtwJumping;
    public float startTimeBtwJumping;

    private int shakeAmount = 5;

    public GameObject deadParticle;

    public static Miniboss3 instance;

    public enum Animations
    {
        Idle = 0,
        Jumping = 1,
        //Falling = 2,
    }

    public Animations currentAnim;

    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        bossHealthBar.SetActive(false);
    }

    void Update()
    {
        healthBar.value = health;
        if (bossTriggerStart)
        {
            bossActivator.GetComponent<BoxCollider2D>().enabled = false;
            bossHealthBar.SetActive(true);
            invisibleWall[0].GetComponent<BoxCollider2D>().enabled = true;
            invisibleWall[1].GetComponent<BoxCollider2D>().enabled = true;
            CameraFollow.instance.minValue.x = 21.46f;
            CameraFollow.instance.maxValue.x = 21.46f;

            if(timeBtwJumping > startTimeBtwJumping && anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                StartCoroutine(Shaking(shakeAmount));
                anim.SetBool("isShaking", true);
                timeBtwJumping = 0;
            }
            else
            {
                timeBtwJumping += Time.deltaTime;
            }
            ChangePhase();

        }
    }

    void ChangePhase()
    {
        if (health <= 0)
        {
            Die();
        }
        else if (health <= 100)
        {
            shakeAmount = 12;
            moveSpeed = 4f;
            slashSpeed = 12f;
            attackRange = 8f;
        }
        else if (health <= 200)
        {
            shakeAmount = 10;
            moveSpeed = 5f;
            slashSpeed = 10f;
            attackRange = 6f;
        }
        else if (health <= 300)
        {
            shakeAmount = 8;
            moveSpeed = 4f;
            slashSpeed = 8f;
            attackRange = 4f;
        }
        else if (health <= 400)
        {
            shakeAmount = 6;
            moveSpeed = 3f;
            slashSpeed = 6f;
        }
    }

    IEnumerator Shaking(int amount)
    {
        ScreenShakeController.instance.StartShake(2f, 0.02f);
        for(int i = 0; i < amount; i++)
        {
            Instantiate(fallingSpikePrefab, new Vector2(Random.Range(13f, 30f), 7.4f), Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
        anim.SetBool("isShaking", false);

    }

    public void Flip()
    {
        if (transform.position.x < target.transform.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (transform.position.x > target.transform.position.x)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    void SavePosBeforeAttack()
    {
        oldPlayerPos = target.transform.position;
    }

    void Attack()
    {
        ScreenShakeController.instance.StartShake(0.2f, 0.02f);
        GameObject slash = Instantiate(slashPrefab, firePoint.position, firePoint.rotation);
        if (transform.position.x < oldPlayerPos.x)
        {
            slash.GetComponent<Rigidbody2D>().AddForce(Vector2.left *-1f* slashSpeed, ForceMode2D.Impulse);
            slash.GetComponent<SpriteRenderer>().flipX = true;
            slash.GetComponent<BoxCollider2D>().offset = new Vector2(slash.GetComponent<BoxCollider2D>().offset.x * -1f, slash.GetComponent<BoxCollider2D>().offset.y);
        }
        else if (transform.position.x > oldPlayerPos.x)
        {
            slash.GetComponent<Rigidbody2D>().AddForce(Vector2.left * slashSpeed, ForceMode2D.Impulse);
        }  

    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(BlinkDamage());

    }

    IEnumerator BlinkDamage()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        sr.color = Color.white;
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
        CameraFollow.instance.maxValue.x = 42f;
        CameraFollow.instance.maxValue.y = 4.5f;
        invisibleWall[0].GetComponent<BoxCollider2D>().enabled = false;
        invisibleWall[1].GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject);
    }
}
