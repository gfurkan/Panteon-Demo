﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDonutMovement : MonoBehaviour
{

    private GameObject stick;
    private bool moveLeft = false, moveRight = false;
    private float targetxPos = 0,startTime = 0, tempMovementSpeed = 0,waitingTime=0;

    [SerializeField] private float movementSpeed = 0, speedDivider = 0, startDelay = 0, moveBackSpeed = 0, waitingDelay = 0;

    void Start()
    {
        stick = transform.GetChild(0).gameObject;
        waitingTime = waitingDelay;
    }

    void Update()
    {
        HalfDonutPositionControl();
        MoveStick(targetxPos,tempMovementSpeed);
    }
    void HalfDonutPositionControl()
    {
        if (stick.transform.localPosition.x == 0.15f)
        {
            waitingTime += Time.deltaTime;
            if (waitingTime >= waitingDelay)
            {
                targetxPos = -0.12f;
                tempMovementSpeed = movementSpeed;
            }
        }
        if (stick.transform.localPosition.x == -0.12f)
        {
            waitingTime = 0;
            targetxPos = 0.15f;
            tempMovementSpeed = moveBackSpeed;
        }
    }
    void MoveStick(float targetXPosition,float movementSpeed)
    {
        startTime += Time.deltaTime;
        if (startTime >= startDelay)
        {
            stick.transform.localPosition = Vector3.MoveTowards(stick.transform.localPosition, new Vector3(targetXPosition, 0, 0), 0.1f * Time.deltaTime * movementSpeed);
        }
    }
}
