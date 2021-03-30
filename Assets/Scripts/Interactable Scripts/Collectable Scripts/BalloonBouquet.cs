using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonBouquet : Collectable
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

    public override void DropLogic()
    {

    }

    public override void Interact()
    {
        base.Interact();
    }
}
