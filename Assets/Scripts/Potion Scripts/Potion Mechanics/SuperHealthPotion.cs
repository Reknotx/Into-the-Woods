using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperHealthPotion : Potion
{
    public override void UsePotion()
    {
        Player.Instance.Health = 20;
    }
}
