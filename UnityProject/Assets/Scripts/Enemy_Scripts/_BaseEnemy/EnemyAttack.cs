using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base Scripts for handling Enemy's Attack state
public class EnemyAttack : StateMachineBehaviour
{
    #region Data Members
    [HideInInspector] public Enemy enemy;
    [Tooltip("Delay time before calling Attack method.")]
    public float delayTime;
    public StatusEffect inflictEffect;
    #endregion

    #region Methods
    // Method for attacking after time delay
    public IEnumerator Attack(float waitTime)
    {
        // Wait for (waitTime)
        yield return new WaitForSeconds(waitTime);

        // Call Attack method
        enemy.Attack();
    }

    // Method for attacking after time delay (Apply StatusEffect)
    public IEnumerator Attack(StatusEffect statusEffect, float waitTime)
    {
        // Wait for (waitTime)
        yield return new WaitForSeconds(waitTime);

        // Call Attack method
        enemy.Attack(statusEffect);
    }
    #endregion
}
