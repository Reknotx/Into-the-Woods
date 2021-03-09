using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avocado : Collectable
{
    private int _uses = 2;

    /// <summary> The number of uses on this Avocado. </summary>
    /// <value> Represents how many hits this item will take before
    /// being destroyed. </value>
    public int Uses 
    {
        get => _uses;
        
        set
        {
            _uses = value;

            if (_uses == 0)
            {
                Player.Instance.PInven.RemoveItem(this);
            }
        }
    }

    public override void Interact()
    {
        ///On pick up is added to inventory.
        ///And gives the player an additional heart

        if (Player.Instance.PInven.AddItem(this)) 
            Player.Instance.BonusHealth += Uses;

        PopupCheck();
    }

    /// <summary> Drops the avocado if we drop it ourselves, or it runs out of uses.</summary>
    public override void DropLogic()
    {
        Player.Instance.BonusHealth -= Uses;
        if (_uses == 0) Destroy(gameObject);
    }
}
