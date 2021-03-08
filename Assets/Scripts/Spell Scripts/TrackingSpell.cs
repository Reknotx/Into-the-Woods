using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrackingSpell : Spell
{
    /// <summary> Local variable to tell us if we are tracking. </summary>
    private bool track = true;

    /// <summary> The enemy we want to track to.</summary>
    public GameObject trackedEnemy;

    protected override void Start()
    {
        base.Start();

        if (PlayerInfo.SpellTracking)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            track = true;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (track && trackedEnemy != null && CalculateDistance() > 0.1f)
        {
            CalculateAngle();
        }
    }

    #region Spell Tracking
    ///<summary>Calculate the vector to the enemy </summary> 
    void CalculateAngle()
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
    float CalculateDistance()
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
    Vector3 Cross(Vector3 v, Vector3 w)
    {

        float xMult = v.y * w.z - v.z * w.y;
        float yMult = v.z * w.x - v.x * w.z;
        float zMult = v.x * w.y - v.y * w.x;

        Vector3 crossProd = new Vector3(xMult, yMult, zMult);
        return crossProd;
    }

    #endregion
}
