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



    #region Integers
    [HideInInspector] public int listLocation;
    [HideInInspector] public int listSize;
    #endregion


    public Image imageRef;

    public List<Sprite> spriteList = new List<Sprite>();
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
            imageRef.sprite = spriteList[0];
        }
        else if (spell is BarrierBreakerSpell)
        {
            imageRef.sprite = spriteList[1];
        }
        else if (spell is FreezeSpell)
        {
            imageRef.sprite = spriteList[2];
        }
        else if (spell is ProtectionSpell)
        {
            imageRef.sprite = spriteList[3];
        }
        else if (spell is UnlockingSpell)
        {
            imageRef.sprite = spriteList[4];
        }
    }
    #endregion
}
