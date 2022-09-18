using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for handling Player's Idle state
public class PlayerIdle : StateMachineBehaviour
{
    #region Data Members
    private PlayerController _playerCtrl;
    #endregion

    #region Unity Callbacks
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get Component
        _playerCtrl = animator.gameObject.GetComponent<PlayerController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Stop Player's Movement
        _playerCtrl.rb.velocity = Vector2.zero;

        // Player inputs "Movement", Transition to "Walk" state
        if (_playerCtrl.moveDir != Vector2.zero)
        {
            // Set bool for state Transition to "Walk"
            animator.SetBool("isWalking", true);
        }
        // Player inputs "Roll", Transition to Roll State
        else if (Input.GetButtonDown("Roll"))
        {
            // Set trigger for state Transition to "Melee Attack"
            animator.SetTrigger("RollTrigger");
        }
        // Player inputs "MeleeAttack", Transition to Attack State
        else if(Input.GetButtonDown("MeleeAttack"))
        {
            // Set trigger for state Transition to "Melee Attack"
            animator.SetTrigger("MeleeTrigger");
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
