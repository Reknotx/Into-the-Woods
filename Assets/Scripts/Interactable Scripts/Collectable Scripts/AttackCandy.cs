using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCandy : Collectable
{
    public override void DropLogic()
    {
        base.DropLogic();
        PlayerInfo.AttackDamage--;
    }

    public override void Interact()
    {
        if (Player.Instance.PInven.AddItem(this))
        {
            PlayerInfo.AttackDamage++;
            Debug.Log("Player Attack now: " + PlayerInfo.AttackDamage);
        }
    }
}
