using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTR02 : MonoBehaviour
{


    public bool startTimer;

    public bool startMove;

    [Header("【Move】")]
    public Vector3 initialPosition;
    public Vector3[] velocity;
    public float[] moveSleepTime;
    public float[] moveDuration;
    public int moveIndex;

    public float timer;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void S02React()
    {
        gameObject.tag = "Deadly";
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        startMove = true;
        startTimer = true;

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (startTimer)
        {
            timer += Time.fixedDeltaTime;
        }

        //Move
        if (startMove)
        {
            if (velocity.Length > 0)
            {
                if (timer >= moveSleepTime[moveIndex] && timer < moveSleepTime[moveIndex] + moveDuration[moveIndex])
                {
                    transform.position += velocity[moveIndex] * Time.fixedDeltaTime;
                }
                else if (timer >= moveSleepTime[moveIndex] + moveDuration[moveIndex])
                {
                    if (moveIndex < velocity.Length - 1)
                    {
                        moveIndex++;
                    }
                }
            }
        }
    }



}
