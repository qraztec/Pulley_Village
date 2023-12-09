using UnityEngine;

public class VaseManager : MonoBehaviour
{
    public GameObject congratulationsCanvas;
    public Collider placementArea;
    private int totalVases = 4;
    private int foundVases = 0;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the "Vase" tag
        if (other.CompareTag("Vase"))
        {
            bool isPlaced = other.GetComponent<MeshRenderer>().enabled; // Example: check if the vase is visible

            if (!isPlaced)
            {
                other.GetComponent<MeshRenderer>().enabled = true; // Example: make the vase visible
                foundVases++;

                if (foundVases == totalVases)
                {
                    // All vases found, show congratulations message
                    congratulationsCanvas.SetActive(true);

                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object leaving the trigger has the "Vase" tag
        if (other.CompareTag("Vase"))
        {
            bool isPlaced = other.GetComponent<MeshRenderer>().enabled; // Example: check if the vase is visible

            if (isPlaced)
            {
                other.GetComponent<MeshRenderer>().enabled = false; // Example: make the vase invisible
                foundVases--;

                // You might want to add logic here to handle the case where a vase is removed from the placement area
            }
        }
    }
}
