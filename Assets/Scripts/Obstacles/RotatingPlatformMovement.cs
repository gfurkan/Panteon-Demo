using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatformMovement : MonoBehaviour
{
    [SerializeField] private float rotatingSpeed = 0, velocityToAdd = 0;

    void Update()
    {
        RotatePlatform();
    }
    void RotatePlatform()
    {
        transform.Rotate(0, 0,-rotatingSpeed*Time.deltaTime);
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 8)
        {
            Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(velocityToAdd, rb.velocity.y,rb.velocity.z);
        }
    }
}
