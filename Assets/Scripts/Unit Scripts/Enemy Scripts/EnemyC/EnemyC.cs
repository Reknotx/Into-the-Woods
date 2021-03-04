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
        ChasePlayer(); 
    }


}
