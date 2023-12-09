using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public class AccelerationCalculator : MonoBehaviour
{
    public Rigidbody object1Rb;
    public Rigidbody object2Rb;
    public Rigidbody object3Rb; // Add more objects as needed
    public TextMeshProUGUI accelerationText;
    public float angle;

    public enum AccelerationType
    {
        AbsoluteDifference,
        M1Only,
        PulleyWithAngle,
        M1SinTheta,
        AbsoluteDifferenceThreeObjects,
        CustomScenario // Add more types as needed
    }

    public AccelerationType accelerationType = AccelerationType.AbsoluteDifference;

    private const float gravity = 10f; // Gravity value (adjust as needed)

    private Dictionary<AccelerationType, Func<Rigidbody, Rigidbody, Rigidbody, float, float>> accelerationFunctions;

    void Update()
    {
        if (object1Rb != null && object2Rb != null)
        {
            float acceleration;

            // Check if the selected acceleration type is in the dictionary
            if (accelerationFunctions.TryGetValue(accelerationType, out var accelerationFunction))
            {
                // Call the appropriate acceleration calculation function
                acceleration = accelerationFunction(object1Rb, object2Rb, object3Rb, angle);
            }
            else
            {
                acceleration = 0f; // Default to zero if acceleration type is not found
            }

            accelerationText.text = $"Acceleration: {acceleration:F2} m/s^2";
        }
       // else
       // {
       //     accelerationText.text = "Assign Rigidbody components in the inspector!";
       // }
    }

    private void Awake()
    {
        InitializeAccelerationFunctions();
    }

    private void InitializeAccelerationFunctions()
    {
        accelerationFunctions = new Dictionary<AccelerationType, Func<Rigidbody, Rigidbody, Rigidbody, float, float>>
        {
            { AccelerationType.AbsoluteDifference, AbsoluteDifferenceAcceleration },
            { AccelerationType.M1Only, M1OnlyAcceleration },
            { AccelerationType.PulleyWithAngle, PulleyWithAngleAcceleration },
            { AccelerationType.M1SinTheta, M1SinThetaAcceleration },
            { AccelerationType.AbsoluteDifferenceThreeObjects, AbsoluteDifferenceThreeObjectsAcceleration },
            { AccelerationType.CustomScenario, CustomScenarioAcceleration }
            // Add more types and functions as needed
        };
    }

    // Define different acceleration calculation functions

    // Absolute difference acceleration formula
    private float AbsoluteDifferenceAcceleration(Rigidbody rb1, Rigidbody rb2, Rigidbody rb3, float angle)
    {
        return (rb1.mass - rb2.mass) * gravity / (rb1.mass + rb2.mass);
    }

    // M1 only acceleration formula
    private float M1OnlyAcceleration(Rigidbody rb1, Rigidbody rb2, Rigidbody rb3, float angle)
    {
        return rb1.mass * gravity / (rb1.mass + rb2.mass);
    }

    // Pulley with angle acceleration formula
    private float PulleyWithAngleAcceleration(Rigidbody rb1, Rigidbody rb2, Rigidbody rb3, float angle)
    {
        return (rb1.mass * Mathf.Sin(Mathf.Deg2Rad * angle) - rb2.mass) * gravity / (rb1.mass + rb2.mass);
    }

    // M1 * Sin(theta) acceleration formula
    private float M1SinThetaAcceleration(Rigidbody rb1, Rigidbody rb2, Rigidbody rb3, float angle)
    {
        return -rb1.mass * Mathf.Sin(Mathf.Deg2Rad * angle) * gravity / (rb1.mass + rb2.mass);
    }

    // Absolute difference acceleration with three objects formula
    private float AbsoluteDifferenceThreeObjectsAcceleration(Rigidbody rb1, Rigidbody rb2, Rigidbody rb3, float angle)
    {
        return (rb1.mass - rb3.mass) * gravity / (rb1.mass + rb2.mass + rb3.mass);
    }

    // Custom scenario acceleration formula
    private float CustomScenarioAcceleration(Rigidbody rb1, Rigidbody rb2, Rigidbody rb3, float angle)
    {
        return (2 * rb2.mass * gravity / rb1.mass) - gravity;
    }
}
