using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger1 : MonoBehaviour
{
    public GameObject monster;
    public GameObject player;

    [SerializeField] private float spawnDelay = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(SpawnSnake());
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    IEnumerator SpawnSnake()
    {
        for (int i = 0; i < 8; i++)
        {
            int direction = 0;
            if (i % 2 == 0)
                direction = 1;
            else
                direction = -1;
            Instantiate(monster, player.transform.position + new Vector3(-11 * direction, 0, 0), transform.rotation);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
