using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for handling GameObject StatusEffect
public class StatusEffectHandler : MonoBehaviour
{
    #region Data Members
    // Structure for holding StatusEffect statistics
    [Serializable] public struct EffectStats
    {
        public StatusEffect statusEffect;
        public GameObject effectSource;
        public float effectDuration;
        public float nextTickTime;
    }

    public List<EffectStats> activeEffects;

    private float _newDuration;
    private float _newTickTime;
    #endregion

    #region Unity Callbacks
    // Update is called once per frame
    void Update()
    {
        // If list is not empty, call HandleEffects
        if (activeEffects != null)
        {
            HandleEffects();
        }
    }
    #endregion

    #region Methods
    // Method for applying an effect
    public void ApplyEffect(StatusEffect effect, GameObject source)
    {
        // Declare new EffectStats
        EffectStats _effectStats = new()
        {
            statusEffect = effect,
            effectSource = source,
            effectDuration = effect.duration,
            nextTickTime = effect.duration - effect.tickRate
        };

        // Check if status effect is already existed
        int index = activeEffects.FindIndex(x => x.statusEffect.name == effect.name);
        // If existed, override existing status effect
        if (index != -1)
        {
            activeEffects[index] = _effectStats;
        }
        // If not, add a new one
        else
        {          
            activeEffects.Add(_effectStats);
        }
    }

    // Method for handling currently active effects
    public void HandleEffects()
    {
        
        // Loop through entire list
        for (int i = 0; i < activeEffects.Count; i++)
        {
            // Initial Activation
            if (activeEffects[i].effectDuration == activeEffects[i].statusEffect.duration)
            {
                // Activate Effect
                if (activeEffects[i].effectSource != null)
                {
                    activeEffects[i].statusEffect.ActivateEffect(gameObject, activeEffects[i].effectSource);
                }
                else
                {
                    activeEffects[i].statusEffect.ActivateEffect(gameObject);
                }
                
                // Assign value to _newTickTime
                _newTickTime = activeEffects[i].nextTickTime;
            }
            // Tick Activation
            else if(activeEffects[i].statusEffect.tickRate > 0 && activeEffects[i].effectDuration <= activeEffects[i].nextTickTime)
            {
                // Activate Effect
                if (activeEffects[i].effectSource != null)
                {
                    activeEffects[i].statusEffect.ActivateEffect(gameObject, activeEffects[i].effectSource);
                }
                else
                {
                    activeEffects[i].statusEffect.ActivateEffect(gameObject);
                }
                // Calculate nextTickTime and assign value to _newTickTime
                _newTickTime = activeEffects[i].nextTickTime - activeEffects[i].statusEffect.tickRate;
            }

            // Update effectDuration and nextTickTime
            _newDuration = activeEffects[i].effectDuration - Time.deltaTime;
            EffectStats _newCurrentEffect = new()
            {
                statusEffect = activeEffects[i].statusEffect,
                effectSource = activeEffects[i].effectSource,
                effectDuration = _newDuration,
                nextTickTime = _newTickTime
            };
            // Override current one with an updated one
            activeEffects[i] = _newCurrentEffect;

            // Remove an Effect when it expired 
            if (activeEffects[i].effectDuration <= 0)
            {
                RemoveEffect(i);
            }
        }
    }

    // Method for removing an effect
    public void RemoveEffect(int indexToRemove)
    {
        // Deactivate Effect
        activeEffects[indexToRemove].statusEffect.DeactivateEffect(gameObject);
        // Remove Effect from list
        activeEffects.RemoveAt(indexToRemove);
    }
    #endregion
}
