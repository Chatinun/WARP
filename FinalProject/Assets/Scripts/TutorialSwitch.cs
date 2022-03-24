using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSwitch : MonoBehaviour
{
    [SerializeField] private GameObject bubbleText;
   
    public bool isPressed;

    private void Start()
    {
        bubbleText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bubbleText.SetActive(true);
            AudioManager.instance.audioPlay("Switch");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bubbleText.SetActive(false);
        }
    }
}
