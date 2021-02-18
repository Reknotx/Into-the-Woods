using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPeas : Collectable
{
    public override void DropLogic()
    {
        base.DropLogic();

        if (Player.Instance.PInven.HasItem(this) == false)
            PlayerInfo.DoubleShot = false;
    }

    public override void Interact()
    {
        if (Player.Instance.PInven.AddItem(this)) 
            PlayerInfo.DoubleShot = true;
    }
}
