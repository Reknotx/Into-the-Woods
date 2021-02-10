using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: Chase O'Connor
/// Date: 2/2/2021
/// <summary>
/// The basic attack spell the player can cast.
/// </summary>
public class AttackSpell : Spell
{
    public float travelDist = 10f;

    private Vector3 spawnLoc;

    public GameObject trackedEnemy;


    private void Start()
    {
        spawnLoc = transform.position;
        if (PlayerInfo.SpellTracking)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, spawnLoc) >= travelDist)
        {
            Debug.Log(Vector3.Distance(transform.position, spawnLoc));
            Destroy(gameObject);
        }

        if (trackedEnemy == null) return;



    }

    public override void TriggerSpellEffect(GameObject other)
    {
        if (other.gameObject.layer != 10) return;

        //Debug.Log("Casting the Attack Spell");
        Destroy(other.gameObject);
        Destroy(gameObject);
        Debug.Log("Damage = " + PlayerInfo.AttackDamage.ToString());
    }
}
