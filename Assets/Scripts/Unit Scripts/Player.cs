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
    #region Public
    /// <summary> The singleton instance of the player. </summary>
    public static Player Instance;

    /// <summary> The player inventory. </summary>
    public Inventory PInven;

    /// <summary> The list of spells the player can cast. </summary>
    public List<GameObject> spells = new List<GameObject>();

    /// <summary> The location that the spell is cast at. </summary>
    public GameObject spellCastLoc;

    /// <summary> The protection bubble around the player when they are protected. </summary>
    public GameObject protectionBubble;

    /// <summary>
    /// The game object that has all of the items in the player inventory as
    /// child game objects.
    /// </summary>
    public GameObject PlayerInvenItems;

    [Tooltip("The speed at which spells are launched from the player.")]
    /// <summary> The speed at which the spell is fired. </summary>
    public float spellSpeed = 500;

    /// <summary> The cooldown tracker for the player spells. </summary>
    [HideInInspector] public SpellCDTicker spellTicker = new SpellCDTicker();
    #endregion

    #region Private
    /// <summary> The private field of the bonus health. </summary>
    private int _bonusHealth;

    /// <summary> The index referencing the currently selected spell. </summary>
    private int _spellIndex = 0;
    #endregion
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
            ///If test is needed to ensure that bonus health
            ///gets removed when taking damage if we have any 
            ///bonus hearts at this point. But still allows us
            ///to gain normal health back when picking up the
            ///heart item
            if (value < base.Health)
            {
                ///Removing health
                if (_bonusHealth > 0)
                {
                    BonusHealth--;
                    return;
                }
                else
                {
                    base.Health = value;
                }
            }
            else
            {
                ///Adding health
                base.Health = Mathf.Clamp(value, 0, 20);
            }

            HealthUI.Instance.UpdateHealth();
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
            if (value < _bonusHealth)
            {
                ///Try and work on this later, it's not important at the moment

                foreach (Transform item in PlayerInvenItems.transform)
                {
                    if (item.GetComponent<Collectable>() is Avocado)
                    {
                        item.GetComponent<Avocado>().Uses--;
                        break;
                    }
                }
            }

            _bonusHealth = value;

            HealthUI.Instance.UpdateBonusHealth();
        }
    }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = Screen.currentResolution.refreshRate;


        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;

        PInven = new Inventory();

        if (spells[0] != null) SelectedSpell = spells[0];
    }

    protected override void Start()
    {
        base.Start();

        BonusHealth = 0;

        UI_Inventory.Instance.SetInventory(PInven);
    }

    #region Updates
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

        spellTicker.Tick(Time.deltaTime);

        /// The player wants to cast a spell.
        if (Input.GetMouseButtonDown(0) && !PlayerInfo.SpellsFrozen && !BrewingSystem.Instance.gameObject.activeSelf) CastSpell();
        else if (Input.mouseScrollDelta.y > 0)
        {
            ///Move to next spell, spell 2 to spell 3
            //Debug.Log("Moving to next spell.");
                        
            _spellIndex++;
            if (_spellIndex == spells.Count) _spellIndex = 0;

            SelectedSpell = spells[_spellIndex];
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            ///Move to previous spell, spell 3 to spell 2
            //Debug.Log("Moving to previous spell.");
                        
            _spellIndex--;
            if (_spellIndex < 0) _spellIndex = spells.Count - 1;
            
            SelectedSpell = spells[_spellIndex];
        }

        /// The player wants to use a potion.
        if (Input.GetKeyDown(KeyCode.Alpha1)
            || Input.GetKeyDown(KeyCode.Alpha2)
            || Input.GetKeyDown(KeyCode.Alpha3))
        {
            UsePotion();
        }

        if (Input.GetKeyDown(KeyCode.R) && UI_Inventory.Instance.gameObject.activeSelf)
        {
            ///Drop item.

            UI_Inventory.Instance.DropItem();
        }

        ///TODO - Delete later when Paul works on Enemy AI
        if (Input.GetKeyDown(KeyCode.BackQuote)) Health--;
        
        /// The player wants to interact with an item.
        /// See the note for this function down below.
        /// Need additional flags.
        if (Input.GetKeyDown(KeyCode.F) && NextToInteractable) InteractWithItem();

        /// The player wants to open their inventory.
        if (Input.GetKeyDown(KeyCode.Tab) && UI_Inventory.Instance != null) OpenInventory();

    }
    #endregion

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
        moveDir = Vector3.zero;

        moveDir = new Vector3(Input.GetAxis("Horizontal"),
                              0f,
                              Input.GetAxis("Vertical")).normalized;

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

        transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
    }
    #endregion

    #region Player Command Functions
    /// Author: Chase O'Connor
    /// Date: 2/2/2021
    /// <summary> Casts's a spell when the player presses the left mouse button. </summary>
    private void CastSpell()
    {
        if (spellTicker.SpellOnCD(SelectedSpell.GetComponent<Spell>())) return;

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
            spell.transform.forward = transform.forward;
        }

        spellTicker.AddToList(firedSpells[0].GetComponent<Spell>());
    }

    /// Author: Chase O'Connor
    /// Date: 2/2/2021
    /// <summary>
    /// Use's a potion when the player presses the 1, 2, or 3 keys
    /// on their keyboard.
    /// </summary>
    private void UsePotion()
    {
        Potion tempPotion = null;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ///Use potion 1
            if (PInven.Potions[0] == null) return;
            
            tempPotion = PInven.Potions[0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ///Use potion 2
            if (PInven.Potions[1] == null) return;

            tempPotion = PInven.Potions[1];            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ///Use potion 3
            if (PInven.Potions[2] == null) return;

            tempPotion = PInven.Potions[2];
        }

        if (tempPotion != null) tempPotion.UsePotion();

        PInven.RemovePotion(tempPotion);
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
        if (NearbyInteractables.Count != 0)
        {
            Interactable interactable = NearbyInteractables[0].GetComponent<Interactable>();

            interactable.Interact();

            NearbyInteractables.Remove(interactable.gameObject);
        }
    }

    /// Author: Chase O'Connor
    /// Date: 2/2/2021
    /// <summary> 
    /// Opens the player's inventory when they press tab on their keyboard.
    /// </summary>
    private void OpenInventory() => UI_Inventory.Instance.InventoryDisplay();
    #endregion

    public override void TakeDamage(int dmgAmount)
    {
        if (PlayerInfo.IsProtected) return;

        base.TakeDamage(dmgAmount);

    }

    #region Special Effects. Put in special class later!!!
    /// Author: Chase O'Connor
    /// Date: 2/15/2021
    /// <summary>
    /// Gives the player a protection bubble for a duration.
    /// </summary>
    /// <param name="protectionDur">The length of the protection spell.</param>
    public IEnumerator ProtectionBubble(float protectionDur)
    {
        PlayerInfo.IsProtected = true;

        if (protectionBubble != null) protectionBubble.SetActive(true);

        yield return new WaitForSeconds(protectionDur);

        PlayerInfo.IsProtected = false;

        if (protectionBubble != null) protectionBubble.SetActive(false);
    }

    /// <summary>
    /// Makes the player "invisible" for an amount of time.
    /// </summary>
    /// <param name="duration">How long the invisibility lasts for.</param>
    public IEnumerator Invisibility(float duration)
    {
        Color color = GetComponent<MeshRenderer>().material.color;
        color.a = 0.5f;

        GetComponent<MeshRenderer>().material.color = color;

        yield return new WaitForSeconds(duration);

        color.a = 1f;
        GetComponent<MeshRenderer>().material.color = color;

    }

    /// Author: Chase O'Connor
    /// Date: 2/19/2021
    /// <summary>
    /// Gives the player double damage.
    /// </summary>
    /// <param name="duration">The length of the double damage effect.</param>
    public IEnumerator DoubleDamage(float duration)
    {
        PlayerInfo.AttackDamage += 5;
        Debug.Log("Player damage is now " + PlayerInfo.AttackDamage);

        yield return new WaitForSeconds(duration);

        PlayerInfo.AttackDamage -= 5;
        Debug.Log("Player damage is now " + PlayerInfo.AttackDamage);
    }

    /// <summary>
    /// Grants the player immunity to having their spell casting frozen.
    /// </summary>
    /// <param name="duration">The duration of the effect.</param>
    public IEnumerator FrozenHeart(float duration)
    {
        PlayerInfo.SpellFreezeImmune = true;
        Debug.Log("Player immune to spell freezing for " + duration + " seconds");

        yield return new WaitForSeconds(duration);

        PlayerInfo.SpellFreezeImmune = false;
    }

    /// Author: Chase O'Connor
    /// Date: 3/2/2021
    /// <summary> Makes the player unable to cast any spells for a certain time. </summary>
    /// <param name="duration">How long the player is unable to cast in seconds.</param>
    public IEnumerator SpellsFrozen(float duration)
    {
        PlayerInfo.SpellsFrozen = true;

        yield return new WaitForSeconds(duration);

        PlayerInfo.SpellsFrozen = false;
    }

    ///Author: Chase O'Connor
    ///Date: 3/2/2021
    /// <summary> Starts the bleed DOT. </summary>
    /// <param name="duration">How long in seconds does the bleed effect last for.</param>
    public IEnumerator Bleed(int duration)
    {
        for (int i = 0; i < duration; i++)
        {
            yield return new WaitForSeconds(1f);
            Health--;
        }
    }
    #endregion
}