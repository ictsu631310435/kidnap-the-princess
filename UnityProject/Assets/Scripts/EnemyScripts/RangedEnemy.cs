using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    #region Data Members
    [Header("Ranged Enemy's Extentions")]
    public GameObject projectile;
    public float[] angleOffsets;
    private int _r;
    public int _n;
    #endregion

    #region Unity Callbacks
    // Start is called before the first frame update
    new void Start()
    {
        // Call base script's Start
        base.Start();

        _n = 0;
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

        /*int old_r = _r;
        while (_r == old_r)
        {
            _r = Random.Range(0, angleOffsets.Length);
        }*/
        Vector2 _pVector = new Vector2(angleOffsets[_n], 1);
        _bullet.GetComponent<Projectile>().Initialize(gameObject, _pVector);

        if (_n < angleOffsets.Length - 1)
        {
            _n++;
        }
        else
        {
            _n = 0;
        }
    }
    #endregion
}
