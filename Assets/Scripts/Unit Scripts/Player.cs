using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

/// Author: Chase O'Connor
/// Date: 2/2/2021
/// <summary>
/// The player class that handles all of the input and logic.
/// </summary>
public class Player : Unit
{
    #region Fields
    /// <summary> The singleton instance of the player. </summary>
    public static Player Instance;

    /// <summary> This is temporary text, for Gif purposes. </summary>
    public Text InteractText;

    #region GameObject references
    /// <summary> The list of spells the player can cast. </summary>
    public List<GameObject> spells = new List<GameObject>();

    /// <summary> The location that the spell is cast at. </summary>
    public GameObject spellCastLoc;

    public GameObject tempInventoryPanel;

    #endregion

    [SerializeField]
    private int PlayerCurrentHealth;

    [Tooltip("The speed at which spells are launched from the player.")]
    /// <summary> The speed at which the spell is fired. </summary>
    public float spellSpeed = 500;

    /// <summary> The private field of the bonus health. </summary>
    private int _bonusHealth;

    /// <summary> The index referencing the currently selected spell. </summary>
    private int _spellIndex = 0;
    #endregion

    #region Properties
    ///<summary> The nearby interactable items. </summary>
    /// <value> A list of interactable items in the player's vicinity. </value>
    public List<GameObject> NearbyInteractables { get; set; } = new List<GameObject>();

    /// <summary> The currently selected spell. </summary>
    /// <value> The GameObject that will be spawned when the player attacks. </value>
    public GameObject SelectedSpell { get; set; }

    /// <summary> Indicates if the player is near an interactable. </summary>
    /// <value> A flag to tell the player that they are next to an interactable item. </value>
    public bool NextToInteractable { get; set; } = false;

    /// <summary>The health of the player.</summary>
    /// <value> The Health property gets/sets the value of the _health field in Unit,
    /// and sends an update to the UI. </value>
    public override int Health
    {
        get => base.Health;

        set
        {
            if (value < base.Health)
            {
                ///Removing health
                if (_bonusHealth > 0)
                    BonusHealth--;
                else
                    base.Health = value;
            }
            else
            {
                ///Adding health
                base.Health = Mathf.Clamp(value, 0, 20);
            }

            PlayerCurrentHealth = base.Health;
            Debug.Log("Player health = " + base.Health.ToString());

            //if (healthText != null)
            //{
            //    healthText.text = "Health: " + base.Health;
            //}
        }
    }

    /// <summary> The bonus health of the player. </summary>
    /// <value>The bonus health property gets/sets the _bonusHealth field of the 
    /// player, and sends an update to the UI.</value>
    public int BonusHealth
    {
        get => _bonusHealth;

        set 
        { 
            _bonusHealth = value;

            Debug.Log("Player bonus health " + _bonusHealth.ToString());
        
        }
    }
    #endregion


    protected override void Awake()
    {
        base.Awake();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;

        if (spells[0] != null) SelectedSpell = spells[0];

        if (tempInventoryPanel != null) tempInventoryPanel.SetActive(false);

        Health = 20;
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
    }

    private void Update()
    {
        Rotate();

        /// The player wants to cast a spell.
        if (Input.GetMouseButtonDown(0)) CastSpell();
        else if (Input.mouseScrollDelta.y > 0)
        {
            ///Move to next spell, spell 2 to spell 3
            Debug.Log("Moving to next spell.");
            
            //spells[_spellIndex].SetActive(false);
            
            _spellIndex++;
            if (_spellIndex == spells.Count) _spellIndex = 0;

            //spells[_spellIndex].SetActive(true);

            SelectedSpell = spells[_spellIndex];
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            ///Move to previous spell, spell 3 to spell 2
            Debug.Log("Moving to previous spell.");
            
            //spells[_spellIndex].SetActive(false);
            
            _spellIndex--;
            if (_spellIndex < 0) _spellIndex = spells.Count - 1;
            
            //spells[_spellIndex].SetActive(true);
            
            SelectedSpell = spells[_spellIndex];
        }

        /// The player wants to use a potion.
        if (Input.GetKeyDown(KeyCode.Alpha1)
            || Input.GetKeyDown(KeyCode.Alpha2)
            || Input.GetKeyDown(KeyCode.Alpha3))
        {
            UsePotion();
        }

        if (Input.GetKeyDown(KeyCode.BackQuote)) Health--;
        /// The player wants to interact with an item.
        /// See the note for this function down below.
        /// Need additional flags.
        if (Input.GetKeyDown(KeyCode.F) && NextToInteractable) InteractWithItem();

        /// The player wants to open their inventory.
        if (Input.GetKeyDown(KeyCode.Tab) && tempInventoryPanel != null) OpenInventory();
    }

    #region Movement
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
    /// <summary>
    /// Rotates the player to face the cursor
    /// </summary>
    private void Rotate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, 1000f, 1 << 31);

        if (hit.collider == null) return;

        transform.LookAt(new Vector3(hit.point.x, 1f, hit.point.z));
    }
    #endregion

    #region Player Command Functions
    /// Author: Chase O'Connor
    /// Date: 2/2/2021
    /// <summary> Casts's a spell when the player presses the left mouse button. </summary>
    private void CastSpell()
    {
        Debug.Log("Casting: " + SelectedSpell.name);

        List<GameObject> firedSpells = new List<GameObject>();

        if (PlayerInfo.DoubleShot)
        {
            Vector3 frontPos = new Vector3(spellCastLoc.transform.position.x + (spellCastLoc.transform.forward.x * 0.5f),
                                           spellCastLoc.transform.position.y,
                                           spellCastLoc.transform.position.z + (spellCastLoc.transform.forward.z * 0.5f));

            firedSpells.Add(Instantiate(SelectedSpell, frontPos, Quaternion.identity));

            firedSpells.Add(Instantiate(SelectedSpell, spellCastLoc.transform.position, Quaternion.identity));
        }
        else
        {
            firedSpells.Add(Instantiate(SelectedSpell, spellCastLoc.transform.position, Quaternion.identity));
        }

        foreach (GameObject spell in firedSpells)
        {
            //Debug.Log("Spell transform: " + spell.transform.position.ToString());
            spell.transform.forward = transform.forward;
            //spell.GetComponent<Rigidbody>().AddForce(transform.forward * spellSpeed);
            
        }
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
        //Debug.Log("Interacting with an item.");

        ///Think about this one later, might be good idea. Remember how casting works.
        //Physics.SphereCast(transform.parent.position, 2.5f, transform.forward, out RaycastHit hit, 1, 1 << 12);
        //if (hit.collider != null) Debug.Log(hit.collider.name);

        if (NearbyInteractables.Count != 0)
        {
            Interactable interactable = NearbyInteractables[0].GetComponent<Interactable>();

            interactable.Interact();

            NearbyInteractables.Remove(interactable.gameObject);
            
            /// Remember to delete this later because of references.
            Destroy(interactable.gameObject);

            if (NearbyInteractables.Count == 0) InteractText.gameObject.SetActive(false);
        }
    }

    /// Author: Chase O'Connor
    /// Date: 2/2/2021
    /// <summary> 
    /// Opens the player's inventory when they press tab on their keyboard.
    /// </summary>
    private void OpenInventory() => tempInventoryPanel.SetActive(!tempInventoryPanel.activeSelf);
    #endregion
}
