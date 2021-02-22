using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : Collectable
{
    public override void DropLogic()
    {
        throw new System.NotImplementedException();
    }

    public override void Interact()
    {
        PlayerInfo.SpellTracking = true;
        base.Interact();
    }
}
