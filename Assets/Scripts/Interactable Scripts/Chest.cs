using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    public ChestLootTable lootTable;

    public ChestTier tier;

    public override void Interact()
    {
        GameObject loot = lootTable.Drop(tier);

        if (loot != null)
            Instantiate(loot, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

}
