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
    public override void TriggerSpellEffect(GameObject other)
    {
        if (other.gameObject.layer != 10) return;

        Debug.Log("Casting the Attack Spell");
        Destroy(other.gameObject);
        Destroy(gameObject);
    }

}
