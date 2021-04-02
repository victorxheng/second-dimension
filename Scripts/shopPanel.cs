using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopPanel : MonoBehaviour {

    public GameObject panel;
    public GameObject joystick1;
    public GameObject joystick2;

    public void openWindow()
    {    
        if (PlayerPrefs.GetInt("menu", 0) == 1)
        {
            Time.timeScale = 0;//pause
        }
        else
        {
           // Menu.SetActive(false);
        }
        panel.SetActive(true);
    }
    public void closeWindow()
    {
        if (PlayerPrefs.GetInt("menu", 0) ==1)
        {
            Time.timeScale = 1;//unpause
        }
        else
        {
          //  Menu.SetActive(true);
        }
        panel.SetActive(false);
        
    }
}
