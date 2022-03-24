using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSwitch : MonoBehaviour
{
    [SerializeField] private Sprite redSwitch;
    [SerializeField] private Sprite greenSwitch;

    [SerializeField] private float doorMoveValue;

    [SerializeField] private GameObject door;
    public bool doorOpen;
    public bool sideWay;

    private Vector3 originPos;

    private void Start()
    {
        originPos = door.transform.position;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Box")
        {
            doorOpen = true;
            GetComponent<SpriteRenderer>().sprite = greenSwitch;
            AudioManager.instance.audioPlay("Switch");
            AudioManager.instance.audioPlay("DoorOpen");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            doorOpen = false;
            GetComponent<SpriteRenderer>().sprite = redSwitch;
            AudioManager.instance.audioPlay("Switch");
            AudioManager.instance.audioPlay("DoorOpen");
        }
    }

    void Update()
    {
        if (doorOpen)
        {
            if (!sideWay)
                door.transform.position = Vector2.MoveTowards(door.transform.position, originPos + new Vector3(0, doorMoveValue, 0), 1f * Time.deltaTime);
            else
                door.transform.position = Vector2.MoveTowards(door.transform.position, originPos + new Vector3(doorMoveValue, 0, 0), 1f * Time.deltaTime);
        }
        else
        {
            if (!sideWay)
                door.transform.position = Vector2.MoveTowards(door.transform.position, originPos, 1f * Time.deltaTime);
            else
                door.transform.position = Vector2.MoveTowards(door.transform.position, originPos, 1f * Time.deltaTime);
        }
    }
}
