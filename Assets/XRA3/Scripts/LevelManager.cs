using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LevelManager : MonoBehaviour
{
    public enum Difficulty { Easy, Medium, Hard }
    public Difficulty currentDifficulty;


    public static LevelManager Instance;
    public float speed = 10f;
    private int score = 0;
    public int diamondValue = 50;
    public TextMeshPro scoreText;
    public TextMeshPro difficultyLevelText;
    private AudioSource audioSource;
    public GameObject endOfLevelMenu;
    public TextMeshPro endOfLevelMessage;

    public AudioClip gameWinAudio;
    public AudioClip gameLoseAudio;

    private bool gameOver = false;

    private void Start(){
        //Difficulty Setter
        SetDifficultyFromPlayerPrefs();
        SetSpeedBasedOnDifficulty();
        difficultyLevelText.text = "" + currentDifficulty;

        audioSource = gameObject.GetComponent<AudioSource>();
        UpdateScoreText();
    }

    private void Update()
    {
        if (!gameOver && !audioSource.isPlaying)
        {
            Win();
        }
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(float strength)
    {
        if (!gameOver)
        {
            int addedValue = (int)(diamondValue + (strength * 10));
            score += addedValue;
            UpdateScoreText();
        }
    }

    public void SubtractScore()
    {
        if (!gameOver)
        {   
            subtractScorePerDifficulty();
            UpdateScoreText();
            if (score < 0)
            {
                Lose();
            }
        }
    }

    public void SubtractScoreDoorCollision()
    {
        if (!gameOver)
        {
            subtractScorePerDifficultyForBlocks();
            UpdateScoreText();
            if (score < 0)
            {
                Lose();
            }
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    private void Win()
    {
        GetComponent<AudioSource>().Stop();
        

        gameOver = true;
        endOfLevelMessage.text = "LEVEL PASSED!";
        endOfLevelMenu.SetActive(true);

        AudioSource.PlayClipAtPoint(gameWinAudio, transform.position);
    }

    private void Lose()
    {
        GetComponent<AudioSource>().Stop();

        gameOver = true;
        endOfLevelMessage.text = "LEVEL FAILED!";
        endOfLevelMenu.SetActive(true);

        AudioSource.PlayClipAtPoint(gameLoseAudio, transform.position);
    }

    public void setSpeed(float newSpeed){
        speed = newSpeed;
    }


    public float getSpeed(){
        return speed;
    }

    internal bool isGameOver()
    {
        return gameOver;
    }

    private void SetDifficultyFromPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("SelectedDifficulty"))
        {
            currentDifficulty = (Difficulty)PlayerPrefs.GetInt("SelectedDifficulty");
        }
        else
        {
            currentDifficulty = Difficulty.Medium;
        }
    }

    private void SetSpeedBasedOnDifficulty()
    {
        switch (currentDifficulty)
        {
            case Difficulty.Easy:
                speed = 6;
                break;
            case Difficulty.Medium:
                speed = 10;
                break;
            case Difficulty.Hard:
                speed = 15;
                break;
        }
    }

    void subtractScorePerDifficulty(){
        switch (currentDifficulty)
        {
            case Difficulty.Easy:
                break;
            case Difficulty.Medium:
                score -= (diamondValue/2);
                break;
            case Difficulty.Hard:
                score -= diamondValue;
                break;
        }
    }

    void subtractScorePerDifficultyForBlocks(){
        switch (currentDifficulty)
        {
            case Difficulty.Easy:
                score -= 50;
                break;
            case Difficulty.Medium:
                score -= 100;
                break;
            case Difficulty.Hard:
                score -= 150;
                break;
        }
    }
}
