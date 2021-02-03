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
    public override void TriggerSpellEffect()
    {
        Debug.Log("Casting the Attack Spell");
    }
}
