using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public string levelSceneName;

    // Start is called before the first frame update
    void Start()
    {
        //// Load levelScene
        //SceneManager.LoadScene(levelSceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        // Load levelScene
        SceneManager.LoadScene(levelSceneName);
    }
}
