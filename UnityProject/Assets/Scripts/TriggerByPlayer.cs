using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Class for controlling Trigger that can only be activated by Player
public class TriggerByPlayer : MonoBehaviour
{
    public UnityEvent<GameObject> OnPlayerEntered; // Subscribed class's method that will be called
    public bool destroyOnTrigger;

    // OnTriggerEnter2D is called when the Collider2D other enter the trigger (2D physics only)
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check for Player
        if (other.CompareTag("Player"))
        {
            OnPlayerEntered?.Invoke(other.gameObject); // Call method of subcribed class
            // Destroy this gameObject if flaged for destroy
            if (destroyOnTrigger)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
