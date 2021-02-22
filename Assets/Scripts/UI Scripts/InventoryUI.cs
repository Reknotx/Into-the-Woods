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
    //public static InventoryUI Instance;
    #endregion

    [SerializeField] private UI_Inventory uiInventory;

    public Inventory inventory;

    private void Awake()
    {
        inventory = new Inventory();

        uiInventory.SetInventory(inventory);

        //ItemWorld.SpawnItemWorld(new Vector3(20, 20), new Item { itemType = Item.ItemType.attackCandy, amount = 1 });
        //ItemWorld.SpawnItemWorld(new Vector3(-20, 20), new Item { itemType = Item.ItemType.avocado, amount = 1 });
        //ItemWorld.SpawnItemWorld(new Vector3(0, -20), new Item { itemType = Item.ItemType.twoPeasInPod, amount = 1 });
        //ItemWorld.SpawnItemWorld(new Vector3(0, -20), new Item { itemType = Item.ItemType.twoPeasInPod, amount = 1 });
        //ItemWorld.SpawnItemWorld(new Vector3(0, -20), new Item { itemType = Item.ItemType.ingredientA, amount = 1 });
        //ItemWorld.SpawnItemWorld(new Vector3(0, -20), new Item { itemType = Item.ItemType.ingredientA, amount = 1 });
    }

    //private void OnTriggerEnter(Collider collider)
    //{
    //    ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
    //    if (itemWorld != null)
    //    {
    //        //touching item
    //        inventory.AddItem(itemWorld.GetItem());
    //        itemWorld.DestroySelf();
    //    }
    //}

    /*
private List<GameObject> _itemPrefabList = new List<GameObject>();

public GameObject inventoryHolder;
public GameObject itemPreFabHolder;

private GameObject item1;
private GameObject item2;
private GameObject item3;
private GameObject item4;
private GameObject item5;

private Vector3 slot1Location;
private Vector3 slot2Location;
private Vector3 slot3Location;
private Vector3 slot4Location;
private Vector3 slot5Location;

public enum ItemType
{
    ingredientA,
    ingredientB,
    ingredientC,
    ingredientD,
    attackCandy,
    avocado,
    balloonBouquet,
    compass,
    heart,
    luckyPenny,
    nightOwlToken,
    totem,
    twoPeasInPod
}

public enum Slot
{
    slot1,
    slot2,
    slot3,
    slot4,
    slot5
}

public Slot slot;
public ItemType item;

public void Awake()
{
    if (Instance != null && Instance != this)
    {
        Destroy(gameObject);
    }

    Instance = this;
}

private void Start()
{

    slot1Location = inventoryHolder.transform.GetChild(0).transform.position;
    slot2Location = inventoryHolder.transform.GetChild(1).transform.position;
    slot3Location = inventoryHolder.transform.GetChild(2).transform.position;
    slot4Location = inventoryHolder.transform.GetChild(3).transform.position;
    slot5Location = inventoryHolder.transform.GetChild(4).transform.position;
    //_attackCandyUI = Instantiate(attackCandyUI, transform.position, transform.rotation);
    //_attackCandyUI.transform.SetParent(inventoryHolder.transform.GetChild(0));
   // _attackCandyUI.transform.position = slot1Location;
}

public void AddInventory()
{
    int slotNumber = 1;

    switch (slotNumber = 1)
    {
        case 1:
            switch (item)
            {
                case ItemType.ingredientA:
                    item1 = Instantiate(_itemPrefabList[0].gameObject, transform.position, transform.rotation);
                    item1.transform.SetParent(inventoryHolder.transform.GetChild(0));
                    item1.transform.position = slot1Location;
                    break;
                case ItemType.ingredientB:
                    item1 = Instantiate(_itemPrefabList[1].gameObject, transform.position, transform.rotation);
                    item1.transform.SetParent(inventoryHolder.transform.GetChild(0));
                    item1.transform.position = slot1Location;
                    break;
                case ItemType.ingredientC:
                    item1 = Instantiate(_itemPrefabList[2].gameObject, transform.position, transform.rotation);
                    item1.transform.SetParent(inventoryHolder.transform.GetChild(0));
                    item1.transform.position = slot1Location;
                    break;
            }
            slotNumber++;
        break;
    }
}

public void IngredientA()
{
    item = ItemType.ingredientA;
}

private void AddIngredientA()
{


}

*/
}
