using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script for managing tutorial
public class TutorialManager : MonoBehaviour
{
    #region Data Members
    public GameObject tutorialPanel;

    public RawImage tutorialImage;

    public Texture[] tutorialTextures;
    private int spriteIndex;
    #endregion

    #region Unity Callbacks
    // Start is called before the first frame update
    void Start()
    {
        // Initialize spriteIndex
        spriteIndex = 0;
        // Stop the game for tutorial messages
        Time.timeScale = 0;
        GameManager.Instance.gamePaused = true;
    }
    #endregion

    #region Methods
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
        // Resume the game
        Time.timeScale = 1;
        GameManager.Instance.gamePaused = false;
        // Destroy tutorialPanel and this GameObject
        Destroy(tutorialPanel);
        Destroy(gameObject);
    }
    #endregion
}
