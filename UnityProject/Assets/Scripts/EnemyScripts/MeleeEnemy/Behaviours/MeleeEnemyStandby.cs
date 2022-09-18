using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// Script handling Melee Enemy's Standby state
public class MeleeEnemyStandby : StateMachineBehaviour
{
    #region Data Members
    private MeleeEnemy _meleeEnemy;
    private Rigidbody2D _rb;
    private AIPath _aiPath;
    private AIDestinationSetter _aiDestination;
    #endregion

    #region Unity Callbacks
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get Components
        _meleeEnemy = animator.GetComponent<MeleeEnemy>();
        _rb = animator.GetComponent<Rigidbody2D>();
        _aiPath = animator.GetComponent<AIPath>();
        _aiDestination = _meleeEnemy.aiDestination;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_meleeEnemy.playerDistance <= _meleeEnemy.standbyRange)
        {
            if (_meleeEnemy.inCombatNum < _meleeEnemy.maxInCombat)
            {
                _meleeEnemy.inCombat = true;
                _aiPath.slowdownDistance = _meleeEnemy.combatRange * 3;
                _aiPath.endReachedDistance = _meleeEnemy.combatRange;

                Debug.Log("Start Chase: Get in to fight");
                animator.SetBool("isChasing", true);
            }
        }
        else
        {
            Debug.Log("Start Chase: Player out of standby range");
            animator.SetBool("isChasing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("onStandby", false);

        _aiDestination.target = _meleeEnemy.player;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
    #endregion
}
