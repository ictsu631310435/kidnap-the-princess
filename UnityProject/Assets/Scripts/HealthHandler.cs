using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Class for handling Health
public class HealthHandler : MonoBehaviour
{
    #region Data Members
    public int maxHealth;
    [field: SerializeField] public int CurrentHealth { get; private set; } // Other can readonly

    // Events
    public UnityEvent<int> OnHealthSet;
    public UnityEvent<int> OnHealthChanged;
    public UnityEvent OnHealthDepleted;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Set Initial Health
        CurrentHealth = maxHealth;

        // Invoke HealthSet event
        OnHealthSet?.Invoke(CurrentHealth);
    }


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
}
