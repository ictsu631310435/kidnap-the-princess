using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for handling Player's Idle State
public class PlayerIdleBehaviour : StateMachineBehaviour
{
    private PlayerController _playerCtrl; // PlayerController scrip

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get Component
        _playerCtrl = animator.gameObject.GetComponent<PlayerController>();

        // Stop Player's Movement
        //_playerCtrl.rb.velocity = Vector2.zero;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Stop Player's Movement
        _playerCtrl.rb.velocity = Vector2.zero;

        // Player inputs Movement, Transition to Walk State
        if (_playerCtrl.moveDir != Vector2.zero)
        {
            // SetBool for State Transition to "Walk"
            animator.SetBool("isWalking", true);
        }
        // Player inputs Roll, Transition to Roll State
        else if (Input.GetButtonDown("Roll"))
        {
            // SetTrigger for State Transition to "Melee Attack"
            animator.SetTrigger("Roll");
        }
        // Player inputs MeleeAttack, Transition to Attack State
        else if(Input.GetButtonDown("MeleeAttack"))
        {
            // SetTrigger for State Transition to "Melee Attack"
            animator.SetTrigger("Attack");
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
