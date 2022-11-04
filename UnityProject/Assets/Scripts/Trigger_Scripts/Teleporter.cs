using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for object Teleporter
public class Teleporter : MonoBehaviour
{
    public Transform destination;

    [Tooltip("Stun for holding the target in place while teleporting (can be left null, if do not want to apply effect).")]
    public StatusEffect stunEffect;

    // Method for teleporting an object
    public void Teleport(GameObject target)
    {
        // Apply a little Stun to hold the target in place for a bit
        if (stunEffect != null && target.TryGetComponent(out StatusEffectHandler _effectHandler))
        {
            _effectHandler.ApplyEffect(stunEffect, gameObject);
        }

        // Set target's position to position of destination
        target.transform.position = destination.position;
        // Set target's rotation to rotation of destination
        target.transform.rotation = destination.localRotation;
    }
}
