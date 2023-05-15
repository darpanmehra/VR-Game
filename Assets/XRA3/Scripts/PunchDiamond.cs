using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchDiamond : MonoBehaviour
{
    private float minStrength = 0.5f;
    private string colorTag;
    public AudioClip correctPunchClip;
    public AudioClip incorrectPunchClip;

    public GameObject smallDiamondBrokenPrefab;
    public GameObject mediumDiamondBrokenPrefab;
    public GameObject bigDiamondBrokenPrefab;

    void Start()
    {
        colorTag = gameObject.tag;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
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
            float strength = OVRInput.GetLocalControllerVelocity(controller).magnitude;
            HandlePunch(controller, strength);
        }

    }

    private void HandlePunch(OVRInput.Controller controller, float strength)
    {
        if (
            (colorTag == "TealDiamond" && controller == OVRInput.Controller.RTouch) ||
            (colorTag == "CoralDiamond" && controller == OVRInput.Controller.LTouch)
        )
        {
            CorrectPunchHelper(controller, strength);
        }
        else
        {
            IncorrectPunchHelper(controller, strength);
        }
    }


    private void CorrectPunchHelper(OVRInput.Controller controller, float strength)
    {
        bool isGameOver = LevelManager.Instance.isGameOver();
        if (!isGameOver)
        {   
            //Stop From moving ahead
            GetComponent<ObjectMovement>().setKeepMoving(false);

            if (strength >= minStrength){
                
                LevelManager.Instance.AddScore(strength);
                StartCoroutine(VibrateController(controller, 0.1f, 0.1f, 0.2f));
                AudioSource.PlayClipAtPoint(correctPunchClip, transform.position);

                if (strength <= 1)
                    BreakDiamond(smallDiamondBrokenPrefab, strength);
                else if (strength <= 2)
                    BreakDiamond(mediumDiamondBrokenPrefab, strength);
                else
                    BreakDiamond(bigDiamondBrokenPrefab, strength);
            }
            

        }
    }

    private void BreakDiamond(GameObject diamondBrokenPrefab, float strength)
    {

        GameObject brokenPrefab = Instantiate(diamondBrokenPrefab, transform.position, transform.rotation);

        brokenPrefab.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        // Get the original object's color
        Color originalColor = GetComponent<Renderer>().material.color;

        //Hide original diamond
        gameObject.SetActive(false);

        // Set the brokenPrefab's colors to match the original object's color
        Renderer[] brokenRenderers = brokenPrefab.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in brokenRenderers)
        {
            renderer.material.color = originalColor;
        }

        //Add Explosion effect
        foreach (Transform child in brokenPrefab.transform)
        {
            Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
            Vector3 explosionDirection = (child.position - transform.position).normalized;
            float explosionForce = strength;
            rb.AddForce(explosionDirection * explosionForce, ForceMode.Impulse);
        }
        
    }

    private void IncorrectPunchHelper(OVRInput.Controller controller, float strength)
    {
        bool isGameOver = LevelManager.Instance.isGameOver();
        if (!isGameOver)
        {
            //Stop From moving ahead
            GetComponent<ObjectMovement>().setKeepMoving(false);
            
            LevelManager.Instance.SubtractScore();
            StartCoroutine(VibrateController(controller, 0.1f, 0.1f, 0.4f));
            AudioSource.PlayClipAtPoint(incorrectPunchClip, transform.position);

            Rigidbody diamondRigidbody = GetComponent<Rigidbody>();
            diamondRigidbody.AddForce(Vector3.forward * 8, ForceMode.Impulse);
            diamondRigidbody.AddForce(Vector3.up * 4, ForceMode.Impulse);

            // Destroy the diamond after a short delay
            Destroy(gameObject, 2f);
        }

    }

    private IEnumerator VibrateController(OVRInput.Controller controller, float duration, float frequency, float amplitude)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, controller);
        yield return new WaitForSeconds(duration);
        OVRInput.SetControllerVibration(0, 0, controller);
    }
}
