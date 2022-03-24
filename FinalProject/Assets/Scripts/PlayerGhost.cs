using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhost : MonoBehaviour
{
    SpriteRenderer sr;
    float timer = 0.2f;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        transform.position = PlayerController.instance.transform.position;
        transform.localScale = PlayerController.instance.transform.localScale;

        sr.sprite = PlayerController.instance.sr.sprite;
        //sr.color = new Vector4(50, 50, 50, 0.1f);
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
