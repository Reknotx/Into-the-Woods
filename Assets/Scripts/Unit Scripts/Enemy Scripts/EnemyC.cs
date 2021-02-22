using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyC : Enemy
{
    ///Movement logic of Enemy C variants stays here
    ///

    private void Start()
    {
        InvokeRepeating("Move", 0.1f, 0.1f);
    }


    protected override void Move()
    {
        ///Base movement logic
    }
}
