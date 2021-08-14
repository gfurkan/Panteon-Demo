using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceandKill : MonoBehaviour
{
    [SerializeField]
    private float verticalForce = 0, horizontalForce = 0;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Animator animator = col.gameObject.GetComponent<Animator>();
            PlayerMovement playerMovement = col.gameObject.GetComponent<PlayerMovement>();
            Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();
            Collider collider = col.gameObject.GetComponent<Collider>();

            Vector3 hitDistance = (col.gameObject.transform.position - col.contacts[col.contactCount - 1].point) * 10;

            SetForce(hitDistance);
            Kill(playerMovement, animator, rb, collider);
            HitRotatingStick(SetForce(hitDistance).x, SetForce(hitDistance).y, rb);
            StartCoroutine("StopCharacter", animator);
        }
    }
    Vector2 SetForce(Vector3 hitDist)
    {
        if (hitDist.z < 0)
        {
            verticalForce = -verticalForce;
        }
        if (hitDist.z > 0)
        {
            verticalForce = Mathf.Abs(verticalForce);
        }
        if (hitDist.x < 0)
        {
            horizontalForce = -horizontalForce;
        }
        if (hitDist.x > 0)
        {
            horizontalForce = Mathf.Abs(horizontalForce);
        }
        Vector2 force = new Vector3(horizontalForce,verticalForce);
        return force;
    }
    void Kill(PlayerMovement playerMovement, Animator animator, Rigidbody rb, Collider collider)
    {
        playerMovement.enabled = false;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;

        collider.isTrigger = true;
        animator.SetBool("Death", true);
    }
    IEnumerator StopCharacter(Animator animator)
    {
        yield return new WaitForSeconds(1);
        animator.applyRootMotion = true;

    }
    void HitRotatingStick(float horizontalForce, float verticalForce, Rigidbody rb)
    {
        rb.AddForce(new Vector3(horizontalForce, 0, verticalForce));
    }
}
