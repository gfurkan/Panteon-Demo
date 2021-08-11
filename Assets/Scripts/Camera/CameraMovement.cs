using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject playerCharacter;
    private Vector3 distance;

    void Start()
    {
        distance = transform.position- playerCharacter.transform.position;
    }

    void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, playerCharacter.transform.position.z + distance.z);
    }
}
