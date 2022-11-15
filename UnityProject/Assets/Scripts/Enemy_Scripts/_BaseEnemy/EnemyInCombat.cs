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

        enemy.charaAnimator.transform.localPosition = new Vector3(0, 0, 0);

        // Set time for next attack
        enemy.nextAttackTime = Time.time + enemy.timeBtwAttacks;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If player out of attackRange, chase after Player
        if (enemy.playerDistance > enemy.attackRange)
        {
            //Debug.Log("Player out of combatRange");
            // Set target to Player
            _aiDestination.target = enemy.player;
            // Set bool for state transition to "Chase"
            animator.SetBool("isChasing", true);
            enemy.charaAnimator.SetBool("isChasing", true);
        }
        // If player in attackRange, try to attack
        else
        {
            // If reached time to attack, attack
            if (Time.time > enemy.nextAttackTime)
            {
                //enemy.nextAttackTime = Time.time + enemy.timeBtwAttacks;
                // Set bool for state transition to "Attack"
                animator.SetTrigger("Attack");
                enemy.charaAnimator.SetTrigger("Attack");
            }
            // If not, turn to face Player
            else
            {
                // Turn to Player
                enemy.Turn();
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Make sure inCombat = false when exit state
        animator.SetBool("inCombat", false);
        enemy.charaAnimator.SetBool("inCombat", false);
    }
    #endregion
}
