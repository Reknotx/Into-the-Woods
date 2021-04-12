using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: JT Esmond
/// Date: 2/16/2021
/// <summary>
/// class that contains the functions that set the different items
/// </summary>
public class ItemWorld : MonoBehaviour
{

    //function for spawning the ItemWorld prefab. Not sure if this is needed since we have other items made already.
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.itemWorldPF, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    private Item item;
    private GameObject _gameObject;

    //function that sets the type of item used
    public void SetItem(Item item)
    {
        this.item = item;
 
    }

    //function that returns the item type
    public Item GetItem()
    {
        return item;
    }

    //function for destroying the object when its picked up. not sure if this is needed, just from the tutorial
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

}
