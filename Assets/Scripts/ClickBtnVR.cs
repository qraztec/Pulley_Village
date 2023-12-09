/*
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRButtonPress : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent OnButtonPress;  // Event to trigger when the button is pressed

    private XRBaseInteractor hoverInteractor;
    private bool isHovering = false;

    // Invoked when an XR Interactor starts hovering over the interactable object
    private void OnHoverEnter(XRBaseInteractor interactor)
    {
        hoverInteractor = interactor;
        isHovering = true;
    }

    // Invoked when an XR Interactor stops hovering over the interactable object
    private void OnHoverExit(XRBaseInteractor interactor)
    {
        hoverInteractor = null;
        isHovering = false;
    }

    private void Start()
    {
        var interactable = GetComponent<XRBaseInteractable>();

        // Subscribe to hover events
        interactable.onHoverEntered.AddListener(OnHoverEnter);
        interactable.onHoverExited.AddListener(OnHoverExit);
    }

    private void Update()
    {
        // Check for a select press on the hovering interactor
        if (isHovering && hoverInteractor != null)
        {
            // Updated condition to check for select action being pressed
            if (hoverInteractor.selectAction.triggered)
            {
                OnButtonPress?.Invoke(); // Trigger button press event
            }
        }
    }
}
*/