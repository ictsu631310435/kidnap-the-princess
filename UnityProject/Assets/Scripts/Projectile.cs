using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for controlling projectile
public class Projectile : MonoBehaviour
{
    #region Data Members
    public float moveSpeed;

    public LayerMask obstacleLayer;
    public LayerMask targetLayer;

    public int damage;

    public StatusEffect inflictEffect;
    private GameObject _shooter;
    #endregion

    #region Unity Callbacks
    // Update is called once per frame
    void Update()
    {
        // Move the projectile (moveSpeed) unit/second
        transform.Translate(moveSpeed * Time.deltaTime * Vector2.up);
    }

    // OnTriggerEnter2D is called when the Collider2D collision enter the trigger
    void OnTriggerEnter2D(Collider2D collision)
    {
        /* Check if collided with an obstacle or an enemy
           (Need to convert layer to layermask using the binary left-shift operator to left-shift 1 by the layer)*/
        if (1 << collision.gameObject.layer == obstacleLayer.value || 1 << collision.gameObject.layer == targetLayer.value)
        {
            // If collided with enemy, deal damage and inflict effect
            if (1 << collision.gameObject.layer == targetLayer.value)
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
                    _effectHandler.ApplyEffect(inflictEffect, _shooter);
                }
            }
            // Destroy after collided with an obstacle or an enemy
            Destroy(gameObject);
        }
    }
    #endregion

    #region Methods
    // Method for the shooter to initialize values
    // Without offset angle
    public void Initialize(GameObject shooter)
    {
        _shooter = shooter;
    }
    // With offset angle
    public void Initialize(GameObject shooter, float offsetAngle)
    {
        _shooter = shooter;

        float zAngle = transform.rotation.eulerAngles.z;
        transform.rotation = Quaternion.Euler(0, 0, zAngle + offsetAngle);
    }
    #endregion
}
