using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// Base Script for Enemy
public abstract class Enemy : MonoBehaviour
{
    #region Data Members
    [Tooltip("Detection range")]
    public float detectRange;
    public bool chasePlayerOnSpawned;
    public float combatRange;
    [Tooltip("Origin of attack")]
    public Transform attackPoint;
    public float meleeRange;
    public LayerMask playerLayer;
    public int attackDamage;
    [Tooltip("Time between attacks")]
    public float timeBtwAttacks;
    [HideInInspector] public float nextAttackTime;
    [HideInInspector] public Transform player;
    [HideInInspector] public float playerDistance;
    
    public float turnSpeed;
    [HideInInspector] public AIPath aiPath;
    [HideInInspector] public AIDestinationSetter aiDestination;
    #endregion

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

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        // Draw sightRange
        Gizmos.DrawWireSphere(transform.position, detectRange);

        // Draw combatRange
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, combatRange);

        // Draw meleeRange
        if (attackPoint != null && meleeRange > 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, meleeRange);
        }
    }
#endif

    public void Turn()
    {
        Vector2 heading = player.position - transform.position;
        Vector2 direction = heading.normalized;

        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
    }

    public Vector3 RandomPosAroundPlayer(float minRange, float maxRange)
    {
        // Get a random point inside circle with radius(maxRange)
        Vector3 _rPos = Random.insideUnitCircle * maxRange;
        // Player's position + rPos = New position
        Vector3 _newPos = player.position + _rPos;

        // If newPos is lower than minimum, get new value until equal or exceed minimum
        while (Vector3.Distance(_newPos, player.position) < minRange || Vector3.Distance(_newPos, transform.position) > maxRange)
        {
            _rPos = (Random.insideUnitCircle * maxRange);
            _newPos = player.position + _rPos;
        }

        return _newPos;
    }

    public abstract void Attack();

    public void Hurt(int currentHealth)
    {
        if (currentHealth <= 0)
        {
            Die();
            return;
        }
    }

    public virtual void Die()
    {
        //PlayerController _playerCtrl = FindObjectOfType<PlayerController>();
        //_playerCtrl.GetComponent<HealthHandler>().ChangeHealth(+1);

        Destroy(gameObject);
    }
}
