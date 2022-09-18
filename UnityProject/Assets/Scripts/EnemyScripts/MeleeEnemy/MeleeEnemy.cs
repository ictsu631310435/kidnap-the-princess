using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// Script for controlling Melee Enemy
public class MeleeEnemy : Enemy
{
    #region Data Members
    public float standbyRange;
    public bool inCombat;
    public LayerMask friendLayer;
    public int inCombatNum;
    public int maxInCombat;
    #endregion

    #region Unity Callbacks
    // Start is called before the first frame update
    new void Start()
    {
        // Set base script's Start
        base.Start();

        // Set Variables'values that differ from base script
        aiPath.slowdownDistance = standbyRange * 3;
        aiPath.endReachedDistance = standbyRange;

        // Detect friends in combat every 1 second
        InvokeRepeating("DetectFighting", 0f, 1f);
    }
    #endregion

    #region Method
    // Method for detecting friends in combat
    public void DetectFighting()
    {
        // Reset number
        inCombatNum = 0;

        // Detect every other enemies
        Collider2D[] others = Physics2D.OverlapCircleAll(transform.position, detectRange, friendLayer);
        foreach (var one in others)
        {
            // Get MeleeEnemy in combat
            if (one.TryGetComponent(out MeleeEnemy _meleeEnemy) && _meleeEnemy.inCombat)
            {
                inCombatNum++; // Add number
            }
        }
    }
    #endregion
}
