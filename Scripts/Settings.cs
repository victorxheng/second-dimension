using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Settings : MonoBehaviour {
    public TextMeshProUGUI musicText;
    public TextMeshProUGUI soundEffectsText;
    public TextMeshProUGUI effectsText;
    
    public void onStart()
    {

        if(PlayerPrefs.GetInt("music", 1) == 1)
        {
            musicText.text = "MUSIC: OFF";
        }
        else if(PlayerPrefs.GetInt("music", 1) == 2)
        {
            musicText.text = "MUSIC: LOW";
        }
        else if (PlayerPrefs.GetInt("music", 1) == 3)
        {
            musicText.text = "MUSIC: NORMAL";
        }
        else
        {
            musicText.text = "MUSIC: HIGH";
        }


        if (PlayerPrefs.GetInt("soundEffects", 1) == 1)
        {
            soundEffectsText.text = "SOUND EFFECTS: ON";
        }
        else
        {
            soundEffectsText.text = "SOUND EFFECTS: OFF";
        }


        if (PlayerPrefs.GetInt("effects", 1) == 1)
        {
            effectsText.text = "GRAPHIC EFFECTS: ON";
        }
        else
        {
            effectsText.text = "GRAPHIC EFFECTS: OFF";
        }     

    }

    public void onMusic()
    {
        if (PlayerPrefs.GetInt("music", 1) == 1)
        {
            PlayerPrefs.SetInt("music", 2);
            musicText.text = "MUSIC: LOW";
        }
        else if (PlayerPrefs.GetInt("music", 1) == 2)
        {
            PlayerPrefs.SetInt("music", 3);
            musicText.text = "MUSIC: NORMAL";

        }
        else if (PlayerPrefs.GetInt("music", 1) == 3)
        {
            PlayerPrefs.SetInt("music", 4);
            musicText.text = "MUSIC: HIGH";
        }
        else
        {
            PlayerPrefs.SetInt("music", 1);
            musicText.text = "MUSIC: OFF";
        }
    }

    public void onSound()
    {
        if (PlayerPrefs.GetInt("soundEffects", 1) == 1)
        {
            PlayerPrefs.SetInt("soundEffects", 0);
            soundEffectsText.text = "SOUND EFFECTS: OFF";
        }
        else
        {
            PlayerPrefs.SetInt("soundEffects", 1);
            soundEffectsText.text = "SOUND EFFECTS: ON";
        }
    }
    public void onEffects()
    {
        if (PlayerPrefs.GetInt("effects", 1) == 1)
        {
            PlayerPrefs.SetInt("effects", 0);
            effectsText.text = "GRAPHIC EFFECTS: OFF";
        }
        else
        {
            PlayerPrefs.SetInt("effects", 1);
            effectsText.text = "GRAPHIC EFFECTS: ON";
        }
    }



}
