using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PotionIngredient : Collectable
{
    /// <summary> The amount of this potion ingredient in our inventory. </summary>
    [HideInInspector] public int amountInInv = 0;
}
