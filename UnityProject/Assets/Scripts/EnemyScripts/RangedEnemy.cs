using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    #region Data Members
    [Header("Ranged Enemy's Extentions")]
    public GameObject projectile;
    #endregion

    #region Unity Callbacks
    // Start is called before the first frame update
    new void Start()
    {
        // Call base script's Start
        base.Start();
    }
    #endregion

    #region Methods
    public override void Attack(StatusEffect statusEffect)
    {
        throw new System.NotImplementedException();
    }

    // Unused
    public override void Attack()
    {
        GameObject _bullet = Instantiate(projectile, attackPoint.position, transform.rotation);

        _bullet.GetComponent<Projectile>().Initialize(gameObject, playerLayer);
    }
    #endregion
}
