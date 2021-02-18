using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierBreakerSpell : Spell
{
    public override void TriggerSpellEffect(GameObject other)
    {
        Debug.Log("Casting the Barrier Breaker Spell");
    }

}
