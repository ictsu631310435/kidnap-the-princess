using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// Script for controlling Melee Enemy
public class MeleeEnemy : Enemy
{
    #region Data Members
    [Header("Melee Enemy's Extentions")]
    public float standbyRange;
    public bool inCombat;
    public LayerMask friendLayer;
    public int inCombatNum;
    public int maxInCombat;

    public GameObject waypointPrefab;
    [HideInInspector] public Transform waypoint;
    [HideInInspector] public float nextReposTime;
    public float minTimeBtwRepos;
    public float maxTimeBtwRepos;
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

        waypoint = Instantiate(waypointPrefab).transform;
        waypoint.position = transform.position;


        // Detect friends in combat every 1 second
        InvokeRepeating("DetectFighting", 0f, 0.5f);
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

    // Method for attacking (melee)
    public override void Attack()
    {
        // Array of all Colliders in targetLayer caught in Range.
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, meleeRange, playerLayer);
        foreach (var collider in hitColliders)
        {
            // Check for HealthSystem
            if (collider.gameObject.TryGetComponent(out HealthHandler _health))
            {
                // Deal damage
                _health.ChangeHealth(-attackDamage);
            }
        }
    }

    public override void Die() 
    {
        Destroy(waypoint.gameObject);

        base.Die();
    }
    #endregion
}
