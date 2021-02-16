using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// Author: JT Esmond
/// Date: 2/8/2021
/// <summary>
/// The class that handles the entire Inventory UI System
/// </summary>
/// 
/// Requirements for functionality:
/// ** hasItem boolean must be set to true whenever the player picks up an item
/// ** the InventoryUpdate() function needs to be run after an item is picked up
/// ** the boolean for the specific item that is picked up must be set to true
/// ** if one of the ingredients is picked up the integer for the specific ingredient needs to incremented once
/// ** the function for the specific ingredient also needs to be run
/// ** the boolean for the different slots need to be set to false, if the player drops an item
/// 
/// Requirements for adding collectibles:
/// ** create a new UI element for the sprite
/// ** set the item's tag to Collectible
/// ** create a new boolean for the item
/// ** if the item is a new ingredient, also create a boolean for it for each of the 5 slots
/// ** if the item is a new ingredient, also create a new integer for the amount of it being carried
/// ** create a new conditional for the new collectible in each of the Slotx() functions (where x is the number of the slot)
///     ** if it is an ingredient mimic the other ingredients
///     ** if it is a normal collectible item mimic the other collectibles
/// ** if the item is a new ingredient, create a new function mimic the other IngredientxUpdate() functions (where x is the letter representing the ingredient)

public class InventoryUI : MonoBehaviour
{
    #region Singleton
    /// <summary> Declaring the class as a singleton </summary>
    public static InventoryUI Instance;
    #endregion

    #region lists
    /// <summary> The lists that hold all of the UI elements </summary>
    private List<GameObject> _inventorySlot1 = new List<GameObject>();
    private List<GameObject> _inventorySlot2 = new List<GameObject>();
    private List<GameObject> _inventorySlot3 = new List<GameObject>();
    private List<GameObject> _inventorySlot4 = new List<GameObject>();
    private List<GameObject> _inventorySlot5 = new List<GameObject>();
    #endregion

    #region booleans

    #endregion

    #region integers

    #endregion

    #region text
    /// <summary> text elements for each inventory slot</summary>
    public Text slot1Text;
    public Text slot2Text;
    public Text slot3Text;
    public Text slot4Text;
    public Text slot5Text;
    #endregion

    #region GameObjects
    /// <summary> gameobjects for each inventory slot </summary>
    public GameObject inventoryHolder;
    #endregion

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    /// <summary> adds the different UI elements to lists for each inventory slot </summary>
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region Functions

    #endregion
}
