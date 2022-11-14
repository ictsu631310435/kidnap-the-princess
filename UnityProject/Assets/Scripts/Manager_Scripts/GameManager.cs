using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Script for managing the game
public class GameManager : MonoBehaviour
{
    #region Data Members
    public static GameManager Instance { get; private set; }

    [Header("Gameplay")]
    public float gameTime;
    public GameObject gameplayPanel;
    public Text timeText;

    [Header("Paused")]
    public bool gamePaused;
    public GameObject pausePanel;

    [Header("GameOver")]
    public GameObject gameOverPanel;
    public Text gameOverTimeText;
    public string titleSceneName;

    [Header("GameClear")]
    public bool gameCleared;
    public bool bossSpawned;
    public bool bossAlive;
    public GameObject winPanel;
    public Text winTimeText;
    #endregion

    #region Unity Callbacks
    // Awake is called whe the script instance is being loaded
    void Awake()
    {
        // Set instance to this object
        Instance = this;

        // Solve timestop after restart level
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // Update gameTime
        gameTime = Time.timeSinceLevelLoad;
        // Update timeText
        timeText.text = FormatTime(gameTime);

        // Pressed Cancel Button
        if (Input.GetButtonDown("Cancel"))
        {
            // If game is not paused, pause game
            if (!gamePaused)
            {
                PauseGame();
            }
            // If game is pauseed, resume game
            else
            {
                ResumeGame();
            }
        }

        // If game cleared, show score screen
        if (gameCleared)
        {
            GameClear();
        }
        // If game not cleared, continue checking
        else
        {
            // If boss is spawned and not alive anymore, game is cleared
            if (bossSpawned && !bossAlive)
            {
                gameCleared = true;
            }
        }
    }
    #endregion

    #region Methods
    // Method for formatting time
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

    // Method for pausing game
    public void PauseGame()
    {
        // Stop time
        Time.timeScale = 0;
        gamePaused = true;
        // Open up paused screen
        pausePanel.SetActive(true);
    }

    // Method for pausing game
    public void ResumeGame()
    {
        // Resume time
        Time.timeScale = 1;
        gamePaused = false;
        // Close paused screen
        pausePanel.SetActive(false);
    }

    // Method for GameOver screen
    public void GameOver()
    {
        // Stop time
        Time.timeScale = 0;
        gamePaused = true;
        // Close Gameplay panel
        gameplayPanel.SetActive(false);
        // Open up GameOver screen
        gameOverPanel.SetActive(true);
        // Update time score
        gameOverTimeText.text = FormatTime(gameTime);
    }

    // Method for restarting level
    public void RestartLevel()
    {
        // Resume time
        Time.timeScale = 1;

        string _sceneName = SceneManager.GetActiveScene().name;
        // Load currently active scene
        SceneManager.LoadScene(_sceneName);
    }

    // Method for going back to Title screen
    public void BackToTitle()
    {
        // Resume time
        Time.timeScale = 1;
        // Load Title scene
        SceneManager.LoadScene(titleSceneName);
    }

    // Method for setting up boss fight
    public void SetBossFight()
    {
        bossAlive = true;
        bossSpawned = true;
    }

    // Method for GameClear screen
    public void GameClear()
    {
        // Stop time
        Time.timeScale = 0;
        // Open up GameClear screen
        winPanel.SetActive(true);
        // Update time score
        winTimeText.text = FormatTime(gameTime);
    }
    #endregion
}
