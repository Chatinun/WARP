using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 20;
    public SpriteRenderer sr;

    public GameObject deadParticle;

    public static Enemy instance;

    [SerializeField] private GameObject floatingTextPrefab;

    void Awake()
    {
        instance = this;

        if (GetComponent<SpriteRenderer>() != null)
            sr = GetComponent<SpriteRenderer>();
        else
            sr = GetComponentInChildren<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        ShowDamage(damage.ToString());
        health -= damage;
        StartCoroutine(BlinkDamage());
        

        if(health <= 0)
        {
            Die();
        }
    }

    void ShowDamage(string text)
    {
        if (floatingTextPrefab)
        {
            GameObject prefab = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;
            Destroy(prefab, 2f);
        }
    }

    IEnumerator BlinkDamage()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        sr.color = Color.white;
    }

    public void Die()
    {
        AudioManager.instance.audioPlay("EnemyDead");
        Instantiate(deadParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
