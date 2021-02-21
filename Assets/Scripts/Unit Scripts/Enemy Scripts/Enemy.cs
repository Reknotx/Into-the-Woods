using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// Author: Paul Hernandez
/// Date: 2/18/2021
/// <summary>
/// Base of all enemy units.
/// </summary>
public class Enemy : Unit
{
    // Player tracking
    [SerializeField] protected GameObject PlayerObject; // The player object.
    protected float playerTrackRate = 0.2f; // How often I'll update the player position.
    protected float distanceFromPlayer; // The distance between me and the player.
    [SerializeField] protected float awarenessRange; // How far I can sense the player.

    // Navigation
    //public bool chasePlayer; // If the enemy is currently chasing the player.
    protected Vector3 myHome; // Wherever I'm placed in the editor will be my "home".
    protected NavMeshAgent agent; // My navmesh agent component.
                                  //public float deAggroTimer; // If the player leaves my range for this time, I'll start going back home.

    // Stats
    // HP is currently handled by Unit.cs

    /// Author: Paul Hernandez
    /// Date: 2/20/2021
    /// <summary>
    /// All AI starts by calling "initialization". It grabs components necessary
    /// for its operations. This way, the designers don't have to fiddle with
    /// enemy stuff in the inspector or hierarchy (hopefully!).
    /// </summary>
    protected virtual void Start()
    {
        InitAI();
    }


    #region BasicAI

    /// Author: Paul Hernandez
    /// Date: 2/18/2021
    /// <summary>
    /// Finds player for navigation, and sets home location.
    /// </summary>
    protected void InitAI()
    {
        // Look for my own navmesh agent
        if (this.gameObject.GetComponent<NavMeshAgent>() != null)
        {
            agent = this.gameObject.GetComponent<NavMeshAgent>();
        }
        else
        {
            Debug.Log("Enemy is missing NavMesh Agent!");
        }

        // Look for player, with a safety check.
        if (Player.Instance.gameObject != null)
        {
            PlayerObject = Player.Instance.transform.parent.gameObject;
        }
        else
        {
            Debug.Log("No player object found!");
        }

        myHome = transform.position; // If I get a "return home" command, I'll go back to where I was placed.
    }

    /// Author: Paul Hernandez
    /// Date: 2/18/2021
    /// <summary>
    /// Invokes constant chasing of player by calling MoveToPlayer() every 0.2 seconds.
    /// </summary>
    protected void ChasePlayer()
    {
        InvokeRepeating("MoveToPlayer", 0f, playerTrackRate);
    }

    /// Author: Paul Hernandez
    /// Date: 2/18/2021
    /// <summary>
    /// Tells AI to move to player's CURRENT position, if it's in awarenessRange.
    /// </summary>
    protected void MoveToPlayer()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, PlayerObject.transform.position);
        if (distanceFromPlayer < awarenessRange)
        {
            agent.SetDestination(PlayerObject.transform.position);
        }
    }

    /// Author: Paul Hernandez
    /// Date: 2/18/2021
    /// <summary>
    /// Tells AI to path to where their initial position.
    /// </summary>
    protected void ReturnHome()
    {
        CancelInvoke(); // Cancels ChasePlayer.
        agent.SetDestination(myHome);
    }

    #endregion
}
