using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// Author: JT Esmond
/// Date: 2/16/2021
/// <summary>
/// This Inventory Class contains the functions that control the functionality of the inventory
/// </summary>
public class Inventory
{
    public event EventHandler OnItemListChanged;

    private List<Item> itemList;

    //holds the actual inventory
    public Inventory()
    {
        itemList = new List<Item>();


        Debug.Log(itemList.Count);
    }


    //function that adds items to the inventory itemList
    public void AddItem(Item item)
    {
        //checks if the item is a stackable item
        if (item.IsStackable())
        {
            //boolean to tell if the stackable item is already in the inventory itemList
            bool itemAlreadyInInventory = false;

            //for each loop that adds the stackable item to the stack of the already existing item.
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }

            // if the item isn't in the inventory it adds it to the inventory itemList
            if(!itemAlreadyInInventory)
            {
                itemList.Add(item);
            }
        }
        //if the item isn't stackable it just adds it to the inventory itemList as normal
        else
        {
            itemList.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    //function that removes items form the inventory itemList
    public void RemoveItem(Item item)
    {
        //checks if the item is stackable
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            //for each loop that removes the item from the stack of existing items
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            //if the item isn't in the inventory and the amount is <= 0  removes it from the itemList
            if (itemInInventory != null && itemInInventory.amount <=0)
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

    public List<Item> GetItemList()
    {
        return itemList;
    }

    
}
