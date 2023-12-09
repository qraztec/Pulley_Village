using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public Transform targetLocation;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private GameObject selectedNPC;
    public float stoppingDistance = 0.5f;

    private void Start()
    {
        selectedNPC = gameObject;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void MoveToTarget()
    {
        if (navMeshAgent != null && targetLocation != null)
        {
            if (animator != null)
            {
                animator.Play("running");
            }
            navMeshAgent.SetDestination(targetLocation.position);

            // Play the running animation
            
        }
    }

    public void Update()
    {
        if (navMeshAgent != null && targetLocation != null && animator != null && Vector3.Distance(selectedNPC.transform.position, targetLocation.position) < stoppingDistance)
        {
            animator.Play("idle");
            navMeshAgent.isStopped = true;
        }
            // Stop the running animation
     //       if (animator != null)
     //   {
     //       animator.Play("idle");
     //   }
     //
     //   // Stop the NPC's movement
     //   if (navMeshAgent != null)
     //   {
     //       navMeshAgent.isStopped = true;
     //   }
    }
}
