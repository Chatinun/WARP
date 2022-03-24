using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ExtraJumpItem : MonoBehaviour
{
    [SerializeField] private GameObject particle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.audioPlay("Diamond");
            PlayerController.instance.doubleJump = true;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponentInChildren<Light2D>().enabled = false;
            Instantiate(particle, transform.position, transform.rotation);
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3);
        GetComponent<Animator>().SetTrigger("isSpawning");
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponentInChildren<Light2D>().enabled = true;
    }
}
