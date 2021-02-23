using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PotionIngredient : Collectable
{
    /// <summary> The amount of this potion ingredient in our inventory. </summary>
    [HideInInspector] public int amountInInv = 0;

    public override void Interact()
    {
        if (Player.Instance.PInven.HasItem(this))
        {
            base.Interact();
            Destroy(gameObject);
        }
        else
        {
            base.Interact();
        }

    }

    /// <summary>
    /// If reached here it means another instance of the potion ingredient needs to be spawned.
    /// </summary>
    public override void DropLogic()
    {
        Instantiate(this.gameObject, Player.Instance.transform.position + Vector3.right, Quaternion.identity).SetActive(true);
    }
}
