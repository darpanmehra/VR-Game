    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorCollisionController : MonoBehaviour
{

    public AudioClip incorrectPunchClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        AudioSource.PlayClipAtPoint(incorrectPunchClip, transform.position);

        OVRInput.Controller controller = OVRInput.Controller.None;

        if (other.CompareTag("LeftController"))
        {
            controller = OVRInput.Controller.LTouch;
        }
        else if (other.CompareTag("RightController"))
        {
            controller = OVRInput.Controller.RTouch;
        }

        if (controller != OVRInput.Controller.None)
        {
            HandleCollision(controller);
        }

    }

    private void HandleCollision(OVRInput.Controller controller){
        bool isGameOver = LevelManager.Instance.isGameOver();
        if (!isGameOver)
        {
            LevelManager.Instance.SubtractScoreDoorCollision();
            StartCoroutine(VibrateController(controller, 0.1f, 0.1f, 1.2f));
            AudioSource.PlayClipAtPoint(incorrectPunchClip, transform.position);
        }
    }

    private IEnumerator VibrateController(OVRInput.Controller controller, float duration, float frequency, float amplitude)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, controller);
        yield return new WaitForSeconds(duration);
        OVRInput.SetControllerVibration(0, 0, controller);
    }

}
