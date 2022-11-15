using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// Base Scripts for handling Enemy's Chase state
public class EnemyChase : StateMachineBehaviour
{
    #region Data Members
    [HideInInspector] public Enemy enemy;
    [HideInInspector] public AIPath aiPath;
    [HideInInspector] public AIDestinationSetter aiDestination;
    #endregion

    #region Unity Callbacks
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get Components
        enemy = animator.GetComponent<Enemy>();
        aiPath = enemy.aiPath;
        aiDestination = enemy.aiDestination;

        enemy.charaAnimator.transform.localPosition = new Vector3(0, 0, -0.45f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Player in detectRange, chase after target
        if (enemy.playerDistance <= enemy.detectRange)
        {
            // If target is not set, set target to Player
            if (aiDestination.target == null)
            {
                aiDestination.target = enemy.player;
            }

            // Reached destination enter "inCombat" state
            if (aiPath.reachedDestination)
            {
                //Debug.Log("Stop chase: in Attack Range");
                // Set bool for state transition to "inCombat"
                animator.SetBool("inCombat", true);
                enemy.charaAnimator.SetBool("inCombat", true);
            }
        }
        // Player out of detectRange, stop chase after reached the last Player position before out of range
        else
        {
            // If target is still set to Player, set target to null
            if (aiDestination.target != null)
            {
                aiDestination.target = null;
            }

            // Stop chase after reached the last Player position before out of range
            if (aiPath.reachedDestination)
            {
                //Debug.Log("Stop chase: Player lost");
                // Set bool for state transition to "Idle"
                animator.SetBool("isChasing", false);
                enemy.charaAnimator.SetBool("isChasing", false);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Make sure isChasing = false when exit state
        animator.SetBool("isChasing", false);
        enemy.charaAnimator.SetBool("isChasing", false);
        // Make sure to not set target to anything when exit state
        aiDestination.target = null;
    }
    #endregion
}
