using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockingSpell : Spell
{
    public override void TriggerSpellEffect(GameObject other)
    {
        Debug.Log("Casting the Unlocking Spell");

        ///When spell hits the chest
        ///
        ///1. The chest will open
        ///2. Item raises out of the chest after it opens. Chest only has one item.
        ///3. Item can be picked up by the player.
        ///4. Chest stays there and can't be hit by the unlocking spell again.
        
        other.GetComponent<RareChest>().Open();
    }
}
