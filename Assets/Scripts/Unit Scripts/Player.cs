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

    public PotionRecipe startPotion;

    [Tooltip("The speed at which spells are launched from the player.")]
    /// <summary> The speed at which the spell is fired. </summary>
    public float spellSpeed = 500;

    /// <summary> The cooldown tracker for the player spells. </summary>
    [HideInInspector] public SpellCDTicker spellTicker = new SpellCDTicker();

    [Tooltip("The animator controller for this player's rig")]
    public Animator animController;

    public AudioSource attackSource;
    #endregion

    #region Private
    /// <summary> The private field of the bonus health. </summary>
    private int _bonusHealth;

    /// <summary> The index referencing the currently selected spell. </summary>
    private int _spellIndex = 0;

    private GameObject _selectedSpell = null;
    #endregion
    #endregion

    #region Properties
    ///<summary> The nearby interactable items. </summary>
    /// <value> A list of interactable items in the player's vicinity. </value>
    public List<GameObject> NearbyInteractables { get; set; } = new List<GameObject>();

    /// <summary> The currently selected spell. </summary>
    /// <value> The GameObject that will be spawned when the player attacks. </value>
    public GameObject SelectedSpell {
        get => _selectedSpell;
        set
        {
            _selectedSpell = value;

            if (SpellUI.Instance != null)
            {
                SpellUI.Instance.UpdateSpellUI(value.GetComponent<Spell>());
            }
        }
    }

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
                if (PInven.HasItem(Inventory.Items.BalloonBouquet))
                {
                    Debug.Log("Has bouquet");
                    ((BalloonBouquet)PInven.GetItem(Inventory.Items.BalloonBouquet)).Uses--;
                }
                ///Removing health
                else if (_bonusHealth > 0)
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
            if (value < _bonusHealth && PInven.HasItem(Inventory.Items.Avocado))
            {
                ((Avocado)PInven.GetItem(Inventory.Items.Avocado)).Uses--;
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

        PlayerInfo.Reset();

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
        startPotion.Craft(PInven, true);

        List<GameObject> moveList = new List<GameObject>();
    }

    #region Updates
    public void FixedUpdate()
    {
        if (BrewingSystem.Instance.gameObject.activeSelf) return;
        ///The player wants to move.
        if (Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.D)
            || Input.GetKey(KeyCode.A))
        {
            animController.SetBool("IsWalking", true);
            Move();
        }
        else
        {
            animController.SetBool("IsWalking", false);
        }

    }

    private void Update()
    {
        if (BrewingSystem.Instance.gameObject.activeSelf) return;
        Rotate();

        spellTicker.Tick(Time.deltaTime);

        /// The player wants to cast a spell.
        if (Input.GetMouseButtonDown(0) && !PlayerInfo.SpellsFrozen && !BrewingSystem.Instance.gameObject.activeSelf) CastSpell();
        else if (Input.GetKeyDown(KeyCode.E) || Input.mouseScrollDelta.y > 0)
        {
            ///Move to next spell, spell 2 to spell 3
            //Debug.Log("Moving to next spell.");
                        
            _spellIndex++;
            if (_spellIndex == spells.Count) _spellIndex = 0;

            SelectedSpell = spells[_spellIndex];
        }
        else if (Input.GetKeyDown(KeyCode.Q) || Input.mouseScrollDelta.y < 0)
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

        //if (Input.GetKeyDown(KeyCode.BackQuote)) Health--;
        
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

        transform.LookAt(new Vector3(hit.point.x, transform.parent.position.y, hit.point.z));
    }
    #endregion

    #region Player Command Functions
    /// Author: Chase O'Connor
    /// Date: 2/2/2021
    /// <summary> Casts's a spell when the player presses the left mouse button. </summary>
    private void CastSpell()
    {
        if (spellTicker.SpellOnCD(SelectedSpell.GetComponent<Spell>())) return;

        StartCoroutine(CastSpellCR());
    }

    public bool FireSpell { get; set; } = false;

    IEnumerator CastSpellCR()
    {
        animController.SetTrigger("CastAttack");

        //yield return new WaitUntil(() => FireSpell);
        yield return new null;

        attackSource.Play();
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

        FireSpell = false;
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

        //Debug.Log(PlayerPrefs.GetInt("playerDamageMultiplier"));

        int multiplier = PlayerPrefs.GetInt(PrefTags.DmgMulti) == 0 ? 1 : PlayerPrefs.GetInt(PrefTags.DmgMulti);

        GetComponent<AudioSource>().Play();
        
        base.TakeDamage(dmgAmount * multiplier);



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
        EffectPopUps.Instance.TurnOn(4, EffectPopUps.Instance.invisibilityActive);
        EffectPopUps.Instance.invisibilityActive = true;

        Color color = GetComponent<MeshRenderer>().material.color;
        color.a = 0.5f;

        GetComponent<MeshRenderer>().material.color = color;

        yield return new WaitForSeconds(duration);

        color.a = 1f;
        GetComponent<MeshRenderer>().material.color = color;

        EffectPopUps.Instance.TurnOff(EffectPopUps.Instance.invisibilityActive);
        EffectPopUps.Instance.invisibilityActive = false;
    }

    /// Author: Chase O'Connor
    /// Date: 2/19/2021
    /// <summary>
    /// Gives the player double damage.
    /// </summary>
    /// <param name="duration">The length of the double damage effect.</param>
    public IEnumerator DoubleDamage(float duration)
    {
        EffectPopUps.Instance.TurnOn(1, EffectPopUps.Instance.doubleDamageActive);
        EffectPopUps.Instance.doubleDamageActive = true;

        PlayerInfo.AttackDamage += 5;
        Debug.Log("Player damage is now " + PlayerInfo.AttackDamage);

        yield return new WaitForSeconds(duration);

        PlayerInfo.AttackDamage -= 5;
        Debug.Log("Player damage is now " + PlayerInfo.AttackDamage);

        EffectPopUps.Instance.TurnOff(EffectPopUps.Instance.doubleDamageActive);
        EffectPopUps.Instance.doubleDamageActive = false;

    }

    /// <summary>
    /// Grants the player immunity to having their spell casting frozen.
    /// </summary>
    /// <param name="duration">The duration of the effect.</param>
    public IEnumerator FrozenHeart(float duration)
    {
        EffectPopUps.Instance.TurnOn(2, EffectPopUps.Instance.frozenHeartActive);
        EffectPopUps.Instance.frozenHeartActive = true;

        PlayerInfo.SpellFreezeImmune = true;
        Debug.Log("Player immune to spell freezing for " + duration + " seconds");

        yield return new WaitForSeconds(duration);

        PlayerInfo.SpellFreezeImmune = false;

        EffectPopUps.Instance.TurnOff(EffectPopUps.Instance.frozenHeartActive);
        EffectPopUps.Instance.frozenHeartActive = false;
    }

    /// Author: Chase O'Connor
    /// Date: 3/2/2021
    /// <summary> Makes the player unable to cast any spells for a certain time. </summary>
    /// <param name="duration">How long the player is unable to cast in seconds.</param>
    public IEnumerator SpellsFrozen(float duration)
    {
        EffectPopUps.Instance.TurnOn(3, EffectPopUps.Instance.frozenActive);
        EffectPopUps.Instance.frozenActive = true;

        PlayerInfo.SpellsFrozen = true;

        yield return new WaitForSeconds(duration);

        PlayerInfo.SpellsFrozen = false;

        EffectPopUps.Instance.TurnOff(EffectPopUps.Instance.frozenActive);
        EffectPopUps.Instance.frozenActive = false;
    }

    ///Author: Chase O'Connor
    ///Date: 3/2/2021
    /// <summary> Starts the bleed DOT. </summary>
    /// <param name="duration">How long in seconds does the bleed effect last for.</param>
    public IEnumerator Bleed(int duration)
    {
        EffectPopUps.Instance.TurnOn(0, EffectPopUps.Instance.bleedActive);
        EffectPopUps.Instance.bleedActive = true;

        for (int i = 0; i < duration; i++)
        {
            yield return new WaitForSeconds(1f);
            TakeDamage(1);
        }

        EffectPopUps.Instance.TurnOff(EffectPopUps.Instance.bleedActive);
        EffectPopUps.Instance.bleedActive = false;
    }
    #endregion
}