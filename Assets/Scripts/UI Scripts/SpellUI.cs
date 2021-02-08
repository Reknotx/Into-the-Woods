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
    #region Fields
    public static SpellUI Instance;
    #endregion

    #region lists
    /// <summary> List that contains all of the spell UI images </summary>
    private List<GameObject> _spellList = new List<GameObject>();
    #endregion

    #region Booleans
    /// ** Change booleans to true when the player picks up the coresponding scroll**
    [HideInInspector] public bool hasBarrierBreakerSpell;
    [HideInInspector] public bool hasFreezeSpell;
    [HideInInspector] public bool hasProtectionSpell;
    [HideInInspector] public bool hasUnlockingSpell;

    /// ** Change booleans around to match which spell the player has selected
    [HideInInspector] public bool attackSpellSelected;
    [HideInInspector] public bool barrierBreakerSpellSelected;
    [HideInInspector] public bool freezeSpellSelected;
    [HideInInspector] public bool protectionSpellSelected;
    [HideInInspector] public bool unlockingSpellSelected;
    #endregion

    #region Integers
    [HideInInspector] public int listLocation;
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
                _spellList.Add(spell.gameObject);
            }
        }

        listLocation = 0;
        UpdateSelectedSpell();

    }

    // Update is called once per frame
    void Update()
    {
        /*
        //temporary keybinds to test the UI
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) //scoll forward
        {
            if (listLocation < 4)
            {
                listLocation++;
                UpdateSelectedSpell();
            }
            else
            {
                listLocation = 0;
                UpdateSelectedSpell();
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) //scroll backward
        {
            if (listLocation > 0)
            {
                listLocation--;
                UpdateSelectedSpell();
            }
            else
            {
                listLocation = 4;
                UpdateSelectedSpell();
            }
        }

        if (Input.GetKeyDown("l"))
        {
            hasBarrierBreakerSpell = true;
            Debug.Log("has barrier breaker Spell");
        }
        if (Input.GetKeyDown("k"))
        {
            hasFreezeSpell = true;
            Debug.Log("has freeze Spell");
        }
        if (Input.GetKeyDown("j"))
        {
            hasProtectionSpell = true;
            Debug.Log("has protection Spell");
        }
        if (Input.GetKeyDown("h"))
        {
            hasUnlockingSpell = true;
            Debug.Log("has unlocking Spell");
        }
        */
    }

    #region Functions
    /// <summary>
    /// Function that runs all of the Spell selection UI
    /// **Call when the player changes Spells**
    /// </summary>
    public void UpdateSelectedSpell()
    {
        /// series of conditionals that check where in the spell list the player is at
        /// also checks if they have that spell, if they do it displays selected spell
        if (listLocation == 0)
        {
            _spellList[0].SetActive(true);
            for (int i = 1; i <= 4; i++)
            {
                _spellList[i].SetActive(false);
            }
        }
        else if (listLocation == 1 && hasBarrierBreakerSpell == true)
        {
            _spellList[0].SetActive(false);
            _spellList[1].SetActive(true);
            for (int i = 2; i <= 4; i++)
            {
                _spellList[i].SetActive(false);
            }
        }
        else if (listLocation == 2 && hasFreezeSpell == true)
        {
            _spellList[0].SetActive(false);
            _spellList[1].SetActive(false);
            _spellList[2].SetActive(true);
            _spellList[3].SetActive(false);
            _spellList[4].SetActive(false);
        }
        else if (listLocation == 3 && hasProtectionSpell == true)
        {
            for (int i = 0; i <= 2; i++)
            {
                _spellList[i].SetActive(false);
            }
            _spellList[3].SetActive(true);
            _spellList[4].SetActive(false);
        }
        else if(listLocation == 4 && hasUnlockingSpell == true)
        {
            for (int i = 0; i <= 3; i++)
            {
                _spellList[i].SetActive(false);
            }
            _spellList[4].SetActive(true);
        }
    }
    #endregion
}
