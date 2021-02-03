using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The base class for objects that can be interacted with by the player.
/// This includes chests and collectables.
/// </summary>
public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact();

}
