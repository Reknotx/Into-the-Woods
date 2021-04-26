using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHoverText : MonoBehaviour
{
    private void Update()
    {
        this.transform.position = Input.mousePosition;
    }
}
