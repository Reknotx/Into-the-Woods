using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Collectable
{
    /// <summary> This should never be called by any function for hearts. </summary>
    public override void DropLogic()
    {
        throw new System.NotImplementedException();
    }

    public override void Interact()
    {
        Debug.Log("Healing the player");
        Player.Instance.Health++;
        Destroy(gameObject);
    }
}
