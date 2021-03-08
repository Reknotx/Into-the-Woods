using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareChest : MonoBehaviour
{
    [Tooltip("The item stored in this chest.")]
    public GameObject storedObj;

    public Animator animator;

    

    /// <summary> Triggers the opening animation for the chest so the item is displayed. </summary>
    public void Open()
    {
        ///Start chest opening animation
        ///Start item rising animation
        ///

        Vector3 spawnPos = new Vector3(transform.position.x, storedObj.transform.position.y, transform.position.z);
        Instantiate(storedObj, spawnPos, Quaternion.identity);
        Destroy(gameObject);
    }
}
