using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public Vector2 speed;
    public float delay;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {

        Destroy(gameObject, delay);


        rb = GetComponent<Rigidbody2D>();
        rb.velocity = speed;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = speed;

    }
}
