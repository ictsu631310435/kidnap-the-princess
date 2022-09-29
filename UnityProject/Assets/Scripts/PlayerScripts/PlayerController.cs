using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for controlling Player's Character
public class PlayerController : MonoBehaviour
{
    #region Data Members
    [Header("Movement Settings")]
    private float _moveX; // Horizontal Movement Input
    private float _moveY; // Vertical Movement Input
    [HideInInspector] public Vector2 moveDir; // Movement Direction
    public float moveSpeed;

    public float turnSpeed;
    [HideInInspector] public Vector3 direction;

    [Header("Roll")]
    public float rollDistance;
    public float rollSpeed;
    public GameObject rollCollider;

    [Header("Combat Settings")]
    [Tooltip("The origin of player's attack.")]
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public int attackDamage;

    [Header("Melee Combat")]
    [Tooltip("Player's melee attack range.")]
    public float meleeRange;

    [Header("Ranged Combat")]
    public GameObject projectile;

    [HideInInspector] public bool canMove;
    [HideInInspector] public bool canAttack;
    #endregion

    #region Unity Callbacks
    // Start is called before the first frame update
    void Start()
    {
        direction = transform.rotation * Vector2.up;
        canMove = true;
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Get Player's MovementInput
        _moveX = Input.GetAxis("Horizontal"); // X Axis Input
        _moveY = Input.GetAxis("Vertical"); // Y Axis Input

        if (canMove)
        {
            // Turn MovementInput into MovementDirection
            moveDir = new Vector2(_moveX, _moveY).normalized;
        }
        else
        {
            moveDir = Vector2.zero;
        }
        
    }

    void OnDrawGizmos()
    {
        if (attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, meleeRange);
    }
    #endregion

    #region Methods
    // Method for Turning Character
    public void Turn()
    {
        // Caculate rotation that player is going to
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, moveDir);
        // Rotate player towards toRotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        // Caculate player's facing direction
        direction = transform.rotation * Vector2.up;
    }

    // Method for Melee Attack
    public void MeleeAttack(StatusEffect inflictEffect)
    {
        // Array of all Colliders in targetLayer caught in Range.
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, meleeRange, enemyLayer);
        foreach (var collider in hitColliders)
        {
            // Check for HealthHandler
            if (collider.gameObject.TryGetComponent(out HealthHandler _health))
            {
                // Deal damage
                _health.ChangeHealth(-attackDamage);
            }
            // Check for StatusEffectHandler
            if (collider.gameObject.TryGetComponent(out StatusEffectHandler _effectHandler))
            {
                // Apply inflictEffect
                _effectHandler.ApplyEffect(inflictEffect, gameObject);
            }
        }
    }

    public void RangedAttack()
    {
        GameObject _bullet = Instantiate(projectile, attackPoint.position, transform.rotation);

        _bullet.GetComponent<Projectile>().Initialize(gameObject);
    }
    #endregion
}
