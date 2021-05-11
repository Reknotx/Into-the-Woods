using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightRoom : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            PlayerInfo.NightRoom = this;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            StartCoroutine(Delay());
        }
    }

    /// Author: JT Esmond
    /// Date: 4/11/2021
    /// <summary>
    /// delay effect that holds the doors open so the player can get out durring the day
    /// </summary>
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        PlayerInfo.NightRoom = null;
    }
}
