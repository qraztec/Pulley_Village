using System.Collections;
using UnityEngine;
using TMPro;


public class FadeScreenAlt : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 2;
    public Color fadeColor;
    public AnimationCurve fadeCurve;
    public string colorPropertyName = "_Color";
    private Renderer rend;

    private TextMeshProUGUI displayText;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;

        if (fadeOnStart)
            FadeIn();

        // Create TextMeshPro text dynamically
        CreateDynamicText();
    }

    void CreateDynamicText()
    {
        GameObject textObject = new GameObject("DynamicText");
        textObject.transform.SetParent(transform); // Set the text as a child of the fade screen object

        // Add TextMeshProUGUI component
        displayText = textObject.AddComponent<TextMeshProUGUI>();
        displayText.text = "Your dynamic text here";
        displayText.fontSize = 24;
        displayText.color = Color.white;

        // Set the position of the dynamic text
        displayText.rectTransform.localPosition = new Vector3(0, 0, 0.014f);

        // Set the sorting layer to ensure proper rendering order
      //  displayText.sortingLayerName = "Default"; // Change to your desired sorting layer name
       // displayText.sortingOrder = 0; // Adjust the sorting order as needed

        // Make sure the text is facing the camera
        displayText.faceColor = displayText.color;

        // Other settings you can adjust
        displayText.alignment = TextAlignmentOptions.Center;
        displayText.rectTransform.sizeDelta = new Vector2(200, 50); // Adjust width and height as needed
        displayText.enableWordWrapping = true; // Enable word wrapping if necessary
    }

    public void FadeIn()
    {
        Fade(1, 0);
    }

    public void FadeOut()
    {
        Fade(0, 1);
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        rend.enabled = true;

        float timer = 0;
        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, fadeCurve.Evaluate(timer / fadeDuration));

            rend.material.SetColor(colorPropertyName, newColor);

            // Optionally, update text transparency
            if (displayText != null)
            {
                Color textColor = displayText.color;
                textColor.a = newColor.a;
                displayText.color = textColor;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;
        rend.material.SetColor(colorPropertyName, newColor2);

        if (alphaOut == 0)
            rend.enabled = false;
    }
}
