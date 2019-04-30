using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    //define bullet and 判断方向
    public bool isFacingRight;
    public GameObject rightFirePos;
    public GameObject leftFirePos;

    private GameObject leftBullet;
    private GameObject rightBullet;

    public GameObject Canvas;


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

    public bool isDead;
    public int colorChangeTimer;

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

        isFacingRight = true;


        rightBullet = Resources.Load<GameObject>("Prefabs/RightBullet");
        leftBullet = Resources.Load<GameObject>("Prefabs/LeftBullet");


    }

    //private void FixedUpdate()
    //{


    //}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("StartPage");
            // DataManager.Instance.BackToMenu();

        }

        Canvas.transform.GetChild(3).GetComponent<Text>().text = DataManager.Instance.deathCount.ToString();

        if (isDead)
        {

            if (colorChangeTimer < 255)
            {
                Canvas.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, colorChangeTimer / 256f);
                Canvas.transform.GetChild(1).GetComponent<Text>().color = new Color(1, 0, 0, colorChangeTimer / 128f);
                Canvas.transform.GetChild(2).GetComponent<Text>().color = new Color(1, 0, 0, colorChangeTimer / 128f);
                colorChangeTimer++;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(DataManager.Instance.currentLevel.ToString());
            }

        }




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

        //player's move
        // Right
        if (Input.GetKey(KeyCode.D))
        {
            //transform.position += new Vector3(0.5f, 0, 0);
            rigit.velocity = new Vector2(MoveSpeed, rigit.velocity.y);
            spRdr.sprite = PlayerRight;
            //
            isFacingRight = true;
        }
        // Left
        else if (Input.GetKey(KeyCode.A))
        {
            // transform.position -= new Vector3(0.5f, 0, 0);
            rigit.velocity = new Vector2(-MoveSpeed, rigit.velocity.y);
            spRdr.sprite = PlayerLeft;
            //
            isFacingRight = false;
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

        if (Input.GetKeyDown(KeyCode.K))
        {
            Fire();
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
        //press K button for shooting

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
                Time.timeScale = 0.05f;
                if (!isDead)
                {
                    DataManager.Instance.deathCount++;
                }
                isDead = true;
                //SceneManager.LoadScene(DataManager.Instance.currentLevel.ToString());
            }

        }

    }

    void OnTriggerEnter2D(Collider2D other)
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
                Time.timeScale = 0.05f;
                if (!isDead)
                {
                    DataManager.Instance.deathCount++;
                }
                isDead = true;
                //SceneManager.LoadScene(DataManager.Instance.currentLevel.ToString());



            }

        }

        else if (other.gameObject.tag == "LevelGate")
        {
            DataManager.Instance.currentLevel++;
            SceneManager.LoadScene(DataManager.Instance.currentLevel.ToString());
            DataManager.Instance.currentSavePoint = -1;
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

    //fire method
    void Fire()
    {
        if (isFacingRight)
        {
            Instantiate(rightBullet, rightFirePos.transform.position, rightBullet.transform.rotation);
        }
        else
        {
            Instantiate(leftBullet, leftFirePos.transform.position, leftBullet.transform.rotation);
        }

    }

    void SaveData()
    {
        DataManager.Instance.haveShield = haveShield;
        DataManager.Instance.shieldFragmentNumber = shieldFragmentNumber;
    }

}
