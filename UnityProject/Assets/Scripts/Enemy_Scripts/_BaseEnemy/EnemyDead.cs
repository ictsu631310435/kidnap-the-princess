using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyDead : StateMachineBehaviour
{
    #region Data Members
    [HideInInspector] public Enemy enemy;
    private AIPath _aIPath;
    private AIDestinationSetter _aiDestination;

    public Vector3 posOffset;
    public float destroyDelay;
    #endregion

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get Components
        enemy = animator.GetComponent<Enemy>();
        _aiDestination = enemy.aiDestination;
        _aIPath = enemy.aiPath;

        enemy.charaAnimator.transform.localPosition = posOffset;

        _aiDestination.target = null;
        _aIPath.canMove = false;
        _aIPath.maxSpeed = 0;

        Destroy(animator.gameObject, destroyDelay);
    }
}
