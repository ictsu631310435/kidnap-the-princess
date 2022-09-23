using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[CreateAssetMenu(fileName = "New KnockBack", menuName = "ScriptableObjects/StatusEffect/KnockBack")]
public class KnockBack : StatusEffect
{
    public float force;

    public override void ActivateEffect(GameObject target, GameObject source)
    {
        Debug.Log("Activate KnockBack");

        if (target.TryGetComponent(out AIPath _aiPath))
        {
            _aiPath.canMove = false;
        }

        if (target.TryGetComponent(out PlayerController _player))
        {
            _player.canMove = false;
            _player.canAttack = false;
        }

        Vector3 _dir = (target.transform.position - source.transform.position).normalized;
        if (target.TryGetComponent(out Rigidbody2D _rb))
        {
            _rb.velocity = Vector2.zero;
            _rb.freezeRotation = true;
            _rb.AddForce(_dir * force);
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

        if (target.TryGetComponent(out Rigidbody2D _rb))
        {
            _rb.velocity = Vector2.zero;

            if (target.TryGetComponent(out Enemy _enemy))
            {
                _rb.freezeRotation = false;
            }
        }
    }
}
