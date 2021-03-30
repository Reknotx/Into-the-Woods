using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : Collectable
{
    public override void DropLogic()
    {
    }

    public override void Interact()
    {
        base.Interact();
    }

    public void UseItem()
    {
        Player.Instance.Health = 20;
        Player.Instance.PInven.RemoveItem(this);
    }
}
