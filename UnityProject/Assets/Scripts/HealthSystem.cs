using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class HealthEvent : UnityEvent<int> { }

public class HealthSystem : MonoBehaviour
{
    public int maxHealth;
    [field: SerializeField] public int CurrentHealth { get; private set; } // Other can readonly

    [Tooltip("Subscribed class's method that will be called when HealthSet")]
    public HealthEvent OnHealthSet;
    [Tooltip("Subscribed class's method that will be called when HealthChanged")]
    public HealthEvent OnHealthChanged;
    [Tooltip("Subscribed class's method that will be called when HealthDepleted")]
    public UnityEvent OnHealthDepleted;

    // Start is called before the first frame update
    void Start()
    {
        // Set Initial Health
        CurrentHealth = maxHealth;

        // Invoke HealthSet event
        OnHealthSet?.Invoke(CurrentHealth);
    }

    // Update is called once per frame
    void Update()
    {

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
