using UnityEngine;
using UnityEngine.SceneManagement;

public class Vasedetect : MonoBehaviour
{
    int totalvases = 0;
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has a specific tag (e.g., "Enemy")
        if (collision.gameObject.CompareTag("Vase"))
        {
            // Destroy the object this script is attached to
            Destroy(collision.gameObject);

            totalvases++;
           
        }

        if(totalvases >= 4){
            Invoke("LoadCredits", 2.0f);
        }
       
    }

    private void LoadCredits()
    {
        SceneManager.LoadScene("Endgame");
    }
}
