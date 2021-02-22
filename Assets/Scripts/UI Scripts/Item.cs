using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// Author: JT Esmond
/// Date: 2/16/2021
/// <summary>
/// All of the variables that pertain to the different items
/// </summary>
[Serializable]
public class Item 
{
    //enum for the different ItemTypes
    public enum ItemType
    {
        ingredientA,
        ingredientB,
        ingredientC,
        ingredientD,
        attackCandy,
        avocado,
        balloonBouquet,
        compass,
        luckyPenny,
        nightOwlToken,
        totem,
        twoPeasInPod
    }
    public ItemType itemType;
    public int amount;

    //not sure if this one is needed. this is just me trying to make the system work in 3D
    public GameObject GetGameObject()
    {
        switch (itemType)
        {
            default:
            //case ItemType.ingredientA: return ItemAssets.Instance.ingredientAObj;
            //case ItemType.ingredientB: return ItemAssets.Instance.ingredientBObj;
            //case ItemType.ingredientC: return ItemAssets.Instance.ingredientCObj;
            //case ItemType.ingredientD: return ItemAssets.Instance.ingredientDObj;
            case ItemType.attackCandy: return ItemAssets.Instance.attackCandyObj;
            case ItemType.avocado: return ItemAssets.Instance.avocadoObj;
            //case ItemType.balloonBouquet: return ItemAssets.Instance.balloonBouquetObj;
            case ItemType.compass: return ItemAssets.Instance.compassObj;
            //case ItemType.luckyPenny: return ItemAssets.Instance.luckyPennyObj;
            //case ItemType.nightOwlToken: return ItemAssets.Instance.nightOwlTokenObj;
            //case ItemType.totem: return ItemAssets.Instance.totemObj;
            case ItemType.twoPeasInPod: return ItemAssets.Instance.twoPeasInPodDObj;
        }
    }

    //Sprite that  returns the different Sprite assets depending on the item type
    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.ingredientA: return ItemAssets.Instance.ingredientASprite;
            case ItemType.ingredientB: return ItemAssets.Instance.ingredientBSprite;
            case ItemType.ingredientC: return ItemAssets.Instance.ingredientCSprite;
            case ItemType.ingredientD: return ItemAssets.Instance.ingredientDSprite;
            case ItemType.attackCandy: return ItemAssets.Instance.attackCandySprite;
            case ItemType.avocado: return ItemAssets.Instance.avocadoSprite;
            case ItemType.balloonBouquet: return ItemAssets.Instance.balloonBouquetSprite;
            case ItemType.compass: return ItemAssets.Instance.compassSprite;
            case ItemType.luckyPenny: return ItemAssets.Instance.luckyPennySprite;
            case ItemType.nightOwlToken: return ItemAssets.Instance.nightOwlTokenSprite;
            case ItemType.totem: return ItemAssets.Instance.totemSprite;
            case ItemType.twoPeasInPod: return ItemAssets.Instance.twoPeasInPodSprite;
        }
    }

    //bool that returns true if the ItemType is stackable, and false if it is not
    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.ingredientA:
            case ItemType.ingredientB:
            case ItemType.ingredientC:
            case ItemType.ingredientD:
                return true;
            case ItemType.attackCandy:
            case ItemType.avocado:
            case ItemType.balloonBouquet:
            case ItemType.compass:
            case ItemType.luckyPenny:
            case ItemType.nightOwlToken:
            case ItemType.totem:
            case ItemType.twoPeasInPod:
                return false;
        }
    }
}
