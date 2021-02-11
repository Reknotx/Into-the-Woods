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
    #region Fields
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

    #region item booleans
    /// <summary> 
    /// booleans for the different items 
    /// add booleans here for individual items
    /// if new items get added
    /// </summary>
    [HideInInspector] public bool hasItem;
    [HideInInspector] public bool hasIngredientA;
    [HideInInspector] public bool hasIngredientB;
    [HideInInspector] public bool hasIngredientC;
    [HideInInspector] public bool hasIngredientD;
    [HideInInspector] public bool hasTotem;
    [HideInInspector] public bool hasLuckyPenny;
    [HideInInspector] public bool hasAttackCandy;
    [HideInInspector] public bool hasBalloonBouquet;
    [HideInInspector] public bool hasHeart;
    [HideInInspector] public bool hasNightOwlToken;
    [HideInInspector] public bool hasPeasInPod;
    [HideInInspector] public bool hasCompass;
    [HideInInspector] public bool hasAvocado;
    #endregion

    #region slot full booleans
    /// <summary> booleans for the different slots being full </summary>
    [HideInInspector] public bool slot1Full;
    [HideInInspector] public bool slot2Full;
    [HideInInspector] public bool slot3Full;
    [HideInInspector] public bool slot4Full;
    [HideInInspector] public bool slot5Full;
    #endregion

    #region ingredient in slot booleans
    /// <summary> 
    /// The booleans denoting which slot an ingredient is in
    /// add booleans here for an ingredient in each slot
    /// </summary>
    private bool AIn1;
    private bool AIn2;
    private bool AIn3;
    private bool AIn4;
    private bool AIn5;
    private bool BIn1;
    private bool BIn2;
    private bool BIn3;
    private bool BIn4;
    private bool BIn5;
    private bool CIn1;
    private bool CIn2;
    private bool CIn3;
    private bool CIn4;
    private bool CIn5;
    private bool DIn1;
    private bool DIn2;
    private bool DIn3;
    private bool DIn4;
    private bool DIn5;
    #endregion

    #endregion

    #region integers

    #region slot location ints
    /// <summary> slot location integers used to access locations in the slot lists </summary>
    private int slot1Location;
    private int slot2Location;
    private int slot3Location;
    private int slot4Location;
    private int slot5Location;
    #endregion

    #region ingredient amount ints
    /// <summary>
    /// integers for the different ingredient amounts
    /// add integers here for new ingredient amounts
    /// </summary>
    [HideInInspector] public int ingredientAAmount;
    [HideInInspector] public int ingredientBAmount;
    [HideInInspector] public int ingredientCAmount;
    [HideInInspector] public int ingredientDAmount;
    #endregion

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
    public GameObject inventorySlot1;
    public GameObject inventorySlot2;
    public GameObject inventorySlot3;
    public GameObject inventorySlot4;
    public GameObject inventorySlot5;
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
        foreach (Transform collectible in inventorySlot1.transform)
        {
            if (collectible.gameObject.CompareTag("Collectible"))
            {
                _inventorySlot1.Add(collectible.gameObject);
            }
        }

        foreach (Transform collectible in inventorySlot2.transform)
        {
            if (collectible.gameObject.CompareTag("Collectible"))
            {
                _inventorySlot2.Add(collectible.gameObject);
            }
        }

        foreach (Transform collectible in inventorySlot3.transform)
        {
            if (collectible.gameObject.CompareTag("Collectible"))
            {
                _inventorySlot3.Add(collectible.gameObject);
            }
        }

        foreach (Transform collectible in inventorySlot4.transform)
        {
            if (collectible.gameObject.CompareTag("Collectible"))
            {
                _inventorySlot4.Add(collectible.gameObject);
            }
        }

        foreach (Transform collectible in inventorySlot5.transform)
        {
            if (collectible.gameObject.CompareTag("Collectible"))
            {
                _inventorySlot5.Add(collectible.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //temporary keybindings to test UI
        if(Input.GetKeyDown("t"))
        {
            hasItem = true;
            hasIngredientA = true;
            ingredientAAmount++;
            InventoryUpdate();
            IngredientAUpdate();
        }
        if(Input.GetKeyDown("y"))
        {
            hasItem = true;
            hasIngredientB = true;
            ingredientBAmount++;
            InventoryUpdate();
            IngredientBUpdate();
        }
        if(Input.GetKeyDown("u"))
        {
            hasItem = true;
            hasIngredientC = true;
            ingredientCAmount++;
            InventoryUpdate();
            IngredientCUpdate();
        }
        if(Input.GetKeyDown("i"))
        {
            hasItem = true;
            hasIngredientD = true;
            ingredientDAmount++;
            InventoryUpdate();
            IngredientDUpdate();
        }
        if(Input.GetKeyDown("o"))
        {
            hasItem = true;
            hasTotem = true;
            InventoryUpdate();
        }
        if (Input.GetKeyDown("p"))
        {
            hasItem = true;
            hasLuckyPenny = true;
            InventoryUpdate();
        }
        if (Input.GetKeyDown("g"))
        {
            hasItem = true;
            hasAttackCandy = true;
            InventoryUpdate();
        }
        if (Input.GetKeyDown("h"))
        {
            hasItem = true;
            hasBalloonBouquet = true;
            InventoryUpdate();
        }
        if (Input.GetKeyDown("j"))
        {
            hasItem = true;
            hasHeart = true;
            InventoryUpdate();
        }
        if (Input.GetKeyDown("k"))
        {
            hasItem = true;
            hasNightOwlToken = true;
            InventoryUpdate();
        }
        if (Input.GetKeyDown("l"))
        {
            hasItem = true;
            hasPeasInPod = true;
            InventoryUpdate();
        }
        if (Input.GetKeyDown("b"))
        {
            hasItem = true;
            hasCompass = true;
            InventoryUpdate();
        }
        if (Input.GetKeyDown("n"))
        {
            hasItem = true;
            hasAvocado = true;
            InventoryUpdate();
        }
        */
    }

    #region Functions

    #region InventoryUpdate()

    /// Author: JT Esmond
    /// Date: 2/10/2021
    /// <summary>
    /// Function that checks to see if a slot is full
    /// if it is, it checks the next slot so on and so forth until it determines the inventory is full
    /// if it isn't, it runs the specific function for that slot\
    /// this function needs to be called on item pick up
    /// </summary>
    public void InventoryUpdate()
    {
        if (!slot1Full)
        {
            Slot1();
        }
        else if (slot1Full && !slot2Full)
        {
            Slot2();
        }
        else if (slot1Full && slot2Full && !slot3Full)
        {
            Slot3();
        }
        else if (slot1Full && slot2Full && slot3Full && !slot4Full)
        {
            Slot4();
        }
        else if (slot1Full && slot2Full && slot3Full && slot4Full && !slot5Full)
        {
            Slot5();
        }
    }

    #endregion

    #region SlotX()

    /// Author: JT Esmond
    /// Date: 2/10/2021
    /// <summary>
    /// this function first checks if the player has an item
    /// if they don't it cancels the function
    /// if they do it the proceeds to check which item is being picked up
    /// once it determines which item is being picked up it sets that items UI element to active in Slot 1
    /// it also sets the boolean for that item to false, to allow other items of the same type to be picked up
    /// along with set the 1st slot to full so other items don't get added to it
    /// if it is an ingredient, it also sets the specific ingredient to hold in that slot
    /// </summary>
    public void Slot1()
    {
        if (!hasItem) return;

        if (hasIngredientA && !AIn1 && !AIn2 && !AIn3 && !AIn4 && !AIn5)
        {
            _inventorySlot1[slot1Location].gameObject.SetActive(false);
            slot1Location = 0;
            _inventorySlot1[slot1Location].gameObject.SetActive(true);
            AIn1 = true;
            hasIngredientA = false;
            slot1Full = true;
        }
        else if (hasIngredientB && !BIn1 && !BIn2 && !BIn3 && !BIn4 && !BIn5)
        {
            _inventorySlot1[slot1Location].gameObject.SetActive(false);
            slot1Location = 1;
            _inventorySlot1[slot1Location].gameObject.SetActive(true);
            BIn1 = true;
            hasIngredientB = false;
            slot1Full = true;
        }
        else if (hasIngredientC && !CIn1 && !CIn2 && !CIn3 && !CIn4 && !CIn5)
        {
            _inventorySlot1[slot1Location].gameObject.SetActive(false);
            slot1Location = 2;
            _inventorySlot1[slot1Location].gameObject.SetActive(true);
            CIn1 = true;
            hasIngredientC = false;
            slot1Full = true;
        }
        else if (hasIngredientD && !DIn1 && !DIn2 && !DIn3 && !DIn4 && !DIn5)
        {
            _inventorySlot1[slot1Location].gameObject.SetActive(false);
            slot1Location = 3;
            _inventorySlot1[slot1Location].gameObject.SetActive(true);
            DIn1 = true;
            hasIngredientD = false;
            slot1Full = true;

        }
        else if (hasTotem)
        {
            _inventorySlot1[slot1Location].gameObject.SetActive(false);
            slot1Location = 4;
            _inventorySlot1[slot1Location].gameObject.SetActive(true);
            hasTotem = false;
            slot1Full = true;
        }
        else if (hasLuckyPenny)
        {
            _inventorySlot1[slot1Location].gameObject.SetActive(false);
            slot1Location = 5;
            _inventorySlot1[slot1Location].gameObject.SetActive(true);
            hasLuckyPenny = false;
            slot1Full = true;
        }
        else if (hasAttackCandy)
        {
            _inventorySlot1[slot1Location].gameObject.SetActive(false);
            slot1Location = 6;
            _inventorySlot1[slot1Location].gameObject.SetActive(true);
            hasAttackCandy = false;
            slot1Full = true;
        }
        else if (hasBalloonBouquet)
        {
            _inventorySlot1[slot1Location].gameObject.SetActive(false);
            slot1Location = 7;
            _inventorySlot1[slot1Location].gameObject.SetActive(true);
            hasBalloonBouquet = false;
            slot1Full = true;
        }
        else if (hasHeart)
        {
            _inventorySlot1[slot1Location].gameObject.SetActive(false);
            slot1Location = 8;
            _inventorySlot1[slot1Location].gameObject.SetActive(true);
            hasHeart = false;
            slot1Full = true;
        }
        else if (hasNightOwlToken)
        {
            _inventorySlot1[slot1Location].gameObject.SetActive(false);
            slot1Location = 9;
            _inventorySlot1[slot1Location].gameObject.SetActive(true);
            hasNightOwlToken = false;
            slot1Full = true;
        }
        else if (hasPeasInPod)
        {
            _inventorySlot1[slot1Location].gameObject.SetActive(false);
            slot1Location = 10;
            _inventorySlot1[slot1Location].gameObject.SetActive(true);
            hasPeasInPod = false;
            slot1Full = true;
        }
        else if (hasCompass)
        {
            _inventorySlot1[slot1Location].gameObject.SetActive(false);
            slot1Location = 11;
            _inventorySlot1[slot1Location].gameObject.SetActive(true);
            hasCompass = false;
            slot1Full = true;
        }
        else if (hasAvocado)
        {
            _inventorySlot1[slot1Location].gameObject.SetActive(false);
            slot1Location = 12;
            _inventorySlot1[slot1Location].gameObject.SetActive(true);
            hasAvocado = false;
            slot1Full = true;
        }
        slot1Text.gameObject.SetActive(false);
    }

    /// Author: JT Esmond
    /// Date: 2/10/2021
    /// <summary>
    /// this function first checks if the player has an item
    /// if they don't it cancels the function
    /// if they do it the proceeds to check which item is being picked up
    /// once it determines which item is being picked up it sets that items UI element to active in Slot 2
    /// it also sets the boolean for that item to false, to allow other items of the same type to be picked up
    /// along with set the 1st slot to full so other items don't get added to it
    /// if it is an ingredient, it also sets the specific ingredient to hold in that slot
    /// </summary>
    public void Slot2()
    {
        if (!hasItem) return;

        if (hasIngredientA && !AIn1 && !AIn2 && !AIn3 && !AIn4 && !AIn5)
        {
            _inventorySlot2[slot2Location].gameObject.SetActive(false);
            slot2Location = 0;
            _inventorySlot2[slot2Location].gameObject.SetActive(true);
            AIn2 = true;
            hasIngredientA = false;
            slot2Full = true;
        }
        else if (hasIngredientB && !BIn1 && !BIn2 && !BIn3 && !BIn4 && !BIn5)
        {
            _inventorySlot2[slot2Location].gameObject.SetActive(false);
            slot2Location = 1;
            _inventorySlot2[slot2Location].gameObject.SetActive(true);
            BIn2 = true;
            hasIngredientB = false;
            slot2Full = true;
        }
        else if (hasIngredientC && !CIn1 && !CIn2 && !CIn3 && !CIn4 && !CIn5)
        {
            _inventorySlot2[slot2Location].gameObject.SetActive(false);
            slot2Location = 2;
            _inventorySlot2[slot2Location].gameObject.SetActive(true);
            CIn2 = true;
            hasIngredientC = false;
            slot2Full = true;
        }
        else if (hasIngredientD && !DIn1 && !DIn2 && !DIn3 && !DIn4 && !DIn5)
        {
            _inventorySlot2[slot2Location].gameObject.SetActive(false);
            slot2Location = 3;
            _inventorySlot2[slot2Location].gameObject.SetActive(true);
            DIn2 = true;
            hasIngredientD = false;
            slot2Full = true;
        }
        else if (hasTotem)
        {
            _inventorySlot2[slot2Location].gameObject.SetActive(false);
            slot2Location = 4;
            _inventorySlot2[slot2Location].gameObject.SetActive(true);
            hasTotem = false;
            slot2Full = true;
        }
        else if (hasLuckyPenny)
        {
            _inventorySlot2[slot2Location].gameObject.SetActive(false);
            slot2Location = 5;
            _inventorySlot2[slot2Location].gameObject.SetActive(true);
            hasLuckyPenny = false;
            slot2Full = true;
        }
        else if (hasAttackCandy)
        {
            _inventorySlot2[slot2Location].gameObject.SetActive(false);
            slot2Location = 6;
            _inventorySlot2[slot2Location].gameObject.SetActive(true);
            hasAttackCandy = false;
            slot2Full = true;
        }
        else if (hasBalloonBouquet)
        {
            _inventorySlot2[slot2Location].gameObject.SetActive(false);
            slot2Location = 7;
            _inventorySlot2[slot2Location].gameObject.SetActive(true);
            hasBalloonBouquet = false;
            slot2Full = true;
        }
        else if (hasHeart)
        {
            _inventorySlot2[slot2Location].gameObject.SetActive(false);
            slot2Location = 8;
            _inventorySlot2[slot2Location].gameObject.SetActive(true);
            hasHeart = false;
            slot2Full = true;
        }
        else if (hasNightOwlToken)
        {
            _inventorySlot2[slot2Location].gameObject.SetActive(false);
            slot2Location = 9;
            _inventorySlot2[slot2Location].gameObject.SetActive(true);
            hasNightOwlToken = false;
            slot2Full = true;
        }
        else if (hasPeasInPod)
        {
            _inventorySlot2[slot2Location].gameObject.SetActive(false);
            slot2Location = 10;
            _inventorySlot2[slot2Location].gameObject.SetActive(true);
            hasPeasInPod = false;
            slot2Full = true;
        }
        else if (hasCompass)
        {
            _inventorySlot2[slot2Location].gameObject.SetActive(false);
            slot2Location = 11;
            _inventorySlot2[slot2Location].gameObject.SetActive(true);
            hasCompass = false;
            slot2Full = true;
        }
        else if (hasAvocado)
        {
            _inventorySlot2[slot2Location].gameObject.SetActive(false);
            slot2Location = 12;
            _inventorySlot2[slot2Location].gameObject.SetActive(true);
            hasAvocado = false;
            slot2Full = true;
        }
        slot2Text.gameObject.SetActive(false);
    }

    /// Author: JT Esmond
    /// Date: 2/10/2021
    /// <summary>
    /// this function first checks if the player has an item
    /// if they don't it cancels the function
    /// if they do it the proceeds to check which item is being picked up
    /// once it determines which item is being picked up it sets that items UI element to active in Slot 3
    /// it also sets the boolean for that item to false, to allow other items of the same type to be picked up
    /// along with set the 1st slot to full so other items don't get added to it
    /// if it is an ingredient, it also sets the specific ingredient to hold in that slot
    /// </summary>
    public void Slot3()
    {
        if (!hasItem) return;

        if (hasIngredientA && !AIn1 && !AIn2 && !AIn3 && !AIn4 && !AIn5)
        {
            _inventorySlot3[slot3Location].gameObject.SetActive(false);
            slot3Location = 0;
            _inventorySlot3[slot3Location].gameObject.SetActive(true);
            AIn3 = true;
            hasIngredientA = false;
            slot3Full = true;
        }
        else if (hasIngredientB && !BIn1 && !BIn2 && !BIn3 && !BIn4 && !BIn5)
        {
            _inventorySlot3[slot3Location].gameObject.SetActive(false);
            slot3Location = 1;
            _inventorySlot3[slot3Location].gameObject.SetActive(true);
            BIn3 = true;
            hasIngredientB = false;
            slot3Full = true;
        }
        else if (hasIngredientC && !CIn1 && !CIn2 && !CIn3 && !CIn4 && !CIn5)
        {
            _inventorySlot3[slot3Location].gameObject.SetActive(false);
            slot3Location = 2;
            _inventorySlot3[slot3Location].gameObject.SetActive(true);
            CIn3 = true;
            hasIngredientC = false;
            slot3Full = true;
        }
        else if (hasIngredientD && !DIn1 && !DIn2 && !DIn3 && !DIn4 && !DIn5)
        {
            _inventorySlot3[slot3Location].gameObject.SetActive(false);
            slot3Location = 3;
            _inventorySlot3[slot3Location].gameObject.SetActive(true);
            DIn3 = true;
            hasIngredientD = false;
            slot3Full = true;
        }
        else if (hasTotem)
        {
            _inventorySlot3[slot3Location].gameObject.SetActive(false);
            slot3Location = 4;
            _inventorySlot3[slot3Location].gameObject.SetActive(true);
            hasTotem = false;
            slot3Full = true;
        }
        else if (hasLuckyPenny)
        {
            _inventorySlot3[slot3Location].gameObject.SetActive(false);
            slot3Location = 5;
            _inventorySlot3[slot3Location].gameObject.SetActive(true);
            hasLuckyPenny = false;
            slot3Full = true;
        }
        else if (hasAttackCandy)
        {
            _inventorySlot3[slot3Location].gameObject.SetActive(false);
            slot3Location = 6;
            _inventorySlot3[slot3Location].gameObject.SetActive(true);
            hasAttackCandy = false;
            slot3Full = true;
        }
        else if (hasBalloonBouquet)
        {
            _inventorySlot3[slot3Location].gameObject.SetActive(false);
            slot3Location = 7;
            _inventorySlot3[slot3Location].gameObject.SetActive(true);
            hasBalloonBouquet = false;
            slot3Full = true;
        }
        else if (hasHeart)
        {
            _inventorySlot3[slot3Location].gameObject.SetActive(false);
            slot3Location = 8;
            _inventorySlot3[slot3Location].gameObject.SetActive(true);
            hasHeart = false;
            slot3Full = true;
        }
        else if (hasNightOwlToken)
        {
            _inventorySlot3[slot3Location].gameObject.SetActive(false);
            slot3Location = 9;
            _inventorySlot3[slot3Location].gameObject.SetActive(true);
            hasNightOwlToken = false;
            slot3Full = true;
        }
        else if (hasPeasInPod)
        {
            _inventorySlot3[slot3Location].gameObject.SetActive(false);
            slot3Location = 10;
            _inventorySlot3[slot3Location].gameObject.SetActive(true);
            hasPeasInPod = false;
            slot3Full = true;
        }
        else if (hasCompass)
        {
            _inventorySlot3[slot3Location].gameObject.SetActive(false);
            slot3Location = 11;
            _inventorySlot3[slot3Location].gameObject.SetActive(true);
            hasCompass = false;
            slot3Full = true;
        }
        else if (hasAvocado)
        {
            _inventorySlot3[slot3Location].gameObject.SetActive(false);
            slot3Location = 12;
            _inventorySlot3[slot3Location].gameObject.SetActive(true);
            hasAvocado = false;
            slot3Full = true;
        }
        slot3Text.gameObject.SetActive(false);
    }

    /// Author: JT Esmond
    /// Date: 2/10/2021
    /// <summary>
    /// this function first checks if the player has an item
    /// if they don't it cancels the function
    /// if they do it the proceeds to check which item is being picked up
    /// once it determines which item is being picked up it sets that items UI element to active in Slot 4
    /// it also sets the boolean for that item to false, to allow other items of the same type to be picked up
    /// along with set the 1st slot to full so other items don't get added to it
    /// if it is an ingredient, it also sets the specific ingredient to hold in that slot
    /// </summary>
    public void Slot4()
    {
        if (!hasItem) return;

        if (hasIngredientA && !AIn1 && !AIn2 && !AIn3 && !AIn4 && !AIn5)
        {
            _inventorySlot4[slot4Location].gameObject.SetActive(false);
            slot4Location = 0;
            _inventorySlot4[slot4Location].gameObject.SetActive(true);
            AIn4 = true;
            hasIngredientA = false;
            slot4Full = true;
        }
        else if (hasIngredientB && !BIn1 && !BIn2 && !BIn3 && !BIn4 && !BIn5)
        {
            _inventorySlot4[slot4Location].gameObject.SetActive(false);
            slot4Location = 1;
            _inventorySlot4[slot4Location].gameObject.SetActive(true);
            BIn4 = true;
            hasIngredientB = false;
            slot4Full = true;
        }
        else if (hasIngredientC && !CIn1 && !CIn2 && !CIn3 && !CIn4 && !CIn5)
        {
            _inventorySlot4[slot4Location].gameObject.SetActive(false);
            slot4Location = 2;
            _inventorySlot4[slot4Location].gameObject.SetActive(true);
            CIn4 = true;
            hasIngredientC = false;
            slot4Full = true;
        }
        else if (hasIngredientD && !DIn1 && !DIn2 && !DIn3 && !DIn4 && !DIn5)
        {
            _inventorySlot4[slot4Location].gameObject.SetActive(false);
            slot4Location = 3;
            _inventorySlot4[slot4Location].gameObject.SetActive(true);
            DIn4 = true;
            hasIngredientD = false;
            slot4Full = true;
        }
        else if (hasTotem)
        {
            _inventorySlot4[slot4Location].gameObject.SetActive(false);
            slot4Location = 4;
            _inventorySlot4[slot4Location].gameObject.SetActive(true);
            hasTotem = false;
            slot4Full = true;
        }
        else if (hasLuckyPenny)
        {
            _inventorySlot4[slot4Location].gameObject.SetActive(false);
            slot4Location = 5;
            _inventorySlot4[slot4Location].gameObject.SetActive(true);
            hasLuckyPenny = false;
            slot4Full = true;
        }
        else if (hasAttackCandy)
        {
            _inventorySlot4[slot4Location].gameObject.SetActive(false);
            slot4Location = 6;
            _inventorySlot4[slot4Location].gameObject.SetActive(true);
            hasAttackCandy = false;
            slot4Full = true;
        }
        else if (hasBalloonBouquet)
        {
            _inventorySlot4[slot4Location].gameObject.SetActive(false);
            slot4Location = 7;
            _inventorySlot4[slot4Location].gameObject.SetActive(true);
            hasBalloonBouquet = false;
            slot4Full = true;
        }
        else if (hasHeart)
        {
            _inventorySlot4[slot4Location].gameObject.SetActive(false);
            slot4Location = 8;
            _inventorySlot4[slot4Location].gameObject.SetActive(true);
            hasHeart = false;
            slot4Full = true;
        }
        else if (hasNightOwlToken)
        {
            _inventorySlot4[slot4Location].gameObject.SetActive(false);
            slot4Location = 9;
            _inventorySlot4[slot4Location].gameObject.SetActive(true);
            hasNightOwlToken = false;
            slot4Full = true;
        }
        else if (hasPeasInPod)
        {
            _inventorySlot4[slot4Location].gameObject.SetActive(false);
            slot4Location = 10;
            _inventorySlot4[slot4Location].gameObject.SetActive(true);
            hasPeasInPod = false;
            slot4Full = true;
        }
        else if (hasCompass)
        {
            _inventorySlot4[slot4Location].gameObject.SetActive(false);
            slot4Location = 11;
            _inventorySlot4[slot4Location].gameObject.SetActive(true);
            hasCompass = false;
            slot4Full = true;
        }
        else if (hasAvocado)
        {
            _inventorySlot4[slot4Location].gameObject.SetActive(false);
            slot4Location = 12;
            _inventorySlot4[slot4Location].gameObject.SetActive(true);
            hasAvocado = false;
            slot4Full = true;
        }
        slot4Text.gameObject.SetActive(false);
    }

    /// Author: JT Esmond
    /// Date: 2/10/2021
    /// <summary>
    /// this function first checks if the player has an item
    /// if they don't it cancels the function
    /// if they do it the proceeds to check which item is being picked up
    /// once it determines which item is being picked up it sets that items UI element to active in Slot 5
    /// it also sets the boolean for that item to false, to allow other items of the same type to be picked up
    /// along with set the 1st slot to full so other items don't get added to it
    /// if it is an ingredient, it also sets the specific ingredient to hold in that slot
    /// </summary>
    public void Slot5()
    {
        if (!hasItem) return;

        if (hasIngredientA && !AIn1 && !AIn2 && !AIn3 && !AIn4 && !AIn5)
        {
            _inventorySlot5[slot5Location].gameObject.SetActive(false);
            slot5Location = 0;
            _inventorySlot5[slot5Location].gameObject.SetActive(true);
            AIn5 = true;
            hasIngredientA = false;
            slot5Full = true;
        }
        else if (hasIngredientB && !BIn1 && !BIn2 && !BIn3 && !BIn4 && !BIn5)
        {
            _inventorySlot5[slot5Location].gameObject.SetActive(false);
            slot5Location = 1;
            _inventorySlot5[slot5Location].gameObject.SetActive(true);
            BIn5 = true;
            hasIngredientB = false;
            slot5Full = true;
        }
        else if (hasIngredientC && !CIn1 && !CIn2 && !CIn3 && !CIn4 && !CIn5)
        {
            _inventorySlot5[slot5Location].gameObject.SetActive(false);
            slot5Location = 2;
            _inventorySlot5[slot5Location].gameObject.SetActive(true);
            CIn5 = true;
            hasIngredientC = false;
            slot5Full = true;
        }
        else if (hasIngredientD && !DIn1 && !DIn2 && !DIn3 && !DIn4 && !DIn5)
        {
            _inventorySlot5[slot5Location].gameObject.SetActive(false);
            slot5Location = 3;
            _inventorySlot5[slot5Location].gameObject.SetActive(true);
            DIn5 = true;
            hasIngredientD = false;
            slot5Full = true;
        }
        else if (hasTotem)
        {
            _inventorySlot5[slot5Location].gameObject.SetActive(false);
            slot5Location = 4;
            _inventorySlot5[slot5Location].gameObject.SetActive(true);
            hasTotem = false;
            slot5Full = true;
        }
        else if (hasLuckyPenny)
        {
            _inventorySlot5[slot5Location].gameObject.SetActive(false);
            slot5Location = 5;
            _inventorySlot5[slot5Location].gameObject.SetActive(true);
            hasLuckyPenny = false;
            slot5Full = true;
        }
        else if (hasAttackCandy)
        {
            _inventorySlot5[slot5Location].gameObject.SetActive(false);
            slot5Location = 6;
            _inventorySlot5[slot5Location].gameObject.SetActive(true);
            hasAttackCandy = false;
            slot5Full = true;
        }
        else if (hasBalloonBouquet)
        {
            _inventorySlot5[slot5Location].gameObject.SetActive(false);
            slot5Location = 7;
            _inventorySlot5[slot5Location].gameObject.SetActive(true);
            hasBalloonBouquet = false;
            slot5Full = true;
        }
        else if (hasHeart)
        {
            _inventorySlot5[slot5Location].gameObject.SetActive(false);
            slot5Location = 8;
            _inventorySlot5[slot5Location].gameObject.SetActive(true);
            hasHeart = false;
            slot5Full = true;
        }
        else if (hasNightOwlToken)
        {
            _inventorySlot5[slot5Location].gameObject.SetActive(false);
            slot5Location = 9;
            _inventorySlot5[slot5Location].gameObject.SetActive(true);
            hasNightOwlToken = false;
            slot5Full = true;
        }
        else if (hasPeasInPod)
        {
            _inventorySlot5[slot5Location].gameObject.SetActive(false);
            slot5Location = 10;
            _inventorySlot5[slot5Location].gameObject.SetActive(true);
            hasPeasInPod = false;
            slot5Full = true;
        }
        else if (hasCompass)
        {
            _inventorySlot5[slot5Location].gameObject.SetActive(false);
            slot5Location = 11;
            _inventorySlot5[slot5Location].gameObject.SetActive(true);
            hasCompass = false;
            slot5Full = true;
        }
        else if (hasAvocado)
        {
            _inventorySlot5[slot5Location].gameObject.SetActive(false);
            slot5Location = 12;
            _inventorySlot5[slot5Location].gameObject.SetActive(true);
            hasAvocado = false;
            slot5Full = true;
        }
        slot5Text.gameObject.SetActive(false);
    }

    #endregion

    #region IngredientXUpdate()

    /// Author: JT Esmond
    /// Date: 2/10/2021
    /// <summary>
    /// this function needs to be called whenever the player picks up ingredient A
    /// it checks which slot the ingredient is in, and then turns on that text element in the UI
    /// it the sets that text element equal to the value of ingredientAAmount
    /// finally it sets both the hasItem and the hasIngredientA booleans to false
    ///     **this is so that when it is called again it will allow it to stack and build the number up
    /// </summary>
    public void IngredientAUpdate()
    {
        if (AIn1)
        {
            slot1Text.gameObject.SetActive(true);
            ingredientAAmount++;
            slot1Text.text = ("" + ingredientAAmount);
        }
        else if (AIn2)
        {
            slot2Text.gameObject.SetActive(true);
            ingredientAAmount++;
            slot2Text.text = ("" + ingredientAAmount);
        }
        else if (AIn3)
        {
            slot3Text.gameObject.SetActive(true);
            ingredientAAmount++;
            slot3Text.text = ("" + ingredientAAmount);
        }
        else if (AIn4)
        {
            slot4Text.gameObject.SetActive(true);
            ingredientAAmount++;
            slot4Text.text = ("" + ingredientAAmount);
        }
        else if (AIn5)
        {
            slot5Text.gameObject.SetActive(true);
            ingredientAAmount++;
            slot5Text.text = ("" + ingredientAAmount);
        }
        hasItem = false;
        hasIngredientA = false;
    }

    /// Author: JT Esmond
    /// Date: 2/10/2021
    /// <summary>
    /// this function needs to be called whenever the player picks up ingredient B
    /// it checks which slot the ingredient is in, and then turns on that text element in the UI
    /// it the sets that text element equal to the value of ingredientBAmount
    /// finally it sets both the hasItem and the hasIngredientB booleans to false
    ///     **this is so that when it is called again it will allow it to stack and build the number up
    /// </summary>
    public void IngredientBUpdate()
    {
        if (BIn1)
        {
            slot1Text.gameObject.SetActive(true);
            ingredientBAmount++;
            slot1Text.text = ("" + ingredientBAmount);
        }
        else if (BIn2)
        {
            slot2Text.gameObject.SetActive(true);
            ingredientBAmount++;
            slot2Text.text = ("" + ingredientBAmount);
        }
        else if (BIn3)
        {
            slot3Text.gameObject.SetActive(true);
            ingredientBAmount++;
            slot3Text.text = ("" + ingredientBAmount);
        }
        else if (BIn4)
        {
            slot4Text.gameObject.SetActive(true);
            ingredientBAmount++;
            slot4Text.text = ("" + ingredientBAmount);
        }
        else if (BIn5)
        {
            slot5Text.gameObject.SetActive(true);
            ingredientBAmount++;
            slot5Text.text = ("" + ingredientBAmount);
        }
        hasItem = false;
        hasIngredientB = false;
    }

    /// Author: JT Esmond
    /// Date: 2/10/2021
    /// <summary>
    /// this function needs to be called whenever the player picks up ingredient C
    /// it checks which slot the ingredient is in, and then turns on that text element in the UI
    /// it the sets that text element equal to the value of ingredientCAmount
    /// finally it sets both the hasItem and the hasIngredientC booleans to false
    ///     **this is so that when it is called again it will allow it to stack and build the number up
    /// </summary>
    public void IngredientCUpdate()
    {
        if (CIn1)
        {
            slot1Text.gameObject.SetActive(true);
            ingredientCAmount++;
            slot1Text.text = ("" + ingredientCAmount);
        }
        else if (CIn2)
        {
            slot2Text.gameObject.SetActive(true);
            ingredientCAmount++;
            slot2Text.text = ("" + ingredientCAmount);
        }
        else if (CIn3)
        {
            slot3Text.gameObject.SetActive(true);
            ingredientCAmount++;
            slot3Text.text = ("" + ingredientCAmount);
        }
        else if (CIn4)
        {
            slot4Text.gameObject.SetActive(true);
            ingredientCAmount++;
            slot4Text.text = ("" + ingredientCAmount);
        }
        else if (CIn5)
        {
            slot5Text.gameObject.SetActive(true);
            ingredientCAmount++;
            slot5Text.text = ("" + ingredientCAmount);
        }
        hasItem = false;
        hasIngredientC = false;
    }

    /// Author: JT Esmond
    /// Date: 2/10/2021
    /// <summary>
    /// this function needs to be called whenever the player picks up ingredient D
    /// it checks which slot the ingredient is in, and then turns on that text element in the UI
    /// it the sets that text element equal to the value of ingredientDAmount
    /// finally it sets both the hasItem and the hasIngredientD booleans to false
    ///     **this is so that when it is called again it will allow it to stack and build the number up
    /// </summary>
    public void IngredientDUpdate()
    {
        if (DIn1)
        {
            slot1Text.gameObject.SetActive(true);
            ingredientDAmount++;
            slot1Text.text = ("" + ingredientDAmount);
        }
        else if (DIn2)
        {
            slot2Text.gameObject.SetActive(true);
            ingredientDAmount++;
            slot2Text.text = ("" + ingredientDAmount);
        }
        else if (DIn3)
        {
            slot3Text.gameObject.SetActive(true);
            ingredientDAmount++;
            slot3Text.text = ("" + ingredientDAmount);
        }
        else if (DIn4)
        {
            slot4Text.gameObject.SetActive(true);
            ingredientDAmount++;
            slot4Text.text = ("" + ingredientDAmount);
        }
        else if (DIn5)
        {
            slot5Text.gameObject.SetActive(true);
            ingredientDAmount++;
            slot5Text.text = ("" + ingredientDAmount);
        }
        hasItem = false;
        hasIngredientD = false;
    }

    #endregion

    #endregion
}
