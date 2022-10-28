using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for controlling Enemy Spawner
public class EnemySpawner : MonoBehaviour
{
    #region Data Members
    [Tooltip("Directions that enemy can face when spawn.")]
    public Vector3[] dir;
    [Tooltip("Enemy that will be spawned.")]
    public GameObject enemy;
    [Tooltip("Randomize spawning direction.")]
    public bool randomDirection;
    [Tooltip("Enemy's direction when spawned (Index of defined directions array).")]
    public int spawnDir;
    private Quaternion _spawnRot; // Enemy's rotation when spawned

    [Tooltip("Parent object (not set = not have parent)")]
    public GameObject parent;

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
    public bool delaySpawn;
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
        // Check if there is enemy
        if (enemy != null)
        {
            if (delaySpawn)
            {
                // Wait for (timeBtwSpawns) seconds
                yield return new WaitForSeconds(timeBtwSpawns);
            }

            // Spawn enemy until numEnemy == 0
            while (numEnemy > 0)
            {
                // Get enemy's spawn direction
                _spawnRot = GetDirection();

                // Create an enemy's clone at this object's position and spawnDir
                GameObject _clone = Instantiate(enemy, transform.position, _spawnRot);
                // Rename to have ID attached
                _clone.name += _clone.GetInstanceID();

                // Set parent if parent is not null
                if (parent != null)
                {
                    _clone.transform.parent = parent.transform;
                }

                // Set chasePlayerOnSpawned value
                if (_clone.TryGetComponent(out Enemy _enemy))
                {
                    _enemy.chasePlayerOnSpawned = chasePlayerOnSpawned;
                }

                // Subtract numEnemy after spawned an enemy 
                numEnemy--;

                // Wait for (timeBtwSpawns) seconds
                yield return new WaitForSeconds(timeBtwSpawns);
            }
        }
        // Destroy gameObject after finished
        Destroy(gameObject);
    }

    // Method for get enemy's spawn direction
    private Quaternion GetDirection()
    {
        if (randomDirection)
        {
            // Randomize integer from 0 to (dir[]'s Lenght - 1)
            int n = Random.Range(0, dir.Length - 1);
            // Return rotation converted from randomized spawn direction
            return Quaternion.Euler(dir[n]);
        }
        else
        {
            // Return rotation converted from spawn direction
            return Quaternion.Euler(dir[spawnDir]);
        }
    }
    #endregion
}
