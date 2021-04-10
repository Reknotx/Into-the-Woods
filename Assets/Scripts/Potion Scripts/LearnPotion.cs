using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: Chase O'Connor
/// Date: 2/20/2021
/// <summary> Provides the player with a new recipe that they can now craft. </summary>
public class LearnPotion : Interactable
{
    /// <summary> The recipe that will be unlocked. </summary>
    [Tooltip("The potion recipe that will be unlocked.")]
    [SerializeField] private PotionRecipe unlockRecipe;

    /// <summary> Unlocks a potion recipe that the player can now craft. </summary>
    public override void Interact()
    {
        BrewingSystem.Instance.AddRecipe(unlockRecipe);
        if (!unlockRecipe.hasCollected)
        {
            PopUpManager.Instance.PotionPopUp(this, unlockRecipe);
            unlockRecipe.hasCollected = true;
        }
        Destroy(gameObject);
    }
}
