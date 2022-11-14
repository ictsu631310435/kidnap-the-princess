using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;

    public RawImage tutorialImage;

    public Texture[] tutorialTextures;
    private int spriteIndex;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize spriteIndex
        spriteIndex = 0;
        // Pause time for tutorial messages
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method for proceeding tutorial
    public void NextButton()
    {
        // If there is a tutorialTexture left, go to the next one
        if (spriteIndex <= tutorialTextures.Length - 1)
        {
            // Change current texture to the next texture
            tutorialImage.texture = tutorialTextures[spriteIndex];
            // Increase spriteIndex
            spriteIndex++;
        }
        // If there is no tutorialTexture left, close tutorial
        else
        {
            CloseTutorial();
        }
    }

    // Method for closing tutorialPanel
    public void CloseTutorial()
    {
        // Resume time
        Time.timeScale = 1;
        // Destroy tutorialPanel and this GameObject
        //Destroy(tutorialPanel);
        //Destroy(gameObject);
    }
}
