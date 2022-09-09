using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for controlling Enemy Spawner
public class EnemySpawner : MonoBehaviour
{
    #region Data Members
    [Tooltip("Directions that character will face when spawn.")]
    public Vector3[] dir;

    [Tooltip("Target that will be spawned.")]
    public GameObject target;

    [Tooltip("Random Spawning Direction.")]
    public bool randomDirection;

    [Tooltip("Target's direction when spawned (Index of defined directions array).")]
    public int spawnDir;

    private Quaternion _spawnRot; // Target's rotation when spawned

    private bool _isSpawned; // Is target spawned

    public enum SpawnOn
    {
        Start, Event
    }
    [Tooltip("When will target be spawned.")]
    public SpawnOn spawnOn;
    #endregion

    #region Unity Callbacks
    // Start is called before the first frame update
    void Start()
    {
        // SpawnOnStart
        if (spawnOn == SpawnOn.Start)
        {
            Spawn();
        }
    }
    #endregion

    #region Methods
    // Method for spawning target
    public void Spawn()
    {
        // Check if there is target and target hasn't spawned yet
        if (target != null && !_isSpawned)
        {
            _spawnRot = GetDirection(); // Get target's spawn direction
            // Instantiate target's clone at this object's position and spawnDir
            Instantiate(target, transform.position, _spawnRot);
            _isSpawned = true;
            Destroy(this.gameObject); // Destroy this gameObject after spawned target
        }
    }

    // Method for get target's spawn direction
    private Quaternion GetDirection()
    {
        if (randomDirection)
            return RandomizeDirection();
        else
            return Quaternion.Euler(dir[spawnDir]);
    }

    // Method for randomize target's spawn direction
    private Quaternion RandomizeDirection()
    {
        // Randomize integer from 0 to (dir[]'s Lenght - 1)
        int n = Random.Range(0, dir.Length - 1);

        // Return spawn direction
        return Quaternion.Euler(dir[n]);
    }
    #endregion
}
