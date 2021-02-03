using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: Chase O'Connor
/// Date: 2/2/2021
/// <summary>
/// The base spell class of all spells the player can cast.
/// </summary>
public abstract class Spell : MonoBehaviour
{
    /// Author: Chase O'Connor
    /// Date: 2/2/2021
    /// <summary> The abstract function that all spells have to write their own functionality. </summary>
    public abstract void TriggerSpellEffect(GameObject other);

    public void OnTriggerEnter(Collider other)
    {
        TriggerSpellEffect(other.gameObject);
    }
}
