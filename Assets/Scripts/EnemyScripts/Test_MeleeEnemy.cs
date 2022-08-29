using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test_MeleeEnemy : MonoBehaviour
{
    private HealthSystem _healthSys;

    public float moveSpeed;
    public float turnSpeed;

    public float sightRange;
    public float attackDistance;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask playerLayer;
    public float timeBtwAttacks;
    [HideInInspector] public float nextAttackTime;
    [HideInInspector] public Transform target;
    [HideInInspector] public float targetDistance;

    private Animator _anim;
    [HideInInspector] public Rigidbody rb;

    //[HideInInspector] public NavMeshAgent navAgent;

    // Start is called before the first frame update
    void Start()
    {
        _healthSys = GetComponent<HealthSystem>();
        target = FindObjectOfType<PlayerController>().transform;
        _anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        /*navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = moveSpeed;
        navAgent.angularSpeed = turnSpeed;
        navAgent.stoppingDistance = attackDistance;*/
    }

    // Update is called once per frame
    void Update()
    {
        targetDistance = Vector3.Distance(transform.position, target.position);
    }

    void OnDrawGizmos()
    {
        // Draw sightRange
        Gizmos.DrawWireSphere(transform.position, sightRange);

        // Draw attackDistance
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        // Draw attackRange
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }

    public void Turn()
    {
        Vector3 heading = target.position - transform.position;
        float distance = heading.magnitude;
        Vector3 moveDir = heading / distance;

        Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
    }

    public void Follow()
    {
        //navAgent.SetDestination(target.position);
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    public void Hurt(int currentHealth)
    {
        Debug.Log(gameObject.name + " Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " Died");
        Destroy(gameObject);
    }

    public void DetectHit(Transform attackPoint, float range, LayerMask targetLayer)
    {
        // Player caught in Range.
        Collider[] hitColliders = Physics.OverlapSphere(attackPoint.position, range, targetLayer);
        foreach (var collider in hitColliders)
        {
            // Check for HealthSystem
            if (collider.gameObject.TryGetComponent(out HealthSystem healthSystem))
            {
                // Deal damage
                Debug.Log(gameObject.name + " MeleeAttack " + collider.name);
                healthSystem.ChangeHealth(-1);
            }
        }
    }
}
