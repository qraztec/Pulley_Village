using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slider2Control : MonoBehaviour
{
    public Slider massSlider;
    public Rigidbody targetObject;
    public TextMeshProUGUI valueText;
    private Transform pulleyTransform;
    // Start is called before the first frame update
    void Start()
    {
        massSlider = GetComponentInChildren<Slider>();
        valueText = GetComponentInChildren<TextMeshProUGUI>();
        pulleyTransform = transform.Find("Pulley");
        targetObject = pulleyTransform.Find("Right")?.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        float sliderValue = massSlider.value;

        // Update the object's mass
        //  targetObject.mass = sliderValue;

        // Display the value on the screen
        // valueText.text = "Mass: " + sliderValue.ToString("F2");
        int roundedValue = Mathf.RoundToInt(sliderValue);

        // Clamp the rounded value within the range of 1 to 10
        int clampedValue = Mathf.Clamp(roundedValue, 1, 30);

        // Update the object's mass
        targetObject.mass = clampedValue;

        // Display the value on the screen
        valueText.text = "Mass: " + clampedValue.ToString();
    }
}