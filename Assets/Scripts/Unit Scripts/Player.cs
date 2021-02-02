using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: Chase O'Connor
/// Date: 2/2/2021
/// <summary>
/// The player class that handles all of the input and logic.
/// </summary>
public class Player : Unit
{
    /// <summary> The singleton instance of the player. </summary>
    public static Player Instance;

    /// Author: Chase O'Connor
    /// Date: 2/2/2021
    /// <summary>
    /// A flag to tell the player that they are next to an interactable
    /// item.
    /// </summary>
    public bool NextToInteractable { get; set; } = false;


    protected override void Awake()
    {
        base.Awake();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    public void FixedUpdate()
    {
        ///The player wants to move.
        if (Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.D)
            || Input.GetKey(KeyCode.A))
        {
            Move();
        }

        /// The player wants to cast a spell.
        if (Input.GetMouseButtonDown(0)) CastSpell();

        /// The player wants to use a potion.
        if (Input.GetKeyDown(KeyCode.Alpha1)
            || Input.GetKeyDown(KeyCode.Alpha2)
            || Input.GetKeyDown(KeyCode.Alpha3))
        {
            UsePotion();
        }

        /// The player wants to interact with an item.
        /// See the note for this function down below.
        /// Need additional flags.
        if (Input.GetKeyDown(KeyCode.F) && NextToInteractable) InteractWithItem();

        /// The player wants to open their inventory.
        if (Input.GetKeyDown(KeyCode.Tab)) OpenInventory();


    }

    /// Author: Chase O'Connor
    /// Date: 2/2/2021
    /// <summary>
    /// The player's move function. Takes their input and assigns it
    /// to the moveDir variable and calls the parent function to actually
    /// move the character.
    /// </summary>
    protected override void Move()
    {

        moveDir = new Vector3(Input.GetAxis("Horizontal"),
                              0f,
                              Input.GetAxis("Vertical"));

        base.Move();

    }

    /// Author: Chase O'Connor
    /// Date: 2/2/2021
    /// <summary> Casts's a spell when the player presses the left mouse button. </summary>
    private void CastSpell()
    {
        Debug.Log("Casting selected spell");
    }

    /// Author: Chase O'Connor
    /// Date: 2/2/2021
    /// <summary>
    /// Use's a potion when the player presses the 1, 2, or 3 keys
    /// on their keyboard.
    /// </summary>
    private void UsePotion()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ///Use potion 1
            Debug.Log("Using potion 1.");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ///Use potion 2
            Debug.Log("Using potion 2.");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ///Use potion 3
            Debug.Log("Using potion 3.");
        }
    }

    /// Author: Chase O'Connor
    /// Date: 2/2/2021
    /// <summary>
    /// Causes the player to interact with an item in the world when
    /// they press the F key.
    /// </summary>
    /// NOTE: It would be a good idea to make this function only be callable
    /// if the player is next to an item that they can interact with. Will
    /// need a flag for that or have objects that have a tirgger collider to
    /// them.
    private void InteractWithItem()
    {
        Debug.Log("Interacting with an item.");
    }

    /// Author: Chase O'Connor
    /// Date: 2/2/2021
    /// <summary> 
    /// Opens the player's inventory when they press tab on their keyboard.
    /// </summary>
    private void OpenInventory()
    {

    }
}
