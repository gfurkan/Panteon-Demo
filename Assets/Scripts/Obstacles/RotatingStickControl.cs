using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingStickControl : MonoBehaviour
{
    [SerializeField]
    private double rotatingSpeed = 0;

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, (float)rotatingSpeed, 0));
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Vector3 hitDistance = col.contacts[0].point - transform.position;
            Debug.Log(hitDistance);
            col.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(hitDistance.x, 0, hitDistance.z));
        }
    }
}
