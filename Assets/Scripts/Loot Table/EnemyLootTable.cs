using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region Structs

[Serializable]
public struct LootInfo
{
    [Tooltip("The name of the item.")]
    public string itemName;
    
    [Tooltip("The prefab for the interactable item.")]
    public GameObject item;
    
    [Tooltip("When turned on this item can only spawn at night.")]
    public bool nightOnly;

    [Range(0f, 100f)]
    public float rate;
}
#endregion


/// <summary>
/// The loot table holds all of the loot info for 
/// the items/drops of the world.
/// </summary>
[CreateAssetMenu(fileName = "New Enemy Loot Table", menuName = "Loot Table/Enemy Loot Table", order = 51)]
public class EnemyLootTable : LootTable
{
    public List<LootInfo> loot = new List<LootInfo>();


    private float GetWeightTotal()
    {
        float total = 0f;

        foreach (LootInfo info in loot)
        {
            total += info.rate;
        }

        return total;

    }


    public GameObject Drop()
    {
        float total = GetWeightTotal();

        insertionSort(loot);

        float num = UnityEngine.Random.Range(0f, total);

        foreach (LootInfo info in loot)
        {
            if (num <= info.rate)
            {
                return info.item;
            }
            else
            {
                num -= info.rate;
            }
        }

        return null;

        void insertionSort(List<LootInfo> unsortedLoot)
        {
            int n = unsortedLoot.Count;
            for (int i = 1; i < n; ++i)
            {
                LootInfo key = unsortedLoot[i];
                int j = i - 1;

                while (j >= 0 && unsortedLoot[j].rate > key.rate)
                {
                    unsortedLoot[j + 1] = unsortedLoot[j];
                    j = j - 1;
                }
                unsortedLoot[j + 1] = key;
            }

            loot.Reverse();
        }
    }

}
