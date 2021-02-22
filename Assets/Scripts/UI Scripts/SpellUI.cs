using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// Author: JT Esmond
/// Date: 2/8/2021
/// <summary>
/// The class that handles the selected spell UI elements
/// </summary>

public class SpellUI : MonoBehaviour
{
    #region Singleton
    public static SpellUI Instance;
    #endregion

    #region lists
    /// <summary> List that contains all of the spell UI images </summary>
    private List<GameObject> _backgroundSpellList = new List<GameObject>();
    [HideInInspector] public List<GameObject> mainSpellList = new List<GameObject>();
    #endregion

    #region Booleans
    /// ** Change booleans to true when the player picks up the coresponding scroll**
    [HideInInspector] public bool hasBarrierBreakerSpell;
    [HideInInspector] public bool hasFreezeSpell;
    [HideInInspector] public bool hasProtectionSpell;
    [HideInInspector] public bool hasUnlockingSpell;

    /// <summary> used in the updateSpellList function to stop the list from adding
    /// every time you pick up a scroll </summary>
    private bool attackSpellAdded;
    private bool barrierBreakerSpellAdded;
    private bool freezeSpellAdded;
    private bool protectionSpellAdded;
    private bool unlockingSpellAdded;
    #endregion

    #region Integers
    [HideInInspector] public int listLocation;
    [HideInInspector] public int listSize;
    #endregion

    #region GameObjects
    public GameObject selectedSpell;
    #endregion

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform spell in selectedSpell.transform)
        {
            if (spell.gameObject.CompareTag("Spell"))
            {
                _backgroundSpellList.Add(spell.gameObject);
            }
        }

        listLocation = 0;
        UpdateSpellList();
        SpellOn();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //temporary keybinds to test the UI
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) //scoll forward
        {
            SpellOff();
            listLocation++;
            if (listLocation == listSize +1) listLocation = 0;
            SpellOn();
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) //scroll backward
        {
            SpellOff();
            listLocation--;
            if (listLocation < 0) listLocation = listSize;
            SpellOn();
        }

        if (Input.GetKeyDown("l"))
        {
            hasBarrierBreakerSpell = true;
            UpdateSpellList();
            Debug.Log("has barrier breaker Spell");
        }
        if (Input.GetKeyDown("k"))
        {
            hasFreezeSpell = true;
            UpdateSpellList();
            Debug.Log("has freeze Spell");
        }
        if (Input.GetKeyDown("j"))
        {
            hasProtectionSpell = true;
            UpdateSpellList();
            Debug.Log("has protection Spell");
        }
        if (Input.GetKeyDown("h"))
        {
            hasUnlockingSpell = true;
            UpdateSpellList();
            Debug.Log("has unlocking Spell");
        }
        */
    }

    #region Functions

    #region SpellOn
    /// Author: JT Esmond
    /// Date: 2/8/2021
    /// <summary>
    /// accesses the main spell list and sets the UI element to true located 
    /// at the index point equal to the listLocation integer
    /// </summary>
    /// **Call when the player changes spell, but after the SpellOff()
    /// and after the listLocation is incremented or decremented**
    public void SpellOn()
    {
        mainSpellList[listLocation].SetActive(true);
    }
    #endregion

    #region SpellOff
    /// Author: JT Esmond
    /// Date: 2/8/2021
    /// <summary>
    /// Accesses the main spell list and sets the UI element to false located
    /// at the index point equal to the listLocation integer
    /// </summary>
    /// **Call 1st when the player changes spells
    public void SpellOff()
    {
        mainSpellList[listLocation].SetActive(false);
    }
    #endregion

    #region UpdateSpellList
    /// Author: JT Esmond
    /// Date: 2/8/2021
    /// <summary>
    /// This function has a series of conditionals that check if the player has
    /// access to a spell, and hasn't added that spell to the main spell list
    /// it then adds them to the main spell list from the background spell list 
    /// that contains all of the UI gameObjects
    /// </summary>
    /// **call when the player learns a new spell**
    public void UpdateSpellList()
    {

        ///adds the basic attack spell to from the background spell list to the main spell list
        if (attackSpellAdded == false)
        {
            mainSpellList.Add(_backgroundSpellList[0].gameObject);
            attackSpellAdded = true;
        }

        ///if the player has the barrier breaker spell, adds the spell to the main spell list
        ///from the background spell list that holds all of the UI elements
        if (hasBarrierBreakerSpell == true && barrierBreakerSpellAdded == false)
        {
            listSize++;
            mainSpellList.Add(_backgroundSpellList[1].gameObject);
            barrierBreakerSpellAdded = true;
        }

        ///if the player has the freeze spell, adds the spell to the main spell list
        ///from the background spell list that holds all of the UI elements
        if (hasFreezeSpell == true && freezeSpellAdded == false)
        {
            listSize++;
            mainSpellList.Add(_backgroundSpellList[2].gameObject);
            freezeSpellAdded = true;
        }

        ///if the player has the protection spell, adds the spell to the main spell list
        ///from the background spell list that holds all of the UI elements
        if (hasProtectionSpell == true && protectionSpellAdded == false)
        {
            listSize++;
            mainSpellList.Add(_backgroundSpellList[3].gameObject);
            protectionSpellAdded = true;
        }

        ///if the player has the unlocking spell, adds the spell to the main spell list
        ///from the background spell list that holds all of the UI elements
        if (hasUnlockingSpell == true && unlockingSpellAdded == false)
        {
            listSize++;
            mainSpellList.Add(_backgroundSpellList[4].gameObject);
            unlockingSpellAdded = true;
        }
    }
    #endregion

    #endregion
}
