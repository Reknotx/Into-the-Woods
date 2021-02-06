using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : Interactable
{
    /// <summary>
    /// The logic that may or may not need to be applied when
    /// the player drops an item.
    /// </summary>
    public abstract void DropLogic();

}
