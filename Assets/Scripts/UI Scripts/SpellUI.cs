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



    #region Booleans

    #endregion

    #region Integers
    [HideInInspector] public int listLocation;
    [HideInInspector] public int listSize;
    #endregion


    public Text spellText;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    #region Functions
    public void UpdateSpellUI(Spell spell)
    {
        if (spell is AttackSpell)
        {
            spellText.text = "Attack Spell";
        }
        else if (spell is BarrierBreakerSpell)
        {
            spellText.text = "Barrier Breaker Spell";

        }
        else if (spell is FreezeSpell)
        {
            spellText.text = "Freeze Spell";
        }
        else if (spell is ProtectionSpell)
        {
            spellText.text = "Protection Spell";
        }
        else if (spell is UnlockingSpell)
        {
            spellText.text = "Unlocking Spell";
        }
    }
    #endregion
}
