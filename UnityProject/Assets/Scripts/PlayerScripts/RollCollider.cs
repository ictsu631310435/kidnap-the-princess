using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollCollider : MonoBehaviour
{
    private Collider2D _collider;
    public LayerMask enemyLayer;
    public StatusEffect inflictEffect;
    private PlayerController _player;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _player = GetComponentInParent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_collider.IsTouchingLayers(enemyLayer) && collision.TryGetComponent(out StatusEffectHandler _effectHandler))
        {
            _effectHandler.ApplyEffect(inflictEffect, _player.gameObject);
        }
    }
}
