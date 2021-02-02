using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: Chase O'Connor
/// Date: 2/2/2021
/// <summary>
/// The base unit class for all of the units in the game.
/// </summary>
public class Unit : MonoBehaviour
{
    /// <summary> The direction this unit will be moving in. </summary>
    protected Vector3 moveDir;

    /// <summary> The rigidbody component of this unit. </summary>
    private Rigidbody unitRB;

    /// <summary> The speed at which this unit moves. </summary>
    [Tooltip("The speed at which this unit moves.")]
    public float speed = 1f;

    protected virtual void Awake()
    {
        if (transform.parent.GetComponent<Rigidbody>() != null)
        {
            unitRB = transform.parent.GetComponent<Rigidbody>();
        }
    }


    /// <summary>
    /// Moves this unit in the direction by moving the rigidbody.
    /// </summary>
    /// NOTE: This function is virtual so that the player and the 
    /// enemies can have differing movement logic and pass in the
    /// direction that they need to move to.
    protected virtual void Move()
    {
        if (moveDir == Vector3.zero) return;

        unitRB.MovePosition(transform.position + (moveDir * speed * Time.deltaTime));

    }
}
