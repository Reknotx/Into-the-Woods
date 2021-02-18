using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// Author: JT Esmond
/// Date: 2/16/2021
/// <summary>
/// the class that handles the main functions of the inventory
/// </summary>
public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    public Transform itemSlotContainer;
    public Transform itemSlotTemplate;
    public Text textAmount;

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

    //function that refreshes the inventory UI 
    private void RefreshInventoryItems()
    {
        //destroys any previous inventory elements
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int limiter = 0;

        //creates new inventory UI elements for the different items in the item list
        foreach(Item item in inventory.GetItemList())
        {
            //limits the size of the inventory to 5
            if (limiter >= 5)
            {
                return;
            }
            
            //creates the new inventory UI elements and sets its position depending on how many items are in the inventory
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(465.9085f + x, itemSlotTemplate.transform.position.y);

            //sets the image on the item to the correct sprite for that item
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            x += 82;

            //increases the amount for the stackable items and displays the proper number in the UI
            if (item.amount >1)
            {
                textAmount.text = (item.amount.ToString());
            }
            else
            {
                textAmount.text = ("");
            }
            limiter++;
        }
    }
}
