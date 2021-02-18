using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCandy : Collectable
{
    public override void DropLogic()
    {
        throw new System.NotImplementedException();
    }

    public override void Interact()
    {
        PlayerInfo.AttackDamage++;
        Debug.Log("Player Attack now: " + PlayerInfo.AttackDamage);
    }
}
