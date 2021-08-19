using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RespawnOpponent : MonoBehaviour
{
    NavMeshAgent agent;
    Rigidbody rb;
    private Vector3 startPosition;
    private float agentSpeed = 4;

    private void Start()
    {
        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }
    public void RespawnCharacter()
    {
        transform.position = startPosition;

        rb.useGravity = true;
        Animator animator = GetComponent<Animator>();
        animator.applyRootMotion = false;

        animator.SetBool("Idle", true);
        animator.SetBool("Death", false);
        animator.SetBool("Run", true);

        Collider col = GetComponent<Collider>();
        col.isTrigger = false;

        agent.speed = agentSpeed;

    }
}
