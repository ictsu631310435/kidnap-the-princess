using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Script for handling Health
public class HealthHandler : MonoBehaviour
{
    #region Data Members
    public int maxHealth;
    [field: SerializeField] public int CurrentHealth { get; private set; } // Other can readonly

    // Events
    public UnityEvent<int> OnHealthInitialized;
    public UnityEvent<int> OnHealthChanged;
    public UnityEvent OnHealthDepleted;
    #endregion

    #region Unity Callbacks
    // Start is called before the first frame update
    void Start()
    {
        // Set Initial Health
        CurrentHealth = maxHealth;

        // Invoke HealthSet event
        OnHealthInitialized?.Invoke(maxHealth);
    }
    #endregion

    #region Methods
    // Method for change health's value
    public void ChangeHealth(int amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth); // Health cannot go below 0

        // Invoke HealthChanged event
        OnHealthChanged?.Invoke(CurrentHealth);

        // Check if Health reach 0
        if (CurrentHealth == 0)
        {
            // Invoke HealthDepleted event
            OnHealthDepleted?.Invoke();
        }
    }
    #endregion
}
