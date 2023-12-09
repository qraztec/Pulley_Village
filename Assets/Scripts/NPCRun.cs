using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCRun : MonoBehaviour
{
    public Transform runDestination;
    public LayerMask npcLayerMask;
    public float maxRaycastDistance = 10f;
    public float stoppingDistance = 1f;

    private GameObject selectedNPC;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Fired");
            DetectAndSelectNPC();
        }
        if (selectedNPC != null && Vector3.Distance(selectedNPC.transform.position, runDestination.position) < stoppingDistance)
        {
            StopNPC(selectedNPC);
        }

    }
    void DetectAndSelectNPC()
    {
        Debug.Log("AtSelectNPC");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxRaycastDistance, npcLayerMask))
        {
            // Check if the hit object is an NPC
         //   if (hit.collider.CompareTag("NPCRun"))
          //  {
                Debug.Log("Detected NPC");
                // Select the NPC
                selectedNPC = hit.collider.gameObject;

                // Trigger the NPC to start running towards a specific location
                StartNPCRunning(selectedNPC);
          //  }
        }
    }

    void StartNPCRunning(GameObject npc)
    {
        Debug.Log("At start running");
        // Assuming you have a NavMeshAgent component on your NPC
        NavMeshAgent agent = npc.GetComponentInParent<NavMeshAgent>();
        agent.enabled = true;
        animator = npc.GetComponentInParent<Animator>();

        animator.Play("Running");

        // Assuming you have a NavMeshAgent component on your NPC
        

        // Set the destination for the NPC to run towards
        agent.SetDestination(runDestination.position);
        Debug.Log("Should be running now");
    }

    void StopNPC(GameObject npc)
    {
        NavMeshAgent agent = npc.GetComponentInParent<NavMeshAgent>();
        agent.enabled = true;
        animator = npc.GetComponentInParent<Animator>();
        // Stop the NavMeshAgent
        animator.Play("Idle");

        // Assuming you have a NavMeshAgent component on your NPC
        

        // Stop the NavMeshAgent
        agent.isStopped = true;
    }
}
