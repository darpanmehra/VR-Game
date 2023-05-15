using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ChangeDifficulty : MonoBehaviour
{
    public OVRInput.Controller controller;
    public TextMeshPro textField;
    public enum Difficulty { Easy, Medium, Hard }
    public Difficulty currentDifficulty;

    // Start is called before the first frame update
    private void Start()
    {
        currentDifficulty = Difficulty.Medium;
        textField.text = "Difficulty: " + currentDifficulty;
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One, controller))
        {
            SwitchDifficulty();
        }
    }

    private void SwitchDifficulty()
    {
        switch (currentDifficulty)
        {
            case Difficulty.Easy:
                currentDifficulty = Difficulty.Medium;
                break;
            case Difficulty.Medium:
                currentDifficulty = Difficulty.Hard;
                break;
            case Difficulty.Hard:
                currentDifficulty = Difficulty.Easy;
                break;
        }
        PlayerPrefs.SetInt("SelectedDifficulty", (int)currentDifficulty);
        textField.text = "Difficulty: " + currentDifficulty;
    }

}
