using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverObject : MonoBehaviour
{
    public float hoverSpeed = 1f;
    public float oscillationSpeed = 2f;
    public float oscillationAmplitude = 0.02f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newYPosition = Mathf.Sin(Time.time * oscillationSpeed) * oscillationAmplitude;
        Vector3 newPosition = new Vector3(transform.position.x, startPosition.y + newYPosition, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, hoverSpeed * Time.deltaTime);
    }
}
