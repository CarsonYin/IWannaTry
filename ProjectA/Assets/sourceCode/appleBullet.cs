using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appleBullet : MonoBehaviour
{
    public Vector3 velocity;
    public float speed;
    public bool willDes;
    public bool moveToPlayer;
    public Vector3 target;
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        if (willDes)
        {
            Destroy(gameObject, delay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (willDes)
        {
            if (moveToPlayer)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed);
            }
            else
            {
                transform.Translate(velocity, Space.Self);
            }
        }


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (willDes)
        {
            //if (collision.gameObject.tag != "Bullet")
            //{
            //    Destroy(gameObject, 0);
            //}

            Destroy(gameObject, 0);
        }
    }
}
