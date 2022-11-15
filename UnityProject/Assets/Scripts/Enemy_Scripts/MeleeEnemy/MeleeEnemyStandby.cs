using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// Script handling Melee Enemy's Standby state
public class MeleeEnemyStandby : StateMachineBehaviour
{
    #region Data Members
    private MeleeEnemy _meleeEnemy;
    private AIPath _aiPath;
    private AIDestinationSetter _aiDestination;
    #endregion

    #region Unity Callbacks
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get Components
        _meleeEnemy = animator.GetComponent<MeleeEnemy>();
        _aiPath = animator.GetComponent<AIPath>();
        _aiDestination = _meleeEnemy.aiDestination;

        _meleeEnemy.charaAnimator.transform.localPosition = new Vector3(0, 0, 0);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If player in standbyRange, try to check if can enter combat or not
        if (_meleeEnemy.playerDistance <= _meleeEnemy.standbyRange)
        {
            // If can enter combat, enter Chase state to get in attackRange
            if (_meleeEnemy.inCombatNum < _meleeEnemy.maxInCombat)
            {
                _meleeEnemy.inCombat = true;
                // Slow down and stop when Player in attackRange
                _aiPath.slowdownDistance = _meleeEnemy.attackRange * 3;
                _aiPath.endReachedDistance = _meleeEnemy.attackRange;

                // Set target to Player
                _aiDestination.target = _meleeEnemy.player;

                //Debug.Log("Start Chase: Get in to fight");
                // Set bool for state transition to "Chase"
                animator.SetBool("isChasing", true);
                _meleeEnemy.charaAnimator.SetBool("isChasing", true);
            }
        }
        // If Player out of standbyRange, enter Chase state to get in range
        else
        {
            // Set target to Player
            _aiDestination.target = _meleeEnemy.player;

            //Debug.Log("Start Chase: Player out of standby range");
            // Set bool for state transition to "Chase"
            animator.SetBool("isChasing", true);
            _meleeEnemy.charaAnimator.SetBool("isChasing", true);
        }

        // If can not enter combat, reposition (chase after waypoint)
        if (_meleeEnemy.inCombatNum >= _meleeEnemy.maxInCombat && Time.time > _meleeEnemy.nextReposTime)
        {
            // Set time delay between each reposition
            _meleeEnemy.nextReposTime = Time.time + Random.Range(_meleeEnemy.minTimeBtwRepos, _meleeEnemy.maxTimeBtwRepos);

            // Slow down and stop when destination in attackRange
            _aiPath.slowdownDistance = _meleeEnemy.attackRange * 3;
            _aiPath.endReachedDistance = _meleeEnemy.attackRange;

            // Get reposition waypoint position
            _meleeEnemy.waypoint.position = _meleeEnemy.RandomPosAroundPlayer(_meleeEnemy.standbyRange, _meleeEnemy.detectRange);
            // Set target to reposition waypoint 
            _aiDestination.target = _meleeEnemy.waypoint;

            //Debug.Log("Reposition");
            // Set bool for state transition to "Chase"
            animator.SetBool("isChasing", true);
            _meleeEnemy.charaAnimator.SetBool("isChasing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Make sure onStandby = false when exit state
        animator.SetBool("onStandby", false);
        _meleeEnemy.charaAnimator.SetBool("onStandby", false);
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
