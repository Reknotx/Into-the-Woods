using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: JT Esmond
/// Date: 2/16/2021
/// <summary>
/// The class that contains references to all of the different item assets
/// </summary>
public class ItemAssets : MonoBehaviour
{

    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    //itemWorld reference
    public Transform itemWorldPF;

    // sprite references
    public Sprite ingredientASprite;
    public Sprite ingredientBSprite;
    public Sprite ingredientCSprite;
    public Sprite ingredientDSprite;
    public Sprite attackCandySprite;
    public Sprite avocadoSprite;
    public Sprite balloonBouquetSprite;
    public Sprite compassSprite;
    public Sprite luckyPennySprite;
    public Sprite nightOwlTokenSprite;
    public Sprite totemSprite;
    public Sprite twoPeasInPodSprite;

    //GameObject references, not sure if these are needed. This was something I added trying to see if I could get the 3D to work

    //public GameObject ingredientAObj;
    //public GameObject ingredientBObj;
    //public GameObject ingredientCObj;
    //public GameObject ingredientDObj;
    public GameObject attackCandyObj;
    public GameObject avocadoObj;
    //public GameObject balloonBouquetObj;
    public GameObject compassObj;
    //public GameObject luckyPennyObj;
    //public GameObject nightOwlTokenObj;
   // public GameObject totemObj;
    public GameObject twoPeasInPodDObj;

}
