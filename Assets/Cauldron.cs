using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : Interactable
{
    /// <summary>
    /// Opens up the brewing system menu.
    /// </summary>
    public override void Interact()
    {
        BrewingSystem.Instance.gameObject.SetActive(true);
    }
}
