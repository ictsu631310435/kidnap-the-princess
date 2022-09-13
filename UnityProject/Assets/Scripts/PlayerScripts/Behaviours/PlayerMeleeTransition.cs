using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for handling Player's Idle State
public class PlayerMeleeTransition : StateMachineBehaviour
{
    private PlayerController _playerCtrl; // PlayerController scrip

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get Component
        _playerCtrl = animator.gameObject.GetComponent<PlayerController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Player inputs MeleeAttack, Transition to Attack State
        if(Input.GetButtonDown("MeleeAttack"))
        {
            // Set Blend Tree's values
            _playerCtrl.meleeCombo++;
            animator.SetTrigger("MeleeTrigger");
            animator.SetFloat("MeleeBlend", _playerCtrl.meleeCombo * 0.5f);
            // SetTrigger for State Transition to "Melee Attack"
            animator.SetFloat("TransitionBlend", _playerCtrl.meleeCombo);
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
