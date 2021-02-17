using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeSpell : Spell
{

    /// <summary> The time that an enemy hit by the spell will be frozen for. </summary>
    public float freezeDuration = 5f;

    public override void TriggerSpellEffect(GameObject other)
    {
        Debug.Log("Casting the Freeze Spell");
        Enemy enemy = other.GetComponent<Enemy>();
        enemy.StartCoroutine(enemy.Freeze(freezeDuration));
    }

}
