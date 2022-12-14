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
    public float attackRange;
    [Tooltip("Origin of attack")]
    public Transform attackPoint;
    public float attackRadius;
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

    [HideInInspector] public Rigidbody2D rb;

    public Animator stateMachine;
    public Animator charaAnimator;

    [Header("Audio")]
    public AudioHandler audioHandler;
    #endregion

    #region Unity Callbacks
    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Get Components
        player = FindObjectOfType<PlayerController>().transform;
        aiPath = GetComponent<AIPath>();
        aiDestination = GetComponent<AIDestinationSetter>();

        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    public virtual void Start()
    {  
        // Set variables values
        aiPath.rotationSpeed = turnSpeed;
        // Slow down and stop when Player in attackRange 
        aiPath.slowdownDistance = attackRange * 3;
        aiPath.endReachedDistance = attackRange;

        // Set target to Player if set to chase Player after spawned
        if (chasePlayerOnSpawned)
        {
            aiDestination.target = player;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get Player distance
        playerDistance = Vector2.Distance(transform.position, player.position);
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        // Draw detectRange
        if (detectRange > 0)
        {
            Gizmos.DrawWireSphere(transform.position, detectRange);
        }

        // Draw combatRange
        if (attackRange > 0)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }

        // Draw attackRadius
        if (attackPoint != null && attackRadius > 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
    }
#endif
    #endregion

    #region Methods

    // Method for Turning Character
    public void Turn()
    {
        Vector2 heading = player.position - transform.position;
        Vector2 direction = heading.normalized;

        // Caculate rotation that Character is going to
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
        // Rotate Character rotation toward toRotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
    }

    // Method for getting random position around player
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

    // Base method for attacking
    // Without applying StatusEffect
    public abstract void Attack();
    //
    // With applying StatusEffect
    public abstract void Attack(StatusEffect statusEffect);

    // Method for targeting Player
    public void PlayerAggro()
    {
        aiDestination.target = player;
    }

    public virtual void Die()
    {
        stateMachine.SetBool("isDead", true);
        charaAnimator.SetBool("isDead", true);

        //Destroy(gameObject);
    }
    #endregion
}
