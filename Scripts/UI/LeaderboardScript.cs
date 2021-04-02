using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardScript : MonoBehaviour
{

    public TextMeshProUGUI leaderboardText;
    public GameObject leaderboardObject;
    
    public TextMeshProUGUI YourName;
    public TextMeshProUGUI YourHighscore;
    public TextMeshProUGUI outputText;

    public GameObject backButton;
    public GameObject updateButton;
    public GameObject submitButton;
    public GameObject refreshButton;
    public GameObject SubmitToLeaderboard;
    
    

    private string SubmitName;
    private string checkName;
    public InputField nameEnter;
    public Text inputfieldText;

    public dreamloLeaderBoard dl;

    private int GetScoreOrRefresh;
    private int repeats;

    public void validateName()
    {
        toggleSubmitFalse();

        checkName = nameEnter.text;
        outputText.text = "PROCESSING...";

        if (checkName.Equals(""))
        {
            outputText.text = "PLEASE ENTER A NAME";
            toggleSubmitTrue();
        }
        else if (checkName.Length > 18)
        {
            outputText.text = "TOO MANY CHARACTERS";
            toggleSubmitTrue();
        }
        else if (checkName.Equals(PlayerPrefs.GetString("YourName", "No Name")))
        {
            outputText.text = "PLEASE ENTER A DIFFERENT NAME";
            toggleSubmitTrue();
        }
        else
        {
            int AsciiCheck = 0;
            for (int i = 0; i < checkName.Length; i++)
            {
                if (checkName[i] < ' ' || (checkName[i] > ' ' && checkName[i] < '0') || (checkName[i] > '9' && checkName[i] < 'A') || checkName[i] > 'z' || (checkName[i] > 'Z' && checkName[i] < '_') || (checkName[i] > '_' && checkName[i] < 'a'))//insert ascii chart
                {
                    AsciiCheck = 1;
                }
            }
            if (AsciiCheck == 1)
            {
                outputText.text = "CONTAINS INVALID CHARACTERS";
                toggleSubmitTrue();
            }
            else
            {
                outputText.text = "LOADING...";
                GetScoreOrRefresh = 1;
                dl.LoadScores();
            }

        }
    }

    public void leaderboardClick()
    {
        GetScoreOrRefresh = 0;
        toggleSubmitTrue();
        setTexts();
        outputText.text = "LOADING...";
        dl.LoadScores();
    }
    public void submitScoreOutput()
    {
        outputText.text = "";
    }


    public void formatScores()
    {
        repeats = 0;
        List<dreamloLeaderBoard.Score> scoreList02 = dl.ToListHighToLow();
        leaderboardText.text = formatList(scoreList02);
        

        setTexts();
        if (GetScoreOrRefresh == 0)
        {
            outputText.text = "";
        }
        else
        {
            GetScoreOrRefresh = 0;
            if (repeats > 0)
            {
                toggleSubmitTrue();
                outputText.text = "Name Taken";
            }
            else
            {
                outputText.text = "UPLOADING...";
                dl.DeletePrevious(PlayerPrefs.GetString("YourName", "No Name"));
            }
        }
    }

    public void UploadScores()
    {
        PlayerPrefs.SetString("YourName", checkName);
        dl.AddScore(checkName);
    }
    private int checkListForRepeats(string[] NameList)
    {
        for (int i = 0; i < NameList.Length; i++)
        {
            if (checkName.Equals(NameList[i]))
            {
                return 1;
            }
        }
        return 0;
    }
    private string formatList(List<dreamloLeaderBoard.Score> scoreList)
    {
        int count = 0;
        string ReturnString = "";
        if (scoreList.Count < 1)
        {
            ReturnString = "NO INTERNET CONNECTION";
            return ReturnString;
        }
        foreach (dreamloLeaderBoard.Score currentScore in scoreList)
        {
            count++;
            ReturnString += count + ". ";
            ReturnString += currentScore.score + " - ";
            
            ReturnString += Clean(currentScore.playerName) + " ";
            ReturnString += "\n";
            if (GetScoreOrRefresh == 1)
            {
                if (checkName.Equals(Clean(currentScore.playerName)))
                {
                    repeats++;
                }
            }
        }
        print(ReturnString);
        return ReturnString;
    }
    public void updateScore()
    {
        if(PlayerPrefs.GetString("YourName", "No Name").Equals("No Name")){
            outputText.text = "SUBMIT A NAME";
        }
        else
        {
            toggleSubmitFalse();
            checkName = nameEnter.text;
            outputText.text = "UPDATING...";
            dl.AddScore(PlayerPrefs.GetString("YourName", "No Name"));
        }
    }

    public void setTexts()
    {
        YourHighscore.text = "WAVES BEAT: " +PlayerPrefs.GetInt("waveNumber", 0);
        YourName.text = "CURRENT NAME: " + PlayerPrefs.GetString("YourName", "No Name");
    }
    /*
    private void setFalse()
    {
        leaderboardObject.SetActive(false);
    }*/
    public void toggleSubmitFalse()
    {
        SubmitToLeaderboard.SetActive(false);
        backButton.SetActive(false);
    }
    public void toggleSubmitTrue()
    {
        SubmitToLeaderboard.SetActive(true);
        backButton.SetActive(true);
    }
    string Clean(string s)
    {
        s = s.Replace("/", "");
        s = s.Replace("|", "");
        s = s.Replace("+", " ");
        return s;

    }

}
