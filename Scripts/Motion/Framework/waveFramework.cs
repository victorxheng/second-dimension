using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class waveFramework : MonoBehaviour {

    public List<List<string>> waveList = new List<List<string>>();
    public EnemySpawn es;
    public TextMeshProUGUI waveText;
    public GameObject WaveTextGO;
    public TextMeshProUGUI waveActiveText;
    public GameObject refillHealth;

    public ShopSelection ss;
    public TutorialSingle ts;

    public void onPlay()
    {
        print("step3");
        PlayerPrefs.SetInt("enemies", 0);

        waveText = WaveTextGO.GetComponent<TextMeshProUGUI>();
        Color c = waveText.color;
        c.a = 0.0f;
        waveText.color = c;
        waveActiveText.text = "WAVE: " + (PlayerPrefs.GetInt("waveNumber", 0) + 1);

        StartCoroutine(createSpawn());
    }

    private void Awake()
    {
        PlayerPrefs.SetInt("adInterval", 1);
    }

    public AdManager am;

    IEnumerator createSpawn()
    {
        print("step4");
        while (SceneManager.GetActiveScene().buildIndex.Equals(0))
        {
            if (PlayerPrefs.GetInt("removeAds", 0) == 0)
            {
                if (PlayerPrefs.GetInt("waveNumber", 0) > 2)
                {
                    if (PlayerPrefs.GetInt("adInterval", 1) == 0)
                    {
                        if (Random.Range(1, 6) <= 2)
                        {
                            PlayerPrefs.SetInt("adInterval", 1);

                            if (Random.Range(1, 4) == 1)
                            {
                                am.ShowAd("video");
                            }
                            else
                            {
                                am.ShowAd("videoSkip");
                            }
                        }
                    }
                    else
                    {
                        PlayerPrefs.SetInt("adInterval", 0);
                    }
                }
            }

            StartCoroutine(refillHealthShow());
            yield return new WaitForSeconds(2);

            PlayerPrefs.SetInt("enemies", 0);

            if (PlayerPrefs.GetInt("waveNumber", 0) == 0)
            {
                StartCoroutine(createTutorial("t 00 02 <= MOVE THIS JOYSTICK TO MOVE AROUND"));
                StartCoroutine(createTutorial("t 05 02 MOVE THIS JOYSTICK TO SHOOT ENEMIES => "));
            }
            else if (PlayerPrefs.GetInt("waveNumber", 0) == 1)
            {
                StartCoroutine(createTutorial("t 02 02 SHOOTING ENEMIES WILL GIVE YOU CASH"));
                StartCoroutine(createTutorial("t 07 02 YOU CAN USE CASH TO BUY \nUPGRADES IN THE SHOP"));
            }
            else if (PlayerPrefs.GetInt("waveNumber", 0) == 2)
            {
                StartCoroutine(createTutorial("t 02 02 FASTER ENEMIES INCOMING"));
                StartCoroutine(createTutorial("t 07 02 HEALTH CAN BE REFILLED ONLY AT THE\nBEGINNING OF EVERY WAVE"));
            }
            else if (PlayerPrefs.GetInt("waveNumber", 0) == 4) StartCoroutine(createTutorial("t 02 02 SHOOTING ENEMIES INCOMING"));
            else if (PlayerPrefs.GetInt("waveNumber", 0) == 8) StartCoroutine(createTutorial("t 02 02 AMORED ENEMIES INCOMING"));
            else if (PlayerPrefs.GetInt("waveNumber", 0) == 12) StartCoroutine(createTutorial("t 02 02 GREEN ENEMIES INCOMING"));
            else if (PlayerPrefs.GetInt("waveNumber", 0) == 14)
            {
                StartCoroutine(createTutorial("t 02 02 QUIET? YOU CAN TURN ON MUSIC IN THE SETTINGS!"));
                StartCoroutine(createTutorial("t 07 02 PRESSING PAUSE WILL OPEN THE MENU"));
            }
            else if (PlayerPrefs.GetInt("waveNumber", 0) == 19) StartCoroutine(createTutorial("t 02 06 NEED CASH? YOU CAN GET MORE IN THE SHOP BY WATHCING ADS!"));
            else if (PlayerPrefs.GetInt("waveNumber", 0) == 24) StartCoroutine(createTutorial("t 02 06 ARE ADS ANNOYING? YOU CAN REMOVE THEM IN THE SHOP\nAND HELP THE DEVELOPER  FOR JUST $3.99!"));
            else if (PlayerPrefs.GetInt("waveNumber", 0) == 28) StartCoroutine(createTutorial("t 02 02 RED GIANTS INCOMING"));
            else if (PlayerPrefs.GetInt("waveNumber", 0) == 30) StartCoroutine(createTutorial("t 02 02 WARNING: GHOSTS"));
            else if (PlayerPrefs.GetInt("waveNumber", 0) == 99) StartCoroutine(createTutorial("t 02 02 GOOD JOB! YOU MADE IT TO WAVE 100!"));
            else if (PlayerPrefs.GetInt("waveNumber", 0) == 999) StartCoroutine(createTutorial("t 02 02 CONGRATS ON GETTING TO WAVE 1000!"));

            es.Start();
            int cyclesAmount =1;
            if (PlayerPrefs.GetInt("waveNumber", 0) > 32) cyclesAmount = Random.Range(Convert.ToInt32(0.09*Math.Sqrt(2* PlayerPrefs.GetInt("waveNumber", 0)) -0.22), Convert.ToInt32(0.09 * Math.Sqrt(2 * PlayerPrefs.GetInt("waveNumber", 0)) - 0.22)+2);
            //print("FORMULA: "+ Convert.ToInt32(0.116 * Math.Sqrt(2 * PlayerPrefs.GetInt("waveNumber", 0)) - 0.4));

            for (int i = 0; i < cyclesAmount; i++)
            {
                createAttackScript();
                yield return new WaitForSeconds(10);
            }
            

            while (PlayerPrefs.GetInt("enemies", 0) != 0)
            {
                yield return new WaitForSeconds(1);
            }

            yield return new WaitForSeconds(1);

            PlayerPrefs.SetInt("waveNumber", PlayerPrefs.GetInt("waveNumber", 0) + 1);
            waveActiveText.text = "WAVE: " + (PlayerPrefs.GetInt("waveNumber", 0) + 1);

        }        
    }
    public void createAttackScript()
    {
        string[] locationArray = chooseLocation();
        
        int seriesCount = 0;
        for (int i = 0; i < locationArray.Length; i++)
        {
            for (int j = 0; j < locationArray[i].Length; j++)
            {
                seriesCount++;
            }
        }

        for (int i = 0; i<locationArray.Length; i++)
        {
            int enemyType = chooseEnemy();
            int spawnAmount = chooseAmount(enemyType);

            int spawnDuration = Random.Range(1, 6);
            if (spawnDuration < 3)
            {
                spawnDuration = Random.Range(1, 6);
            }
            else if (spawnDuration < 5)
            {
                spawnDuration = Random.Range(3, 6);
            }

            for (int j = 0; j < locationArray[i].Length; j++)
            {
                int spawnLocation = locationArray[i][j] - 48;
                print("SPAWN DURATION: "+spawnDuration);

                int currentSpawn = Convert.ToInt32(spawnAmount / seriesCount);
                if (currentSpawn == 0) currentSpawn++;

                es.ExecuteSpawn(enemyType, spawnLocation, currentSpawn, spawnDuration);
            }
        }
        
    }
    private int chooseEnemy()
    {

        int EnemyType;
        if (PlayerPrefs.GetInt("waveNumber", 0) == 0 || PlayerPrefs.GetInt("waveNumber", 0) == 1) EnemyType = 0;
        else if (PlayerPrefs.GetInt("waveNumber", 0) == 2 || PlayerPrefs.GetInt("waveNumber", 0) == 3) EnemyType = 1;
        else if (PlayerPrefs.GetInt("waveNumber", 0) == 4|| PlayerPrefs.GetInt("waveNumber", 0) == 6) EnemyType = 8;
        else if (PlayerPrefs.GetInt("waveNumber", 0) == 8 || PlayerPrefs.GetInt("waveNumber", 0) == 10) EnemyType = 2;
        else if (PlayerPrefs.GetInt("waveNumber", 0) == 12 || PlayerPrefs.GetInt("waveNumber", 0) == 14) EnemyType = 3;
        else if (PlayerPrefs.GetInt("waveNumber", 0) == 15) EnemyType = 9;
        else if (PlayerPrefs.GetInt("waveNumber", 0) == 17 || PlayerPrefs.GetInt("waveNumber", 0) == 19) EnemyType = 4;
        else if (PlayerPrefs.GetInt("waveNumber", 0) == 20) EnemyType = 10;
        else if (PlayerPrefs.GetInt("waveNumber", 0) == 22 || PlayerPrefs.GetInt("waveNumber", 0) == 24) EnemyType = 5;
        else if (PlayerPrefs.GetInt("waveNumber", 0) == 25 || PlayerPrefs.GetInt("waveNumber", 0) == 27) EnemyType = 6;
        else if (PlayerPrefs.GetInt("waveNumber", 0) == 28) EnemyType = 7;
        else if (PlayerPrefs.GetInt("waveNumber", 0) == 30||PlayerPrefs.GetInt("waveNumber", 0) == 32) EnemyType = 11;
        else
        {

            int EasyOther = Random.Range(1, 3);
            int ShootingNormal = Random.Range(1, 3);
            if (EasyOther == 1 || PlayerPrefs.GetInt("waveNumber", 0) < 8)
            {
                if (ShootingNormal == 1 || PlayerPrefs.GetInt("waveNumber", 0) < 4)
                {
                    int SlowFast = Random.Range(1, 4);
                    if (SlowFast < 2 || PlayerPrefs.GetInt("waveNumber", 0) < 2) EnemyType = 0;
                    else EnemyType = 1;
                }
                else
                {
                    EnemyType = 8;
                }
            }
            else
            {
                if (ShootingNormal == 1 || PlayerPrefs.GetInt("waveNumber", 0) < 15)
                {
                    int other = Random.Range(1, 8);
                    if (other == 1) EnemyType = 1;
                    else if (other == 2 || PlayerPrefs.GetInt("waveNumber", 0) < 12) EnemyType = 2;
                    else if (other == 3 || PlayerPrefs.GetInt("waveNumber", 0) < 17) EnemyType = 3;
                    else if (other == 4 || PlayerPrefs.GetInt("waveNumber", 0) < 22) EnemyType = 4;
                    else if (other == 5 || PlayerPrefs.GetInt("waveNumber", 0) < 25) EnemyType = 5;
                    else if (other == 6 || PlayerPrefs.GetInt("waveNumber", 0) < 28) EnemyType = 6;
                    else EnemyType = 7;
                }
                else
                {
                    int other = Random.Range(1, 6);
                    if (other <= 2) EnemyType = 8;
                    else if (other == 3 || PlayerPrefs.GetInt("waveNumber", 0) < 20) EnemyType = 9;
                    else if (other == 4 || PlayerPrefs.GetInt("waveNumber", 0) < 30) EnemyType = 10;
                    else EnemyType = 11;
                }
            }
        }
        return EnemyType;
    }
    private int chooseAmount(int EnemyType)
    {
        if (EnemyType == 0) return Random.Range(50, 60) + Convert.ToInt32(PlayerPrefs.GetInt("waveNumber", 0) * 1.25f);
        if (EnemyType == 1) return Random.Range(40, 50) + Convert.ToInt32(PlayerPrefs.GetInt("waveNumber", 0) * 1.15f);
        if (EnemyType == 2) return Random.Range(31, 40) + Convert.ToInt32(PlayerPrefs.GetInt("waveNumber", 0) * 0.8f);
        if (EnemyType == 3) return Random.Range(22, 31) + Convert.ToInt32(PlayerPrefs.GetInt("waveNumber", 0) * 0.5f);
        if (EnemyType == 4) return Random.Range(14, 22) + Convert.ToInt32(PlayerPrefs.GetInt("waveNumber", 0) * 0.25f);
        if (EnemyType == 5) return Random.Range(8, 14) + Convert.ToInt32(PlayerPrefs.GetInt("waveNumber", 0) * 0.2f);
        if (EnemyType == 6) return Random.Range(3,8) + Convert.ToInt32(PlayerPrefs.GetInt("waveNumber", 0) * 0.01f);
        if (EnemyType == 7) return Random.Range(1,3) + Convert.ToInt32(PlayerPrefs.GetInt("waveNumber", 0) * 0.005f);
        if (EnemyType == 8) return Random.Range(25, 32) + Convert.ToInt32(PlayerPrefs.GetInt("waveNumber", 0) * 0.2f);
        if (EnemyType == 9) return Random.Range(15, 20) + Convert.ToInt32(PlayerPrefs.GetInt("waveNumber", 0) * 0.08f);
        if (EnemyType == 10) return Random.Range(10, 15) + Convert.ToInt32(PlayerPrefs.GetInt("waveNumber", 0) * 0.05f);
        else return Random.Range(5, 14) + Convert.ToInt32(PlayerPrefs.GetInt("waveNumber", 0) * 0.01f);
    }
    
    private string[] chooseLocation()
    {
        int firstGroup = Random.Range(1, 10);
        int secondGroup;
        if (firstGroup == 1) secondGroup = Random.Range(1, 5);
        else if (firstGroup == 2) secondGroup = Random.Range(5, 7);
        else if (firstGroup == 3) secondGroup = Random.Range(7, 11);
        else if (firstGroup == 4) secondGroup = Random.Range(11, 13);
        else if (firstGroup == 5) secondGroup = Random.Range(13, 15);
        else if (firstGroup == 6) secondGroup = Random.Range(15, 17);
        else if (firstGroup == 7) secondGroup = Random.Range(17, 19);
        else if (firstGroup == 8) secondGroup = Random.Range(19, 21);
        else if (firstGroup == 9) secondGroup = Random.Range(21, 23);
        else secondGroup = Random.Range(23, 26);

        if (secondGroup == 1) return new string[] { "345", "2" };
        else if (secondGroup == 2) return new string[] { "245", "3" };
        else if (secondGroup == 3) return new string[] { "243", "5" };
        else if (secondGroup == 4) return new string[] { "253", "4" };
        else if (secondGroup == 5) return new string[] { "5", "67" };
        else if (secondGroup == 6) return new string[] { "4", "89" };
        else if (secondGroup == 7) return new string[] { "5", "67", "4" };
        else if (secondGroup == 8) return new string[] { "67", "4" };
        else if (secondGroup == 9) return new string[] { "5", "89", "4" };
        else if (secondGroup == 10) return new string[] { "89", "5" };
        else if (secondGroup == 11) return new string[] { "86", "2" };
        else if (secondGroup == 12) return new string[] { "97", "3" };
        else if (secondGroup == 13) return new string[] { "5", "4" };
        else if (secondGroup == 14) return new string[] { "2", "3" };
        else if (secondGroup == 15) return new string[] { "1" };
        else if (secondGroup == 16) return new string[] { "6", "7", "8", "9" };
        else if (secondGroup == 17) return new string[] { "5" };
        else if (secondGroup == 18) return new string[] { "4" };
        else if (secondGroup == 19) return new string[] { "45", "2" };
        else if (secondGroup == 20) return new string[] { "45", "3" };
        else if (secondGroup == 21) return new string[] { "86", "2", "45" };
        else if (secondGroup == 22) return new string[] { "97", "3", "45" };
        else if (secondGroup == 23) return new string[] { "0", "0" };
        else if (secondGroup == 24) return new string[] { "1", "1" };
        else return new string[] { "0", "1" };
    }
    IEnumerator createAttack(string Code)
    {
        int EnemyType = (Code[0] - 48) * 10 + (Code[1] - 48);
        int spawnLocation = Code[3]-48;
        int spawnSpeed;
        int spawnDuration;
        int spawnDelay;
        if(PlayerPrefs.GetInt("waveNumber", 0) > 30)
        {
            spawnSpeed = (Code[5] - 48) * 100 + (Code[6] - 48) * 10 + (Code[7] - 48);
            spawnDuration = (Code[9] - 48) * 100 + (Code[10] - 48) * 10 + (Code[11] - 48);
            spawnDelay = (Code[13] - 48) * 100 + (Code[14] - 48) * 10 + (Code[15] - 48);

        }
        else
        {
            spawnSpeed = (Code[5] - 48) * 10000 + (Code[6] - 48) * 1000 + (Code[7] - 48) * 100 + (Code[8] - 48) * 10 + (Code[9] - 48);
            spawnDuration = (Code[11] - 48) * 100 + (Code[12] - 48) * 10 + (Code[13] - 48);
            spawnDelay = (Code[15] - 48) * 100 + (Code[16] - 48) * 10 + (Code[17] - 48);
        }
        yield return new WaitForSeconds(spawnDelay);
        es.ExecuteSpawn(EnemyType,spawnLocation,spawnSpeed,spawnDuration);
    }
    IEnumerator createTutorial(string Code)
    {
        int delay = (Code[2] - 48) * 10 + (Code[3] - 48);
        int seconds = (Code[5] - 48) * 10 + (Code[6] - 48);

        string resultText = "";
        for(int i = 8; i<Code.Length; i++)
        {
            resultText += Convert.ToChar(Code[i]);
        }

        print(resultText);
        print(delay + "");
        print(seconds + "");
        yield return new WaitForSeconds(delay);
        print("wait complete");
        ts.tutorialText(resultText, seconds);

    }
    public Animator healthAnimation;
    IEnumerator refillHealthShow()
    {
        if(PlayerPrefs.GetInt("waveNumber", 0) != 0)
        {
            healthAnimation.SetInteger("HealthPos", Mathf.Abs(1));
            ss.refillHealthShow();
            yield return new WaitForSeconds(0.1f);
            PlayerPrefs.SetInt("waveHealth", PlayerPrefs.GetInt("playerHealth", 10));
            StartCoroutine(FadeWaveText());
            yield return new WaitForSeconds(4f);
            healthAnimation.SetInteger("HealthPos", Mathf.Abs(0));
        }

    }
    IEnumerator FadeWaveText()
    {
        waveText.text = "WAVE " + (PlayerPrefs.GetInt("waveNumber", 0)+1);

        waveText = WaveTextGO.GetComponent<TextMeshProUGUI>();
        Color n = waveText.color;
        n.a = 0f;
        waveText.color = n;
        for (float f = 0.05f; f <= 1; f += 0.05f)
        {
            Color c = waveText.color;
            c.a = f;
            waveText.color = c;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(2f);
        for (float f = 1; f >= -0.05; f -= 0.05f)
        {
            Color c = waveText.color;
            c.a = f;
            waveText.color = c;
            yield return new WaitForSeconds(0.05f);

        }
    }

}
