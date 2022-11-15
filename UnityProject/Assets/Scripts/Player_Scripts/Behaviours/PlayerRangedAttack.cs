using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for handling Player's Melee Attack state
public class PlayerRangedAttack : StateMachineBehaviour
{
    #region Data Members
    private PlayerController _playerCtrl;
    [Tooltip("Delay time before shoot projectile.")]
    public float delayTime;
    #endregion

    #region Unity Callbacks
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get Component
        _playerCtrl = animator.gameObject.GetComponent<PlayerController>();

        _playerCtrl.audioHandler.Play("Fireball");

        _playerCtrl.charaAnimator.transform.localPosition = new Vector3(0, 0, -0.35f);

        // Call RangedAttack method
        _playerCtrl.Invoke("RangedAttack", delayTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
