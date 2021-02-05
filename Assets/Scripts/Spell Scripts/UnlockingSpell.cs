using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockingSpell : Spell
{
    public override void TriggerSpellEffect(GameObject other)
    {
        Debug.Log("Casting the Unlocking Spell");
    }

}
