using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCSceneLoader : MonoBehaviour
{
    public NPCDialogue npc;
    public string SceneToLoad;
    public float delayInSeconds = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (npc.currentResponseIndex == npc.responses.Length - 1)
        {
            StartCoroutine(LoadSceneAsync());
            
        }
    }

    IEnumerator LoadSceneAsync()
    {
        // Wait for the specified delay


        // Your scene loading logic here
        //   if (!string.IsNullOrEmpty(SceneToLoad))
        //   {
        //       yield return new WaitForSeconds(10);
        //       SceneManager.LoadScene(SceneToLoad);
        //   }
        //   else
        //   {
        //       Debug.LogError("Scene name/path is not set in YourOtherScript.");
        //   }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneToLoad);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SceneManager.LoadScene(SceneToLoad);
    }
}
