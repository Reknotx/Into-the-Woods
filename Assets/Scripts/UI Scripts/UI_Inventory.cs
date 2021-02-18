using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// Author: JT Esmond
/// Date: 2/16/2021
/// <summary>
/// the class that handles the main functions of the inventory
/// </summary>
public class UI_Inventory : SingletonPattern<UI_Inventory>
{
    private Inventory inventory;
    public Transform itemSlotContainer;
    public Transform itemSlotTemplate;
    public Text textAmount;

    public List<Image> itemSlots = new List<Image>();

    protected override void Awake()
    {
        base.Awake();
    }

    // SetInventory function that 
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
    }

    //event function that calls RefreshInventoryItems function
    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    /// Author: JT
    /// Date: 2/16/2021
    /// <summary>
    /// Refreshes the inventory UI to display our current items.
    /// </summary>
    /// Edit: Chase O'Connor
    /// Function was restructred to work with our current system rather than the
    /// system that was made following the tutorial.
    private void RefreshInventoryItems()
    {
        //destroys any previous inventory elements
        //foreach (Transform child in itemSlotContainer)
        //{
        //    if (child == itemSlotTemplate) continue;
        //    Destroy(child.gameObject);
        //}

        //int x = 0;

        //creates new inventory UI elements for the different items in the item list
        for (int i = 0; i < inventory.ItemList.Count; i++)
        {
            Collectable item = inventory.ItemList[i];
            //limits the size of the inventory to 5

            itemSlots[i].sprite = item.UISprite;

            #region Old UI system
            ////creates the new inventory UI elements and sets its position depending on how many items are in the inventory
            //RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            //itemSlotRectTransform.gameObject.SetActive(true);
            //itemSlotRectTransform.anchoredPosition = new Vector2(465.9085f + x, itemSlotTemplate.transform.position.y);

            ////sets the image on the item to the correct sprite for that item
            //Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            //image.sprite = item.UISprite;

            //x += 82;
            #endregion

            //increases the amount for the stackable items and displays the proper number in the UI
            if (item is PotionIngredient ingredient && ingredient.amountInInv > 1)
            {
                itemSlots[i].transform.GetChild(0).GetComponent<Text>().text = ingredient.amountInInv.ToString();
            }
            else
            {
                itemSlots[i].transform.GetChild(0).GetComponent<Text>().text = "";
            }
        }
    }

}
