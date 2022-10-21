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
        currentCamera.gameObject.SetActive(false);
        currentCamera.Priority = 0;

        newCamera.gameObject.SetActive(true);
        newCamera.Priority = 10;
        currentCamera = newCamera;
    }
}
