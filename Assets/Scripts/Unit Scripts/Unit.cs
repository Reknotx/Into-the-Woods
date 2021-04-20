﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: Chase O'Connor
/// Date: 2/2/2021
/// <summary>
/// The base unit class for all of the units in the game.
/// </summary>
public class Unit : MonoBehaviour
{
    #region Fields
    /// <summary> The speed at which this unit moves. </summary>
    [Tooltip("The speed at which this unit moves.")]
    public float speed = 1f;

    /// <summary> The direction this unit will be moving in. </summary>
    protected Vector3 moveDir;

    /// <summary> The private variable of this unit's current health. </summary>
    private int _currHealth;

    [SerializeField] private int StartHealth = 1;

    /// <summary> The rigidbody component of this unit. </summary>
    private Rigidbody unitRB;
    #endregion

    #region Properties
    /// <summary> Unit health property. </summary>
    /// <value> Sets/Gets the current health of this unit. 
    /// If health reaches zero the unit is killed. </value>
    public virtual int Health
    {
        get => _currHealth;

        set
        {
            _currHealth = value;

            if (_currHealth <= 0)
            {
                if (this is Player player)
                {
                    if (player.PInven.HasItem(Inventory.Items.Totem))
                    {
                        ((Totem)player.PInven.GetItem(Inventory.Items.Totem)).UseItem();
                    }
                    else
                    {
                        Camera.main.transform.parent = null;
                        WinLoseUI.Instance.YouLose();
                    }
                }
                else if (this is Enemy enemy && PlayerInfo.CurrentRoom != null)
                {
                    enemy.Drop();
                    PlayerInfo.CurrentRoom.RemoveEnemy(enemy);
                }

                if (_currHealth <= 0) Destroy(gameObject);
            }
        }
    }
    #endregion

    protected virtual void Awake()
    {
        if (transform.parent != null
            && transform.parent.CompareTag("Obj Container")
            && transform.parent.GetComponent<Rigidbody>() != null)
        {
            unitRB = transform.parent.GetComponent<Rigidbody>();
        }
        else
        {
            unitRB = GetComponent<Rigidbody>();
        }

    }

    protected virtual void Start()
    {
        Health = StartHealth;
    }

    /// <summary>
    /// Deals damage to the unit
    /// </summary>
    /// <param name="dmgAmount">The amount of damage dealt to this Unit.</param>
    public virtual void TakeDamage(int dmgAmount)
    {
        Health -= dmgAmount;
    }


    #region Movement
    /// /// Author: Chase O'Connor
    /// Date: 2/2/2021
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
    #endregion
}
