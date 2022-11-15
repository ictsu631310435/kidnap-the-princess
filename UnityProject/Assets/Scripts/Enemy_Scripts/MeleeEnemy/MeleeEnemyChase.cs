using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// Script for handling Melee Enemy's Chase state
public class MeleeEnemyChase : EnemyChase
{
    private MeleeEnemy _meleeEnemy;

    #region Unity Callbacks
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get Components
        _meleeEnemy = animator.GetComponent<MeleeEnemy>();
        aiPath = _meleeEnemy.aiPath;
        aiDestination = _meleeEnemy.aiDestination;

        _meleeEnemy.charaAnimator.transform.localPosition = new Vector3(0, 0, -0.45f);

        // Change slowdownDistance and endReachedDistance base on entered combat or not
        if (_meleeEnemy.inCombat)
        {   // If entered combat, slow down and stop when target in attackRange
            aiPath.slowdownDistance = _meleeEnemy.attackRange * 3;
            aiPath.endReachedDistance = _meleeEnemy.attackRange;
        }
        else
        {
            // If not entered combat, slow down and stop when target in standbyRange
            aiPath.slowdownDistance = _meleeEnemy.standbyRange * 3;
            aiPath.endReachedDistance = _meleeEnemy.standbyRange;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Player in detectRange, chase after target
        if (_meleeEnemy.playerDistance <= _meleeEnemy.detectRange)
        {
            // If target is not set, set target to Player
            if (aiDestination.target == null)
            {
                aiDestination.target = _meleeEnemy.player;
            }

            // Reached destination prepare to enter next state
            if (aiPath.reachedDestination)
            {
                // If can enter combat, enter inCombat state
                if (_meleeEnemy.inCombat)
                {
                    //Debug.Log("Stop chase: in Attack Range");
                    // Set bool for state transition to "inCombat"
                    animator.SetBool("inCombat", true);
                    _meleeEnemy.charaAnimator.SetBool("inCombat", true);
                }
                // If  can not, enter onStandby state
                else
                {
                    //Debug.Log("Stop chase: Reached destination");
                    // Set bool for state transition to "Standby"
                    animator.SetBool("onStandby", true);
                    _meleeEnemy.charaAnimator.SetBool("onStandby", true);
                }
            }
        }
        // Player out of detectRange, stop chase after reached the last Player position before out of range
        else
        {
            // If target is still set to Player, set target to null
            if (aiDestination.target != null)
            {
                aiDestination.target = null;
                // Can not enter combat
                _meleeEnemy.inCombat = false;
            }

            // Stop chase after reached the last Player position before out of range
            if (aiPath.reachedDestination)
            {
                //Debug.Log("Stop chase: Player lost");
                // Set bool for state transition to "Idle"
                animator.SetBool("isChasing", false);
                _meleeEnemy.charaAnimator.SetBool("isChasing", false);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Make sure isChasing = false when exit state
        animator.SetBool("isChasing", false);
        _meleeEnemy.charaAnimator.SetBool("isChasing", false);
        // Make sure to not set target to anything when exit state
        aiDestination.target = null;
    }
    #endregion
}
