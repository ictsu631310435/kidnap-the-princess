using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// Base Scripts for handling Enemy's Idle state
public class EnemyIdle : StateMachineBehaviour
{
    #region Data Members
    [HideInInspector] public Enemy enemy;
    private AIDestinationSetter _aiDestination;
    #endregion

    #region Unity Callbacks
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get Components
        enemy = animator.GetComponent<Enemy>();
        _aiDestination = enemy.aiDestination;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If Player is inside detection range, target Player
        if (enemy.playerDistance <= enemy.detectRange)
        {
            // Set target to Player
            _aiDestination.target = enemy.player;
        }
        // If targeted Player, transition to "Chase" state
        if (_aiDestination.target == enemy.player)
        {
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
    #endregion
}
