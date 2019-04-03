using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCountrol : MonoBehaviour
{



    public bool OnTheFloor;
    //public bool jumped;
    public bool DoubleJumped;
    public bool Jumping;
    public bool DoubleJumping;

    public float MoveSpeed;
    public float JumpSpeed;

    public float jumpTime;

    private Rigidbody2D rigit;
    private SpriteRenderer spRdr;
    private Sprite PlayerRight;
    private Sprite PlayerLeft;

    private float timer;
    public int levelNum;


    void Awake()
    {
        levelNum = 0;

        Time.timeScale = 5;

        if (MoveSpeed == 0)
        {
            MoveSpeed = 8f;
        }
        if (JumpSpeed == 0)
        {
            JumpSpeed = 15f;
        }

        rigit = GetComponent<Rigidbody2D>();
        spRdr = GetComponent<SpriteRenderer>();

        PlayerLeft = Resources.Load<Sprite>("PlayerPNGs/I-wannaRightLeft");
        PlayerRight = Resources.Load<Sprite>("PlayerPNGs/I-wannaLeftRight");

        timer = 0;
    }


    // Use this for initialization
    void Start()
    {
        DoubleJumped = false;
    }

    // Update is called once per frame
    void Update()
    {

        // Right
        if (Input.GetKey(KeyCode.D))
        {
            //transform.position += new Vector3(0.5f, 0, 0);
            rigit.velocity = new Vector2(MoveSpeed, rigit.velocity.y);
            spRdr.sprite = PlayerRight;
        }
        // Left
        else if (Input.GetKey(KeyCode.A))
        {
            // transform.position -= new Vector3(0.5f, 0, 0);
            rigit.velocity = new Vector2(-MoveSpeed, rigit.velocity.y);
            spRdr.sprite = PlayerLeft;
        }
        else
        {
            rigit.velocity = new Vector2(0, rigit.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (OnTheFloor)
            {
                Jumping = true;
                // jumped = false;
            }
            else
            {
                if (!DoubleJumped)
                {
                    DoubleJumping = true;
                }
            }
            //else if (!DoubleJumped)
            //{
            //    DoubleJumping = true;
            //    DoubleJumped = false;
            //}
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            timer = 0;
            if (DoubleJumped)
            {
                DoubleJumping = false;
            }
            if (!OnTheFloor)
            {
                Jumping = false;
            }
        }

        // Jump
        if (Input.GetKey(KeyCode.J))
        {
            if (Jumping)
            {
                //rigit.AddForce(new Vector2(0, 20f), ForceMode2D.Impulse);

                //  rigit.velocity = new Vector2(rigit.velocity.x, JumpSpeed);

                if (timer < jumpTime)
                {
                    rigit.velocity = new Vector2(rigit.velocity.x, JumpSpeed * timer);
                    timer += Time.deltaTime;
                }
                else
                {
                    timer = 0;
                    Jumping = false;
                }
            }
            else if (!OnTheFloor)
            {
                if (DoubleJumping)
                {
                    DoubleJumped = true;
                    //Jumping = false;
                    // rigit.AddForce(new Vector2(0, 20f), ForceMode2D.Impulse);

                    //rigit.velocity = new Vector2(rigit.velocity.x, JumpSpeed);
                    if (timer < jumpTime)
                    {
                        rigit.velocity = new Vector2(rigit.velocity.x, JumpSpeed * timer);
                        //rigit.AddForce(new Vector2(0, JumpSpeed * Time.deltaTime), ForceMode2D.Impulse);
                        timer += Time.deltaTime;
                    }
                    else
                    {
                        timer = 0;
                        DoubleJumping = false;

                    }

                }
            }
            OnTheFloor = false;

        }

        // player.transform.Translate()
        //  player.rigit.AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));


    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Deadly")
        {
            //   Debug.Log("你挂了");
            switch (levelNum)
            {
                case 0:
                    SceneManager.LoadScene("Level1");
                    break;
                case 1:
                    SceneManager.LoadScene("Level1");
                    break;
                default:
                    break;
            }
        }
        //else if (other.gameObject.tag == "SavePoint")
        //{
        //    //  SceneManager.LoadScene("Test");
        //}
        else if (other.gameObject.tag == "LevelGate")
        {
            SceneManager.LoadScene("Level1");
            levelNum++;
        }

    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            OnTheFloor = true;
            DoubleJumped = false;
            //DoubleJumping = false;
        }
    }


}
