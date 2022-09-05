using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_MeleeEnemyIdle : StateMachineBehaviour
{
    private Test_MeleeEnemy _MeleeEnemy;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _MeleeEnemy = animator.GetComponent<Test_MeleeEnemy>();

        _MeleeEnemy.rb.velocity = Vector2.zero;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_MeleeEnemy.targetDistance <= _MeleeEnemy.sightRange)
        {
            if (_MeleeEnemy.targetDistance > _MeleeEnemy.attackDistance)
            {
                animator.SetBool("isChasing", true);
            }
            else if (Time.time > _MeleeEnemy.nextAttackTime)
            {
                animator.SetTrigger("Attack");
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
}
