using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCutscene : MonoBehaviour
{
    public string nextSceneName;

    void OnEnable()
    {
        // Load nextScene
        SceneManager.LoadScene(nextSceneName);
    }
}
