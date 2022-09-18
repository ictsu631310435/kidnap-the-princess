using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for controlling Enemy Spawner
public class EnemySpawner : MonoBehaviour
{
    #region Data Members
    [Tooltip("Directions that enemy will face when spawn.")]
    public Vector3[] dir;
    [Tooltip("Enemy that will be spawned.")]
    public GameObject enemy;
    [Tooltip("Randomize spawning direction.")]
    public bool randomDirection;
    [Tooltip("Enemy's direction when spawned (Index of defined directions array).")]
    public int spawnDir;
    private Quaternion _spawnRot; // Enemy's rotation when spawned

    private bool _isSpawned; // Is enemy spawned

    public bool chasePlayerOnSpawned;

    [Tooltip("Number of enemies.")]
    public int numEnemy;
    public float timeBtwSpawns;

    public enum SpawnOn
    {
        Start, Event
    }
    [Tooltip("When will enemy be spawned.")]
    public SpawnOn spawnOn;
    #endregion

    #region Unity Callbacks
    // Start is called before the first frame update
    void Start()
    {
        // SpawnOnStart
        if (spawnOn == SpawnOn.Start)
        {
            StartCoroutine(SpawnCoroutine());
        }
    }
    #endregion

    #region Methods
    // Method for starting SpawnCoroutine
    public void Spawn()
    {
        StartCoroutine(SpawnCoroutine());
    }

    // Method for spawning enemy
    private IEnumerator SpawnCoroutine()
    {
        // Check if there is enemy and enemy hasn't spawned yet
        if (enemy != null)
        {
            while (numEnemy > 0)
            {
                _spawnRot = GetDirection(); // Get enemy's spawn direction
                // Instantiate enemy's clone at this object's position and spawnDir
                GameObject enemyClone = Instantiate(enemy, transform.position, _spawnRot);
                // Set chasePlayerOnSpawned value
                if (enemyClone.TryGetComponent(out Enemy _enemyComp))
                {
                    _enemyComp.chasePlayerOnSpawned = chasePlayerOnSpawned;
                }
                numEnemy--;

                yield return new WaitForSeconds(timeBtwSpawns);
            }
        }
        Destroy(this.gameObject); // Destroy this gameObject after spawned all enemies
    }

    // Method for get enemy's spawn direction
    private Quaternion GetDirection()
    {
        if (randomDirection)
            return RandomizeDirection();
        else
            return Quaternion.Euler(dir[spawnDir]);
    }

    // Method for randomize enemy's spawn direction
    private Quaternion RandomizeDirection()
    {
        // Randomize integer from 0 to (dir[]'s Lenght - 1)
        int n = Random.Range(0, dir.Length - 1);

        // Return spawn direction
        return Quaternion.Euler(dir[n]);
    }
    #endregion
}
