using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDamagePotion : Potion
{
    public override void UsePotion()
    {
        Player.Instance.StartCoroutine(Player.Instance.DoubleDamage(10f));
    }
}
