using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : MonoBehaviour
{
    private bool activate;
    [SerializeField] private float speed = 8f;

    [SerializeField] private float direction = 1;

    private void Update()
    {
        transform.Translate(Vector2.left * direction * speed * Time.deltaTime);
    }

    private void OnBecameVisible()
    {
        AudioManager.instance.audioPlay("Saw");
    }

    private void OnBecameInvisible()
    {
        //AudioManager.instance.audioSource.Stop();
    }

}
