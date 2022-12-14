using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for handling Hero Enemy's Chase state
public class HeroEnemyChase : EnemyChase
{
    private HeroEnemy _heroEnemy;

    #region Unity Callbacks
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get Components
        _heroEnemy = animator.GetComponent<HeroEnemy>();
        aiPath = _heroEnemy.aiPath;
        aiDestination = _heroEnemy.aiDestination;

        // Set time for next attack
        _heroEnemy.nextAttackTime = Time.time + _heroEnemy.timeBtwRangedAttacks;

        _heroEnemy.charaAnimator.transform.localPosition = new Vector3(0, 0, -0.45f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Player in detectRange, chase after target
        if (_heroEnemy.playerDistance <= _heroEnemy.detectRange)
        {
            // If target is Player, set target to Player
            if (aiDestination.target != _heroEnemy.player)
            {
                aiDestination.target = _heroEnemy.player;
            }

            if (_heroEnemy.playerDistance <= _heroEnemy.rangedAttackRange && Time.time > _heroEnemy.nextAttackTime)
            {
                aiDestination.target = animator.gameObject.transform;
                // Set bool for state transition to "Attack"
                animator.SetTrigger("Attack");
                _heroEnemy.charaAnimator.SetTrigger("Attack");
            }

            // Reached destination enter "inCombat" state
            if (aiPath.reachedDestination)
            {
                //Debug.Log("Stop chase: in Attack Range");
                // Set bool for state transition to "inCombat"
                animator.SetBool("inCombat", true);
                _heroEnemy.charaAnimator.SetBool("inCombat", true);
            }
        }
        // Player out of detectRange, stop chase after reached the last Player position before out of range
        else
        {
            // If target is still set to Player, set target to null
            if (aiDestination.target != null)
            {
                aiDestination.target = null;
            }

            // Stop chase after reached the last Player position before out of range
            if (aiPath.reachedDestination)
            {
                //Debug.Log("Stop chase: Player lost");
                // Set bool for state transition to "Idle"
                animator.SetBool("isChasing", false);
                _heroEnemy.charaAnimator.SetBool("isChasing", false);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Make sure isChasing = false when exit state
        animator.SetBool("isChasing", false);
        _heroEnemy.charaAnimator.SetBool("isChasing", false);
        // Make sure to not set target to anything when exit state
        aiDestination.target = null;
    }
    #endregion
}
