using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObstacleMovement : MonoBehaviour
{
    [SerializeField] private float movementDelay = 0, obstacleSpeed = 0;

    private float sidePosition = 0,time=0;

    void Update()
    {
        ObstaclePositionControl();
        MoveObstacle(sidePosition);
    }
    void ObstaclePositionControl()
    {
        if (transform.position.x == -5.5f) 
        {
            sidePosition = 5.5f;
        }
        if (transform.position.x == 5.5f)
        {
            sidePosition = -5.5f;
        }
    }
    void MoveObstacle(float value)
    {
        time += Time.deltaTime;
        if (time > movementDelay)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(value, transform.position.y, transform.position.z), 0.5f * Time.deltaTime * obstacleSpeed);
        }
    }
}
