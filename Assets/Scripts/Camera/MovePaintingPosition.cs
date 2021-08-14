using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePaintingPosition : MonoBehaviour
{
    [SerializeField]
    private Transform cameraPosition;
    [SerializeField]
    private GameObject paintingWall;

    private float time = 0, waitingTime = 4;
    private void Update()
    {
        time += Time.deltaTime;
        if (time > waitingTime)
        {
            MoveCamera();
        }
    }
    void MoveCamera()
    {
        paintingWall.SetActive(true);
        transform.position = Vector3.MoveTowards(transform.position, cameraPosition.position,  5* Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, cameraPosition.rotation, 0.9f * Time.deltaTime*5);
    }
}
