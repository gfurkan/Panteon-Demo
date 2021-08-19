using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePaintingPosition : MonoBehaviour
{
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private GameObject paintingWall;
    [SerializeField] private float waitingTime = 0;
    private float time = 0;

    private void LateUpdate()
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
