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

    #region Unity Callbacks
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get Component
        enemy = animator.GetComponent<Enemy>();

        enemy.StartCoroutine(Attack(enemy, delayTime));
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.Turn();
    }
    #endregion

    #region Methods
    // Method for attacking after time delay
    public IEnumerator Attack(Enemy enemy, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        enemy.Attack();
    }

    public IEnumerator Attack(Enemy enemy, StatusEffect statusEffect, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        enemy.Attack(statusEffect);
    }
    #endregion
}
