using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void replayButton()
    {
        PlayerPrefs.SetInt("Current Score", 0);
        SceneManager.LoadScene("Main Menu");
    }
    public void quitButton()
    {
        PlayerPrefs.SetInt("Current Score", 0);
        Application.Quit();
        Debug.Log("Player quit game");
    }
    public void Highscores()
    {
        SceneManager.LoadScene("Highscore");
    }
}
