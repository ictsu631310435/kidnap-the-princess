using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// Script for switching cameras
public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera currentCamera;

    // Method for switching cameras
    public void SwitchCamera(CinemachineVirtualCamera newCamera)
    {
        // Disable currentCamera
        currentCamera.gameObject.SetActive(false);
        // Set priority to low to be take over by newCamera
        currentCamera.Priority = 0;

        // Enable newCamera
        newCamera.gameObject.SetActive(true);
        // Set priority to high to take over from currentCamera
        newCamera.Priority = 10;
        // Update currentCamera
        currentCamera = newCamera;
    }
}
