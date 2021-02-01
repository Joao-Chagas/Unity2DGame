using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int score;
    public int totalScore;

    public static GameController instance;
    public GameObject pauseObj;
    public GameObject gameOverObj;

    public Text healthText;
    public Text scoreText;

    public bool isPaused;

    // Awake é inicializado antes de todos os métodos start() do seu projeto
    void Awake()
    {
        instance = this;
        totalScore = PlayerPrefs.GetInt("score");

    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
    }

    public void UpdateScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();

        PlayerPrefs.SetInt("score", score);
    }

    public void UpdateLives(int value)
    {
        healthText.text = $"x {value.ToString()}";
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            pauseObj.SetActive(isPaused);
        }
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void GameOver()
    {
        gameOverObj.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
}
