using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObstacleMovement : MonoBehaviour
{
    private bool moveLeft = false, moveRight = false;
    private float sidePosition = 0;

    [SerializeField]
    private float obstacleSpeed = 0;

    void Start()
    {
        
    }

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
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(value, transform.position.y, transform.position.z), 0.5f*Time.deltaTime*obstacleSpeed);
    }
}
