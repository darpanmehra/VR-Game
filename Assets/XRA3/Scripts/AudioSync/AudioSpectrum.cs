using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach this to the gameobject holding the audiosource

[RequireComponent (typeof (AudioSource))]
public class AudioSpectrum : MonoBehaviour
{
    public static float[] samples = new float[128];
    public static float spectrumValue;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
    }

    void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Hamming);

        if(samples != null && samples.Length > 0)
        {
            spectrumValue = samples[0] * 100;
        }
    }

}
