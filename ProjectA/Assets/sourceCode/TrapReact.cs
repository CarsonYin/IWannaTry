using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapReact : MonoBehaviour
{

    public enum TrapMoveDirection
    {
        Vertical,
        Horizontal
    }

    public bool stopIfReachBlock;

    [Header("【Move】")]
    [Header("负数的话向下/左 Negtive for Down/Left")]
    public float moveSpeed;
    public TrapMoveDirection direction;
    public float moveDistance;

    [Header("【Scale】")]
    [Header("负数的话缩小 Negative for Smaller")]
    public float scaleSpeed;
    public float scaleFinal;
    private float realScaleSpeed;
    private float realScaleFinal;

    [Header("【Rotate】")]
    [Header("负数的话顺时针 Negative for Clockwise")]
    public float rotateSpeed;
    public float rotateTotalAngle;
    public float rotatedAngle;

    private bool startMove;
    private bool startScale;
    private bool startRotate;

    private Vector3 initPosi;
   // private Vector3 initScale;

    void Awake()
    {
        //  stopIfReachBlock = true;
        // direction = TrapMoveDirection.Vertical;

        //moveDistance = 5f;
        //moveSpeed = -5f;

        //scaleSpeed = 0f;
        //scale = 1f;

        //rotateSpeed = 0f;
        //rotateTotalAngle = 0f;
        //rotatedAngle = 0f;

        //startMove = false;
        //startScale = false;
        //startRotate = false;

        realScaleFinal = scaleFinal * transform.localScale.x;
        realScaleSpeed = scaleSpeed * transform.localScale.x;
    }

    public void React()
    {
        if (moveSpeed != 0)
        {
            startMove = true;
            initPosi = transform.position;
        }

        if (scaleSpeed != 0)
        {
            startScale = true;
           // initScale = transform.localScale;
        }

        if (rotateSpeed != 0)
        {
            startRotate = true;
            rotatedAngle = 0;
        }

    }

    void Update()
    {
        if (startMove)
        {
            if (Vector3.Distance(transform.position, initPosi) <= moveDistance)
            {
                switch (direction)
                {
                    case TrapMoveDirection.Vertical:
                        transform.position += new Vector3(0, moveSpeed, 0);
                        break;
                    case TrapMoveDirection.Horizontal:
                        transform.position += new Vector3(moveSpeed, 0, 0);
                        break;
                }
            }
        }

        if (startScale)
        {
            if (scaleSpeed > 0)
            {
                if (transform.localScale.x < realScaleFinal)
                {
                    transform.localScale += new Vector3(realScaleSpeed, realScaleSpeed, 0);
                }
            }
            else
            {
                if (transform.localScale.x > realScaleFinal)
                {
                    transform.localScale += new Vector3(realScaleSpeed, realScaleSpeed, 0);
                }
            }
        }

        if (startRotate)
        {
            if (rotateSpeed > 0)
            {
                if (rotatedAngle < rotateTotalAngle)
                {
                    transform.Rotate(Vector3.forward, rotateSpeed);
                    rotatedAngle += rotateSpeed;
                }
            }
            else {
                if (rotatedAngle > rotateTotalAngle)
                {
                    transform.Rotate(Vector3.forward, rotateSpeed);
                    rotatedAngle += rotateSpeed;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            if (stopIfReachBlock)
            {
                startMove = false;
                startScale = false;
                startRotate = false;
            }
        }
    }
}
