using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiraijinKunai : MonoBehaviour
{
    //public GameObject teleDes;
    public HiraijinType direction;


    public Rigidbody2D rigit;

    public float speed;
    public float flyTime;

    public float timer;

    public bool hadStopped;
    public bool hadTimeOut;

    public enum HiraijinType
    {
        Ver = 0,
        Right = 1,
        Left = 2
    }

    private void Awake()
    {
        //if (direction == HiraijinType.Ver)
        //{

        rigit = gameObject.GetComponent<Rigidbody2D>();



        // }
    }

    // Start is called before the first frame update
    void Start()
    {
        // rigit = gameObject.GetComponent<Rigidbody2D>();

    }

    private void OnEnable()
    {
        //if (!rigit)
        //{
        //    gameObject.AddComponent<Rigidbody2D>();
        //    rigit = gameObject.GetComponent<Rigidbody2D>();
        //}

        timer = 0;
        hadStopped = false;
        hadTimeOut = false;
        transform.SetParent(null);


        gameObject.GetComponent<PolygonCollider2D>().enabled = true;

        if (direction == HiraijinType.Ver)
        {
            rigit.constraints = ~RigidbodyConstraints2D.FreezePositionY;  // 冻结除Y之外所有
            //rigit.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rigit.constraints = ~RigidbodyConstraints2D.FreezePositionX;
            //rigit.constraints = RigidbodyConstraints2D.FreezeRotation;
        }


    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;



        switch (direction)
        {
            case HiraijinType.Right:


                if (!hadStopped)
                {
                    if (timer < flyTime)
                    {
                        transform.Translate(speed * Time.deltaTime, 0, 0, Space.World);
                    }
                    else
                    {
                        hadTimeOut = true;
                    }
                }
                break;

            case HiraijinType.Left:

                if (!hadStopped)
                {
                    if (timer < flyTime)
                    {
                        transform.Translate(-speed * Time.deltaTime, 0, 0, Space.World);
                    }
                    else
                    {
                        hadTimeOut = true;
                    }
                }
                break;
            case HiraijinType.Ver:
                if (!hadStopped)
                {
                    if (timer > flyTime)
                    {
                        hadTimeOut = true;
                    }
                }
                break;
            default:
                break;

        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (direction == HiraijinType.Ver)
        {
            if (collision.gameObject.tag == "Ground")
            {
                gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                rigit.constraints = RigidbodyConstraints2D.FreezeAll;
                hadStopped = true;
            }
        }
        else
        {
            if (collision.gameObject.tag == "Wall")
            {
                gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                transform.SetParent(collision.transform);
                //Destroy(rigit);
                rigit.constraints = RigidbodyConstraints2D.FreezeAll;
                hadStopped = true;
            }
        }
    }


}
