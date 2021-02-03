using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeSpell : Spell
{
    public override void TriggerSpellEffect(GameObject other)
    {
        Debug.Log("Casting the Freeze Spell");
    }

}
