using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ChestLootInfo
{
    [Tooltip("The name of the item.")]
    public string itemName;

    [Tooltip("The prefab for the interactable item.")]
    public GameObject item;

    [Tooltip("When turned on this item can only spawn at night.")]
    public bool nightOnly;

    [Tooltip("The rate at which each item drops from the chests.")]
    public DropRates rates;

    [System.Serializable]
    public struct DropRates
    {
        [Range(0f, 100f)]
        public float lowTierRate;

        [Range(0f, 100f)]
        public float midTierRate;

        [Range(0f, 100f)]
        public float highTierRate;
    }
}


public enum ChestTier
{
    Low,
    Mid,
    High
}

[CreateAssetMenu]
public class ChestLootTable : LootTable
{
    public List<ChestLootInfo> chestLoot = new List<ChestLootInfo>();

    public float GetTierWeightTotal(ChestTier tier)
    {
        float totalWeights = 0f;

        foreach (ChestLootInfo item in chestLoot)
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
