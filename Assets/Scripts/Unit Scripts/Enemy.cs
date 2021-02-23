using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    /// <summary> If the enemy is frozen they can't move. </summary>
    public bool IsFrozen { get; set; }

<<<<<<< Updated upstream
=======
    // Stats
    // HP is currently handled by Unit.cs

    public override int Health
    {
        get => base.Health;
        set
        {
            base.Health = value;
            Debug.Log("Called enemy health property");
        }
    }

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



>>>>>>> Stashed changes

    /// <summary>
    /// Freezes the enemy in place for the specified amount of time.
    /// </summary>
    /// <param name="freezeDuration">The amount of real world time the enemy is frozen for.</param>
    public IEnumerator Freeze(float freezeDuration)
    {
        IsFrozen = true;

        yield return new WaitForSeconds(freezeDuration);

        IsFrozen = false;
    }

}
