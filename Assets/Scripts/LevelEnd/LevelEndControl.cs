using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndControl : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            Animator animator = other.gameObject.GetComponent<Animator>();
            animator.SetBool("Run", false);
            animator.SetBool("Death", false);
            animator.SetBool("Dance", true);
            animator.applyRootMotion = true;

            other.gameObject.GetComponent<PlayerMovement>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

            Camera.main.GetComponent<CameraMovement>().enabled = false;
            Camera.main.GetComponent<MovePaintingPosition>().enabled = true;
        }
    }
}
