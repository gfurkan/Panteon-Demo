using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDonutMovement : MonoBehaviour
{
    private GameObject stick;

    private bool moveLeft = false, moveRight = false;
    private float targetxPos = 0,time = 0;
    private double tempMovementSpeed = 0;

    [SerializeField]
    private double movementSpeed = 0,speedDivider=0;
    [SerializeField]
    private float startDelay = 0;

    void Start()
    {
        stick = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        HalfDonutPositionControl();
        MoveStick(targetxPos,(float)tempMovementSpeed);
    }
    void HalfDonutPositionControl()
    {
        if (stick.transform.localPosition.x == 0.15f)
        {
            targetxPos = -0.12f;
            tempMovementSpeed = movementSpeed;
        }
        if (stick.transform.localPosition.x == -0.12f)
        {
            targetxPos = 0.15f;
            tempMovementSpeed = movementSpeed / speedDivider;
        }
    }
    void MoveStick(float targetXPosition,float movementSpeed)
    {
        time += Time.deltaTime;
        if (time >=startDelay)
        {
            stick.transform.localPosition = Vector3.MoveTowards(stick.transform.localPosition, new Vector3(targetXPosition, 0, 0), 0.1f * Time.deltaTime * movementSpeed);
        }
    }
}
