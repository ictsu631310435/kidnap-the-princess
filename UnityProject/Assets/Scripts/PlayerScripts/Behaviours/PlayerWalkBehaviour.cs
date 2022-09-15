using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for handling Player's Walk State
public class PlayerWalk : StateMachineBehaviour
{
    private PlayerController _playerCtrl; // PlayerController script

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
        if (_playerCtrl.moveDir != Vector2.zero)
        {
            _playerCtrl.Turn(); // Turn Character to MovementDirection
            _playerCtrl.Move(); // Move Character

            // Player inputs Roll, Transition to Roll State
            if (Input.GetButtonDown("Roll"))
            {
                // SetTrigger for State Transition to "Melee Attack"
                animator.SetTrigger("RollTrigger");
            }
            // Player inputs attack, Transition to Attack State
            else if (Input.GetButtonDown("MeleeAttack"))
            {
                // SetTrigger for State Transition to "Melee Attack"
                animator.SetTrigger("MeleeTrigger");
            }
        }
        // Player has stopped walking
        else
        {
            // SetBool for State Transition to "Idle"
            animator.SetBool("isWalking", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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
}
