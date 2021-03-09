using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// These enemies move around randomly.
/// They don't track the player.
/// </summary>

public class EnemyB : Enemy
{


    [SerializeField] protected float wanderCooldown; // How often does the unit walk to a random point?

    [SerializeField] protected float wanderDistance; // How far to go?


    protected override void Start()
    {
        if (wanderCooldown <= 0)
        {
            wanderCooldown = 4f;
            Debug.Log("Please use a positive value for wanderCooldown. Set to default.");
        }

        if (wanderDistance <= 0)
        {
            wanderDistance = 3f;
            Debug.Log("Please use a positive value for wanderDistance. Set to default.");
        }

        base.Start();


    }


    #region Wandering AI
    /// <summary>
    /// These handle the core wandering logic used by all the EnemyB subtypes.
    /// They all use some variant of this, but call it with different timings.
    /// </summary>

    protected void WanderToPoint()
    {
        agent.SetDestination(PickRandomPoint(agent.transform.position, wanderDistance, 31));
        //Debug.Log(this.gameObject.transform.position);
    }

    /// Author: Paul Hernandez
    /// Date: 3/1/2021
    /// <summary>
    /// Choose a random point on a given layer.
    /// </summary>
    /// <param name="origin">Where to calculate the navigation from (the agent's current position).</param>
    /// <param name="distance">Distance to look for point.</param>
    /// <param name="layermask">What navmesh layer you're checking against. Currently, always 31 for "Ground".</param>
    /// <returns>Returns the random position to move to. Use with agent.SetDestination.</returns>
    protected static Vector3 PickRandomPoint (Vector3 origin, float distance, int layermask)
    {
        // Pick a random direction to look for a point in.
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        // Relative to our agent's position.
        randomDirection += origin;

        // Point on the navmesh.
        NavMeshHit navHit;

        // Get that position on the navmesh of the layermask.
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        // Return the point found.
        return navHit.position;
    }
#endregion
}
