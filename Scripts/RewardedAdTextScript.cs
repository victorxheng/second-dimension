using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardedAdTextScript : MonoBehaviour {
    public TextMeshProUGUI cashText;
     public void onClick()
    {
        cashText.text = "+"+((PlayerPrefs.GetInt("waveNumber", 0) * 65)+500 )+" CASH BY WATCHING AN AD";
    }
}
