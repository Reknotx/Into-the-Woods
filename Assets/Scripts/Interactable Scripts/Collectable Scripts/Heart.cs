using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Collectable
{
    public override void Interact()
    {
        Debug.Log("Healing the player");
        Player.Instance.Health++;
        Destroy(gameObject);
    }
}
