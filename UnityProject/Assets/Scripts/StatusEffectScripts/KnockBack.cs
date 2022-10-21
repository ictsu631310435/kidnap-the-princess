using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// Script for KnockBack StatusEffect
[CreateAssetMenu(fileName = "New KnockBack", menuName = "ScriptableObjects/StatusEffect/KnockBack")]
public class KnockBack : StatusEffect
{
    public float force;

    #region Methods
    public KnockBack()
    {
        name = "KnockBack";
    }

    #region Inherited
    // Method for activating effect
    // With source
    public override void ActivateEffect(GameObject target, GameObject source)
    {
        Vector3 _dir = (target.transform.position - source.transform.position).normalized;
        KnockbackEffect(target, _dir);
    }
    // Without source
    public override void ActivateEffect(GameObject target)
    {
        KnockbackEffect(target, Vector2.down);
    }

    // Method for deactivating effect
    public override void DeactivateEffect(GameObject target)
    {
        if (target.TryGetComponent(out AIPath _aiPath))
        {
            _aiPath.canMove = true;
        }
        else if (target.TryGetComponent(out PlayerController _player))
        {
            _player.canMove = true;
            _player.canAttack = true;
        }

        if (target.TryGetComponent(out Rigidbody2D _rb))
        {
            _rb.velocity = Vector2.zero;

            if (target.TryGetComponent(out Enemy _enemy))
            {
                _rb.freezeRotation = false;
            }
        }
    }
    #endregion

    // Method for KnockBack effect
    public void KnockbackEffect(GameObject target, Vector3 direction)
    {
        if (target.TryGetComponent(out AIPath _aiPath))
        {
            _aiPath.canMove = false;
        }
        else if (target.TryGetComponent(out PlayerController _player))
        {
            _player.canMove = false;
            _player.canAttack = false;
        }

        if (target.TryGetComponent(out Rigidbody2D _rb))
        {
            _rb.velocity = Vector2.zero;
            _rb.freezeRotation = true;
            _rb.AddForce(direction * force, ForceMode2D.Impulse);
        }
    }
    #endregion
}
