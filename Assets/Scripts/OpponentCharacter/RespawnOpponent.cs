using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RespawnOpponent : MonoBehaviour
{
    NavMeshAgent agent;
    Vector3 startPosition;
    float agentSpeed = 0;

    private void Start()
    {
        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        agentSpeed = agent.speed;
    }
    public void RespawnCharacter()
    {
        transform.position = startPosition;

        Animator animator = GetComponent<Animator>();
        animator.applyRootMotion = false;

        animator.SetBool("Idle", true);
        animator.SetBool("Death", false);
        animator.SetBool("Run", true);
        //animator.SetBool("Idle", false);

        Collider col = GetComponent<Collider>();
        col.isTrigger = false;


        agent.speed = agentSpeed;

    }
}
