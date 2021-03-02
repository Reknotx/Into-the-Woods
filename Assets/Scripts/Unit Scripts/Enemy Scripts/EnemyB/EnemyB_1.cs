using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Codename: "Runny"
/// "A low-level variant of this enemy will simply run around 
/// to keep the player moving around the space. "
/// Does contact damage.
/// </summary>

public class EnemyB_1 : EnemyB
{

    protected override void Start()
    {
        base.Start();

        // Custom stats
        if (Health == 0)
        {
            Health = 15;
        }
        

    }
}
