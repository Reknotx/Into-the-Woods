using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Codename: "Bomby"
/// "A mid-level variant of this enemy will run around the map, but also 
/// drop exploding “bombs” that will detonate after 2 seconds of being dropped. 
/// The enemy will drop a bomb every 5 seconds. 
/// These enemies will have 30 HP (6 hits from the player)
/// and will deal 2 damage to the player. 
/// The bombs, if the player is in the radius, will also deal 2 damage."
/// </summary>

public class EnemyB_2 : EnemyB
{
    [SerializeField] protected GameObject liveBombPrefab;
    //[SerializeField] protected float bombDropCooldown; // How long to wait between dropping bombs.

    protected override void Start()
    {
        base.Start();

        // Custom stats
        if (Health == 0)
        {
            Health = 30;
        }

        

    }

    protected override void Awake()
    {
        base.Awake();
        InvokeRepeating("DropBomb", wanderCooldown, wanderCooldown);
    }

    protected void DropBomb()
    {
        if (IsFrozen) return;

        if (this.gameObject.activeSelf == true)
        {
            // WanderToPoint();
            Instantiate(liveBombPrefab, this.transform.position, this.transform.rotation);
        }
        
    }

}
