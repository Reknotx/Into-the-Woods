using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: Chase O'Connor
/// Date: 2/2/2021
/// <summary> Base potion class for all potions. </summary>
public abstract class Potion
{
    
    /// Author: Chase O'Connor
    /// Date: 2/2/2021
    /// <summary>
    /// Abstract function implemented by each special potion for their unique 
    /// functionality.
    /// </summary>
    public abstract void UsePotion();
    
}
