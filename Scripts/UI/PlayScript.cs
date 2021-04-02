using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayScript : MonoBehaviour { 

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ResetWaves()
    {
        PlayerPrefs.SetInt("waveNumber", 200);
        PlayerPrefs.SetInt("Tier1", 0);
        PlayerPrefs.SetInt("Tier2", 0);
        PlayerPrefs.SetInt("Tier3", 0);
        PlayerPrefs.SetInt("Tier4", 0);
        PlayerPrefs.SetInt("Tier5", 0);

        PlayerPrefs.SetInt("maxHealth", 100);
        PlayerPrefs.SetInt("fireRate", 13);
        PlayerPrefs.SetInt("bulletSpeed", 50);
        PlayerPrefs.SetInt("moveSpeed", 20);
        PlayerPrefs.SetInt("cashDrop", 5); 

       PlayerPrefs.SetInt("cashAmount",10000);
        PlayerPrefs.SetInt("removeAds", 0);
    }
}
