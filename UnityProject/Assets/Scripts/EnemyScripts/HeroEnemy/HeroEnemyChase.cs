using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

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
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Player in detectRange, chase after target
        if (_heroEnemy.playerDistance <= _heroEnemy.detectRange)
        {
            // If target is not set, set target to Player
            if (aiDestination.target == null)
            {
                aiDestination.target = _heroEnemy.player;
            }

            if (_heroEnemy.playerDistance <= _heroEnemy.rangedAttackRange && Time.time > _heroEnemy.nextAttackTime)
            {
                aiDestination.target = animator.gameObject.transform;
                // Set bool for state transition to "Attack"
                animator.SetTrigger("Attack");
            }

            // Reached destination enter "inCombat" state
            if (aiPath.reachedDestination)
            {
                //Debug.Log("Stop chase: in Attack Range");
                // Set bool for state transition to "inCombat"
                animator.SetBool("inCombat", true);
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
            }
        }
    }
    #endregion
}
