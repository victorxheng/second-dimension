using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyAudio : MonoBehaviour {
    public static DontDestroyAudio instance;
    private AudioSource audiosource;
    private void Awake()
    {
        audiosource = gameObject.GetComponent<AudioSource>();
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }        
    }
    private void Update()
    {
        if (PlayerPrefs.GetInt("music", 1) == 1)
        {
            audiosource.volume = 0.0f;
        }
        else if (PlayerPrefs.GetInt("music", 1) == 2)
        {
            audiosource.volume = 0.15f;
        }
        else if (PlayerPrefs.GetInt("music", 1) == 3)
        {
            audiosource.volume = 0.3f;
        }
        else
        {
            audiosource.volume = 0.5f;
        }
    }
}
