using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float detectRange;
    public bool chaseOnSpawned;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask playerLayer;
    public float timeBtwAttacks;
    [HideInInspector] public float nextAttackTime;
    [HideInInspector] public Transform player;
    [HideInInspector] public float playerDistance;

    void OnDrawGizmos()
    {
        // Draw sightRange
        Gizmos.DrawWireSphere(transform.position, detectRange);

        // Draw attackDistance
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, attackDistance);

        // Draw attackRange
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }

    public void Hurt(int currentHealth)
    {
        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        Debug.Log(gameObject.name + " Hurt");
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " Died");
        Destroy(gameObject);
    }
}
