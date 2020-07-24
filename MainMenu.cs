using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("Current Score", 0);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Easy()
    {
        PlayerPrefs.SetInt("Current Score", 0);
        SceneManager.LoadScene("Easy Level");
    }
    public void Medium()
    {
        PlayerPrefs.SetInt("Current Score", 0);
        SceneManager.LoadScene("Medium Level");
    }
    public void Hard()
    {
        PlayerPrefs.SetInt("Current Score", 0);
        SceneManager.LoadScene("Hard Level");
    }
    public void Highscores()
    {
        SceneManager.LoadScene("Highscore");
    }
    public void Reset()
    {
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt("Score" + i.ToString(), 0);
            PlayerPrefs.SetString("Name" + i.ToString(), "");
        }
    }
}