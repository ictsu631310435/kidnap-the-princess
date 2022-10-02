using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float gameTime;
    public Text timeText;

    public bool gamePaused;
    public GameObject pausePanel;

    public GameObject gameOverPanel;
    public Text gameOverTimeText;

    void Awake()
    {
        Instance = this;    
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameTime = Time.timeSinceLevelLoad;
        timeText.text = FormatTime(gameTime);

        if (Input.GetButtonDown("Cancel"))
        {
            if (!gamePaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public string FormatTime(float time)
    {
        // Get minutes from time
        int minutes = Mathf.FloorToInt(time / 60);
        // Get seconds from time
        int seconds = Mathf.FloorToInt(time % 60);
        // Get milliseconds from time
        int milliseconds = Mathf.FloorToInt(time % 1 * 1000);
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        gamePaused = true;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        gamePaused = false;
        pausePanel.SetActive(false);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gamePaused = true;
        gameOverPanel.SetActive(true);
        gameOverTimeText.text = FormatTime(gameTime);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;

        string _sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(_sceneName);
    }
}
