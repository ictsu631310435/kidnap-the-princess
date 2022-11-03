using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

// Script for managing game settings
public class SettingsManager : MonoBehaviour
{
    #region Data Member
    public Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    private Resolution[] resolutions;
    public Vector2 minResolution;

    public AudioMixer audioMixer;
    public Slider masterVolumeSlider;
    public Slider effectVolumeSlider;
    public Slider musicVolumeSlider;
    #endregion

    #region Unity Callbacks
    // Awake is called when the script instance is being loaded
    void Awake()
    {
        #region Fullscreen Settings
        // If key is not existed in PlayerPrefs, create a new one
        if (!PlayerPrefs.HasKey("isFullscreen"))
        {
            PlayerPrefs.SetInt("isFullscreen", Screen.fullScreen ? 1 : 0);

#if UNITY_EDITOR
            Debug.Log("Created new PlayerPrefs key: isFullscreen");
            Debug.Log("SetInt isFullscreen to " + PlayerPrefs.GetInt("isFullscreen"));
#endif
        }
        // If key is already existed, get data from key
        else
        {
            // Toggle fullscreen true/false
            fullscreenToggle.isOn = PlayerPrefs.GetInt("isFullscreen") != 0;

#if UNITY_EDITOR
            Debug.Log("Loaded PlayerPrefs key: isFullscreen");
#endif
        }
        #endregion

        #region Resolution Settings
        // Get all resolutions that the moniter support
        resolutions = Screen.resolutions;
        // Clear dropdown options
        resolutionDropdown.ClearOptions();

        List<string> options = new();

        int currentResolutionIndex = resolutions.Length - 1; // High resolutions are at the end of the array
        // Add resolutions to options list
        for (int i = resolutions.Length - 1; i > 0; i--)
        {
            // Skip lower resolutions, use minResolution as minimum
            if (resolutions[i].width >= minResolution.x &&
                resolutions[i].height >= minResolution.y)
            {
                // Each option displays like "1920 x 1080"
                string option = resolutions[i].width + " x " + resolutions[i].height;
                // Add each option to the list
                options.Add(option);
            }

            // Get currentResolutionIndex
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Add options to resolutionDropdown
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();

        // If key is not existed in PlayerPrefs, create a new one
        if (!PlayerPrefs.HasKey("resolutionIndex"))
        {
            // Set resolution
            resolutionDropdown.value = currentResolutionIndex;
            // Save setting
            PlayerPrefs.SetInt("resolutionIndex", currentResolutionIndex);

#if UNITY_EDITOR
            Debug.Log("Created new PlayerPrefs key: resolutionIndex");
            Debug.Log("SetInt resolutionIndex to " + PlayerPrefs.GetInt("resolutionIndex"));
#endif
        }
        // If key is already existed, get data from key
        else
        {
            // Set resolution
            resolutionDropdown.value = PlayerPrefs.GetInt("resolutionIndex");

#if UNITY_EDITOR
            Debug.Log("Loaded PlayerPrefs key: resolutionIndex");
#endif
        }
        #endregion

        #region Volume Settings
        #region Master Volume
        if (!PlayerPrefs.HasKey("masterVolume"))
        {
            PlayerPrefs.SetFloat("masterVolume", 0);

#if UNITY_EDITOR
            Debug.Log("Created new PlayerPrefs key: masterVolume");
            Debug.Log("SetFloat masterVolume to " + PlayerPrefs.GetFloat("masterVolume"));
#endif
        }
        // If key is already existed, get data from key
        else
        {
            // Set masterVolume
            masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume");

#if UNITY_EDITOR
            Debug.Log("Loaded PlayerPrefs key: masterVolume");
#endif
        }
        #endregion

        #region Sound Effect Volume
        if (!PlayerPrefs.HasKey("effectVolume"))
        {
            PlayerPrefs.SetFloat("effectVolume", 0);

#if UNITY_EDITOR
            Debug.Log("Created new PlayerPrefs key: effectVolume");
            Debug.Log("SetFloat effectVolume to " + PlayerPrefs.GetFloat("effectVolume"));
#endif
        }
        // If key is already existed, get data from key
        else
        {
            // Set effectVolume
            effectVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume");

#if UNITY_EDITOR
            Debug.Log("Loaded PlayerPrefs key: effectVolume");
#endif
        }
        #endregion

        #region Music Volume
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 0);

#if UNITY_EDITOR
            Debug.Log("Created new PlayerPrefs key: musicVolume");
            Debug.Log("SetFloat musicVolume to " + PlayerPrefs.GetFloat("musicVolume"));
#endif
        }
        // If key is already existed, get data from key
        else
        {
            // Set effectVolume
            musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");

#if UNITY_EDITOR
            Debug.Log("Loaded PlayerPrefs key: musicVolume");
#endif
        }
        #endregion
        #endregion
    }
    #endregion

    #region Methods
    // Method for setting Resolution
    public void SetResolution(int index)
    {
        // Get resolution from index
        Resolution resolution = resolutions[(resolutions.Length - 1) - index];
        // Set resolution
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        // Save setting
        PlayerPrefs.SetInt("resolutionIndex", index);

#if UNITY_EDITOR
        Debug.Log("resolution = " + resolution);
        Debug.Log("SetInt resolutionIndex to " + PlayerPrefs.GetInt("resolutionIndex"));
#endif
    }

    // Method for toggling fullscreen/windowed
    public void SetFullscreen(bool value)
    {
        // Toggle fullscreen true/false
        Screen.fullScreen = value;
        // Save setting
        PlayerPrefs.SetInt("isFullscreen", value ? 1 : 0);

#if UNITY_EDITOR
        Debug.Log("fullsScreen = " + value);
        Debug.Log("SetInt isFullscreen to " + PlayerPrefs.GetInt("isFullscreen"));
#endif
    }

    // Method for setting Master Volume
    public void SetMasterVolume(float value)
    {
        // Set masterVolume channel volume in audioMixer
        audioMixer.SetFloat("masterVolume", value);
        // Save setting
        PlayerPrefs.SetFloat("masterVolume", value);

#if UNITY_EDITOR
        Debug.Log("masterVolume = " + value);
        Debug.Log("SetFloat masterVolume to " + PlayerPrefs.GetFloat("masterVolume"));
#endif
    }

    // Method for setting Sound Effect Volume
    public void SetEffectVolume(float value)
    {
        // Set effectVolume channel volume in audioMixer
        audioMixer.SetFloat("effectVolume", value);
        // Save setting
        PlayerPrefs.SetFloat("effectVolume", value);

#if UNITY_EDITOR
        Debug.Log("effectVolume = " + value);
        Debug.Log("SetFloat effectVolume to " + PlayerPrefs.GetFloat("effectVolume"));
#endif
    }

    // Method for setting Music Volume
    public void SetMusicVolume(float value)
    {
        // Set musicVolume channel volume in audioMixer
        audioMixer.SetFloat("musicVolume", value);
        // Save setting
        PlayerPrefs.SetFloat("musicVolume", value);

#if UNITY_EDITOR
        Debug.Log("musicVolume = " + value);
        Debug.Log("SetFloat musicVolume to " + PlayerPrefs.GetFloat("musicVolume"));
#endif
    }
    #endregion
}
