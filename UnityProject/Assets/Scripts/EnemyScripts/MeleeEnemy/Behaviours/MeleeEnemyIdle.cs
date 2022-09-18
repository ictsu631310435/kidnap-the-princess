using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// Scripts for handling MeleeEnemy's Idle state
public class MeleeEnemyIdle : StateMachineBehaviour
{
    #region Data Members
    private MeleeEnemy _meleeEnemy;
    private Rigidbody2D _rb;
    private AIDestinationSetter _aiDestination;
    #endregion

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get Components
        _meleeEnemy = animator.GetComponent<MeleeEnemy>();
        _rb = animator.GetComponent<Rigidbody2D>();
        _aiDestination = _meleeEnemy.aiDestination;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If Player is inside detection range, target Player
        if (_meleeEnemy.playerDistance <= _meleeEnemy.detectRange)
        {
            _aiDestination.target = _meleeEnemy.player;
        }
        // If targeted Player, transition to "Chase" state
        if (_aiDestination.target == _meleeEnemy.player)
        {
            //Debug.Log("Chase");
            // Set bool for state Transition to "Chase"
            animator.SetBool("isChasing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
}
