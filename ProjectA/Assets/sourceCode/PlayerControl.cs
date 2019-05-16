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

    public bool isOnVerPlatform;
    public bool isBelowVerPlatform;

    [SerializeField]
    private GameObject hiraijinKunai;
    [SerializeField]
    private GameObject hiraijinKunaiLeft;
    [SerializeField]
    private GameObject hiraijinKunaiVer;

    [SerializeField]
    private float shieldTimer;

    [SerializeField]
    private bool hadPlacedHiraijin;
    private bool canPlaceHiraijin;


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
        DataManager.Instance.RespawnSettings(DataManager.Instance.startOptions);
        // Respawn Settings Ends

        shield = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        shield.enabled = false;

        isFacingRight = true;


        rightBullet = Resources.Load<GameObject>("Prefabs/RightBullet");
        leftBullet = Resources.Load<GameObject>("Prefabs/LeftBullet");

        if (true)
        {
            canPlaceHiraijin = true;
        }

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
        if (Input.GetKeyDown(KeyCode.R))
        {
            DataManager.Instance.startOptions = DataManager.StartOptions.Respawn;
            SceneManager.LoadScene(DataManager.Instance.currentLevel.ToString());
            DataManager.Instance.deathCount++;

        }

        Canvas.transform.GetChild(3).GetComponent<Text>().text = DataManager.Instance.deathCount.ToString();
        Canvas.transform.GetChild(5).GetComponent<Text>().text = shieldFragmentNumber.ToString();
        Canvas.transform.GetChild(6).GetComponent<Text>().text = shieldFragmentNeeded.ToString();

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
                DataManager.Instance.startOptions = DataManager.StartOptions.Respawn;
                SceneManager.LoadScene(DataManager.Instance.currentLevel.ToString());
            }

        }

        //Hiraijin

        if (true)
        { // can use Hiraijin
            if (hiraijinKunai && hiraijinKunai.activeSelf)
            {
                if (hiraijinKunai.GetComponent<HiraijinKunai>().hadTimeOut)
                {
                    hadPlacedHiraijin = false;
                    canPlaceHiraijin = true;
                }
                else
                {
                    hadPlacedHiraijin = hiraijinKunai.GetComponent<HiraijinKunai>().hadStopped;
                }
            }

            if (hiraijinKunaiLeft && hiraijinKunaiLeft.activeSelf)
            {
                if (hiraijinKunaiLeft.GetComponent<HiraijinKunai>().hadTimeOut)
                {
                    hadPlacedHiraijin = false;
                    canPlaceHiraijin = true;
                }
                else
                {
                    hadPlacedHiraijin = hiraijinKunaiLeft.GetComponent<HiraijinKunai>().hadStopped;
                }
            }

            if (hiraijinKunaiVer && hiraijinKunaiVer.activeSelf)
            {
                if (hiraijinKunaiVer.GetComponent<HiraijinKunai>().hadTimeOut)
                {
                    hadPlacedHiraijin = false;
                    canPlaceHiraijin = true;
                }
                else
                {
                    hadPlacedHiraijin = hiraijinKunaiVer.GetComponent<HiraijinKunai>().hadStopped;
                }
            }

            if (canPlaceHiraijin)
            {
                if (Input.GetKeyDown(KeyCode.U))
                {

                    if (isFacingRight)
                    {
                        canPlaceHiraijin = false;
                        if (!hiraijinKunai)
                        {
                            hiraijinKunai = new GameObject();
                            hiraijinKunai = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/HiraijinKunai"));
                            hiraijinKunai.GetComponent<HiraijinKunai>().direction = HiraijinKunai.HiraijinType.Right;
                        }
                        else
                        {
                            hiraijinKunai.SetActive(true);
                        }

                        hiraijinKunai.transform.position = transform.position + new Vector3(0.5f, 1.3f, 0);
                    }

                    else
                    {
                        canPlaceHiraijin = false;
                        if (!hiraijinKunaiLeft)
                        {
                            hiraijinKunaiLeft = new GameObject();
                            hiraijinKunaiLeft = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/HiraijinKunaiLeft"));
                            hiraijinKunaiLeft.GetComponent<HiraijinKunai>().direction = HiraijinKunai.HiraijinType.Left;
                        }
                        else
                        {
                            hiraijinKunaiLeft.SetActive(true);
                        }

                        hiraijinKunaiLeft.transform.position = transform.position + new Vector3(-0.5f, 1.3f, 0);
                    }
                    // Instantiate<GameObject>(hiraijinKunai);

                }

                else if (Input.GetKeyDown(KeyCode.I))
                {
                    canPlaceHiraijin = false;
                    if (!hiraijinKunaiVer)
                    {
                        hiraijinKunaiVer = new GameObject();
                        hiraijinKunaiVer = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/HiraijinKunaiVer"));
                        hiraijinKunaiVer.GetComponent<HiraijinKunai>().direction = HiraijinKunai.HiraijinType.Ver;
                    }
                    else
                    {
                        hiraijinKunaiVer.SetActive(true);
                    }
                    if (isFacingRight)
                    {
                        hiraijinKunaiVer.transform.position = transform.position + new Vector3(2.61f, 1.5f, 0);
                    }
                    else
                    {
                        hiraijinKunaiVer.transform.position = transform.position + new Vector3(-2.61f, 1.5f, 0);
                    }
                    //Instantiate<GameObject>(hiraijinKunai);
                }

            }
            else
            {
                if (hadPlacedHiraijin)
                {

                    if (Input.GetKeyDown(KeyCode.U))
                    {
                        if (hiraijinKunai && hiraijinKunai.activeSelf)
                        {
                            transform.position = hiraijinKunai.transform.GetChild(0).transform.position;
                            hiraijinKunai.SetActive(false);
                            canPlaceHiraijin = true;
                            hadPlacedHiraijin = false;
                            OnTheFloor = false;
                        }
                        else if (hiraijinKunaiLeft && hiraijinKunaiLeft.activeSelf)
                        {
                            transform.position = hiraijinKunaiLeft.transform.GetChild(0).transform.position;
                            hiraijinKunaiLeft.SetActive(false);
                            canPlaceHiraijin = true;
                            hadPlacedHiraijin = false;
                            OnTheFloor = false;
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.I))
                    {
                        if (hiraijinKunaiVer && hiraijinKunaiVer.activeSelf)
                        {
                            transform.position = hiraijinKunaiVer.transform.GetChild(0).transform.position;
                            hiraijinKunaiVer.SetActive(false);
                            canPlaceHiraijin = true;
                            hadPlacedHiraijin = false;
                            OnTheFloor = false;
                        }
                    }

                    else if (Input.GetKeyDown(KeyCode.O))
                    {
                        if (hiraijinKunai)
                        {
                            hiraijinKunai.SetActive(false);
                        }
                        if (hiraijinKunaiLeft)
                        {
                            hiraijinKunaiLeft.SetActive(false);
                        }
                        if (hiraijinKunaiVer)
                        {
                            hiraijinKunaiVer.SetActive(false);
                        }
                        canPlaceHiraijin = true;
                        hadPlacedHiraijin = false;
                    }
                }
            }


        }







        //end Hiraijin


        // items
        // shield
        if (shieldFragmentNumber >= shieldFragmentNeeded)
        {
            //shieldFragmentNumber = 0;
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
            isOnVerPlatform = false;
            isBelowVerPlatform = false;


        }

        // player.transform.Translate()
        //  player.rigit.AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        //pss K button for shooting

    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Deadly")
        {
            if (haveShield)
            {
                shieldOn = true;
                haveShield = false;
                shieldFragmentNumber = 0;

                shield.enabled = true;
            }
            else if (shieldOn)
            {

            }
            else
            {
                //Time.timeScale = 0.05f;
                Dead();

                //if (!isDead)
                //{
                //    DataManager.Instance.deathCount++;
                //}
                //isDead = true;

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
                //Time.timeScale = 0.05f;
                Dead();

                //if (!isDead)
                //{
                //    DataManager.Instance.deathCount++;
                //}
                //isDead = true;

                //SceneManager.LoadScene(DataManager.Instance.currentLevel.ToString());
            }

        }

        else if (other.gameObject.tag == "LevelGate")
        {
            SaveData();
            DataManager.Instance.startOptions = DataManager.StartOptions.NextLevel;
            DataManager.Instance.currentLevel++;
            SceneManager.LoadScene(DataManager.Instance.currentLevel.ToString());
            DataManager.Instance.currentSavePoint = -1;
        }
        else if (other.gameObject.tag == "SavePoint")
        {
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
        else if (other.gameObject.tag == "RemoveRigitbody")
        {
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(other.gameObject.GetComponent<Rigidbody2D>());
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Deadly")
        {
            Dead();
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            OnTheFloor = true;
            DoubleJumped = false;
            //DoubleJumping = false;
            if (isBelowVerPlatform)
            {
                Dead();
            }
        }
        if (other.gameObject.tag == "VerPlatGround")
        {
            isOnVerPlatform = true;
        }
        if (other.gameObject.tag == "VerPlatCeiling")
        {
            isBelowVerPlatform = true;
        }
        if (other.gameObject.tag == "Ceiling")
        {
            if (isOnVerPlatform)
            {
                Dead();
            }
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

    void Dead()
    {
        if (!isDead)
        {
            DataManager.Instance.deathCount++;
            Time.timeScale = 0.05f;
        }
        isDead = true;
    }


}
