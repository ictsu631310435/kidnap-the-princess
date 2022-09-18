using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class Enemy : MonoBehaviour
{
    public float detectRange;
    public bool chasePlayerOnSpawned;
    public float combatRange;

    public Transform attackPoint;
    public float attackRange;
    public LayerMask playerLayer;
    public float timeBtwAttacks;
    [HideInInspector] public float nextAttackTime;
    [HideInInspector] public Transform player;
    [HideInInspector] public float playerDistance;
    
    public float turnSpeed;
    [HideInInspector] public AIPath aiPath;
    [HideInInspector] public AIDestinationSetter aiDestination;

    void Awake()
    {
        // Get Components
        player = FindObjectOfType<PlayerController>().transform;
        aiPath = GetComponent<AIPath>();
        aiDestination = GetComponent<AIDestinationSetter>();
    }

    // Start is called before the first frame update
    public void Start()
    {  
        // Set Variables'values
        aiPath.rotationSpeed = turnSpeed;
        
        // Set destination to Player's position if want to chase Player after spawned
        if (chasePlayerOnSpawned)
        {
            aiDestination.target = player;
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector2.Distance(transform.position, player.position);
    }

    void OnDrawGizmos()
    {
        // Draw sightRange
        Gizmos.DrawWireSphere(transform.position, detectRange);

        // Draw combatRange
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, combatRange);

        // Draw attackRange
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }

    public void Turn()
    {
        Vector2 heading = player.position - transform.position;
        Vector2 direction = heading.normalized;

        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
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
