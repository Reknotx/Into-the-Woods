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
    public GameObject trackedEnemy;


    protected override void Start()
    {
        base.Start();

        if (PlayerInfo.SpellTracking)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

    }

    public override void TriggerSpellEffect(GameObject other)
    {
        //Debug.Log("Casting the Attack Spell");
        Destroy(other.gameObject);
        Destroy(gameObject);
        //Debug.Log("Damage = " + PlayerInfo.AttackDamage.ToString());
    }
}
