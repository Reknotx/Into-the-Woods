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

    public enum Items
    {
        Avocado,
        AttackCandy,
        SpecialCurrency,
        PotionIngredientA,
        PotionIngredientB,
        PotionIngredientC,
        PotionIngredientD,
        Totem,
        LuckyPenny,
        BalloonBouquet,
        NightOwlToken,
        TwoPeas,
        Compass
    }


    public event EventHandler OnItemListChanged;

    /// <summary> The list of potions we currently have. </summary>
    private Potion[] potions;

    /// <summary> The list of collectable items in our inventory. </summary>
    private List<Collectable> itemList;

    /// <summary> The reference to the collectable items in our inventory. </summary>
    public List<Collectable> ItemList => itemList;

    /// <summary> The array of our potions </summary>
    public Potion[] Potions => potions;

    public Dictionary<Items, bool> prevCollected;

    /// <summary> The inventory constructor, initializes the lists and arrays. </summary>
    public Inventory()
    {
        itemList = new List<Collectable>();
        potions = new Potion[3];

        prevCollected = new Dictionary<Items, bool>()
        {
            {Items.AttackCandy, false },
            {Items.Avocado, false },
            {Items.SpecialCurrency, false },
            {Items.PotionIngredientA, false },
            {Items.PotionIngredientB, false },
            {Items.PotionIngredientC, false },
            {Items.PotionIngredientD, false },
            {Items.Totem, false },
            {Items.LuckyPenny, false },
            {Items.BalloonBouquet, false },
            {Items.NightOwlToken, false },
            {Items.TwoPeas, false },
            {Items.Compass, false }
        };

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
                        Vector3 dropPos = RandDropPos();
                        dropPos.y = 0.7f;

                        UnityEngine.Object.Instantiate(ingredient.gameObject, dropPos, Quaternion.identity).SetActive(true);
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

        #region Internal Functions
        ///Internal function to drop the item.
        void DropItem(Collectable droppedItem)
        {
            droppedItem.transform.parent = null;

            Vector3 dropPos = RandDropPos();
           
            //Vector3 dropDelta = Player.Instance.transform.position - dropPos;

            //Debug.Log("Drop Pos mag: " + dropDelta.magnitude);

            dropPos.y = 0.7f;

            droppedItem.transform.position = dropPos;
            droppedItem.gameObject.SetActive(true);
            itemList.Remove(droppedItem);
            droppedItem.DropLogic();
        }

        Vector3 RandDropPos()
        {
            System.Random rand = new System.Random();

            Vector3 dropPos = Vector3.zero;

            while(true)
            {
                int dropMod = rand.Next(0, 2) == 0 ? -1 : 1;

                float dropX = Mathf.Clamp((float)rand.NextDouble(), 0.5f, 1f) * 2f * dropMod;

                dropMod = rand.Next(0, 2) == 0 ? -1 : 1;

                float dropZ = Mathf.Clamp((float)rand.NextDouble(), 0.5f, 1f) * 2f * dropMod;

                dropPos = Player.Instance.transform.position + new Vector3(dropX, 0f, dropZ);

                Ray ray = Camera.main.ViewportPointToRay(Camera.main.WorldToViewportPoint(dropPos));

                int layerMask = ~((1 << 30) | (1 << 0) | (1 << 15));

                Physics.Raycast(ray, out RaycastHit hit, 1000f, layerMask);

                Debug.Log(hit.collider.gameObject.layer);

                if (hit.collider.gameObject.layer == 31)
                {
                    Debug.Log("Nothing here, placing object.");
                    break;
                }
                else
                {
                    Debug.Log("Making another pass to avoid stacking.");
                }
            }

            //return Player.Instance.transform.position + new Vector3(dropX, 0f, dropZ);
            return dropPos;
        }
        #endregion
    }

    #region Has Item
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

    public bool HasItem(Items item)
    {
        if (ItemList.Count == 0) return false;

        bool returnVal = false;

        foreach (Collectable invenItem in itemList)
        {
            switch (item)
            {
                case Items.Avocado:
                    if (invenItem is Avocado) returnVal = true;
                    break;

                case Items.AttackCandy:
                    if (invenItem is AttackCandy) returnVal = true;
                    break;

                case Items.SpecialCurrency:
                    //if (invenItem is SpecialCurrency) returnVal = true;
                    break;

                case Items.PotionIngredientA:
                    if (invenItem is PotionIngredientA) returnVal = true;
                    break;

                case Items.PotionIngredientB:
                    if (invenItem is PotionIngredientB) returnVal = true;
                    break;

                case Items.PotionIngredientC:
                    if (invenItem is PotionIngredientC) returnVal = true;
                    break;

                case Items.PotionIngredientD:
                    if (invenItem is PotionIngredientD) returnVal = true;
                    break;

                case Items.Totem:
                    if (invenItem is Totem) returnVal = true;
                    break;

                case Items.LuckyPenny:
                    if (invenItem is LuckyPenny) returnVal = true;
                    break;

                case Items.BalloonBouquet:
                    if (invenItem is BalloonBouquet) returnVal = true;
                    break;

                case Items.NightOwlToken:
                    if (invenItem is NightOwlToken) returnVal = true;
                    break;

                case Items.TwoPeas:
                    if (invenItem is TwoPeas) returnVal = true;
                    break;

                case Items.Compass:
                    if (invenItem is Compass) returnVal = true;
                    break;

                default:
                    break;
            }

            if (returnVal == true) break;
        }

        return returnVal;
    }
    #endregion

    #region Get Item
    public Collectable GetItem(Collectable item)
    {
        foreach (Collectable invenItem in itemList)
        {
            if (invenItem.GetType() == item.GetType()) return invenItem;
        }
        return null;

    }

    public Collectable GetItem(Items item)
    {
        if (ItemList.Count == 0) return null;

        Collectable returnVal = null;

        foreach (Collectable invenItem in itemList)
        {
            switch (item)
            {
                case Items.Avocado:
                    if (invenItem is Avocado) returnVal = invenItem;
                    break;

                case Items.AttackCandy:
                    if (invenItem is AttackCandy) returnVal = invenItem;
                    break;

                case Items.SpecialCurrency:
                    //if (invenItem is SpecialCurrency) returnVal = true;
                    break;

                case Items.PotionIngredientA:
                    if (invenItem is PotionIngredientA) returnVal = invenItem;
                    break;

                case Items.PotionIngredientB:
                    if (invenItem is PotionIngredientB) returnVal = invenItem;
                    break;

                case Items.PotionIngredientC:
                    if (invenItem is PotionIngredientC) returnVal = invenItem;
                    break;

                case Items.PotionIngredientD:
                    if (invenItem is PotionIngredientD) returnVal = invenItem;
                    break;

                case Items.Totem:
                    if (invenItem is Totem) returnVal = invenItem;
                    break;

                case Items.LuckyPenny:
                    if (invenItem is LuckyPenny) returnVal = invenItem;
                    break;

                case Items.BalloonBouquet:
                    if (invenItem is BalloonBouquet) returnVal = invenItem;
                    break;

                case Items.NightOwlToken:
                    if (invenItem is NightOwlToken) returnVal = invenItem;
                    break;

                case Items.TwoPeas:
                    if (invenItem is TwoPeas) returnVal = invenItem;
                    break;

                case Items.Compass:
                    if (invenItem is Compass) returnVal = invenItem;
                    break;

                default:
                    break;
            }

            if (returnVal == true) break;
        }

        return returnVal;
    }
    #endregion

    /// <summary>
    /// Checks to see if the collected item has been picked up
    /// before by the player.
    /// </summary>
    /// <param name="item">The collected item.</param>
    /// <returns>True if we have collected it before, false if first time.</returns>
    /// <remarks>This function also sets the value of the dictionary associated with this item
    /// to true if this is the first time we have picked it up.</remarks>
    public bool HasCollectedBefore(Collectable item)
    {
        Items type = GetType(item);

        bool returnVal = prevCollected[type];

        if (!returnVal)
        {
            ///If this is our first time picking up the item we 
            ///must set the value in the dictionary to true.
            prevCollected[type] = true;
        }

        return returnVal;

        Items GetType(Collectable i)
        {
            if (i is Avocado) return Items.Avocado;
            
            else if (i is Totem) return Items.Totem;
            
            else if (i is TwoPeas) return Items.TwoPeas;
            
            else if (i is Compass) return Items.Compass;
            
            else if (i is LuckyPenny) return Items.LuckyPenny;
            
            else if (i is AttackCandy) return Items.AttackCandy;
            
            else if (i is Currency) return Items.SpecialCurrency;
            
            else if (i is NightOwlToken) return Items.NightOwlToken;
            
            else if (i is BalloonBouquet) return Items.BalloonBouquet;
            
            else if (i is PotionIngredientA) return Items.PotionIngredientA;
            
            else if (i is PotionIngredientB) return Items.PotionIngredientB;
            
            else if (i is PotionIngredientB) return Items.PotionIngredientC;
            
            else if (i is PotionIngredientB) return Items.PotionIngredientD;

            return Items.AttackCandy;
        }

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
