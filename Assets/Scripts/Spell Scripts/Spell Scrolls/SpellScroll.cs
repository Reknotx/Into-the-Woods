using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// Author: Chase O'Connor
/// Date: 3/23/2021
/// <summary>  A spell scroll to teach the player a new spell. </summary>

[CreateAssetMenu]
public class SpellScroll : ScriptableObject
{
    /// <summary> The spell that will be taught to the player. </summary>
    [Header("The spell that will be taught to the player.")]
    public GameObject spellToLearn;

    [HideInInspector] public bool hasCollected;

}
