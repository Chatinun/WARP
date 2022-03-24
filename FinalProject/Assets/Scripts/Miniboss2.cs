using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Miniboss2 : MonoBehaviour
{
    private SpriteRenderer sr;
    private BoxCollider2D col;
    public GameObject bossActivator;
    public GameObject[] invisibleWall;
    public GameObject bossHealthBar;
    public Slider healthBar;
    public Transform[] spawner;
    public GameObject bat;
    public GameObject deadParticle;

    private bool batSpawned;

    //Y
    [SerializeField] float speedY = 4f;
    [SerializeField] float height = 0.2f;
    Vector3 pos;

    //X
    [SerializeField] float speedX;
    [SerializeField] int startingPoint;

    //Stat
    [SerializeField] private int health = 450;
    [SerializeField] private float beamTime = 2f;
    [SerializeField] private float waitTime = 1f;

    public bool bossTriggerStart;

    [SerializeField] private GameObject laserBeam;
    [SerializeField] private Transform firePoint;

    private float timeBtwAttack;
    public float startTimeBtwAttack;
    private bool attack;

    public Transform[] points;
    private int i;

    public static Miniboss2 instance;

    private void Awake()
    {
        instance = this;
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();

    }

    private void Start()
    {
        pos = transform.position;
        col.enabled = false;
        bossHealthBar.SetActive(false);
        //transform.position = points[startingPoint].position;
    }

    private void Update()
    {
        healthBar.value = health;
        if (bossTriggerStart)
        {
            col.enabled = true;
            bossActivator.GetComponent<BoxCollider2D>().enabled = false;
            bossHealthBar.SetActive(true);
            invisibleWall[0].GetComponent<BoxCollider2D>().enabled = true;
            invisibleWall[1].GetComponent<BoxCollider2D>().enabled = true;
            CameraFollow.instance.minValue.x = 21.46f;
            CameraFollow.instance.maxValue.x = 21.46f;
            if (timeBtwAttack <= startTimeBtwAttack)
            {
                attack = false;
                timeBtwAttack += Time.deltaTime;

                //transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time * speedY) * height + pos.y, transform.position.z);

                if (Vector2.Distance(transform.position, points[i].position) < 0.1f)
                {
                    i++;
                    if (i == points.Length)
                    {
                        i = 0;
                    }
                }
                transform.position = Vector2.MoveTowards(transform.position, points[i].position, speedX * Time.deltaTime);
            }
            else
            {
                if (!attack)
                {
                    StartCoroutine(Attack(waitTime, beamTime));
                    
                }
                    
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
            speedX = 10f;
            startTimeBtwAttack = 1.5f;
            waitTime = 0.2f;
            beamTime = 0.5f;
        }else if (health <= 200)
        {
            if (!batSpawned)
            {
                StartCoroutine(SpawnBat(5));
                batSpawned = true;
            }
            speedX = 9f;
            startTimeBtwAttack = 1.75f;
            waitTime = 0.4f;
            beamTime = 0.75f;

        }
        else if (health <= 300)
        {
            speedX = 8f;
            startTimeBtwAttack = 2f;
            waitTime = 0.5f;
            beamTime = 1f;
        }
        else if (health <= 400)
        {
            speedX = 6f;
            startTimeBtwAttack = 3f;
            waitTime = 0.8f;
            beamTime = 1.5f;
        }
    }

    IEnumerator SpawnBat(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int num = Random.Range(0, 2);
            Instantiate(bat, spawner[num].position, spawner[num].rotation);
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator Attack(float waitTime, float beamTime)
    {
        attack = true;
        yield return new WaitForSeconds(waitTime);
        AudioManager.instance.audioPlay("Laser");
        ScreenShakeController.instance.StartShake(beamTime, 0.03f);
        float height = laserBeam.GetComponent<SpriteRenderer>().bounds.size.y;
        GameObject LaserBeamClone = Instantiate(laserBeam, firePoint.position - new Vector3(0, height / 2, 0), firePoint.rotation);
        Destroy(LaserBeamClone, beamTime);
        yield return new WaitForSeconds(beamTime);
        AudioManager.instance.audioSource.Stop();
        yield return new WaitForSeconds(waitTime);
        timeBtwAttack = 0;
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
        CameraFollow.instance.maxValue.x = 25f;
        invisibleWall[0].GetComponent<BoxCollider2D>().enabled = false;
        invisibleWall[1].GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject);
    }
}

