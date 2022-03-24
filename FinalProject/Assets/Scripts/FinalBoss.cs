using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalBoss : MonoBehaviour
{
    [Header("Environment")]
    public GameObject bossActivator;
    public GameObject bossHealthBar;
    public Slider healthBar;

    public Transform[] firePoint;

    public Animator anim;

    public GameObject head;
    public GameObject leftHand;
    public GameObject rightHand;

    public Transform leftHandFirePoint;
    public Transform rightHandFirePoint;

    public GameObject slashPrefab;
    public GameObject beamPrefab;
    public GameObject fallingSpikePrefab;
    public GameObject punchParticle;
    public GameObject[] monsterPrefab;
    public Transform[] spawner;

    public float slashSpeed;
    public float enrageSlashSpeed;

    public float health;

    public float startTimeBtwSpawn;
    private float timeBtwSpawn;

    public float startTimeBtwSpike;
    private float timeBtwSpike;

    public bool isEnraged;
    private bool isDie;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {

    }

    void Update()
    {
        if (!isDie)
        {
            healthBar.value = health;

            if (timeBtwSpawn >= startTimeBtwSpawn)
            {
                StartCoroutine(SpawnMonster(4));
                timeBtwSpawn = 0;
            }
            else
            {
                timeBtwSpawn += Time.deltaTime;
            }


            if (isEnraged)
            {
                if (timeBtwSpike >= startTimeBtwSpike)
                {
                    StartCoroutine(SpikeFalling(8));
                    timeBtwSpike = 0;
                }
                else
                {
                    timeBtwSpike += Time.deltaTime;
                }
            }

            ChangePhase();
        }
        
    }

    void ChangePhase()
    {
        if (health <= 0 && !isDie)
        {
            StartCoroutine(Die());
        }
        else if(health <= 450)
        {
            isEnraged = true;
            anim.SetTrigger("Enrage");
        }
    }

    IEnumerator Die()
    {
        isDie = true;
        anim.SetBool("isDead", true);
        ScreenShakeController.instance.StartShake(1.5f, 0.05f);
        bossHealthBar.SetActive(false);
        Time.timeScale = 0.25f;
        GetComponentInChildren<FinalBossLeftHand>().Die();
        yield return new WaitForSeconds(0.1f);
        GetComponentInChildren<FinalBossRightHand>().Die();
        yield return new WaitForSeconds(0.1f);
        GetComponentInChildren<FinalBossHead>().Die();
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1;
        yield return new WaitForSeconds(1.5f);
        GoToEndCutScene();

    }

    void GoToEndCutScene()
    {
        PlayerPrefs.DeleteAll();
        LevelLoader.instance.LoadNextLevel();
    }

    IEnumerator SpikeFalling(int amount)
    {
        ScreenShakeController.instance.StartShake(2f, 0.02f);
        for (int i = 0; i < amount; i++)
        {
            Instantiate(fallingSpikePrefab, new Vector2(Random.Range(-8.5f, 8.5f), 8f), Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }

    }

    void AttackLeftHand()
    {
        AudioManager.instance.audioPlay("BullAttack");
        Instantiate(punchParticle, new Vector2(leftHandFirePoint.position.x, -2f), leftHandFirePoint.rotation);
        GameObject leftSlash = Instantiate(slashPrefab, new Vector2(leftHandFirePoint.position.x , -2f), leftHandFirePoint.rotation);
        leftSlash.GetComponent<Rigidbody2D>().AddForce(Vector2.left * slashSpeed, ForceMode2D.Impulse);
        GameObject rightSlash = Instantiate(slashPrefab, new Vector2(leftHandFirePoint.position.x, -2f), leftHandFirePoint.rotation);
        rightSlash.GetComponent<Rigidbody2D>().AddForce(Vector2.left * -1f * slashSpeed, ForceMode2D.Impulse);
        rightSlash.GetComponent<SpriteRenderer>().flipX = true;
        rightSlash.GetComponent<BoxCollider2D>().offset = new Vector2(rightSlash.GetComponent<BoxCollider2D>().offset.x * -1f, rightSlash.GetComponent<BoxCollider2D>().offset.y);
        ScreenShakeController.instance.StartShake(0.2f, 0.05f);
        Destroy(leftSlash, 5f);
        Destroy(rightSlash, 5f);
    }

    void AttackRightHand()
    {
        AudioManager.instance.audioPlay("BullAttack");
        Instantiate(punchParticle, new Vector2(rightHandFirePoint.position.x, -2f), rightHandFirePoint.rotation);
        GameObject leftSlash = Instantiate(slashPrefab, new Vector2(rightHandFirePoint.position.x, -2f), rightHandFirePoint.rotation);
        leftSlash.GetComponent<Rigidbody2D>().AddForce(Vector2.left * slashSpeed, ForceMode2D.Impulse);
        GameObject rightSlash = Instantiate(slashPrefab, new Vector2(rightHandFirePoint.position.x, -2f), rightHandFirePoint.rotation);
        rightSlash.GetComponent<Rigidbody2D>().AddForce(Vector2.left * -1f * slashSpeed, ForceMode2D.Impulse);
        rightSlash.GetComponent<SpriteRenderer>().flipX = true;
        rightSlash.GetComponent<BoxCollider2D>().offset = new Vector2(rightSlash.GetComponent<BoxCollider2D>().offset.x * -1f, rightSlash.GetComponent<BoxCollider2D>().offset.y);
        ScreenShakeController.instance.StartShake(0.2f, 0.05f);
        Destroy(leftSlash, 5f);
        Destroy(rightSlash, 5f);
    }

    void EnrageAttackLeftHand()
    {
        AudioManager.instance.audioPlay("BullAttack");
        Instantiate(punchParticle, new Vector2(leftHandFirePoint.position.x, -2f), leftHandFirePoint.rotation);
        GameObject leftSlash = Instantiate(slashPrefab, new Vector2(leftHandFirePoint.position.x, -2f), leftHandFirePoint.rotation);
        leftSlash.GetComponent<Rigidbody2D>().AddForce(Vector2.left * enrageSlashSpeed, ForceMode2D.Impulse);
        GameObject rightSlash = Instantiate(slashPrefab, new Vector2(leftHandFirePoint.position.x, -2f), leftHandFirePoint.rotation);
        rightSlash.GetComponent<Rigidbody2D>().AddForce(Vector2.left * -1f * enrageSlashSpeed, ForceMode2D.Impulse);
        rightSlash.GetComponent<SpriteRenderer>().flipX = true;
        rightSlash.GetComponent<BoxCollider2D>().offset = new Vector2(rightSlash.GetComponent<BoxCollider2D>().offset.x * -1f, rightSlash.GetComponent<BoxCollider2D>().offset.y);
        ScreenShakeController.instance.StartShake(0.2f, 0.05f);
        Destroy(leftSlash, 5f);
        Destroy(rightSlash, 5f);
    }

    void EnrageAttackRightHand()
    {
        AudioManager.instance.audioPlay("BullAttack");
        Instantiate(punchParticle, new Vector2(rightHandFirePoint.position.x, -2f), rightHandFirePoint.rotation);
        GameObject leftSlash = Instantiate(slashPrefab, new Vector2(rightHandFirePoint.position.x, -2f), rightHandFirePoint.rotation);
        leftSlash.GetComponent<Rigidbody2D>().AddForce(Vector2.left * enrageSlashSpeed, ForceMode2D.Impulse);
        GameObject rightSlash = Instantiate(slashPrefab, new Vector2(rightHandFirePoint.position.x, -2f), rightHandFirePoint.rotation);
        rightSlash.GetComponent<Rigidbody2D>().AddForce(Vector2.left * -1f * enrageSlashSpeed, ForceMode2D.Impulse);
        rightSlash.GetComponent<SpriteRenderer>().flipX = true;
        rightSlash.GetComponent<BoxCollider2D>().offset = new Vector2(rightSlash.GetComponent<BoxCollider2D>().offset.x * -1f, rightSlash.GetComponent<BoxCollider2D>().offset.y);
        ScreenShakeController.instance.StartShake(0.2f, 0.05f);
        Destroy(leftSlash, 5f);
        Destroy(rightSlash, 5f);
    }
    void AttackBeam(float beamTime)
    {
        AudioManager.instance.audioPlay("Laser");
        //StartCoroutine(AttackBeamCoroutine(waitTime, beamTime));
        ScreenShakeController.instance.StartShake(beamTime, 0.03f);
        float height = beamPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        GameObject LaserBeamClone = Instantiate(beamPrefab, firePoint[0].position - new Vector3(0, height / 2, 0), firePoint[0].rotation);
        GameObject LaserBeamClone2 = Instantiate(beamPrefab, firePoint[1].position - new Vector3(0, height / 2, 0), firePoint[1].rotation);
        Destroy(LaserBeamClone, beamTime);
        Destroy(LaserBeamClone2, beamTime);
    }

    IEnumerator SpawnMonster(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            int monsterRandom = Random.Range(0, 7);
            int spawnerRandom = Random.Range(0, 2);
            if(monsterRandom == 3)
            {
                Instantiate(monsterPrefab[monsterRandom], spawner[0].position, spawner[0].rotation);
            }
            else if(monsterRandom == 4)
            {
                Instantiate(monsterPrefab[monsterRandom], spawner[1].position, spawner[1].rotation);
            }else if(monsterRandom == 5)
            {
                Instantiate(monsterPrefab[monsterRandom], new Vector2(spawner[spawnerRandom].position.x, spawner[spawnerRandom].position.y + 4f), spawner[spawnerRandom].rotation);
            }
            else if (monsterRandom == 6)
            {
                Instantiate(monsterPrefab[monsterRandom], new Vector2(spawner[spawnerRandom].position.x, spawner[spawnerRandom].position.y + 5f), spawner[spawnerRandom].rotation);
            }
            else
            {
                Instantiate(monsterPrefab[monsterRandom], spawner[spawnerRandom].position, spawner[spawnerRandom].rotation);
            }
            
            yield return new WaitForSeconds(0.5f);
        }
    }

}
