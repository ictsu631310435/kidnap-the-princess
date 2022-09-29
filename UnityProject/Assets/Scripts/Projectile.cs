using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed;

    [HideInInspector] public Collider2D getCollider;
    public LayerMask obstacleLayer;
    public LayerMask enemyLayer;

    public int damage;

    public StatusEffect inflictEffect;
    private GameObject source;

    void Awake()
    {
        getCollider = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }

    // OnTriggerEnter2D is called when the Collider2D collision enter the trigger
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if collided with an obstacle or an enemy
        if (getCollider.IsTouchingLayers(obstacleLayer) || getCollider.IsTouchingLayers(enemyLayer))
        {
            // If collided with enemy, deal damage and inflict effect
            if (getCollider.IsTouchingLayers(enemyLayer))
            {
                // Check for HealthHandler
                if (collision.TryGetComponent(out HealthHandler _health))
                {
                    // Deal damage
                    _health.ChangeHealth(-damage);
                }
                // Check for StatusEffectHandler
                if (collision.TryGetComponent(out StatusEffectHandler _effectHandler))
                {
                    // Inflict effect
                    _effectHandler.ApplyEffect(inflictEffect, source);
                }
            }
            // Destroy after collided with an obstacle or an enemy
            Destroy(gameObject);
        }
    }

    public void Initialize(GameObject shooter)
    {
        source = shooter;
    }
    public void Initialize(GameObject shooter, LayerMask targetLayer)
    {
        source = shooter;
        obstacleLayer += targetLayer;
    }
}
