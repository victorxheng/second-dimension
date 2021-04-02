using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Player : MonoBehaviour {

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI enemiesText;
    public TextMeshProUGUI outputText;
    public TextMeshProUGUI waveActiveText;

    //public TextMeshProUGUI highscoreText;
    //public TextMeshProUGUI prevText;

    public waveFramework wf;
    public activePanel ap;
    public GameObject mp;
    public GameObject BackgroundAudio;

    public GameObject gameOver;

    public Animator menuAnimator;

    private void Awake()
    {
        
        print("setFalse");
        gameOver.SetActive(false);

        PlayerPrefs.SetInt("playerHealth", PlayerPrefs.GetInt("waveHealth", PlayerPrefs.GetInt("maxHealth", 10)));
        waveActiveText.text = "WAVE: " + (PlayerPrefs.GetInt("waveNumber", 0) + 1);

        //PlayerPrefs.SetInt("playerKills", 0);
        //PlayerPrefs.SetInt("playerHealth", 1000);
        PlayerPrefs.SetInt("RevivesUsed", 0);
        PlayerPrefs.SetInt("enemiesActive", 0);
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        if (PlayerPrefs.GetInt("playerHealth", 10) < 1)
        {
            if(PlayerPrefs.GetInt("RevivesUsed", 0) == 0)
            {
                Time.timeScale = 0f;
                outputText.text = "REVIVE (AD)";
                gameOver.SetActive(true);
            }
            else
            {
                RestartGame();
            }
        }
        healthText.text = "HEALTH: " + PlayerPrefs.GetInt("playerHealth", 10);
        cashText.text = "CASH: " + PlayerPrefs.GetInt("cashAmount", 0);
        enemiesText.text = "ENEMIES ACTIVE: " + PlayerPrefs.GetInt("enemies", 0);
        // highscoreText.text = "HIGHSCORE: " + PlayerPrefs.GetInt("playerHighscore", 0);
        // prevText.text = "PREVIOUS SCORE: " + PlayerPrefs.GetInt("previousKills", 0);

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
