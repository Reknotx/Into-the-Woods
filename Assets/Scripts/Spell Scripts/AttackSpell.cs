using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: Chase O'Connor
/// Date: 2/2/2021
/// <summary>
/// The basic attack spell the player can cast.
/// </summary>
public class AttackSpell : TrackingSpell
{
    public override void TriggerSpellEffect(GameObject other)
    {
        //Debug.Log("Casting the Attack Spell");
        //Destroy(other.gameObject);

        other.GetComponent<Enemy>().Health -= PlayerInfo.AttackDamage;

        //Debug.Log("Damage = " + PlayerInfo.AttackDamage.ToString());
    }
}
