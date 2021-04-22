using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: JT Esmond
/// Date: 1/10/2021
/// <summary>
/// interactable class that lets the player win the game if they have killed a boss
/// </summary>
public class CageInteractable : Interactable
{
    public override void Interact()
    {
        if(WinLoseUI.Instance.bossDead == true)
        {
            WinLoseUI.Instance.YouWin();
        }
    }
}
