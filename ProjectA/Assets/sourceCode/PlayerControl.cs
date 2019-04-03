﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
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

    public bool haveShield;
    public bool shieldOn;
    public int shieldFragmentNeeded;
    public int shieldFragmentNumber;
    public float shieldRemainTime;
    [SerializeField]
    private float shieldTimer;

    private SpriteRenderer shield;

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

        PlayerLeft = Resources.Load<Sprite>("PNGs/I-wannaRightLeft");
        PlayerRight = Resources.Load<Sprite>("PNGs/I-wannaLeftRight");

        timer = 0;


    }


    // Use this for initialization
    void Start()
    {
        DoubleJumped = false;

        // Respawn Settings
        DataManager.Instance.RespawnSettings();
        // Respawn Settings Ends

        shield = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        shield.enabled = false;


    }

    // Update is called once per frame
    void Update()
    {

        // items
        // shield
        if (shieldFragmentNumber >= shieldFragmentNeeded)
        {
            shieldFragmentNumber = 0;
            haveShield = true;
        }

        if (shieldOn)
        {
            if (shieldTimer < shieldRemainTime)
            {
                shieldTimer += Time.deltaTime;
            }
            else
            {
                shieldOn = false;
                shieldTimer = 0;
                shield.enabled = false;
            }
        }

        // end shield




        // end items

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
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Deadly")
        {
            if (haveShield)
            {
                shieldOn = true;
                haveShield = false;

                shield.enabled = true;
            }
            else if (shieldOn)
            {

            }
            else
            {
                SceneManager.LoadScene(DataManager.Instance.currentLevel.ToString());
            }

        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "LevelGate")
        {
            DataManager.Instance.currentLevel++;
            SceneManager.LoadScene(DataManager.Instance.currentLevel.ToString());
        }
        else if (other.gameObject.tag == "SavePoint")
        {
            SaveData();


            // other.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            //other.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("PNGs/SavePointHighLighted");

            int savePointNumber = other.gameObject.GetComponent<SavePointData>().number;    // order number of new save point

            // update save point data in current scene manager for change sprite
            other.gameObject.transform.parent.GetComponent<CurrentSceneManager>().ResetSavePointSprite(savePointNumber);
            DataManager.Instance.ChangeSavePoint(savePointNumber);  // update save point data in data manager for respawn

        }
        else if (other.gameObject.tag == "ShieldFrag")
        {
            shieldFragmentNumber++;
            Destroy(other.gameObject);
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

    void SaveData()
    {
        DataManager.Instance.haveShield = haveShield;
        DataManager.Instance.shieldFragmentNumber = shieldFragmentNumber;
    }

}
