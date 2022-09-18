using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script for handling HealthBar
public class HealthBar : MonoBehaviour
{
    [HideInInspector] public Slider slider;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void InitHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void SetHealth(int newHealth)
    {
        slider.value = newHealth;
    }
}
