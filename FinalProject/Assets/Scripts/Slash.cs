using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public float speed;
    public float direction;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
