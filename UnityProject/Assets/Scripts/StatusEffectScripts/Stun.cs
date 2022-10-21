using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// Script for Stun StatusEffect
[CreateAssetMenu(fileName = "New Stun", menuName = "ScriptableObjects/StatusEffect/Stun")]
public class Stun : StatusEffect
{
    #region Methods
    public Stun()
    {
        name = "Stun";
    }

    #region Inherited
    // Method for activating effect
    // With source
    public override void ActivateEffect(GameObject target, GameObject source)
    {
        ActivateEffect(target);
    }
    // Without source
    public override void ActivateEffect(GameObject target)
    {
        if (target.TryGetComponent(out AIPath _aiPath))
        {
            _aiPath.canMove = false;
        }

        if (target.TryGetComponent(out PlayerController _player))
        {
            _player.canMove = false;
            _player.canAttack = false;
        }

        if (target.TryGetComponent(out Rigidbody2D _rb))
        {
            _rb.velocity = Vector2.zero;
            _rb.freezeRotation = true;
        }
    }

    // Method for deactivating effect
    public override void DeactivateEffect(GameObject target)
    {
        if (target.TryGetComponent(out AIPath _aiPath))
        {
            _aiPath.canMove = true;
        }

        if (target.TryGetComponent(out PlayerController _player))
        {
            _player.canMove = true;
            _player.canAttack = true;
        }

        if (target.TryGetComponent(out Enemy _enemy) && target.TryGetComponent(out Rigidbody2D _rb))
        {
            _rb.freezeRotation = false;
        }
    }
    #endregion
    #endregion
}
