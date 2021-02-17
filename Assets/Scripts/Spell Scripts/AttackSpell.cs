using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: Chase O'Connor
/// Date: 2/2/2021
/// <summary>
/// The basic attack spell the player can cast.
/// </summary>
public class AttackSpell : Spell
{
    /// <summary> Local variable to tell us if we are tracking. </summary>
    private bool track = true;

    /// <summary>Spell rotation speed </summary>
    public float rotationSpeed = 100.0f;

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

    public override void TriggerSpellEffect(GameObject other)
    {
        //Debug.Log("Casting the Attack Spell");
        Destroy(other.gameObject);

        //other.GetComponent<Enemy>().Health -= PlayerInfo.AttackDamage;

        //Debug.Log("Damage = " + PlayerInfo.AttackDamage.ToString());
    }
    /// BREAK IN THE CODE
    /// Below is the code from a previous tutorial I've used

    
}
