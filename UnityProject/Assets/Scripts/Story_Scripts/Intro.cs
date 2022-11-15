using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public string levelSceneName;

    void OnEnable()
    {
        // Load levelScene
        SceneManager.LoadScene(levelSceneName);
    }
}
