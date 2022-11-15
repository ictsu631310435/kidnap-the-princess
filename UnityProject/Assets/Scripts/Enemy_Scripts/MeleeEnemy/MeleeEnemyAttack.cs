using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for MeleeEnemy's Attack state
public class MeleeEnemyAttack : EnemyAttack
{
    #region Unity Callbacks
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get Component
        enemy = animator.GetComponent<Enemy>();

        enemy.charaAnimator.transform.localPosition = new Vector3(0, 0, -0.45f);

        // Attack after a delay
        enemy.StartCoroutine(Attack(inflictEffect, delayTime));
    }
    #endregion
}
