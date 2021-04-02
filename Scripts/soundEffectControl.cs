using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundEffectControl : MonoBehaviour {


	// Use this for initialization
	void Start () {
        AudioSource soundEffect = gameObject.GetComponent<AudioSource>();
        if(PlayerPrefs.GetInt("soundEffects", 1) == 1)
        {
            soundEffect.enabled = true;
        }
        else
        {
            soundEffect.enabled = false;
        }
        
    }
}
