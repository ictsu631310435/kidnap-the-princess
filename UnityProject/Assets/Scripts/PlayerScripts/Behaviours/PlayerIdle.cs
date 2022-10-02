using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for handling Player's Idle state
public class PlayerIdle : StateMachineBehaviour
{
    #region Data Members
    private PlayerController _playerCtrl;
    private Rigidbody2D _rigidbody;
    private StatusEffectHandler _effectHandler;
    #endregion

    #region Unity Callbacks
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get Component
        _playerCtrl = animator.gameObject.GetComponent<PlayerController>();
        _rigidbody = animator.gameObject.GetComponent<Rigidbody2D>();
        _effectHandler = animator.gameObject.GetComponent<StatusEffectHandler>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!GameManager.Instance.gamePaused)
        {
            // Player inputs "Movement", Transition to "Walk" state
            if (_playerCtrl.moveDir != Vector2.zero && _playerCtrl.canMove)
            {
                // Set bool for state Transition to "Walk"
                animator.SetBool("isWalking", true);
            }
            // Player inputs "Roll", Transition to Roll State
            else if (Input.GetButtonDown("Roll") && _playerCtrl.canMove)
            {
                // Set trigger for state Transition to "Melee Attack"
                animator.SetTrigger("Roll");
            }
            // Player inputs "MeleeAttack", Transition to Melee Attack state
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
            // Player do not inputs anything and not effected by StatusEffect
            else if (_effectHandler.currentEffect.Count == 0)
            {
                // Stop Player's Movement
                _rigidbody.velocity = Vector2.zero;
            }
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
