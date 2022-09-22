using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
    public new string name;
    public float duration;
    public float tickRate;

    public abstract void ActivateEffect(GameObject target, GameObject source);

    public abstract void DeactivateEffect(GameObject target);
}
