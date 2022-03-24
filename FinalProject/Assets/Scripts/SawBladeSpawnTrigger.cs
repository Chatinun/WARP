using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBladeSpawnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject[] sawBlade;
    public GameObject player;
    private bool activate;

    Vector3 originPos;

    private void Start()
    {
        originPos = door.transform.position;
    }

    private void Update()
    {
        if (activate)
        {
            //Door Closing
            door.transform.position = Vector2.MoveTowards(door.transform.position, originPos + new Vector3(0, -3.5f, 0), 2f * Time.deltaTime);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            activate = true;
            AudioManager.instance.audioPlay("DoorOpen");
            GetComponent<BoxCollider2D>().enabled = false;
            InvokeRepeating("SawSpawning", 2f, 2f);
        }
    }

    void SawSpawning()
    {
        int num = Random.Range(0, 2);
        if(num == 0)
        {
            Instantiate(sawBlade[num], new Vector3(player.transform.position.x + 11, -2.05f, player.transform.position.z), transform.rotation);
        }
        else
        {
            Instantiate(sawBlade[num], new Vector3(player.transform.position.x - 11, -2.05f, player.transform.position.z), transform.rotation);
        }
        ScreenShakeController.instance.StartShake(1.5f, 0.02f);
    }
}
