using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD instance;
    public Transform Player;
    public TextMeshProUGUI playerInformation;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 200f;

        if (SceneManager.GetActiveScene().name == "Main Menu")
            PlayerPrefs.SetInt("Current Score", 0);
        
        GlobalStats.score = PlayerPrefs.GetInt("Current Score");
        GlobalStats.health = 3;
        GlobalStats.shield = 3;
        playerInformation.text = "Coins: " + GlobalStats.score.ToString() + "\nHealth: " + GlobalStats.health.ToString();

        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        playerInformation.text = SceneManager.GetActiveScene().name + "\nTime Remaining: " + time.ToString("f0")
        + "\n\nScore: " + GlobalStats.score.ToString() + "\nShield: " + GlobalStats.shield.ToString() + "\nHealth: " + GlobalStats.health.ToString()
        +"\n\nPitch: " + Player.transform.rotation.x + "\nYaw: " + Player.transform.rotation.y + "\nRoll: " + Player.transform.rotation.z
        + "\n\nPosition x: " + Player.transform.position.x + "\nPosition y: " + Player.transform.position.y + "\nPosition z: " + Player.transform.position.z;
            
        time -= Time.deltaTime;

        if (time < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Game Over");
        }
    }

    public void ChangeScore(int coinValue)
    {
        GlobalStats.score += coinValue;
        PlayerPrefs.SetInt("Current Score", GlobalStats.score);
    }
    public void SubtractHealth(int healthValue)
    {
        GlobalStats.health -= healthValue;
    }
    public void AddHealth(int healthValue)
    {
        GlobalStats.health += healthValue;
    }
    public void SubtractShield(int shieldValue)
    {
        GlobalStats.shield -= shieldValue;
    }
    public void RechargeShield()
    {
        GlobalStats.shield = 3;
    }
}