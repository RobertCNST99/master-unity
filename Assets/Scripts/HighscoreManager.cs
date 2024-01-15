using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;
using TMPro;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class HighscoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject scoreObject = GameObject.FindWithTag("ScoreDeath");
        TextMeshProUGUI scoreText = scoreObject.GetComponent<TextMeshProUGUI>();

        int score = PlayerPrefs.GetInt("Score", 0);
        scoreText.text = "Score: " + score;

        GameObject highscoreObject = GameObject.FindWithTag("HighscoreDeath");
        TextMeshProUGUI highscoreText = highscoreObject.GetComponent<TextMeshProUGUI>();

        int highscore = PlayerPrefs.GetInt("Highscore", 0);
        highscoreText.text = "Highscore: " + highscore;
    }
}
