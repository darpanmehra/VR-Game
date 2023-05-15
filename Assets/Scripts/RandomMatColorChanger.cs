using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMatColorChanger : MonoBehaviour
{
    MeshRenderer _renderer;

    // Update is called once per frame
    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
    }
    public void ChangeMaterialColor()
    {
        _renderer.material.color = ProduceRandomColor();
    }

    Color ProduceRandomColor()
    {
        var r = Random.Range(0.0f, 1.0f);
        var b = Random.Range(0.0f, 1.0f);
        var g = Random.Range(0.0f, 1.0f);

        return new Color(r, g, b);
    }
}
