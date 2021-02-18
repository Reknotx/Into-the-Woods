using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Unit
{
    // Player tracking
    private GameObject PlayerObject; // The player object.
    private float playerTrackRate = 0.2f; // How often I'll update the player position.
    private float distanceFromPlayer; // The distance between me and the player.
    public float awarenessRange; // How far I can sense the player.

    // Navigation
    //public bool chasePlayer; // If the enemy is currently chasing the player.
    private Vector3 myHome; // Wherever I'm placed in the editor will be my "home".
    public NavMeshAgent agent; // My navmesh agent component.
    //public float deAggroTimer; // If the player leaves my range for this time, I'll start going back home.

    // Stats
    public int currentHP;

        /// <summary>
        /// This needs to be cleaned up and some functionality put into enemy variants.
        /// I just wrote it this way to confirm that I had it working.
        /// </summary>
    void Start()
    {
        
    }



    void InitAI()
    {
        // Look for player, with a safety check.
        if (Player.Instance != null)
        {
            PlayerObject = Player.Instance.transform.parent.gameObject;
        }
        else
        {
            Debug.Log("No player object found!");
        }

        myHome = transform.position; // If I get a "return home" command, I'll go back to where I was placed.
    }

    /// <summary
    /// Behavior to use when enemy should non-stop chase player and try to make contact.
    /// </summary>
    void ChasePlayer()
    {
        InvokeRepeating("MoveToPlayer", 0f, playerTrackRate);
    }

    void MoveToPlayer()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, PlayerObject.transform.position);
        if (distanceFromPlayer < awarenessRange)
        {
            agent.SetDestination(PlayerObject.transform.position);
        }
    }

    void ReturnHome()
    {
        CancelInvoke();
        agent.SetDestination(myHome);
    }

}
