using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpponentMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform endPosition;
    [SerializeField] private float agentSpeed = 0;

    private bool start = true;
    InputManager inputManager;

    void Start()
    {
        inputManager = InputManager.Instance;

    }
    void Update()
    {
        if (inputManager.touched)
        {
            if (start)
            {
                agent.destination = new Vector3(transform.position.x, transform.position.y, endPosition.position.z);
                Animator animator = GetComponent<Animator>();
                animator.SetBool("Run", true);
                agent.speed = agentSpeed;
                start = false;
            }
        }
    }
}
