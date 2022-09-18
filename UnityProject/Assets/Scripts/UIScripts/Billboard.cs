using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for rotating UI to always look at camera
public class Billboard : MonoBehaviour
{
    [HideInInspector] public Transform cam; // Camera's transform

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // LateUpdate is called every frame, if the Behaviour is enabled
    void LateUpdate()
    {
        // Alway look at camera
        transform.LookAt(transform.position + cam.forward);
    }
}
