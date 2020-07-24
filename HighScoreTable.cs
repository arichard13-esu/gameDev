using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HighScoreTable : MonoBehaviour
{
    public TextMeshProUGUI highscoreText;
    public TMP_InputField inputText;

    public GameObject inputContainer;
    public GameObject highscoreContainer;

    const int scoreCount = 5;
    string[] initialsArray = new string[scoreCount];
    int[] scoreArray = new int[scoreCount];

    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        loadPlayerPrefs();

        if (GlobalStats.score > scoreArray[scoreCount - 1])
        {
            inputContainer.SetActive(true);
            highscoreContainer.SetActive(false);
        }
        else
            outputUpdatedScores();
    }
    void loadPlayerPrefs()
    {
        for (int i = 0; i < scoreCount; i++)
        {
            scoreArray[i] = PlayerPrefs.GetInt("Score" + i.ToString(), 0);
            initialsArray[i] = PlayerPrefs.GetString("Name" + i.ToString(), "");
        }
    }
    void savePlayerPrefs()
    {
        for (int i = 0; i < scoreCount; i++)
        {
            PlayerPrefs.SetInt("Score" + i.ToString(), scoreArray[i]);
            PlayerPrefs.SetString("Name" + i.ToString(), initialsArray[i]);
        }
    }
    void insertScore()
    {
        string tmpName1 = inputText.text;
        string tmpName2;
        int tmpScore1 = GlobalStats.score;
        int tmpScore2;

        for (int i = 0; i < scoreCount; i++)
        {
            if (tmpScore1 > scoreArray[i])
            {
                tmpScore2 = scoreArray[i];
                tmpName2 = initialsArray[i];

                scoreArray[i] = tmpScore1;
                initialsArray[i] = tmpName1;

                tmpScore1 = tmpScore2;
                tmpName1 = tmpName2;
            }
        }
    }
    public void submitScore()
    {
        insertScore();
        savePlayerPrefs();
        inputContainer.SetActive(false);
        highscoreContainer.SetActive(true);
        outputUpdatedScores();
    }
    void outputUpdatedScores()
    {
        highscoreText.text = "";
        for (int i = 0; i < scoreCount; i++)
        {
            highscoreText.text += (i + 1).ToString() + "\t" + initialsArray[i] + "\t\t" + scoreArray[i] + "\n";
        }
    }
    public void replayButton()
    {
        PlayerPrefs.SetInt("Current Score", 0);
        SceneManager.LoadScene("Easy Level");
    }
    public void quitButton()
    {
        PlayerPrefs.SetInt("Current Score", 0);
        Application.Quit();
        Debug.Log("Player quit game");
    }
}