using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for controlling Player's Character
public class PlayerController : MonoBehaviour
{
    #region Data Members
    private float _moveX; // Horizontal Movement Input
    private float _moveY; // Vertical Movement Input
    [HideInInspector] public Vector2 moveDir; // Movement Direction
    [Tooltip("Player's movement speed.")]
    public float moveSpeed;

    [HideInInspector] public Rigidbody2D rb;

    [Tooltip("Player's turning speed.")]
    public float turnSpeed;
    [HideInInspector] public Vector3 dir; // Rolling Direction
    public float rollForce;

    [Tooltip("The origin of player's attack.")]
    public Transform attackPoint;
    [Tooltip("Player's melee attack range.")]
    public float meleeRange;
    [Tooltip("The layer that enemies are in.")]
    public LayerMask enemyLayer;
    public int attackDamage;

    [HideInInspector] public bool canMove;
    [HideInInspector] public bool canAttack;
    #endregion

    #region Unity Callbacks
    // Start is called before the first frame update
    void Start()
    {
        // Get Component
        rb = GetComponent<Rigidbody2D>();

        dir = Quaternion.RotateTowards(transform.rotation, transform.rotation, turnSpeed) * Vector2.up;
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
    // Method for Moving Character
    public void Move()
    {
        rb.velocity = moveDir * moveSpeed;
    }

    // Method for Turning Character
    public void Turn()
    {
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, moveDir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);

        dir = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime) * Vector2.up;
    }

    // Method for Rolling Character
    public void Roll()
    {
        //rb.velocity = dir * rollForce;
        rb.AddForce(dir * rollForce);
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
    #endregion
}
