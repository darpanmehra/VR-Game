using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class SelectScene : MonoBehaviour
{
    public string sceneName;
    public AudioClip punchAudio;
    public GameObject DiamondBrokenPrefab;

    // Start is called before the first frame update
    void Start()
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

        StartCoroutine(VibrateController(controller, 0.1f, 0.1f, 0.1f));
        AudioSource.PlayClipAtPoint(punchAudio, transform.position);
        HandleSelection();
    }

    private void HandleSelection()
    {
        BreakDiamond(DiamondBrokenPrefab, 2.2f);
    }


    private void BreakDiamond(GameObject diamondBrokenPrefab, float strength)
    {

        GameObject brokenPrefab = Instantiate(diamondBrokenPrefab, transform.position, transform.rotation);


        brokenPrefab.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        // Get the original object's color
        Color originalColor = GetComponent<Renderer>().material.color;

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

        Invoke("ChangeSceneAfterDelay", 3);
        gameObject.SetActive(false);
    }

    private IEnumerator VibrateController(OVRInput.Controller controller, float duration, float frequency, float amplitude)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, controller);
        yield return new WaitForSeconds(duration);
        OVRInput.SetControllerVibration(0, 0, controller);
    }

    private void ChangeSceneAfterDelay()
    {
        SceneManager.LoadScene(sceneName);
    }
}
