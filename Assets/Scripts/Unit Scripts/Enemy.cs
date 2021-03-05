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

    /// <summary> If the enemy is frozen they can't move. </summary>
    public bool IsFrozen { get; set; }

    // Stats
    // HP is currently handled by Unit.cs
    protected bool onCooldownShoot; // Whether this unit's shooting attack is on cooldown.
    protected bool onCooldownBody; // Whether this unit's contact damage attack is on cooldown.
    [SerializeField] protected bool contactDamage; // Do I hurt the player on touch?
    [SerializeField] protected int contactDamageAmount; // How much damage do I do on touch?

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

        this.gameObject.GetComponent<NavMeshAgent>().speed = speed; // Set my "speed" variable inherited from Unit to my NavMeshAgent speed.
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

    #region Contact Damage

    protected void OnTriggerStay(Collider other)
    {
        if (contactDamage && !onCooldownBody && other.gameObject.layer == 8) // If "Player" layer.
        {
            Player.Instance.TakeDamage(contactDamageAmount);
            StartCoroutine(TackleCooldown(1f));
            //Debug.Log(onCooldown);
        }
    }

    /// Author: Paul Hernandez
    /// Date: 2/22/2021
    /// <summary>
    /// This puts the body contact hitbox for this unit on cooldown.
    /// </summary>
    protected IEnumerator TackleCooldown(float time)
    {
        onCooldownBody = true;
        yield return new WaitForSeconds(time);
        onCooldownBody = false;
    }

    #endregion



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
