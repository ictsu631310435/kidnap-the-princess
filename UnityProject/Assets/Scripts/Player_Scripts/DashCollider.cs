using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for controlling DashCollider
public class DashCollider : MonoBehaviour
{
    #region Data Members
    public LayerMask enemyLayer;
    public StatusEffect inflictEffect;
    private PlayerController _player;
    #endregion

    #region Unity Callbacks
    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Get Component
        _player = GetComponentInParent<PlayerController>();
    }

    // OnTriggerEnter2D is called when the Collider2D collision enter the trigger
    void OnTriggerEnter2D(Collider2D collision)
    {
        /* Check if collided with an enemy and enemy has StatusEffectHandler then apply KnockBack StatusEffect
           (Need to convert layer to layermask using the binary left-shift operator to left-shift 1 by the layer)*/
        if (1 << collision.gameObject.layer == enemyLayer.value
            && collision.TryGetComponent(out StatusEffectHandler _effectHandler))
        {
            // Apply KnockBack StatusEffect
            _effectHandler.ApplyEffect(inflictEffect, _player.gameObject);
        }
    }
    #endregion
}
