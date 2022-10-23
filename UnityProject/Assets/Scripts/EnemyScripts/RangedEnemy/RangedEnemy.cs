using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for Ranged Enemy
public class RangedEnemy : Enemy
{
    #region Data Members
    [Header("Ranged Enemy's Extentions")]
    public GameObject projectile;
    public float[] angleOffsets;
    // angleOffsets array index
    public int _n;
    #endregion

    #region Unity Callbacks
    // Start is called before the first frame update
    new void Start()
    {
        // Call base script's Start
        base.Start();

        // Initialize angleOffsets array index
        _n = 0;
    }
    #endregion

    #region Methods
    // Method for attacking (Ranged)
    public override void Attack()
    {
        // Create projectile clone at attackPoint
        GameObject _bullet = Instantiate(projectile, attackPoint.position, transform.rotation);
        // Get projectile vector
        Vector2 _pVector = new Vector2(angleOffsets[_n], 1);
        // Initialize projectile
        _bullet.GetComponent<Projectile>().Initialize(gameObject, _pVector);

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
