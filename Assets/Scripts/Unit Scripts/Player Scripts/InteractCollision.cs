using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: Chase O'Connor
/// Date: 2/2/2021
/// <summary>
/// Checks for collisions on the interactable layer.
/// </summary>
/// <remarks>
/// When a Game Object on the interactable layer enters the trigger it
/// is added to the appropriate list on the Player script.
/// </remarks>
public class InteractCollision : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != 12) return;

        if (other.GetComponent<CageInteractable>() != null && WinLoseUI.Instance.bossDead == false)
        {

        }
        
        
        if (!Player.Instance.NearbyInteractables.Contains(other.gameObject))
            Player.Instance.NearbyInteractables.Add(other.gameObject);

        Player.Instance.NextToInteractable = true;
        other.transform.GetChild(0).gameObject.SetActive(true);
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 12) return;

        if (Player.Instance.NearbyInteractables.Contains(other.gameObject))
            Player.Instance.NearbyInteractables.Remove(other.gameObject);

        if (Player.Instance.NearbyInteractables.Count == 0)
        {
            Player.Instance.NextToInteractable = false;
        }
        other.transform.GetChild(0).gameObject.SetActive(false);
    }
}
