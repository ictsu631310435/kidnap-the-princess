using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// Script for handling Melee Enemy's Chase state
public class MeleeEnemyChase : StateMachineBehaviour
{
    #region Data Members
    private MeleeEnemy _enemy;
    private AIPath _aiPath;
    private AIDestinationSetter _aiDestination;
    #endregion

    #region Unity Callbacks
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get Components
        _enemy = animator.GetComponent<MeleeEnemy>() as MeleeEnemy;
        _aiPath = _enemy.aiPath;
        _aiDestination = _enemy.aiDestination;

        if (_enemy.inCombat)
        {
            _aiPath.slowdownDistance = _enemy.attackRange * 3;
            _aiPath.endReachedDistance = _enemy.attackRange;
        }
        else
        {
            _aiPath.slowdownDistance = _enemy.standbyRange * 3;
            _aiPath.endReachedDistance = _enemy.standbyRange;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_enemy.playerDistance <= _enemy.detectRange)
        {
            if (_aiDestination.target == null)
            {
                _aiDestination.target = _enemy.player;
            }

            if (_aiPath.reachedDestination)
            {
                if (_enemy.inCombat)
                {
                    Debug.Log("Stop chase: in Attack Range");
                    animator.SetBool("inCombat", true);
                }
                else
                {
                    Debug.Log("Stop chase: Reached destination");
                    animator.SetBool("onStandby", true);
                }
            }
        }
        else
        {
            if (_aiDestination.target != null)
            {
                _aiDestination.target = null;

                _enemy.inCombat = false;
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
    #endregion
}
