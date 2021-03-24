using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnSpell : Interactable
{
    /// <summary> "The spell that will be taught to the player." </summary>
    [Tooltip("The spell that will be taught to the player.")]
    public SpellScroll spellScroll;

    
    public override void Interact()
    {
        if (!Player.Instance.spells.Contains(spellScroll.spellToLearn))
            Player.Instance.spells.Add(spellScroll.spellToLearn);
    }
}
