using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemy : Enemy
{



    protected override void Awake()
    {
        base.Awake();
    }

    private void FixedUpdate()
    {
        if (IsFrozen) return;

        Move();
    }

    protected override void Move()
    {
        Vector3 tempDir = Player.Instance.transform.position - transform.position;

        moveDir = tempDir.normalized;
        base.Move();
    }
}
