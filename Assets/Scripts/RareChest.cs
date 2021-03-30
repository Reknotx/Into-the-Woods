using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareChest : MonoBehaviour
{
    public Animator animator;

    public ChestLootTable lootTable;

    public ChestTier tier;

    /// <summary> Triggers the opening animation for the chest so the item is displayed. </summary>
    public void Open()
    {
        ///Start chest opening animation
        ///Start item rising animation
        ///


        GameObject item = lootTable.Drop(tier);

        if (item != null)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, item.transform.position.y + .5f, transform.position.z);
            Instantiate(item, spawnPos, Quaternion.identity);

        }


        Destroy(gameObject);
    }
}
