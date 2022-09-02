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
        //playerCtrl.rb2d.velocity = Vector2.zero;
        _playerCtrl.rb.velocity = Vector3.zero;

        // Set AttackMode Bool 
        animator.SetBool("isRange", _playerCtrl.isRange);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Player inputs movement, Transition to Walk State
        if (_playerCtrl.moveDir != Vector3.zero)
        {
            // SetBool for State Transition to "Walk"
            animator.SetBool("isWalking", true);
        }
        // Player inputs attack, Transition to Attack State
        else if(Input.GetButtonDown("Attack"))
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
