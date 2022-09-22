using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New KnockBack", menuName = "ScriptableObjects/StatusEffect/KnockBack")]
public class KnockBack : StatusEffect
{
    float force;

    public override void ActivateEffect(GameObject target, GameObject source)
    {
        Debug.Log("Activate KnockBack");
    }

    public override void DeactivateEffect(GameObject target)
    {
        Debug.Log("Deactivate KnockBack");
    }
}
