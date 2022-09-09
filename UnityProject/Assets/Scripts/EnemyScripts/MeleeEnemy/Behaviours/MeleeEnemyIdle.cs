using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MeleeEnemyIdle : StateMachineBehaviour
{
    private MeleeEnemy _meleeEnemy;
    private Rigidbody2D _rb;
    private AIPath _aiPath;
    private AIDestinationSetter _aiDestination;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _meleeEnemy = animator.GetComponent<MeleeEnemy>();
        _rb = animator.GetComponent<Rigidbody2D>();
        _aiPath = animator.GetComponent<AIPath>();
        _aiDestination = animator.GetComponent<AIDestinationSetter>();

        _rb.velocity = Vector2.zero;
        _aiDestination.target = null;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_meleeEnemy.chasePlayerOnSpawned || _meleeEnemy.playerDistance <= _meleeEnemy.detectRange)
        {
            _aiDestination.target = _meleeEnemy.player;    
        }

        if (_aiDestination.target == _meleeEnemy.player)
        {
            Debug.Log("Chase");
            animator.SetBool("isChasing", true);
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
