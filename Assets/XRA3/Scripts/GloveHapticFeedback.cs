using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloveHapticFeedback : MonoBehaviour
{
    public OVRInput.Controller controller;
    public AudioClip hapticSound;
    

    private void OnTriggerEnter(Collider other)
    {
        OVRInput.Controller otherController = OVRInput.Controller.None;

        if (other.CompareTag("RightController"))
        {
            otherController = OVRInput.Controller.RTouch;
        }

        if (otherController != OVRInput.Controller.None)
        {
            AudioSource.PlayClipAtPoint(hapticSound, transform.position);
            StartCoroutine(VibrateController(otherController, 0.1f, 0.1f, 0.2f));
            StartCoroutine(VibrateController(controller, 0.1f, 0.1f, 0.2f));
        }

    }

    private IEnumerator VibrateController(OVRInput.Controller controller, float duration, float frequency, float amplitude)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, controller);
        yield return new WaitForSeconds(duration);
        OVRInput.SetControllerVibration(0, 0, controller);
    }
}
