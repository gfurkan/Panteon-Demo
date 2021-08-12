using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCharacter : MonoBehaviour
{
    [SerializeField]
    private float verticalForce = 0, horizontalForce = 0;
    private bool hitOneTime = false;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Animator animator = col.gameObject.GetComponent<Animator>();
            PlayerMovement playerMovement = col.gameObject.GetComponent<PlayerMovement>();
            Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();

            if (transform.tag == "Obstacle")
            {
                animator.applyRootMotion = true;
                Kill(playerMovement,animator,rb);
            }
            if (transform.tag == "Stick")
            {
                Vector3 hitDistance = (col.gameObject.transform.position - col.contacts[col.contactCount - 1].point) * 10;

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
                HitRotatingStick(verticalForce, horizontalForce,rb);
                Kill(playerMovement, animator, rb);
                StartCoroutine("StopCharacter", animator);
            }
        }
    }
    void Kill(PlayerMovement playerMovement,Animator animator,Rigidbody rb)
    {
        playerMovement.enabled = false;
        rb.useGravity = false;

        animator.SetBool("Death", true);
        //rb.velocity = Vector3.zero;
    }
    IEnumerator StopCharacter(Animator animator)
    {
        yield return new WaitForSeconds(1);
        animator.applyRootMotion = true;

    }
    public void HitRotatingStick(float verticalSpeed, float horizontalSpeed,Rigidbody rb)
    {
        rb.AddForce(new Vector3(horizontalSpeed, 0, verticalSpeed));
    }

}
