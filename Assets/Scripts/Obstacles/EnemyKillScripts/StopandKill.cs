using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StopandKill : MonoBehaviour
{

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 8)
        {
            Animator animator = col.gameObject.GetComponent<Animator>();
            Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();
            Collider collider = col.gameObject.GetComponent<Collider>();

            animator.applyRootMotion = true;
            if (col.gameObject.tag == "Player")
            {
                PlayerMovement playerMovement = col.gameObject.GetComponent<PlayerMovement>();
                playerMovement.enabled = false;
            }
            if (col.gameObject.tag == "Opponent")
            {
                NavMeshAgent agent = col.gameObject.GetComponent<NavMeshAgent>();
;
                agent.speed = 0;
            }
            Kill(animator, rb, collider);
        }
    }
    void Kill(Animator animator, Rigidbody rb, Collider collider)
    {
        rb.velocity = Vector3.zero;
        rb.useGravity = false;

        collider.isTrigger = true;
        animator.SetBool("Run", false);
        animator.SetBool("Death", true);
    }
}
