using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 12) return;

        if (!Player.Instance.nearbyInteractables.Contains(other.gameObject))
            Player.Instance.nearbyInteractables.Add(other.gameObject);

        Player.Instance.NextToInteractable = true;
        Player.Instance.InteractText.gameObject.SetActive(true);
    }


    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.layer != 12) return;

    //    Player.Instance.NextToInteractable = true;
    //    Player.Instance.InteractText.gameObject.SetActive(true);
    //}


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 12) return;

        if (Player.Instance.nearbyInteractables.Contains(other.gameObject))
            Player.Instance.nearbyInteractables.Remove(other.gameObject);

        if (Player.Instance.nearbyInteractables.Count == 0)
        {
            Player.Instance.NextToInteractable = false;
            Player.Instance.InteractText.gameObject.SetActive(false);
        }
    }
}
