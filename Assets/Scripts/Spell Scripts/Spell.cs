using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: Chase O'Connor
/// Date: 2/2/2021
/// <summary>
/// The base spell class of all spells the player can cast.
/// </summary>
public abstract class Spell : MonoBehaviour
{
    public LayerMask layerToHit;

    /// <summary> The maximum distance this spell can travel. </summary>
    [Tooltip("The maximum distance this spell can travel.")]
    public float maxTravelDist = 10f;

    /// <summary> The speed that this spell moves at. </summary>
    [Tooltip("The maximum distance this spell can travel.")]
    public float speed = 10f;

    /// <summary> The cooldown of this spell. </summary>
    [Tooltip("The cooldown of this spell.")]
    public float coolDown = 5f;

    /// <summary> The cooldown left on this spell. </summary>
    [HideInInspector] public float remainingCooldown = 5f;

    /// <summary> The distance that the spell has traveled since spawning. </summary>
    protected float distTraveled = 0f;

    /// <summary> The location that the spell was spawned at. </summary>
    protected Vector3 spawnLoc;

    /// Author: Chase O'Connor
    /// Date: 2/2/2021
    /// <summary> The abstract function that all spells have to write their own functionality. </summary>
    public abstract void TriggerSpellEffect(GameObject other);


    protected virtual void Start()
    {
        spawnLoc = transform.position;
        remainingCooldown = coolDown;
        //Debug.Log("Remaining cooldown = " + remainingCooldown);
        //Player.Instance.spellTicker.AddToList(this);
    }

    public virtual void FixedUpdate()
    {
        Move();
    }

    protected virtual void Update()
    {
        if (distTraveled >= maxTravelDist)
        {
            //Debug.Log(Vector3.Distance(transform.position, spawnLoc));
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        Vector3 currPos = transform.position;

        Vector3 newPos = transform.position + (transform.forward * speed * Time.deltaTime);

        float posDelta = Vector3.Distance(currPos, newPos);

        GetComponent<Rigidbody>().MovePosition(newPos);

        distTraveled += posDelta;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 27)
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == 13 && (1 << collision.gameObject.layer) != layerToHit.value)
        {
            Destroy(gameObject);
        }

        if ((1 << collision.gameObject.layer) != layerToHit.value) return;

        TriggerSpellEffect(collision.gameObject);

        Destroy(gameObject);
    }
}
