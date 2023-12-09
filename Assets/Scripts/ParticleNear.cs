using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleNear : MonoBehaviour
{
    public ParticleSystem particleSystem;
   

    private void Start()
    {
        // Assuming ParticleSystem and BoxCollider are attached to the same GameObject
        

        // Disable particle system initially
        particleSystem.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is the VR user
        if (other.CompareTag("Player"))
        {
            // Enable particle system
            particleSystem.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider is the VR user
        if (other.CompareTag("Player"))
        {
            // Disable particle system
            particleSystem.Stop();
        }
    }
}
