using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : Collectable, IUseable
{
    private bool Used { get; set; }

    public override void DropLogic()
    {
        if (Used) Destroy(gameObject);
    }

    public override void Interact()
    {
        base.Interact();
    }

    public void UseItem()
    {
        Used = true;
        Player.Instance.Health = 20;
        Player.Instance.PInven.RemoveItem(this);
    }
}
