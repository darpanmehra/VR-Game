using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerInput : MonoBehaviour
{
    public OVRInput.Controller controller;
    public OVRInput.Button button;

    public UnityEvent customEvent;

    private OVRGrabbable _ovrGrabbable;

    void Start()
    {
        _ovrGrabbable = GetComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        // OVRInput.Update();

        // input recognized from either controller
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            // Do something
        }

        // Right trigger
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            // Do something
        }

        // controller and button specified through the variables
        if (_ovrGrabbable.isGrabbed && OVRInput.GetDown(button, controller))
        {
            // Do something
            TriggerVibration(0.01f, 0.01f, controller);
            customEvent.Invoke();
        }

        // returns a float of the trigger's current state on the specified controller
        float triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller);

        // returns a Vector2 of the controller's thumbstick’s current state.
        // (X/Y range of -1.0f to 1.0f)
        Vector2 joystickValue = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controller);
        Debug.Log(joystickValue);

        // returns true if the primary thumbstick has been moved upwards more than halfway.
        // (Up/Down/Left/Right - Interpret the thumbstick as a D-pad).
        bool isThumbstickUp = OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp);
    }

    void TriggerVibration(float frequency, float amplitude, OVRInput.Controller controller)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, controller);
    }
}
