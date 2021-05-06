using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// Author: JT Esmond
/// Date: 2/16/2021
/// <summary>
/// the class that handles the main functions of the inventory
/// </summary>
public class UI_Inventory : SingletonPattern<UI_Inventory>
{
    readonly int imageIndex = 0;
    readonly int amountIndex = 1;


    /// <summary> The reference to the player inventory. </summary>
    private Inventory inventory;

    /// <summary> The list of the item sprites that are displayed. </summary>
    public List<GameObject> itemSlots = new List<GameObject>();

    /// <summary> The list of the potion sprites that are displayed. </summary>
    public List<Image> potionSlots = new List<Image>();

    /// <summary> The parent game object of the inventory slots. </summary>
    private GameObject inventoryContainer;

    private GameObject inventoryBackground;

    /// <summary> The hover text UI component. Written to by HoverText(). </summary>
    //public Text hoverTextUI; 
    public TMPro.TMP_Text hoverTextUI;

    protected override void Awake()
    {
        base.Awake();

        inventoryContainer = transform.GetChild(0).gameObject;
        inventoryBackground = transform.GetChild(1).gameObject;

        inventoryContainer.SetActive(false);
        inventoryBackground.SetActive(false);
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
        foreach (GameObject image in itemSlots)
        {
            image.transform.GetChild(imageIndex).GetComponent<Image>().sprite = null;
            image.transform.GetChild(imageIndex).GetComponent<Image>().enabled = false;
            image.transform.GetChild(amountIndex).GetComponent<Text>().text = "";
        }

        foreach (Image potionImage in potionSlots)
        {
            potionImage.sprite = null;
        }

        //int x = 0;

        //creates new inventory UI elements for the different items in the item list
        ///Updates the inventory UI
        for (int i = 0; i < inventory.ItemList.Count; i++)
        {
            Collectable item = inventory.ItemList[i];
            
            itemSlots[i].transform.GetChild(imageIndex).GetComponent<Image>().sprite = item.UISprite;
            itemSlots[i].transform.GetChild(imageIndex).GetComponent<Image>().enabled = true;

            //increases the amount for the stackable items and displays the proper number in the UI
            if (item is PotionIngredient ingredient && ingredient.amountInInv > 1)
            {
                itemSlots[i].transform.GetChild(amountIndex).GetComponent<Text>().text = ingredient.amountInInv.ToString();
            }
            else
            {
                itemSlots[i].transform.GetChild(amountIndex).GetComponent<Text>().text = "";
            }
        }

        ///Updates potions
        for (int i = 0; i < inventory.Potions.Length; i++)
        {
            if (inventory.Potions[i] == null)
            {
                potionSlots[i].gameObject.SetActive(false);
                continue;
            }
            potionSlots[i].sprite = inventory.Potions[i].potionSprite;
            potionSlots[i].gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Turns the inventory item slots on or of depending on their state.
    /// </summary>
    public void InventoryDisplay()
    {
        inventoryContainer.SetActive(!inventoryContainer.activeSelf);
        inventoryBackground.SetActive(!inventoryBackground.activeSelf);
    }

    /// <summary> The integer that shows the focus of dropping an item from inventory. </summary>
    private int focus = -1;

    public void AssignFocus(int focus)
    {
        //Debug.Log("Focus assigned");

        this.focus = focus;
        HoverText();
    }

    public void DropItem()
    {
        if (focus == -1) return;

        if (inventory.ItemList.Count < focus) return;

        inventory.RemoveItem(inventory.ItemList[focus - 1]);
    }

    /// <summary>
    /// This will update the hover text on the Inventory slots.
    /// </summary>
    private void HoverText()
    {
        // if hovering over something (focus), 
        if (focus != -1 && inventory.ItemList.Count >= focus)
        {
            // Get list of items
            // RefreshInventoryItems();
            if (inventory.ItemList[focus-1] != null)
            {
                // grab specific focused item's collectible script
                // then update displayed text and enable UI.
                hoverTextUI.text = inventory.ItemList[focus - 1].GetComponent<Collectable>().description;
            }
        }
        else if (focus == -1) // else( not hovering over anything) then disable hover UI.
        {
            hoverTextUI.text = "";
        }
    }
}
