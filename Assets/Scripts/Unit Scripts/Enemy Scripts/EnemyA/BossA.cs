using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Codename: "Rocky"
/// "A low-level variant of this enemy shoots projectiles at the player. 
/// These will have 15 HP (3 hits from the player) and the projectiles 
/// that they release will deal 1 damage to the player."
/// </summary>

public class BossA : EnemyA
{

    protected override void Start()
    {
        base.Start();

        // Custom stats
        if (Health == 0)
        {
            Health = 100;
        }
        if (shotCooldown == 0)
        {
            shotCooldown = 0.5f;
        }

        // Might need to replace this with Unit function when it's done.

    }


}
