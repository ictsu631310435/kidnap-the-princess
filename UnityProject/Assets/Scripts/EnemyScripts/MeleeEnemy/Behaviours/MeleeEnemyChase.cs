using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MeleeEnemyChase : StateMachineBehaviour
{
    private MeleeEnemy _meleeEnemy;
    private AIPath _aiPath;
    private AIDestinationSetter _aiDestination;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _meleeEnemy = animator.GetComponent<MeleeEnemy>();
        _aiPath = animator.GetComponent<AIPath>();
        _aiDestination = animator.GetComponent<AIDestinationSetter>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_meleeEnemy.playerDistance <= _meleeEnemy.detectRange)
        {
            if (_aiDestination.target == null)
            {
                _aiDestination.target = _meleeEnemy.player;
            }

            if (_aiPath.reachedDestination)
            {
                Debug.Log("Stop chase: Reached destination");
                animator.SetBool("inCombat", true);
            }
        }
        else
        {
            if (_aiDestination.target != null)
            {
                _aiDestination.target = null;
            }

            if (_aiPath.reachedDestination)
            {
                Debug.Log("Stop chase: Player lost");
                animator.SetBool("isChasing", false);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isChasing", false);

        _aiDestination.target = null;
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
}
