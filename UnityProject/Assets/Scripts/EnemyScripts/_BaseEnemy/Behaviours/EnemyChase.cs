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
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy.playerDistance <= enemy.detectRange)
        {
            if (aiDestination.target == null)
            {
                aiDestination.target = enemy.player;
            }

            if (aiPath.reachedDestination)
            {
                Debug.Log("Stop chase: in Attack Range");
                animator.SetBool("inCombat", true);
            }
        }
        else
        {
            if (aiDestination.target != null)
            {
                aiDestination.target = null;
            }

            if (aiPath.reachedDestination)
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

        aiDestination.target = null;
    }
    #endregion
}
