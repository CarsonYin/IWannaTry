using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapEngine : MonoBehaviour
{
    [SerializeField]
  

    public float timer;

    [Header("【Move】")]
    public Vector3[] velocity;
    public float[] moveSleepTime;
    public float[] moveDuration;
    public int moveIndex;


    [Header("【Scale】")]
    [Header("负数的话缩小 Negative for Smaller")]
    public float[] scaleSpeed;
    public float[] scaleFinal;
    public float[] scaleSleepTime;

    public float[] realScaleSpeed;
    public float[] realScaleFinal;


    [Header("【Rotate】")]
    [Header("负数的话顺时针 Negative for Clockwise")]
    public float[] rotateSpeed;
    public float[] rotateTotalAngle;
    public float[] rotatedAngle;
    public float[] rotateSleepTime;

    private bool startMove;
    private bool startScale;
    private bool startRotate;

    private Vector3 initPosi;

    private bool startTimer;

    void Awake()
    {

        timer = 0;
        for (int i = 0; i < scaleFinal.Length; i++)
        {
            realScaleFinal[i] = scaleFinal[i] * transform.localScale.x;
            realScaleSpeed[i] = scaleSpeed[i]* transform.localScale.x;
        }



    }

    public void React()
    {
        startMove = true;
        initPosi = transform.position;

        startScale = true;

        startRotate = true;
        // rotatedAngle[0] = 0;
        startTimer = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            timer += Time.deltaTime;
        }

        //Move
        if (startMove)
        {
            if (timer >= moveSleepTime[moveIndex] && timer < moveSleepTime[moveIndex] + moveDuration[moveIndex])
            {
                transform.position += velocity[moveIndex];

            }
            else if(timer >= moveSleepTime[moveIndex] + moveDuration[moveIndex]) {
                if (moveIndex < velocity.Length - 1)
                {
                    moveIndex++;
                }
            }



        }

    }
}
