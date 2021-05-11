using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrewingOption : MonoBehaviour
{

    public Text potionName;
    public Text description;
    public Text requirements;
    public Image potionImage;
    public Button brewButton;

    public PotionRecipe recipe;

    public List<Image> ingredientSprites;
}
