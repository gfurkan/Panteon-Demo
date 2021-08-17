using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelEndControl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {

            Animator animator = other.gameObject.GetComponent<Animator>();
            animator.SetBool("Run", false);
            animator.SetBool("Death", false);
            animator.SetBool("Dance", true);
            animator.applyRootMotion = true;
           
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<PlayerRank>().sortRanks = false;
                other.gameObject.GetComponent<PlayerMovement>().enabled = false;
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;

                Camera.main.GetComponent<CameraMovement>().enabled = false;
                Camera.main.GetComponent<MovePaintingPosition>().enabled = true;
            }
            if (other.gameObject.tag == "Opponent")
            {
                PlayerRank player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRank>();
                player.DecreaseListLength(other.gameObject);

                other.gameObject.GetComponent<Collider>().enabled = false;
                other.gameObject.GetComponent<NavMeshAgent>().enabled = false;
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }
}
