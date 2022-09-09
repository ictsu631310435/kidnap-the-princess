using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MeleeEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        aiPath = GetComponent<AIPath>();
        aiPath.rotationSpeed = turnSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector2.Distance(transform.position, player.position);
    }
}
