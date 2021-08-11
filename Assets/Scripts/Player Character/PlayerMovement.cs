using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    InputManager inputManager;

    [SerializeField]
    private double runningSpeed = 0,swerveSpeed=0;

    private bool startRunning = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        inputManager = InputManager.Instance;
    }

    void Update()
    {
        if (inputManager.touched)
        {
            if (!startRunning)
            {
                StartRunning();
            }
        }
        if (startRunning)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 1 * (float)runningSpeed) ;
            PlayerControl(inputManager.direction);
        }
    }
    void StartRunning()
    {
        animator.SetBool("Run", true);
        animator.SetBool("Idle", false);
        startRunning = true;
    }
    void PlayerControl(float touchDirection)
    {
        transform.position = new Vector3(transform.position.x + touchDirection * Time.deltaTime*(float)swerveSpeed, transform.position.y, transform.position.z);
    }
}
