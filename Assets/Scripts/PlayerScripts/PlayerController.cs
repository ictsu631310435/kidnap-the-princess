using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for controlling Player's Character
public class PlayerController : MonoBehaviour
{
    #region Data Members
    private float _moveX; // Horizontal Movement Input
    private float _moveZ; // Vertical Movement Input if 3D Scene
    [HideInInspector] public Vector3 moveDir; // Movement Direction

    [Tooltip("Player's movement speed.")]
    public float moveSpeed;

    [HideInInspector] public Rigidbody rb;

    [Tooltip("Player's turning speed.")]
    public float turnSpeed;

    [Tooltip("Player's attack mode, 0 is melee and 1 is range.")]
    public bool isRange;
    [Tooltip("The origin of player's attack.")]
    public Transform attackPoint;
    [Tooltip("Player's melee attack range.")]
    public float meleeRange;
    [Tooltip("The layer that enemies are in.")]
    public LayerMask enemyLayer;
    #endregion

    #region Unity Callbacks
    // Start is called before the first frame update
    void Start()
    {
        // Get Component
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get Player's MovementInput
        _moveX = Input.GetAxis("Horizontal"); // X Axis Input
        _moveZ = Input.GetAxis("Vertical"); // Z Axis Input
        // Turn MovementInput into MovementDirection
        moveDir = new Vector3(_moveX, 0, _moveZ).normalized;
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
        Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
    }
    
    // Method for Melee Hit Detection
    public void DetectHit(Transform attackPoint, float range, LayerMask targetLayer)
    {
        // Array of all Enemies caught in Range.
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
    #endregion
}
