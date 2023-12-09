using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizLoader : MonoBehaviour
{
    public Button vrButton;
    public string scene;
    // Start is called before the first frame update
    void Start()
    {
        vrButton.onClick.AddListener(OnVRButtonClick);
    }

    // Update is called once per frame
    void OnVRButtonClick()
    {
        StartCoroutine(LoadSceneAsync());
       // SceneManager.LoadScene(scene);
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SceneManager.LoadScene(scene);
    }
}
