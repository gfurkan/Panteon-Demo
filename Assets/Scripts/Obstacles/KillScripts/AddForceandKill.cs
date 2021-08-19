using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AddForceandKill : MonoBehaviour
{
    [SerializeField] private float verticalForce = 0, horizontalForce = 0;
    LevelManager levelManager;

    private void Start()
    {
        levelManager = LevelManager.Instance;
    }
    private void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.layer == 8)
        {
            Animator animator = col.gameObject.GetComponent<Animator>();
            Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();
            Collider collider = col.gameObject.GetComponent<Collider>();

            Vector3 hitDistance = (col.gameObject.transform.position - col.contacts[col.contactCount - 1].point) * 10;
            SetForce(hitDistance);

            if (col.gameObject.tag == "Player")
            {
                PlayerMovement playerMovement = col.gameObject.GetComponent<PlayerMovement>();
                playerMovement.enabled = false;
                levelManager.levelFail = true;
            }
            if (col.gameObject.tag == "Opponent")
            {
                NavMeshAgent agent = col.gameObject.GetComponent<NavMeshAgent>();
                agent.speed = 0;
            }
            Kill(animator, rb, collider);
            PushCharacter(SetForce(hitDistance).x, SetForce(hitDistance).y, rb);
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
    void Kill(Animator animator, Rigidbody rb, Collider collider)
    {
        rb.velocity = Vector3.zero;
        rb.useGravity = false;

        collider.isTrigger = true;
        animator.SetBool("Run", false);
        animator.SetBool("Death", true);
    }
    IEnumerator StopCharacter(Animator animator)
    {
        yield return new WaitForSeconds(1);
        animator.applyRootMotion = true;

    }
    void PushCharacter(float horizontalForce, float verticalForce, Rigidbody rb)
    {
        rb.AddForce(new Vector3(horizontalForce, 0, verticalForce));
    }
}
