using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            GetComponentInParent<TrackingSpell>().trackedEnemy = other.gameObject;
        }
    }
}
