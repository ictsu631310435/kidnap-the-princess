using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// Base Scripts for handling Enemy's InCombat state
public class EnemyInCombat : StateMachineBehaviour
{
    #region Data Members
    [HideInInspector] public Enemy enemy;
    private AIDestinationSetter _aiDestination;
    #endregion

    #region Unity Callbacks
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get Components
        enemy = animator.GetComponent<Enemy>();

        _aiDestination = enemy.aiDestination;
        enemy.nextAttackTime = Time.time + enemy.timeBtwAttacks;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy.playerDistance > enemy.attackRange)
        {
            Debug.Log("Player out of combatRange");
            _aiDestination.target = enemy.player;
            animator.SetBool("isChasing", true);
        }
        else
        {
            if (Time.time > enemy.nextAttackTime)
            {
                enemy.nextAttackTime = Time.time + enemy.timeBtwAttacks;
                animator.SetTrigger("Attack");
            }
            else
            {
                enemy.Turn();
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("inCombat", false);
    }
    #endregion
}
