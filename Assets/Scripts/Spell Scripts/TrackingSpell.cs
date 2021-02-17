using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrackingSpell : Spell
{
    /// <summary> Local variable to tell us if we are tracking. </summary>
    private bool track = true;

    protected override void Start()
    {
        base.Start();

        if (PlayerInfo.SpellTracking)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            track = true;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (track && trackedEnemy != null && CalculateDistance() > 0.1f)
        {
            CalculateAngle();
        }
    }
}
