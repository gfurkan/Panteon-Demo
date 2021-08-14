using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopandKill : MonoBehaviour
{

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Animator animator = col.gameObject.GetComponent<Animator>();
            PlayerMovement playerMovement = col.gameObject.GetComponent<PlayerMovement>();
            Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();
            Collider collider = col.gameObject.GetComponent<Collider>();

            if (transform.tag == "Obstacle")
            {
                animator.applyRootMotion = true;
                Kill(playerMovement, animator, rb, collider);
            }
        }
    }
    void Kill(PlayerMovement playerMovement, Animator animator, Rigidbody rb, Collider collider)
    {
        playerMovement.enabled = false;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        collider.isTrigger = true;
        animator.SetBool("Death", true);
    }
}
