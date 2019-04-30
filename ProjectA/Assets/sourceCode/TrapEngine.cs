using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapEngine : MonoBehaviour
{
    [SerializeField]

    public bool isSelfRepeating;
    //private bool alreadyReset;

    public bool startMove;
    public bool startScale;
    public bool startRotate;
    public bool startSelfRotate;

    public bool startTimer;

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

    public void Reset(bool isSelfRepeating)
    {

        if (isSelfRepeating)
        {
            startTimer = true;
        }
        else
        {
            startTimer = false;

        }

        timer = 0;
        moveIndex = 0;
        scaleIndex = 0;
        rotateIndex = 0;

        transform.position = initialPosition;
        transform.localScale = initialScale;
        transform.rotation = initialSelfRotation;

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
                    else
                    {
                        if (isSelfRepeating)
                        {
                            Reset(isSelfRepeating);
                        }
                        else
                        {
                            startMove = false;
                        }
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
                    transform.localScale += scaleSpeed[scaleIndex] * Time.fixedDeltaTime;
                }
                else if (timer >= scaleSleepTime[scaleIndex] + scaleDuration[scaleIndex])
                {
                    if (scaleIndex < scaleSpeed.Length - 1)
                    {
                        scaleIndex++;
                    }
                    else
                    {
                        if (isSelfRepeating)
                        {
                            Reset(isSelfRepeating);
                        }
                        else
                        {
                            startScale = false;
                        }
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
                    transform.RotateAround(rotateCenterPoint[rotateIndex], Vector3.forward, rotateSpeed[rotateIndex] * Time.fixedDeltaTime);
                }
                else if (timer >= rotateSleepTime[rotateIndex] + rotateDuration[rotateIndex])
                {
                    if (rotateIndex < rotateSpeed.Length - 1)
                    {
                        rotateIndex++;
                    }
                    else
                    {
                        if (isSelfRepeating)
                        {
                            Reset(isSelfRepeating);
                        }
                        else
                        {
                            startRotate = false;
                        }
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
                    transform.Rotate(0, 0, selfRotateSpeed[selfRotateIndex] * Time.fixedDeltaTime);
                }
                else if (timer >= selfRotateSleepTime[selfRotateIndex] + selfRotateDuration[selfRotateIndex])
                {
                    if (selfRotateIndex < selfRotateSpeed.Length - 1)
                    {
                        selfRotateIndex++;
                    }
                    else
                    {
                        if (isSelfRepeating)
                        {
                            Reset(isSelfRepeating);
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
}
