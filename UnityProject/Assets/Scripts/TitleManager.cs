using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public string levelSceneName;

    public GameObject settingsPanel;
    public GameObject creditPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (settingsPanel.activeSelf)
            {
                settingsPanel.SetActive(false);
            }
            else if (creditPanel.activeSelf)
            {
                creditPanel.SetActive(false);
            }
        }
    }

    public void StartButton() 
    {
        SceneManager.LoadScene(levelSceneName);
    }

    public void SetActiveSettings(bool value)
    {
        settingsPanel.SetActive(value);
    }

    public void SetActiveCredit(bool value)
    {
        creditPanel.SetActive(value);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
