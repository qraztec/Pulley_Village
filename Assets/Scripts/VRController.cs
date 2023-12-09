using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRController : MonoBehaviour
{
    public float raycastDistance = 10f;
    public LayerMask npcLayer;

    private void Start()
    {
        //Debug.Log("starting");
    }

    private void Update()
    {
        
        if (Input.GetButtonDown("Fire2"))  // Replace "Fire1" with the input button for your VR controller
        {
          //  Debug.Log("Fired");
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, npcLayer))
            {
                // Check if the hit object is an NPC
                NPCDialogue npcDialogue = hit.collider.GetComponent<NPCDialogue>();
                if (npcDialogue != null)
                {
                    // Trigger NPC dialogue
                    npcDialogue.DisplayNextDialogue();
                }
                else
                {
              //      Debug.Log("Not working");
                }
            }
            else
            {
         //       Debug.Log("Not working");
            }
        }
    }
}
