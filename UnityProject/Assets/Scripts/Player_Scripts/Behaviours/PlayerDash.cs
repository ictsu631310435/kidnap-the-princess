using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for handling Player's Dash state
public class PlayerDash : StateMachineBehaviour
{
    #region Data Members
    private PlayerController _playerCtrl;
    private Rigidbody2D _rigidbody;

    private Vector3 _startPoint;
    #endregion

    #region Unity Callbacks
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get Component
        _playerCtrl = animator.gameObject.GetComponent<PlayerController>();
        _rigidbody = animator.gameObject.GetComponent<Rigidbody2D>();

        _playerCtrl.charaAnimator.transform.localPosition = new Vector3(0, 0, -0.25f);

        // Calculate start location
        _startPoint = _playerCtrl.transform.position;

        // Dash by AddForce 
        _rigidbody.AddForce(_playerCtrl.direction * _playerCtrl.dashSpeed, ForceMode2D.Impulse);
        // Enable dashCollider for pushing Enemy
        _playerCtrl.dashCollider.SetActive(true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Calculate Player's distance from _startPoint
        float _distanceFromStart = Vector2.Distance(_startPoint, _playerCtrl.transform.position);

        // If dashed enough distance, stop dashing
        if (_distanceFromStart >= _playerCtrl.dashDistance && _rigidbody.velocity != Vector2.zero)
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Disable dashCollider when exit state
        _playerCtrl.dashCollider.SetActive(false);
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
