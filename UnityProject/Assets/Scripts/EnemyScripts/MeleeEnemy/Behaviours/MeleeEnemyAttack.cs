using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// Scripts for handling MeleeEnemy's Attack state
public class MeleeEnemyAttack : EnemyAttack
{
    #region Unity Callbacks
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get Component
        enemy = animator.GetComponent<MeleeEnemy>();

        enemy.StartCoroutine(Attack(enemy, inflictEffect, delayTime));
    }
    #endregion
}
