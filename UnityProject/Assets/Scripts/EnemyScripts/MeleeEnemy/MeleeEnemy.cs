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
        // Call base script's Start
        base.Start();

        // Set Variables'values that differ from base script
        aiPath.slowdownDistance = standbyRange * 3;
        aiPath.endReachedDistance = standbyRange;

        // Create a waypoint
        waypoint = Instantiate(waypointPrefab).transform;
        waypoint.parent = transform.parent;
        waypoint.position = transform.position;
        // Rename to have ID attached
        waypoint.name += waypoint.GetInstanceID();

        // Detect friends in combat every 1 second
        InvokeRepeating("DetectFighting", 0f, 0.5f);
    }
    #endregion

    #region Methods
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
            if (one.TryGetComponent(out MeleeEnemy meleeEnemy) && meleeEnemy.inCombat)
            {
                inCombatNum++; // Add number
            }
        }
    }

    // Method for attacking (melee)
    public override void Attack(StatusEffect statusEffect)
    {
        // Array of all Colliders in targetLayer caught in Range.
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, playerLayer);
        foreach (var collider in hitColliders)
        {
            // Check for HealthSystem
            if (collider.gameObject.TryGetComponent(out HealthHandler _health))
            {
                // Deal damage
                _health.ChangeHealth(-attackDamage);
            }
            // Check for StatusEffectHandler
            if (collider.gameObject.TryGetComponent(out StatusEffectHandler _effectHandler))
            {
                // Apply inflictEffect
                _effectHandler.ApplyEffect(statusEffect, gameObject);
            }
        }
    }

    public override void Die() 
    {
        Destroy(waypoint.gameObject);

        base.Die();
    }

    // Unused
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
