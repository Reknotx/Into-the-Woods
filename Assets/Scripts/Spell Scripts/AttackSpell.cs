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
    public float maxTravelDist = 5f, speed = 10f;
    private float distTraveled = 0f;

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

    public void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        if (distTraveled >= maxTravelDist)
        {
            Debug.Log(Vector3.Distance(transform.position, spawnLoc));
            Destroy(gameObject);
        }

        if (trackedEnemy == null) return;
    }


    private void Move()
    {
        Vector3 currPos = transform.position;

        Vector3 newPos = transform.position + (transform.forward * speed * Time.deltaTime);

        float posDelta = Vector3.Distance(currPos, newPos);

        GetComponent<Rigidbody>().MovePosition(newPos);

        distTraveled += posDelta;

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
