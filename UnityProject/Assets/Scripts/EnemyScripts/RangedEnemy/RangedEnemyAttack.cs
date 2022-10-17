using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for Ranged Enemy's Attack state
public class RangedEnemyAttack : EnemyAttack
{
    #region Unity Callbacks
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get Component
        enemy = animator.GetComponent<Enemy>();

        // Attack after a delay
        enemy.StartCoroutine(Attack(delayTime));
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Turn toward Player for more accurate shot
        enemy.Turn();
    }
    #endregion
}
