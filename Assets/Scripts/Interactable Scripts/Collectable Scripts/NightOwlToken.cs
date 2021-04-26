using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightOwlToken : Collectable
{
    public override void DropLogic()
    {
        RoomRestriction.Instance.nightOwl = false;
    }

    public override void Interact()
    {
        RoomRestriction.Instance.nightOwl = true;
        base.Interact();

    }
}
