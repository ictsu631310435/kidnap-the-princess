using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MeleeEnemyInCombat : StateMachineBehaviour
{
    #region Data Members
    private MeleeEnemy _meleeEnemy;
    private AIPath _aiPath;
    private AIDestinationSetter _aiDestination;
    #endregion

    #region Unity Callbacks
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get Components
        _meleeEnemy = animator.GetComponent<MeleeEnemy>();
        _aiDestination = _meleeEnemy.aiDestination;

        _meleeEnemy.nextAttackTime = Time.time + _meleeEnemy.timeBtwAttacks;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_meleeEnemy.playerDistance > _meleeEnemy.attackRange * 2)
        {
            Debug.Log("Player out of attackRange");
            _aiDestination.target = _meleeEnemy.player;
            animator.SetBool("isChasing", true);
        }
        else
        {
            if (Time.time > _meleeEnemy.nextAttackTime)
            {
                Debug.Log("Attack");
                _meleeEnemy.nextAttackTime = Time.time + _meleeEnemy.timeBtwAttacks;
                animator.SetTrigger("Attack");
            }
            else
            {
                _meleeEnemy.Turn();
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("inCombat", false);
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
