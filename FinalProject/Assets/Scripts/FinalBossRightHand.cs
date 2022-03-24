using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossRightHand : MonoBehaviour
{
    public Material flashMaterial;
    private Material originalMaterial;

    public GameObject deadParticle;
    private bool isDie;

    SpriteRenderer sr;
    FinalBoss boss;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        boss = GetComponentInParent<FinalBoss>();
    }
    private void Start()
    {
        originalMaterial = sr.material;
    }
    public void TakeDamage(int damage)
    {
        boss.health -= damage;
        StartCoroutine(BlinkDamage());
    }

    IEnumerator BlinkDamage()
    {
        sr.material = flashMaterial;
        yield return new WaitForSeconds(0.05f);
        sr.material = originalMaterial;
    }

    public void Die()
    {
        Instantiate(deadParticle, transform.position, transform.rotation);
        sr.enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        AudioManager.instance.audioPlay("Dead");
        StartCoroutine(Die2());
    }

    IEnumerator Die2()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
