using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RunNPCDialogue : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI dialogueText;
    public string[] responses;
    private int currentResponseIndex = 0;

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
             dialogueText.text = "No more responses.";


         }
    }

}
