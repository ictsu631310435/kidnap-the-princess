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

    #region Unity Callbacks
    public override void Start()
    {
        base.Start();

        GameManager.Instance.SetBossFight();
    }
    #endregion

    #region Methods
    // Method for attacking (Melee)
    public override void Attack(StatusEffect statusEffect)
    {
        audioHandler.Play("Melee");

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
        audioHandler.Play("Fireball");

        // Create projectile clone at attackPoint
        GameObject _bullet = Instantiate(projectile, attackPoint.position, transform.rotation);
        // Initialize projectile
        _bullet.GetComponent<Projectile>().Initialize(gameObject);
    }

    public override void Die()
    {
        // End boss fight
        GameManager.Instance.bossAlive = false;

        base.Die();
    }
    #endregion
}
