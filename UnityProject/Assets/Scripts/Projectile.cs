using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed;
    [HideInInspector] public Collider2D getCollider;
    public LayerMask collisionLayer;

    public int damage;

    public StatusEffect inflictEffect;
    [HideInInspector] public GameObject source;

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (getCollider.IsTouchingLayers(collisionLayer))
        {
            if (collision.TryGetComponent(out HealthHandler _health))
            {
                _health.ChangeHealth(-damage);
            }
            if (collision.TryGetComponent(out StatusEffectHandler _effectHandler))
            {
                _effectHandler.ApplyEffect(inflictEffect, source);
            }

            Destroy(gameObject);
        }
    }

    public void Initialize(GameObject shooter, LayerMask targetLayer)
    {
        source = shooter;
        collisionLayer += targetLayer;
    }
}
