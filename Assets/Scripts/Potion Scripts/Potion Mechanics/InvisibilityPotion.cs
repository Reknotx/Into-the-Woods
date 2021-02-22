using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilityPotion : Potion
{
    public override void UsePotion()
    {
        Player.Instance.StartCoroutine(Player.Instance.Invisibility(10f));
    }
}
