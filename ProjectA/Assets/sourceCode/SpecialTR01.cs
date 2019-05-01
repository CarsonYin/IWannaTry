using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTR01 : MonoBehaviour
{


    public bool startTimer;

    public bool startScale;

    [Header("【Scale】")]
    public Vector3 initialScale;
    public Vector3[] scaleSpeed;
    //public Vector3[] scaleFinal;
    public float[] scaleSleepTime;
    public float[] scaleDuration;
    public int scaleIndex;

    public float timer;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void S01React()
    {
        startScale = true;
        startTimer = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (startTimer)
        {
            timer += Time.fixedDeltaTime;
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
                }
            }
        }
    }



}
