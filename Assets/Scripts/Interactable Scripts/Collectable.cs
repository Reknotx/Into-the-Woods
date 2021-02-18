using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : Interactable
{
    /// <summary>
    /// The logic that may or may not need to be applied when
    /// the player drops an item.
    /// </summary>
    /// 

    public Sprite UISprite;

    public virtual void DropLogic()
    {
        Player.Instance.PInven.RemoveItem(this);
    }

    public override void Interact()
    {
        ///Add to inventory
        Player.Instance.PInven.AddItem(this);
    }

}
