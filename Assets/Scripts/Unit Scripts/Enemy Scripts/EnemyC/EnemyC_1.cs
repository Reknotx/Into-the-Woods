using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Codename: "Chasey"
/// "A low-level variant of this enemy will chase the player 
/// around the space. This enemy will have 15 HP and when 
/// touching the player, they will deal 1 damage per hit."
/// </summary>


public class EnemyC_1 : EnemyC
{

    protected override void Start()
    {
        base.Start();

        // Custom stats
        _health = 15;
        // Might need to replace this with Unit function when it's done.

    }

}
