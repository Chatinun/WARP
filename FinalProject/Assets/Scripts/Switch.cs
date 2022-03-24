using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite redSwitch;
    public Sprite greenSwitch;

    public GameObject door;

    public float doorMoveValue;

    private Vector3 originPos;

    public bool sideWay;

    public bool doorOpen;

    private bool playSound;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        originPos = door.transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") && !doorOpen)
        {
            sr.sprite = greenSwitch;
            doorOpen = true;
            AudioManager.instance.audioPlay("Switch");
        }
    }

    private void Update()
    {
        if (doorOpen)
        {
            if (!playSound)
            {
                AudioManager.instance.audioPlay("DoorOpen");
                playSound = true;
            }
            
            if (!sideWay)
                door.transform.position = Vector2.MoveTowards(door.transform.position, originPos + new Vector3(0, doorMoveValue, 0), 1f * Time.deltaTime);
            else
                door.transform.position = Vector2.MoveTowards(door.transform.position, originPos + new Vector3(doorMoveValue, 0, 0), 1f * Time.deltaTime);
        }
    }
}
