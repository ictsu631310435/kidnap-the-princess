using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Class for controlling Trigger that can only be activated by Player
public class TriggerByPlayer : MonoBehaviour
{
    [Tooltip("Subscribed class's method that will be called")]
    public UnityEvent OnPlayerEntered;

    // OnTriggerEnter2D is called when the Collider2D other enter the trigger (2D physics only)
    void OnTriggerEnter(Collider other)
    {
        // Check for Player
        if (other.CompareTag("Player"))
        {
            OnPlayerEntered?.Invoke(); // Call method of subcribed class
            Destroy(this.gameObject); // Destroy this gameObject after finished event
        }
    }
}
