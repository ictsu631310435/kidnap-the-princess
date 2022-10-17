using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// Script for handling Room transitioning
public class RoomTransition : MonoBehaviour
{
    #region Data Members
    public CinemachineConfiner confiner;

    public Transform destination;
    public Collider targetBounding;
    #endregion

    #region Methods
    // Method for Room transitioning
    public void Transition(GameObject gameObject)
    {
        // Teleport player to destination in the next room
        gameObject.transform.position = destination.position;
        gameObject.transform.rotation = destination.localRotation;

        // Change BoundingVolume to the next room's BoundingVolume
        confiner.m_BoundingVolume = targetBounding;
    }
    #endregion
}
