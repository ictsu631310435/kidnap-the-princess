using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Script for managing Title screen
public class TitleManager : MonoBehaviour
{
    #region Data Members
    public GameObject mainMenu;
    public string levelSceneName;

    public GameObject settingsPanel;
    public GameObject creditPanel;
    #endregion

    #region Unity Callbacks
    // Update is called once per frame
    void Update()
    {
        // Pressed "Cancel" button (Escape key) to close settingsPanel or creditPanel
        if (Input.GetButtonDown("Cancel"))
        {
            // If settingPanel is opened, close it
            if (settingsPanel.activeSelf)
            {
                SetActiveSettings(false);
            }
            // If creditPanel is opend, close it
            else if (creditPanel.activeSelf)
            {
                SetActiveCredit(false);
            }
        }
    }
    #endregion

    #region Methods
    // Method for StartButton
    public void StartButton() 
    {
        // Load levelScene
        SceneManager.LoadScene(levelSceneName);
    }

    // Method for opening/closing Settings screen
    public void SetActiveSettings(bool value)
    {
        // Close/open mainMenu
        mainMenu.SetActive(!value);
        // Open/close settingsPanel
        settingsPanel.SetActive(value);
    }

    // Method for opening/closing Credit screen
    public void SetActiveCredit(bool value)
    {
        // Close/open mainMenu
        mainMenu.SetActive(!value);
        // Open/close settingsPanel
        creditPanel.SetActive(value);
    }

    // Method for QuitButton
    public void QuitButton()
    {
        // Quit application
        Application.Quit();
    }
    #endregion
}
