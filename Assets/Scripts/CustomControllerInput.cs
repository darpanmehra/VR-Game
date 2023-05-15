using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CustomControllerInput : MonoBehaviour
{
    public OVRInput.Controller controller;
    public OVRInput.Button button;

    public UnityEvent onButtonPress;
    public UnityEvent onButtonRelease;

    private OVRGrabbable _ovrGrabbable;
    void Start()
    {
        _ovrGrabbable = GetComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(button, controller))
        {
            onButtonPress.Invoke();
        }

        if (OVRInput.GetUp(button, controller))
        {
            onButtonRelease.Invoke();
        }

        //Debug.Log(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch).magnitude);
    }
}
