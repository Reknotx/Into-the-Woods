using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A type of enemy that chases the player.
/// </summary>


public class EnemyC : Enemy
{

    ///Movement logic of Enemy C variants stays here
    ///

    protected override void Start()
    {
        base.Start();

        // All enemy C types chase the player.
        //ChasePlayer(); 
        
    }

    protected override void FixedUpdate()
    {
        if (IsFrozen) return;
        base.FixedUpdate();
        MoveToPlayer();
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
            transform.LookAt(PlayerObject.transform);
            gameObject.transform.eulerAngles = new Vector3(0f, gameObject.transform.eulerAngles.y, 0f); // reset other rotations asides Y.
            this.GetComponent<Rigidbody>().AddForce(this.transform.forward * movementSpeed * 0.1f, ForceMode.Impulse);

        }
        else
        {
            // stop moving
        }
    }
}
