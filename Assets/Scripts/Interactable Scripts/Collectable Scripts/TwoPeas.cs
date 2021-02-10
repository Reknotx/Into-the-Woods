using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPeas : Collectable
{
    public override void DropLogic()
    {
        throw new System.NotImplementedException();
    }

    public override void Interact()
    {
        PlayerInfo.DoubleShot = true;
    }
}
