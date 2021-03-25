using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Structs
[Serializable]
public struct DropRates
{
    [Range(0f, 1f)]
    public float lowTierRate;
    
    [Range(0f, 1f)]
    public float midTierRate;
    
    [Range(0f, 1f)]
    public float highTierRate;
}

[Serializable]
public struct LootI
{
    [Tooltip("The name of the item.")]
    public string itemName;
    
    [Tooltip("The prefab for the interactable item.")]
    public GameObject item;
    
    [Tooltip("The rate at which each item drops from the chests.")]
    public DropRates rates;
}
#endregion

public enum ChestTier
{
    Low,
    Mid,
    High
}

/// <summary>
/// The loot table holds all of the loot info for 
/// the items/drops of the world.
/// </summary>
[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public List<LootI> loot = new List<LootI>();
    //public List<LootInfo> items = new List<LootInfo>();




    public float GetTierWeightTotal(ChestTier tier)
    {
        float totalWeights = 0f;
        
        foreach (LootI item in loot)
        {
            switch (tier)
            {
                case ChestTier.Low:
                    totalWeights += item.rates.lowTierRate;
                    break;

                case ChestTier.Mid:
                    totalWeights += item.rates.midTierRate;
                    break;

                case ChestTier.High:
                    totalWeights += item.rates.highTierRate;
                    break;

                default:
                    break;
            }
        }

        return totalWeights;
    }

}
