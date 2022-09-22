using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectHandler : MonoBehaviour
{
    [Serializable]
    public struct EffectStats
    {
        public StatusEffect statusEffect;
        public GameObject effectSource;
        public float effectDuration;
        public float nextTickTime;
    }

    public List<EffectStats> currentEffect;

    public StatusEffect[] testEffect;
    public GameObject testSource;

    private float _newDuration;
    private float _newTickTime;

    // Update is called once per frame
    void Update()
    {
        // If list is not empty, call HandleEffects
        if (currentEffect != null)
        {
            HandleEffects();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && testEffect[0] != null)
        {
            ApplyEffect(testEffect[0], testSource);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && testEffect[1] != null)
        {
            ApplyEffect(testEffect[1], testSource);
        }
    }

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
        int index = currentEffect.FindIndex(x => x.statusEffect.name == effect.name);
        // If existed, override existing status effect
        if (index != -1)
        {
            currentEffect[index] = _effectStats;
        }
        // If not, add a new one
        else
        {          
            currentEffect.Add(_effectStats);
        }
    }

    // Method for handling currently active effects
    public void HandleEffects()
    {
        // Loop through entire list
        for (int i = 0; i < currentEffect.Count; i++)
        {
            // Initial Activation
            if (currentEffect[i].effectDuration == currentEffect[i].statusEffect.duration)
            {
                // Activate Effect
                currentEffect[i].statusEffect.ActivateEffect(gameObject, currentEffect[i].effectSource);
                // Assign value to _newTickTime
                _newTickTime = currentEffect[i].nextTickTime;
            }
            else
            // Tick Activation
            if (currentEffect[i].statusEffect.tickRate > 0 && currentEffect[i].effectDuration <= currentEffect[i].nextTickTime)
            {
                // Activate Effect
                currentEffect[i].statusEffect.ActivateEffect(gameObject, currentEffect[i].effectSource);
                // Calculate nextTickTime and assign value to _newTickTime
                _newTickTime = currentEffect[i].nextTickTime - currentEffect[i].statusEffect.tickRate;
            }

            // Remove Effect when Expire 
            if (currentEffect[i].effectDuration <= 0)
            {
                RemoveEffect(i);
                continue;
            }

            // Update effectDuration and nextTickTime
            _newDuration = currentEffect[i].effectDuration - Time.deltaTime;
            EffectStats _newCurrentEffect = new()
            {
                statusEffect = currentEffect[i].statusEffect,
                effectSource = currentEffect[i].effectSource,
                effectDuration = _newDuration,
                nextTickTime = _newTickTime
            };
            // Override current one with an updated one
            currentEffect[i] = _newCurrentEffect;
        }
    }

    // Method for removing an effect
    public void RemoveEffect(int indexToRemove)
    {
        // Deactivate Effect
        currentEffect[indexToRemove].statusEffect.DeactivateEffect(gameObject);
        // Remove Effect from list
        currentEffect.RemoveAt(indexToRemove);
    }
}
