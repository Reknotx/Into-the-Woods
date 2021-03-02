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

    /// <summary> The array of our potions </summary>
    public Potion[] Potions => potions;

    /// <summary> The inventory constructor, initializes the lists and arrays. </summary>
    public Inventory()
    {
        itemList = new List<Collectable>();
        potions = new Potion[3];

        //Debug.Log(itemList.Count);
    }

    #region Items
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
            if (!ingredientInInven && itemList.Count <= 4)
            {
                itemList.Add(item);
                item.GetComponent<PotionIngredient>().amountInInv = 1;
            }
            else
            {
                ///The ingredient amount was incremented and we want to update the UI now.
                OnItemListChanged?.Invoke(this, EventArgs.Empty);
                return false;
            }
        }
        //if the item isn't stackable it just adds it to the inventory itemList as normal
        else
        {
            ///Collectable is just a standard item.
            if (itemList.Count == 5) return false;
            
            itemList.Add(item);
        }

        TurnOffItem(item);
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
    /// 
    ///Change the way that items are dropped.
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
                    if (ingredient.amountInInv > 1)
                    {
                        ingredient.DropLogic();
                    }
                    ingredient.amountInInv--;
                    ingredientInInven = ingredient;
                }
            }
            //if the item isn't in the inventory and the amount is <= 0  removes it from the itemList
            if (ingredientInInven != null && ingredientInInven.amountInInv <= 0)
            {
                DropItem(item);
            }
        }
        //if the item isn't stackable it removes it from the inventroy itemList
        else
        {
            DropItem(item);

        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);


        ///Internal function to drop the item.
        void DropItem(Collectable droppedItem, bool IsIngredient = false)
        {
            System.Random rand = new System.Random();

            int dropMod = rand.Next(0, 2) == 0 ? -1 : 1;

            float dropX = Mathf.Clamp((float)rand.NextDouble(), 0.5f, 1f) * 2f * dropMod;
            dropMod = rand.Next(0, 2) == 0 ? -1 : 1;
            float dropZ = Mathf.Clamp((float)rand.NextDouble(), 0.5f, 1f) * 2f * dropMod;

            droppedItem.transform.parent = null;
            Vector3 dropPos = Player.Instance.transform.position + new Vector3(dropX, 0f, dropZ);

            Vector3 dropDelta = Player.Instance.transform.position - dropPos;


            Debug.Log("Drop Pos mag: " + dropDelta.magnitude);


            dropPos.y = 0.7f;

            droppedItem.transform.position = dropPos;
            droppedItem.gameObject.SetActive(true);
            itemList.Remove(droppedItem);
            droppedItem.DropLogic();
        }
    }


    /// Author: Chase O'Connor
    /// Date: 2/17/2021
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

    public Collectable GetItem(Collectable item)
    {
        foreach (Collectable invenItem in itemList)
        {
            if (invenItem.GetType() == item.GetType()) return invenItem;
        }


        return null;

    }
    #endregion

    #region Potions
    ///Author: Chase O'Connor
    ///Date: 2/18/2021
    /// <summary>
    /// Add a potion to our inventory.
    /// </summary>
    /// <param name="potion">The potion to add to our inventory</param>
    /// <returns></returns>
    public bool AddPotion(Potion potion)
    {
        if (potion == null)
        {
            Debug.LogError("Potion null");
            return false;
        }

        for (int i = 0; i < potions.Length; i++)
        {
            if (potions[i] == null)
            {
                potions[i] = potion;
                OnItemListChanged?.Invoke(this, EventArgs.Empty);
                return true;
            }
        }

        return false;
    }

    public void RemovePotion(Potion potion)
    {
        for (int i = 0; i < potions.Length; i++)
        {
            if (potions[i] == potion)
            {
                potions[i] = null;
                break;
            }
        }

        for (int i = 0; i < potions.Length - 1; i++)
        {
            if (potions[i] == null)
            {
                Potion temp = potions[i + 1];
                potions[i] = temp;
                potions[i + 1] = null;
            }
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    /// Author: Chase O'Connor
    /// Date: 2/18/2021
    /// <summary>
    /// Removes an ingredient item from the inventory.
    /// </summary>
    /// <param name="requirement">The ingredient we want to remove.</param>
    public void RemoveIngredient(Requirement requirement)
    {
        List<PotionIngredient> removeList = new List<PotionIngredient>();


        foreach (Collectable item in itemList)
        {
            if (item is PotionIngredient ingredient)
            {
                switch (requirement)
                {
                    case Requirement.IngredientA:
                        if (ingredient is PotionIngredientA)
                            ingredient.amountInInv--;
                        break;

                    case Requirement.IngredientB:
                        if (ingredient is PotionIngredientB)
                            ingredient.amountInInv--;
                        break;

                    case Requirement.IngredientC:
                        if (ingredient is PotionIngredientC)
                            ingredient.amountInInv--;
                        break;

                    case Requirement.IngredientD:
                        if (ingredient is PotionIngredientD)
                            ingredient.amountInInv--;
                        break;
                }

                if (ingredient.amountInInv == 0)
                {
                    removeList.Add(ingredient);
                }
            }
        }

        if (removeList.Count != 0)
        {
            foreach (PotionIngredient item in removeList)
            {
                itemList.Remove(item);
            }
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    /// Author: Chase O'Connor
    /// Date: 2/18/2021
    /// <summary>
    /// Searches our inventory for the potion ingredient we want and gives the amount we have.
    /// </summary>
    /// <param name="resource">The ingredient requirement we need.</param>
    /// <returns>The amount of the resource in our inventory at this time.</returns>
    public int IngredientCount(Requirement resource)
    {
        if (itemList.Count == 0) return 0;

        foreach (Collectable item in itemList)
        {
            if (item is PotionIngredient ingredient)
            {
                switch (resource)
                {
                    case Requirement.IngredientA:
                        if (ingredient is PotionIngredientA)
                            return ingredient.amountInInv;
                        break;

                    case Requirement.IngredientB:
                        if (ingredient is PotionIngredientB)
                            return ingredient.amountInInv;
                        break;

                    case Requirement.IngredientC:
                        if (ingredient is PotionIngredientC)
                            return ingredient.amountInInv;
                        break;

                    case Requirement.IngredientD:
                        if (ingredient is PotionIngredientD)
                            return ingredient.amountInInv;
                        break;
                }
            }
        }
        return 0;
    }

    /// <summary>
    /// Counts the number of potions in our inventory.
    /// </summary>
    /// <returns>The number of potions we have in our inventory.</returns>
    public int PotionCount()
    {
        int count = 0;
        for (int i = 0; i < potions.Length; i++)
        {
            if (potions[i] == null) continue;

            count++;
        }

        return count;
    }
    #endregion

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


    

    ///Work on the below stuff later
    #region Iterator. Work on later

    /// <summary> Returns an iterator to sequentially run through the inventory list. </summary>
    /// <returns>An iterator over the player's inventory.</returns>
    public IIterator GetInventoryIterator()
    {
        return new InventoryIterator(this);
    }


    protected class InventoryIterator : IIterator
    {

        int index;
        private List<Collectable> itemList;
        private Inventory invenRef;

        public InventoryIterator(List<Collectable> list)
        {
            index = -1;
            itemList = list;
            Next();
        }

        public InventoryIterator(Inventory inventory)
        {
            index = -1;
            invenRef = inventory;
            Next();
        }


        public Collectable GetCurrentItem()
        {
            return invenRef.itemList[index];
        }

        public bool hasMoreItems()
        {
            return index == -1;
        }

        public void Next()
        {
            index++;

            if (index == invenRef.itemList.Count) index = -1;
        }
    }

    #endregion
}
