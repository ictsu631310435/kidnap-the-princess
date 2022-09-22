using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// Script for handling Melee Enemy's Chase state
public class MeleeEnemyChase : StateMachineBehaviour
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
        _aiPath = animator.GetComponent<AIPath>();
        _aiDestination = animator.GetComponent<AIDestinationSetter>();

        if (_meleeEnemy.inCombat)
        {
            _aiPath.slowdownDistance = _meleeEnemy.attackRange * 3;
            _aiPath.endReachedDistance = _meleeEnemy.attackRange;
        }
        else
        {
            _aiPath.slowdownDistance = _meleeEnemy.standbyRange * 3;
            _aiPath.endReachedDistance = _meleeEnemy.standbyRange;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_meleeEnemy.playerDistance <= _meleeEnemy.detectRange)
        {
            if (_aiDestination.target == null)
            {
                _aiDestination.target = _meleeEnemy.player;
            }

            if (_aiPath.reachedDestination)
            {
                if (_meleeEnemy.inCombat)
                {
                    Debug.Log("Stop chase: in Attack Range");
                    animator.SetBool("inCombat", true);
                }
                else
                {
                    Debug.Log("Stop chase: Reached destination");
                    animator.SetBool("onStandby", true);
                }
            }
        }
        else
        {
            if (_aiDestination.target != null)
            {
                _aiDestination.target = null;

                _meleeEnemy.inCombat = false;
            }

            if (_aiPath.reachedDestination)
            {
                Debug.Log("Stop chase: Player lost");
                animator.SetBool("isChasing", false);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isChasing", false);

        _aiDestination.target = null;
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
