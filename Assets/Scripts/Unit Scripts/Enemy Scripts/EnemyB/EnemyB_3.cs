using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Codename: "Shieldy"
/// "A high-level variant of this enemy will run around the map 
/// but also have a shield that will prohibit the player from damaging them. 
/// The enemy will be able to put up their shield every 5 seconds
/// and have 50 HP (10 hits from the player). 
/// This enemy will take away 3 damage if it touches the player."
/// </summary>

public class EnemyB_3 : EnemyB
{

    protected override void Start()
    {
        base.Start();

        // Custom stats
        if (Health == 0)
        {
            Health = 50;
        }

    }

    IEnumerator fireCooldown(float time)
    {
        onCooldownShoot = true;
        yield return new WaitForSeconds(time);
        onCooldownShoot = false;
    }
}
