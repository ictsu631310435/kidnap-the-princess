using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for Ranged Enemy
public class RangedEnemy : Enemy
{
    #region Data Members
    [Header("Ranged Enemy's Extentions")]
    public GameObject projectile;
    [Tooltip("X offset for projectile vector.")]
    [Range(-45f, 45f)]
    public float[] angleOffsets;
    private int _n; // xOffsets array index
    #endregion

    #region Unity Callbacks
    // Start is called before the first frame update
    new void Start()
    {
        // Call base script's Start
        base.Start();

        // Initialize xOffsets array index
        _n = 0;
    }
    #endregion

    #region Methods
    // Method for attacking (Ranged)
    public override void Attack()
    {
        // Create projectile clone at attackPoint
        GameObject _bullet = Instantiate(projectile, attackPoint.position, transform.rotation);
        // Initialize projectile
        _bullet.GetComponent<Projectile>().Initialize(gameObject, angleOffsets[_n]);

        // If still in array length, increase index
        if (_n < angleOffsets.Length - 1)
        {
            _n++;
        }
        // If exceed array length, reset index
        else
        {
            _n = 0;
        }
    }

    // Unused
    public override void Attack(StatusEffect statusEffect)
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
