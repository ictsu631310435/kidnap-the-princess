using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for handling Player's Melee Attack state
public class PlayerMeleeAttack : StateMachineBehaviour
{
    #region Data Members
    private PlayerController _playerCtrl;

    public float delayTime;

    public StatusEffect inflictEffect;
    #endregion

    #region Unity Callbacks
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get Component
        _playerCtrl = animator.gameObject.GetComponent<PlayerController>();

        _playerCtrl.charaAnimator.transform.localPosition = new Vector3(0, 0, -0.35f);

        // Call MeleeAttack method
        //_playerCtrl.MeleeAttack(inflictEffect);
        _playerCtrl.StartCoroutine(DelayHit(delayTime));
    }

    public IEnumerator DelayHit(float waitTime)
    {
        // Wait for (waitTime)
        yield return new WaitForSeconds(waitTime);

        // Call Attack method
        _playerCtrl.MeleeAttack(inflictEffect);
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
