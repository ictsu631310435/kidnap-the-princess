using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for handling Player's Roll state
public class PlayerRoll : StateMachineBehaviour
{
    #region Data Members
    private PlayerController _playerCtrl;
    private Rigidbody2D _rigidbody;

    private Vector3 _target;
    #endregion

    #region Unity Callbacks
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get Component
        _playerCtrl = animator.gameObject.GetComponent<PlayerController>();
        _rigidbody = animator.gameObject.GetComponent<Rigidbody2D>();

        // Calculate target location
        _target = _playerCtrl.transform.position + (_playerCtrl.direction * _playerCtrl.rollDistance);

        _rigidbody.AddForce(_playerCtrl.direction * _playerCtrl.rollSpeed, ForceMode2D.Impulse);
        _playerCtrl.rollCollider.SetActive(true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if (_playerCtrl.transform.position == _target)
        //{
            //_rigidbody.velocity = Vector2.zero;
        //}
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerCtrl.rollCollider.SetActive(false);
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
