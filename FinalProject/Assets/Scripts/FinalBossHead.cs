using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossHead : MonoBehaviour
{
    public GameObject deadParticle;
    public void TakeDamage(int damage)
    {
        GetComponentInParent<FinalBoss>().health -= damage;
        StartCoroutine(BlinkDamage());
    }

    IEnumerator BlinkDamage()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.05f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void Die()
    {
        Instantiate(deadParticle, transform.position, transform.rotation);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        AudioManager.instance.audioPlay("Dead");
        StartCoroutine(Die2());
    }

    IEnumerator Die2()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
