using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    InputManager inputManager;
    LevelManager levelManager;

    [SerializeField] private float runningSpeed = 0, swerveSpeed = 0;

    private bool startRunning = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        inputManager = InputManager.Instance;
        levelManager = LevelManager.Instance;
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
         rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 5);
         PlayerControl(inputManager.direction);
        }

        #region Player Fall Control

        GameObject rotatingPlatform = GameObject.FindGameObjectWithTag("RotatingPlatform");
        if (transform.position.y < rotatingPlatform.transform.position.y+1)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
            levelManager.levelFail = true;
        }
        #endregion
    }

    void StartRunning()
    {
        animator.SetBool("Run", true);
        animator.SetBool("Idle", false);
        startRunning = true;
    }
    void PlayerControl(float touchDirection)
    {
        float horizontalPosition = transform.position.x + touchDirection * Time.deltaTime * swerveSpeed;
        //horizontalPosition = Mathf.Clamp(horizontalPosition, -5.5f, 5.5f);
        transform.position = new Vector3(horizontalPosition, transform.position.y, transform.position.z);
    }
}
