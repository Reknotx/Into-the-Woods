using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ChestDropRates
{
    [Range(0f, 1f)]
    public float lowTierRate;

    [Range(0f, 1f)]
    public float midTierRate;

    [Range(0f, 1f)]
    public float highTierRate;
}


/// Author: Chase O'Connor
/// Date: 3/25/3021
/// <summary>
/// The loot info holds the loot info for the specific item.
/// Such as drop rates, the object itself, where it spawns, etc.
/// </summary>
[CreateAssetMenu]
public class LootInfo : ScriptableObject
{
    public GameObject item;

    public string itemName;

    public Sprite invSprite;

    public ChestDropRates rates;
}
