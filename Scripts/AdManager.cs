using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour {
    public activePanel ap;
    public TextMeshProUGUI outputText;
    public GameObject gameOver;
    public TextMeshProUGUI cashText;
    public GameObject moreCash;
    public GameObject playButton;

#if UNITY_ANDROID
    string gameID = "2980653";
#elif UNITY_IOS
        string gameID = "2980652";
#endif

    bool testMode = false;
    void Awake()
    {
        cashText.text = "(ADVERTISEMENT)";
        moreCash.SetActive(false);
        Advertisement.Initialize(gameID, testMode);
    }
    

    public void ShowAd(string zone = "")
    {
        if (string.Equals(zone, ""))
            zone = null;

        ShowOptions options = new ShowOptions();
        options.resultCallback = AdCallbackhandler;

        if (Advertisement.IsReady(zone))
        {
            ap.onAd();
            Advertisement.Show(zone, options);            
        }
    }


    void AdCallbackhandler(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                ap.offAd();
                break;
            case ShowResult.Skipped:
                ap.offAd();
                break;
            case ShowResult.Failed:
                ap.offAd();
                break;
        }
    }

    public void ShowAdelse(string zone = "")
    {
        if (PlayerPrefs.GetInt("removeAds", 0) == 0)
        {
            if (string.Equals(zone, ""))
                zone = null;

            if (Advertisement.IsReady(zone))
            {
                Advertisement.Show(zone);
            }
        }
    }     




    public void ShowReward(string zone = "")
    {
        outputText.text = "LOADING...";
        if (string.Equals(zone, ""))
            zone = null;

        ShowOptions options2 = new ShowOptions();
        options2.resultCallback = AdCallbackhandler2;

        if (Advertisement.IsReady(zone))
        {
            Advertisement.Show(zone, options2);
            outputText.text = "SHOWING...";
        }
        else
        {
            outputText.text = "FAILED TO LOAD (TRY AGAIN)";
        }
    }
    void AdCallbackhandler2(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                outputText.text = "AD COMPLETE";
                PlayerPrefs.SetInt("RevivesUsed", 1);
                PlayerPrefs.SetInt("playerHealth", PlayerPrefs.GetInt("maxHealth", 10));
                gameOver.SetActive(false);
                ap.openWindow();
                break;
            case ShowResult.Skipped:
                outputText.text = "AD SKIPPED (NO REVIVE)";
                break;
            case ShowResult.Failed:
                outputText.text = "AD FAILED (NO REVIVE)";
                break;
        }
    }
    public void openReward()
    {
        cashText.text = "(ADVERTISEMENT)";
    }

    public void ShowNextReward(string zone = "")
    {
        cashText.text = "LOADING...";
        if (string.Equals(zone, ""))
            zone = null;

        ShowOptions options2 = new ShowOptions();
        options2.resultCallback = AdCallbackhandler3;

        if (Advertisement.IsReady(zone))
        {
            Advertisement.Show(zone, options2);
            cashText.text = "SHOWING...";
        }
        else
        {
            cashText.text = "FAILED TO LOAD (TRY AGAIN)";
        }
    }
    public TextMeshProUGUI shopCashText;
    public void AdCallbackhandler3(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                cashText.text = "AD COMPLETE";
                print("adComplete");
                PlayerPrefs.SetInt("cashAmount", PlayerPrefs.GetInt("cashAmount", 0)+ ((PlayerPrefs.GetInt("waveNumber", 0) * 65) + 500));
                shopCashText.text = "$" + PlayerPrefs.GetInt("cashAmount", 0);
                moreCash.SetActive(false);
                break;
            case ShowResult.Skipped:
                cashText.text = "AD SKIPPED (NO REVIVE)";
                break;
            case ShowResult.Failed:
                cashText.text = "AD FAILED (NO REVIVE)";
                break;
        }
    }





    //timesink



}
