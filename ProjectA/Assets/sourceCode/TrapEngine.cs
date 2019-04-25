using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapEngine : MonoBehaviour
{
    [SerializeField]


    public float timer;

    [Header("【Move】")]
    public Vector3 initialPosition;
    public Vector3[] velocity;
    public float[] moveSleepTime;
    public float[] moveDuration;
    public int moveIndex;


    [Header("【Scale】")]
    public Vector3 initialScale;
    public Vector3[] scaleSpeed;
    //public Vector3[] scaleFinal;
    public float[] scaleSleepTime;
    public float[] scaleDuration;
    public int scaleIndex;
    // public float[] realScaleSpeed;
    //public float[] realScaleFinal;



    [Header("【Rotate】")]
    public float[] rotateSpeed;
    //public float[] rotateTotalAngle;
    //public float[] rotatedAngle;
    public Vector3[] rotateCenterPoint;
    public float[] rotateSleepTime;
    public float[] rotateDuration;
    public int rotateIndex;

    [Header("【Self Rotate】")]
    public Quaternion initialSelfRotation;
    public float[] selfRotateSpeed;
    //public float[] rotateTotalAngle;
    //public float[] rotatedAngle;
    public float[] selfRotateSleepTime;
    public float[] selfRotateDuration;
    public int selfRotateIndex;





    private bool startMove;
    private bool startScale;
    private bool startRotate;
    private bool startSelfRotate;

    private bool startTimer;

    void Awake()
    {

        timer = 0;
        //for (int i = 0; i < scaleFinal.Length; i++)
        //{
        //    //  realScaleFinal[i] = scaleFinal[i] * transform.localScale.x;
        //    //  realScaleSpeed[i] = scaleSpeed[i]* transform.localScale.x;
        //}

        initialPosition = transform.position;
        initialScale = transform.localScale;
        initialSelfRotation = transform.rotation;

    }

    public void React()
    {
        startMove = true;
        startScale = true;
        startRotate = true;
        startSelfRotate = true;

        startTimer = true;
    }

    public void Reset()
    {
        startTimer = false;
        timer = 0;
        moveIndex = 0;
        scaleIndex = 0;
        rotateIndex = 0;

        transform.position = initialPosition;
        transform.localScale = initialScale;
        transform.rotation = initialSelfRotation;
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
            if (velocity.Length > 0)
            {
                if (timer >= moveSleepTime[moveIndex] && timer < moveSleepTime[moveIndex] + moveDuration[moveIndex])
                {
                    transform.position += velocity[moveIndex] * Time.deltaTime;
                }
                else if (timer >= moveSleepTime[moveIndex] + moveDuration[moveIndex])
                {
                    if (moveIndex < velocity.Length - 1)
                    {
                        moveIndex++;
                    }
                    else
                    {
                        startMove = false;
                    }
                }
            }
        }

        if (startScale)
        {
            if (scaleSpeed.Length > 0)
            {
                if (timer >= scaleSleepTime[scaleIndex] && timer < scaleSleepTime[scaleIndex] + scaleDuration[scaleIndex])
                {
                    transform.localScale += scaleSpeed[scaleIndex] * Time.deltaTime;
                }
                else if (timer >= scaleSleepTime[scaleIndex] + scaleDuration[scaleIndex])
                {
                    if (scaleIndex < scaleSpeed.Length - 1)
                    {
                        scaleIndex++;
                    }
                    else
                    {
                        startScale = false;
                    }
                }
            }
        }

        if (startRotate)
        {
            if (rotateSpeed.Length > 0)
            {
                if (timer >= rotateSleepTime[rotateIndex] && timer < rotateSleepTime[rotateIndex] + rotateDuration[rotateIndex])
                {
                    //  transform.localScale += scaleSpeed[scaleIndex];
                    transform.RotateAround(rotateCenterPoint[rotateIndex],Vector3.forward, rotateSpeed[rotateIndex] * Time.deltaTime);
                }
                else if (timer >= rotateSleepTime[rotateIndex] + rotateDuration[rotateIndex])
                {
                    if (rotateIndex < rotateSpeed.Length - 1)
                    {
                        rotateIndex++;
                    }
                    else
                    {
                        startRotate = false;
                    }
                }
            }
        }

        if (startSelfRotate)
        {
            if (selfRotateSpeed.Length > 0)
            {
                if (timer >= selfRotateSleepTime[selfRotateIndex] && timer < selfRotateSleepTime[selfRotateIndex] + selfRotateDuration[selfRotateIndex])
                {
                    //  transform.localScale += scaleSpeed[scaleIndex];
                    transform.Rotate(0, 0, selfRotateSpeed[selfRotateIndex] * Time.deltaTime);
                }
                else if (timer >= selfRotateSleepTime[selfRotateIndex] + selfRotateDuration[selfRotateIndex])
                {
                    if (selfRotateIndex < selfRotateSpeed.Length - 1)
                    {
                        selfRotateIndex++;
                    }
                    else
                    {
                        startSelfRotate = false;
                    }
                }
            }
        }



    }
}
