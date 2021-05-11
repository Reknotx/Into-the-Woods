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

    [SerializeField] protected float wanderMaxTime;
    protected float wanderMinTime;
    [SerializeField] protected float wanderCooldown; // How often does the unit walk to a random point?

    [SerializeField] protected GameObject myModel;
    //[SerializeField] protected float wanderDistance; // How far to go?

    protected int wanderDirection;


    protected override void Start()
    {
        StartCoroutine(WanderSustain(0.5f));

        base.Start();


    }

    protected override void FixedUpdate()
    {
        if (IsFrozen) return;

        base.FixedUpdate();
        WanderInDirection();

    }

    #region Wandering AI
    /// <summary>
    /// These handle the core wandering logic used by all the EnemyB subtypes.
    /// They all use some variant of this, but call it with different timings.
    /// </summary>
    /// 

    // New wander behavior.
    protected void WanderInDirection()
    {

        if (wanderDirection < 2) // Up or down
        {
            if (wanderDirection == 0) // Up
            {
                this.GetComponent<Rigidbody>().AddForce(this.transform.forward * movementSpeed * 0.1f, ForceMode.Impulse);
            }
            else // 1, Down
            {
                this.GetComponent<Rigidbody>().AddForce(-this.transform.forward * movementSpeed * 0.1f, ForceMode.Impulse);
            }
        }
        else
        {
            if (wanderDirection == 2) // Left
            {
                this.GetComponent<Rigidbody>().AddForce(-this.transform.right * movementSpeed * 0.1f, ForceMode.Impulse);
            }
            else // 3, Right
            {
                this.GetComponent<Rigidbody>().AddForce(this.transform.right * movementSpeed * 0.1f, ForceMode.Impulse);
                
            }
        }

        myModel.transform.LookAt(this.transform.position + this.GetComponent<Rigidbody>().velocity);
        //Vector3 dir = new Vector3();
        //dir.x = th
        //transform.rotation = Quaternion.LookRotation(this.GetComponent<Rigidbody>())

    }

    public IEnumerator WanderSustain(float distance)
    {
        yield return new WaitForSeconds(distance);

        wanderDirection = Random.Range(0, 4);
        wanderCooldown = Random.Range(wanderMinTime, wanderMaxTime);
        StartCoroutine(WanderSustain(wanderCooldown));
    }


    protected void WanderToPoint()
    {
        if (this.gameObject.activeSelf == true)
        {
            //agent.SetDestination(PickRandomPoint(agent.transform.position, wanderDistance, 31));
        }
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
        //NavMeshHit navHit;

        // Get that position on the navmesh of the layermask.
        //NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        // Return the point found.
        //return navHit.position;
        return new Vector3();
    }
#endregion
}
