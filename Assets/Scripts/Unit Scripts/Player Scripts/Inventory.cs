using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: JT Esmond
/// Date: 2/16/2021
/// <summary>
/// This Inventory Class contains the functions that control the functionality of the inventory
/// </summary>
public class Inventory
{
    public event EventHandler OnItemListChanged;

    /// <summary> The list of potions we currently have. </summary>
    private Potion[] potions;

    /// <summary> The list of collectable items in our inventory. </summary>
    private List<Collectable> itemList;

    /// <summary> The reference to the collectable items in our inventory. </summary>
    public List<Collectable> ItemList => itemList;

    //holds the actual inventory
    public Inventory()
    {
        itemList = new List<Collectable>();
        potions = new Potion[3];

        //Debug.Log(itemList.Count);
    }

    //function that adds items to the inventory itemList
    /// Author: JT
    /// Date: 2/16/2021
    /// <summary>
    /// Add the item to the inventory if there is room.
    /// </summary>
    /// <param name="item">The item to add.</param>
    /// <returns>False if the inventory is full, true otherwise.</returns>
    /// Edit: Chase O'Connor
    /// Function was restructred to work with our current system rather than the
    /// system that was made following the tutorial.
    public bool AddItem(Collectable item)
    {
        if (itemList.Count == 5) return false;

        ///checks if the item is a potion ingredient
        if (item is PotionIngredient)
        {
            //boolean to tell if the potion ingredient is already in the inventory itemList
            bool ingredientInInven = false;

            //for each loop that adds the  item to the stack of the already existing item.
            foreach (Collectable invenItem in itemList)
            {
                if (invenItem is PotionIngredient ingredient
                    && invenItem.GetType() == item.GetType())
                {
                    ingredient.amountInInv++;
                    ingredientInInven = true;
                    break;
                }
            }

            // if the item isn't in the inventory it adds it to the inventory itemList
            if (!ingredientInInven)
            {
                itemList.Add(item);
                item.GetComponent<PotionIngredient>().amountInInv = 1;
                TurnOffItem(item);
            }
        }
        //if the item isn't stackable it just adds it to the inventory itemList as normal
        else
        {
            ///Collectable is just a standard item.
            itemList.Add(item);
            TurnOffItem(item);
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        return true;
    }

    /// Author: JT
    /// Date: 2/16/2021
    /// <summary>
    /// Remove the item from inventory.
    /// </summary>
    /// <param name="item">The item to remove.</param>
    /// Edit: Chase O'Connor
    /// Function was restructred to work with our current system rather than the
    /// system that was made following the tutorial.
    /// FURTHER NOTICE
    /// This function might need to be restructed again based on how the dropping
    /// of items from our inventory is handled as the system from the tutorial is 
    /// still vastly different from the one we have. More planning will be done to 
    /// deal with this.
    public void RemoveItem(Collectable item)
    {
        //checks if the item is stackable
        if (item is PotionIngredient)
        {
            PotionIngredient ingredientInInven = null;
            //for each loop that removes the item from the stack of existing items
            foreach (Collectable invenItem in itemList)
            {
                if (invenItem is PotionIngredient ingredient
                    && invenItem.GetType() == item.GetType())
                {
                    ingredient.amountInInv--;
                    ingredientInInven = ingredient;
                }
            }
            //if the item isn't in the inventory and the amount is <= 0  removes it from the itemList
            if (ingredientInInven != null && ingredientInInven.amountInInv <= 0)
            {
                itemList.Remove(item);
            }
        }
        //if the item isn't stackable it removes it from the inventroy itemList
        else
        {
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Loops through the list searching for the specific item.
    /// </summary>
    /// <param name="item">The type of item we want to search for.</param>
    /// <returns>True if we have the item, false otherwise.</returns>
    public bool HasItem(Collectable item)
    {
        if (itemList.Count == 0) return false;

        foreach (Collectable invenItem in itemList)
        {
            if (invenItem.GetType() == item.GetType()) return true;
        }
        return false;
    }

    /// Author: Chase O'Connor
    /// Date: 2/18/2021
    /// <summary>
    /// Turns off the item in the world and changes it's parent to 
    /// the storage game object for the player so they have the
    /// references.
    /// </summary>
    /// <param name="item">The item that was successfully added to the inventory.</param>
    private void TurnOffItem(Collectable item)
    {
        item.gameObject.transform.parent = Player.Instance.PlayerInvenItems.transform;
        item.gameObject.transform.localPosition = Vector3.zero;
        item.gameObject.SetActive(false);
    }
}
