using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class activePanel : MonoBehaviour {

    public GameObject panel;
    public GameObject parent;
    public GameObject refillHealth;
    Image image;
    bool isActive= false;

    public GameObject button1;
    public GameObject button2;

    public GameObject EnemiesActive;
    public GameObject Health;
    public GameObject Cash;
    public GameObject Wave;

    public GameObject FadeParent;
    public GameObject joystickObject;
    public GameObject joystickObject2;

    public GameObject PlayButton;

    public Animator menuAnimator;
    public Animator leftAnimator;
    public Animator rightAnimator;
    public Animator textAnimator;
    public Animator settingsAnimator;

    public TextMeshProUGUI fadeText;

    private void Awake()
    {

        PlayerPrefs.SetInt("menu", 0);
        menuAnimator.SetInteger("Menu", Mathf.Abs(0));
        settingsAnimator.SetInteger("Settings", Mathf.Abs(0));
        fadeText.text = "MOVE ANY JOYSTICK TO BEGIN";
    }
    private void Start()
    {
        Time.timeScale = 1;//unpause
        image = panel.GetComponent<Image>();
        colorNormal();

        StartCoroutine(FadeTextPattern());
        StartCoroutine(checkForMovement());        
    }

    public void onAd()
    {
        Time.timeScale = 0f;
        button1.SetActive(false);
        button2.SetActive(false);
    }
    public void offAd()
    {
        Time.timeScale = 1f;
        button1.SetActive(true);
        button2.SetActive(true);
    }

    public void openWindow()
    {
        fadeText.text = "MOVE ANY JOYSTICK TO RESUME";

        leftAnimator.SetInteger("Dissapear", 0);
        rightAnimator.SetInteger("Dissapear", 0);

        PlayerPrefs.SetInt("menu", 2);
        menuAnimator.SetInteger("Menu", Mathf.Abs(2));

        StartCoroutine(FadeTextPattern());
        StartCoroutine(checkForMovement());

        textAnimator.SetBool("onPlay", false);
        textAnimator.SetBool("onPause", true);

        panel.SetActive(true);
        //parent.SetActive(true);

        if (refillHealth.active)
        {
            refillHealth.SetActive(false);
            isActive = true;
        }
        else
        {
            isActive = false;
        }

        Time.timeScale = 0;//pause
    }
    public void closeWindow()
    {
        PlayerPrefs.SetInt("menu", 1);
        menuAnimator.SetInteger("Menu", Mathf.Abs(1));
        settingsAnimator.SetInteger("Settings", Mathf.Abs(0));
        Time.timeScale = 1;//unpause
        panel.SetActive(false);
        //parent.SetActive(false);

        textAnimator.SetBool("onPlay", true);
        textAnimator.SetBool("onPause", false);

        if (isActive)
        {
            refillHealth.SetActive(true);
            isActive = false;
        }
    }

    public Joystick joystick1;
   // public Joystick joystick2;
    public waveFramework wf;

    IEnumerator checkForMovement()
    {
            //joystickObject.SetActive(false);
            //joystickObject2.SetActive(false);
            yield return new WaitForSecondsRealtime(1f);
            //joystickObject.SetActive(true);
            //joystickObject2.SetActive(true);
        

        while (joystick1.Horizontal == 0 && joystick1.Vertical == 0)
        {            
            yield return new WaitForSecondsRealtime(Time.deltaTime);
        }
        print("step1");
        onTap();
    }
    public void onTap()
    {
        leftAnimator.SetInteger("Dissapear",1);
        rightAnimator.SetInteger("Dissapear", 1);
        print("step2");
        if (PlayerPrefs.GetInt("menu", 0) == 0)
            wf.onPlay();
        closeWindow();
    }
    IEnumerator FadeTextPattern()
    {
        setAlphaColor(0);

        if (PlayerPrefs.GetInt("menu", 0) == 0)
        {
            yield return new WaitForSecondsRealtime(0.5f);
        }

        while (PlayerPrefs.GetInt("menu", 0) !=1)
        {
            setAlphaColor(0);
            for (float f = 0.1f; f <= 0.5; f += 0.025f)
            {
                setAlphaColor(f);
                yield return new WaitForSecondsRealtime(0.05f);
            }
            yield return new WaitForSecondsRealtime(0.2f);
            for (float f = 0.5f; f >= 0.1; f -= 0.025f)
            {
                setAlphaColor(f);
                yield return new WaitForSecondsRealtime(0.05f);
            }
        }
        setAlphaColor(0);
    }

    public Image Background1;
    public Image Handle1;
    public Image Background;
    public Image Handle;
    public TextMeshProUGUI shootText;
    public TextMeshProUGUI moveText;
    public TextMeshProUGUI tapText;

    private void setAlphaColor(float f)//set renders
    {
        if(PlayerPrefs.GetInt("menu", 0) != 1)
        {
            imageSet(Background1, f);
            imageSet(Handle1, f);
            imageSet(Background, f);
            imageSet(Handle, f);
            textSet(tapText, f);
        }
        else
        {
            imageSet(Background1, 0);
            imageSet(Handle1, 0);
            imageSet(Background, 0);
            imageSet(Handle, 0);
            textSet(tapText, 0);
        }
        textSet(shootText, f);
        textSet(moveText, f);
    }
    private void imageSet(Image img, float f)
    {        
        img.color = new Color(img.color.r, img.color.g, img.color.b, f);
    }
    private void textSet(TextMeshProUGUI txt, float f)
    {
        txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, f);
    }

    private void hideTexts()
    {
        EnemiesActive.SetActive(false);
        Health.SetActive(false);
        //Cash.SetActive(false);
        //Wave.SetActive(false);
    }

    private void showTexts()
    {
        EnemiesActive.SetActive(true);
        Health.SetActive(true);
        //Cash.SetActive(true);
        //Wave.SetActive(true);
    }

    public void colorNormal()
    {
        image.color = new Color(0, 0, 0, 0.4f);
        FadeParent.SetActive(true);
        joystickObject.SetActive(true);
        joystickObject2.SetActive(true);
    }
    public void colorDark()
    {
        FadeParent.SetActive(false);
        joystickObject.SetActive(false);
        joystickObject2.SetActive(false);
        image.color = new Color(0, 0, 0, 0.9f);
    }

    public void MenuSettings()
    {
        menuAnimator.SetInteger("Menu", Mathf.Abs(1));
        settingsAnimator.SetInteger("Settings", Mathf.Abs(1));
    }
    public void SettingsMenu()
    {
        menuAnimator.SetInteger("Menu", Mathf.Abs(0));
        settingsAnimator.SetInteger("Settings", Mathf.Abs(0));
    }

}
