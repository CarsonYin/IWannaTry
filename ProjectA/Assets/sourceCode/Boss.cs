using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public int HP;
    public int HP2;
    public int HPMax;
    public int HPMax2;
    public float hpFloat;
    public float hpFloat2;

    public float velocity1;
    public float velocity2;

    public GameObject[] roots;
    public GameObject[] roots2;

    private GameObject apple;
    public int index;
    public int index2;

    public GameObject apple1;
    public GameObject apple2; 
    public GameObject apple3;

    public GameObject player;
    public GameObject levelGate;

    public float shootGap;
    public float shootGap2;
    public float timer2;



    // Start is called before the first frame update
    void Start()
    {
        HP = HPMax;
        HP2 = HPMax2; 
    }

    // Update is called once per frame
    void Update()
    {
        timer2 += Time.deltaTime;

        if (HP2 <= 0) {
            levelGate.SetActive(true);
            gameObject.SetActive(false);
        }

        hpFloat = (float)HP / HPMax;
        hpFloat2 = (float)HP2 / HPMax2;

        if (HP > 0)
        {
            if (index >= roots.Length)
            {
                index = 0;
            }

            if (index % 2 == 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, roots[index].transform.position, Time.deltaTime * velocity1);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, roots[index].transform.position, Time.deltaTime * velocity2);
                if (timer2 >= shootGap)
                {
                    Fire2();
                    timer2 = 0;
                }

            }
            if (transform.position == roots[index].transform.position)
            {
                if (index % 2 == 0)
                {
                    Fire1();
                }
                index++;
            }
        }
        else {


            if (index2 >= roots2.Length)
            {
                index2 = 0;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, roots2[index2].transform.position, Time.deltaTime * velocity2);
                if (timer2 >= shootGap2)
                {
                    Fire3();
                    timer2 = 0;
                }

            }
            if (transform.position == roots2[index2].transform.position)
            {

                index2++;
            }



        }







    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (HP > 0) {
                HP--;
            }
            else {
                HP2--;
            }

        }
    }


    void Fire1()
    {
        Vector3 scal = new Vector3(0.1f, 0.1f, 0.1f);
        for (int i = 0; i < 360; i += 10)
        {
            apple1.transform.Rotate(Vector3.forward, 10, Space.Self);
            GameObject go = Instantiate(apple1, apple1.transform.position, apple1.transform.rotation);
            go.GetComponent<appleBullet>().willDes = true;
            go.transform.localScale = scal;

        }
    }

    void Fire2() {
        Vector3 scal = new Vector3(0.1f, 0.1f, 0.1f);
        Vector3 pp = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) * 100;
        GameObject go = Instantiate(apple2, apple2.transform.position, apple2.transform.rotation);
        go.GetComponent<appleBullet>().willDes = true;
        go.GetComponent<appleBullet>().moveToPlayer = true;
        go.GetComponent<appleBullet>().target = pp;
        go.transform.localScale = scal;

    }

    void Fire3()
    {
        Vector3 scal = new Vector3(0.1f, 0.1f, 0.1f);
        apple3.transform.Rotate(Vector3.forward, 10, Space.Self);
        GameObject go = Instantiate(apple3, apple3.transform.position, apple3.transform.rotation);
        go.GetComponent<appleBullet>().willDes = true;
        go.transform.localScale = scal;

    }


}
