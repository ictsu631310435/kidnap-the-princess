using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for controlling Hero Enemy
public class HeroEnemy : Enemy
{
    #region Data Members
    [Header("Hero Enemy's Extentions")]
    public float rangedAttackRange;
    public GameObject projectile;
    [Tooltip("Time between ranged attacks")]
    public float timeBtwRangedAttacks;
    #endregion

    #region Methods
    // Method for attacking (Melee)
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

    // Method for attacking (Ranged)
    public override void Attack()
    {
        // Create projectile clone at attackPoint
        GameObject _bullet = Instantiate(projectile, attackPoint.position, transform.rotation);
        // Initialize projectile
        _bullet.GetComponent<Projectile>().Initialize(gameObject);
    }
    #endregion
}
