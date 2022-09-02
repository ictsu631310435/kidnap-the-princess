using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Interface for Damageable Object
public interface IDamageable
{
    int Health { get; set; }

    // Method for taking Damage
    void TakeDamage(int damage);
}
