using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenHeartPotion : Potion
{
    public override void UsePotion()
    {
        Player.Instance.StartCoroutine(Player.Instance.FrozenHeart(10f));
    }
}
