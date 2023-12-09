using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGeneral : MonoBehaviour
{
    public Transform teleportDestination;
    public GameObject player;
    public bool Teleportation;
    // Start is called before the first frame update
    public void Tele()
    {
        Debug.Log("tele went");
        if (Teleportation)
        {
            player.transform.position = teleportDestination.position;
        }
        // Check if the object touched is the player
        
    }
}
