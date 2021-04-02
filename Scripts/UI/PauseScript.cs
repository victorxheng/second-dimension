using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour {

    public Button button;
    public Sprite playImage;
    public Sprite pauseImage;
    bool pause;
    private void Awake()
    {
        Time.timeScale = 1;
    }
    void Start()
    {
        button.GetComponent<Image>();
        button.image.overrideSprite = pauseImage;
        pause = false;
    }
    public void onClick()
    {
        if (pause)
        {
            ContinueGame();
            pause = false;
        }
        else if (!pause)
        {
            PauseGame();
            pause = true;
        }
    }
      
    private void PauseGame()
    {
        button.image.overrideSprite = playImage;
        Time.timeScale = 0;
        //Disable scripts that still work while timescale is set to 0
    }
    private void ContinueGame()
    {
        button.image.overrideSprite = pauseImage;
        Time.timeScale = 1;
        //enable the scripts again
    }
}
