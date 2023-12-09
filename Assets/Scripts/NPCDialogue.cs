using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] responses;
    public int currentResponseIndex = 0;
    NPCMovement npcMovement;
    TeleportGeneral telep;

    private void Start()
    {
        // Attempt to get the NPCMovement script in the parent hierarchy
        npcMovement = GetComponentInParent<NPCMovement>();
        telep = GetComponent<TeleportGeneral>();
    }

    public void DisplayNextDialogue()
    {
        Debug.Log("Performing");
        if (currentResponseIndex < responses.Length)
        {
            dialogueText.text = responses[currentResponseIndex];
            currentResponseIndex++;
        }
         else
         {
             if (npcMovement != null)
        {
            npcMovement.MoveToTarget();
        }
             if (telep!= null)
            {
                telep.Tele();
            }
        }
    }
   // public void StopNPCMovement()
   // {
   //     if (npcMovement != null)
   //     {
   //         npcMovement.StopMoving();
   //     }
   // }
}
