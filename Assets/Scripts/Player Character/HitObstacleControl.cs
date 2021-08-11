using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObstacleControl : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    PlayerMovement playerMovement;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
            playerMovement = GetComponent<PlayerMovement>();
            playerMovement.enabled = false;

            rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.velocity = Vector3.zero;

            animator = GetComponent<Animator>();
            animator.applyRootMotion = true;
            animator.SetBool("Death", true);
        }
    }
}
