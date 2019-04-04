using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    private float m_nextFire;

    public float FireRate = 2.0f;
    public GameObject bullet;
    public float BulletSpeed;


    // Update is called once per frame
    void FixedUpdate() {

        m_nextFire = m_nextFire + Time.fixedDeltaTime;

        if (Input.GetKeyDown(KeyCode.K) && m_nextFire>FireRate)
        {
            //timer 0
            m_nextFire = 0;

            //create bullet
            GameObject m_bullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;

            //speed

        }

    }
}
