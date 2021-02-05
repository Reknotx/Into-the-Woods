using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The base class for objects that can be interacted with by the player.
/// This includes chests and collectables.
/// </summary>
public abstract class Interactable : MonoBehaviour
{
    /// <summary>
    /// The unique logic of the interactable item when the player
    /// interacts with it.
    /// </summary>
    public abstract void Interact();

}
