using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : Collectable
{
    public override void DropLogic()
    {
        base.DropLogic();

        if (Player.Instance.PInven.HasItem(this) == false)
            PlayerInfo.SpellTracking = false;
    }

    public override void Interact()
    {
        if (Player.Instance.PInven.AddItem(this)) PlayerInfo.SpellTracking = true;
    }
}
