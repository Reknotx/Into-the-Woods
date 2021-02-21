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

    /// <summary> The enemy we want to track to.</summary>
    public GameObject trackedEnemy;

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
        if ((1 << collision.gameObject.layer) != layerToHit.value) return;

        TriggerSpellEffect(collision.gameObject);

        Destroy(gameObject);
    }

    #region Spell Tracking
    ///<summary>Calculate the vector to the enemy </summary> 
    protected void CalculateAngle()
    {

        // Spell's foward facing vector
        Vector3 sF = this.transform.forward;
        ///<summary> Distance vector to the tracked enemy </summary>
        Vector3 eD = trackedEnemy.transform.position - this.transform.position;

        // Calculate the dot product
        float dot = sF.x * eD.x + sF.z * eD.z;
        float angle = Mathf.Acos(dot / (sF.magnitude * eD.magnitude));

        // Draw a ray showing the Spell's forward facing vector
        Debug.DrawRay(this.transform.position, sF * 10.0f, Color.green, 2.0f);
        // Draw a ray showing the vector to the tracked enemy
        Debug.DrawRay(this.transform.position, eD, Color.red, 2.0f);

        int clockwise = 1;

        // Check the y value of the crossproduct and negate the direction if less than 0
        if (Cross(sF, eD).y < 0.0f)
            clockwise = -1;

        // Use our rotation
        this.transform.Rotate(0.0f, (angle * clockwise * Mathf.Rad2Deg) * 0.05f, 0.0f);
    }

    /// <summary>Calculate the distance from the spell to the tracked enemy</summary>
    /// <returns>The distance from the spell to the enemy.</returns>
    protected float CalculateDistance()
    {

        // Spell position
        Vector3 sP = this.transform.position;
        // Enemy position
        Vector3 eP = trackedEnemy.transform.position;

        // Calculate the distance using pythagoras
        float distance = Mathf.Sqrt(Mathf.Pow(sP.x - eP.x, 2.0f) +
                         Mathf.Pow(sP.y - eP.y, 2.0f) +
                         Mathf.Pow(sP.z - eP.z, 2.0f));

        // Calculate the distance using Unitys vector distance function
        float unityDistance = Vector3.Distance(sP, eP);

        // Return the calculated distance
        return distance;
    }

    ///<summary>Calculate the Cross Product </summary> 
    ///<returns>A vector 3 that contains the cross product between two points.</returns>
    protected Vector3 Cross(Vector3 v, Vector3 w)
    {

        float xMult = v.y * w.z - v.z * w.y;
        float yMult = v.z * w.x - v.x * w.z;
        float zMult = v.x * w.y - v.y * w.x;

        Vector3 crossProd = new Vector3(xMult, yMult, zMult);
        return crossProd;
    }

    #endregion
}
