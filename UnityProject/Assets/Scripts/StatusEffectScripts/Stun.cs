using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[CreateAssetMenu(fileName = "New Stun", menuName = "ScriptableObjects/StatusEffect/Stun")]
public class Stun : StatusEffect
{
    public override void ActivateEffect(GameObject target, GameObject source)
    {
        Debug.Log("Activate Stun");

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

    public override void DeactivateEffect(GameObject target)
    {
        Debug.Log("Deactivate KnockBack");

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
}
