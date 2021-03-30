using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyPenny : Collectable
{
    public override void DropLogic()
    {
        if (Player.Instance.PInven.HasItem(this) == false)
            PlayerInfo.DoubleHarvest = false;
    }

    public override void Interact()
    {
        if (Player.Instance.PInven.AddItem(this))
            PlayerInfo.DoubleHarvest = true;

        PopupCheck();
    }
}
