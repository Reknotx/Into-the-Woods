using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: Chase O'Connor
/// Date: 2/16/2021
/// <summary>
/// A spell cooldown class that handles the cooldown of player spells.
/// </summary>
public class SpellCDTicker
{

    /// <summary> The spells currently on cooldown. </summary>
    private List<Spell> spellsOnCD = new List<Spell>();


    public SpellCDTicker()
    {
        spellsOnCD = new List<Spell>();
    }

    /// Author: Chase O'Connor
    /// Date: 2/16/2021
    /// <summary>
    /// Ticks every frame to reduce the amount of the remaining cooldown on each spell.
    /// </summary>
    /// <param name="deltaTime">The time difference since the last frame.</param>
    public void Tick(float deltaTime)
    {
        if (spellsOnCD.Count == 0) return;

        //Debug.Log("Spells on CD = " + spellsOnCD.Count);

        List<Spell> removeList = new List<Spell>();

        foreach (Spell spell in spellsOnCD)
        {
            spell.remainingCooldown -= deltaTime;

            if (spell.remainingCooldown <= 0f)
            {
                removeList.Add(spell);
            }
        }

        if (removeList.Count == 0) return;


        foreach (Spell removedSpell in removeList)
        {
            spellsOnCD.Remove(removedSpell);
        }

        removeList.Clear();
    }

    /// Author: Chase O'Connor
    /// Date: 2/16/2021
    /// <summary> Adds a spell to the cooldown list. </summary>
    /// <param name="spell">The spell to add to the cooldown list.</param>
    public void AddToList(Spell spell)
    {
        spell.remainingCooldown = spell.coolDown;

        spellsOnCD.Add(spell);
    }

    /// Author: Chase O'Connor
    /// Date: 2/16/2021
    /// <summary>
    /// Checks the cooldown list to see if a spell is currently on cooldown.
    /// </summary>
    /// <param name="spell">The spell to look for.</param>
    /// <returns>True if the spell is located in the list, false otherwise.</returns>
    public bool SpellOnCD(Spell spell)
    {
        foreach (Spell spellCD in spellsOnCD)
        {
            if (spellCD.GetType() == spell.GetType()) return true;
        }
        return false;
    }
}
