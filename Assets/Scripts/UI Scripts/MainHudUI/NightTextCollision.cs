using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightTextCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 8 && !LightingManager.Instance.night)
        {
            NightText.Instance.On();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            NightText.Instance.Off();
        }
    }
}
