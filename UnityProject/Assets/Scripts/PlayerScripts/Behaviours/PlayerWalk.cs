using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for handling Player's Walk state
public class PlayerWalk : StateMachineBehaviour
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
        // Check if Player is still walking
        if (_playerCtrl.moveDir != Vector2.zero && _playerCtrl.canMove)
        {
            _playerCtrl.Turn(); // Turn Character to Direction of Movement
            _playerCtrl.Move(); // Move Character

            // Player inputs "Roll", Transition to "Roll" state
            if (Input.GetButtonDown("Roll") && _playerCtrl.canMove)
            {
                // Set trigger for state Transition to "Melee Attack"
                animator.SetTrigger("Roll");
            }
            // Player inputs "MeleeAttack", Transition to "Melee Attack" state
            else if (Input.GetButtonDown("MeleeAttack") && _playerCtrl.canAttack)
            {
                // Set trigger for state Transition to "Melee Attack"
                animator.SetTrigger("MeleeAtk");
            }
            // Player inputs "RangedAttack", Transition to Ranged Attack state
            else if (Input.GetButtonDown("RangedAttack") && _playerCtrl.canAttack)
            {
                // Set trigger for state Transition to "Ranged Attack"
                animator.SetTrigger("RangedAtk");
            }
        }
        // Player has stopped walking
        else
        {
            // Set bool for state Transition to "Idle"
            animator.SetBool("isWalking", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Stop Player's Movement
        _playerCtrl.rb.velocity = Vector2.zero;
        // Make sure isWalking = false when Exit this State
        animator.SetBool("isWalking", false);
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
