using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Codename: "Blasty"
/// "A mid-level variant of this enemy will chase the player 
/// around the map and when in a specific radius of the player, 
/// the enemy will stop and detonate near the player. 
/// This enemy will have 30 HP and will deal 2 damage if the player 
/// is in the blast radius of their detonation."
/// </summary>


public class EnemyC_2 : EnemyC
{
    [SerializeField] protected float initDetonationRange; // When player is in this range, start exploding.
    [SerializeField] protected GameObject explosionPrefab; // Explosion object. Uses prefab similar to EnemyBomb.

    // Enemy inherits chase behavior from EnemyC base class.

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        // Very simple AI, just uses the base chasing behavior.
        // When in detonation range, it spawns the explosion object and destroys itself.
        distanceFromPlayer = Vector3.Distance(transform.position, PlayerObject.transform.position);
        if (distanceFromPlayer < initDetonationRange)
        {
            //Debug.Log(distanceFromPlayer);
            Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }


}
