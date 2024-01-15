using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;
using TMPro;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool _gameOver = false;
    int _enemyKilledCounter = 0;
    int highscore;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    void Start()
    {
        GameObject scoreObject = GameObject.FindWithTag("Score");
        scoreText = scoreObject.GetComponent<TextMeshProUGUI>();

        GameObject highscoreObject = GameObject.FindWithTag("Highscore");
        highscoreText = highscoreObject.GetComponent<TextMeshProUGUI>();

        LoadHighscore();
        UpdateScoreText();
    }

    public void LogEnemyKilledCounter()
    {
        _enemyKilledCounter++;
        UpdateScoreText();
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("Start", 0);
        PlayerPrefs.Save();
        SaveHighscore();
        SaveScore();
        _gameOver = true;
        SceneManager.LoadScene(2);
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + _enemyKilledCounter;
    }

    void SaveHighscore()
    {
        if (_enemyKilledCounter > highscore)
        {
            PlayerPrefs.SetInt("Highscore", _enemyKilledCounter);
            PlayerPrefs.Save();
        }
    }

    void SaveScore()
    {
        PlayerPrefs.SetInt("Score", _enemyKilledCounter);
        PlayerPrefs.Save();
    }

    void LoadHighscore()
    {
        highscore = PlayerPrefs.GetInt("Highscore", 0);
        highscoreText.text = "Highscore: " + highscore;
    }
}
