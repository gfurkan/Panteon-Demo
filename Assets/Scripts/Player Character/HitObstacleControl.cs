using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObstacleControl : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private float verticalForce = 0,horizontalForce=0;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
            playerMovement.enabled = false;

            rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.velocity = Vector3.zero;

            animator = GetComponent<Animator>();
            animator.applyRootMotion = true;
            animator.SetBool("Death", true);
         }

        if (col.gameObject.tag == "RotatingStick")
        {
            Vector3 hitDistance = (transform.position - col.contacts[col.contactCount-1].point)*10;

            if (hitDistance.z < 0)
            {
                verticalForce = -verticalForce;
            }
            if (hitDistance.z > 0)
            {
                verticalForce = Mathf.Abs(verticalForce);
            }
            if (hitDistance.x < 0)
            {
                horizontalForce = -horizontalForce;
            }
            if (hitDistance.x > 0)
            {
                horizontalForce = Mathf.Abs(horizontalForce);
            }

            playerMovement.HitRotatingStick(verticalForce, horizontalForce);
        }
    }
}
