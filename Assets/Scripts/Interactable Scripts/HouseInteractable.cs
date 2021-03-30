using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: JT Esmond
/// Date: 3/29/2021
/// <summary>
/// interactable class that allows the player to set the time of day.
/// </summary>
public class HouseInteractable : Interactable
{
    public override void Interact()
    {
        if (LightingManager.Instance.night)
        {
            ClockUI.Instance.SetTime();
        }
    }
}
