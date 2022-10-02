using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollCollider : MonoBehaviour
{
    #region Data Members
    private Collider2D _collider;
    public LayerMask enemyLayer;
    public StatusEffect inflictEffect;
    private PlayerController _player;
    #endregion

    #region Unity Callbacks
    // Awake is called when the script instance is being loaded
    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _player = GetComponentInParent<PlayerController>();
    }

    // OnTriggerEnter2D is called when the Collider2D collision enter the trigger
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_collider.IsTouchingLayers(enemyLayer) && collision.TryGetComponent(out StatusEffectHandler _effectHandler))
        {
            _effectHandler.ApplyEffect(inflictEffect, _player.gameObject);
        }
    }
    #endregion
}
