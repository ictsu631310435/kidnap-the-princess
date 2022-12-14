using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for handling Player's Walk state
public class PlayerWalk : StateMachineBehaviour
{
    #region Data Members
    private PlayerController _playerCtrl;
    private Rigidbody2D _rigidbody;
    #endregion

    #region Unity Callbacks
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get Components
        _playerCtrl = animator.gameObject.GetComponent<PlayerController>();
        _rigidbody = animator.gameObject.GetComponent<Rigidbody2D>();

        _playerCtrl.charaAnimator.transform.localPosition = new Vector3(0, 0, -0.35f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!GameManager.Instance.gamePaused)
        {
            // Check if Player is stopped walking
            if (_playerCtrl.moveDir == Vector2.zero || !_playerCtrl.canMove)
            {
                // Set bool for state Transition to "Idle"
                animator.SetBool("isWalking", false);
                _playerCtrl.charaAnimator.SetBool("isWalking", false);
            }
            // Player is still walking
            else
            {
                // Turn Character to Direction of Movement
                _playerCtrl.Turn();
                // Move Character using velocity
                _rigidbody.velocity = _playerCtrl.moveDir * _playerCtrl.moveSpeed;

                // Player inputs "Roll", Transition to "Roll" state
                if (Input.GetButtonDown("Dash") && _playerCtrl.canMove)
                {
                    // Set trigger for state Transition to "Melee Attack"
                    animator.SetTrigger("Dash");
                    _playerCtrl.charaAnimator.SetTrigger("Dash");
                }
                // Player inputs "MeleeAttack", Transition to "Melee Attack" state
                else if (Input.GetButtonDown("MeleeAttack") && _playerCtrl.canAttack)
                {
                    // Set trigger for state Transition to "Melee Attack"
                    animator.SetTrigger("MeleeAtk");
                    _playerCtrl.charaAnimator.SetTrigger("MeleeAtk");
                }
                // Player inputs "RangedAttack", Transition to Ranged Attack state
                else if (Input.GetButtonDown("RangedAttack") && _playerCtrl.canAttack)
                {
                    // Set trigger for state Transition to "Ranged Attack"
                    animator.SetTrigger("RangedAtk");
                    _playerCtrl.charaAnimator.SetTrigger("RangedAtk");
                }
            }
        }   
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Stop Player's Movement
        _rigidbody.velocity = Vector2.zero;

        // Make sure isWalking = false when Exit this State
        animator.SetBool("isWalking", false);
        _playerCtrl.charaAnimator.SetBool("isWalking", false);
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
