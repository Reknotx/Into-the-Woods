using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// Author: Chase O'Connor
/// Date: 2/18/2021
/// <summary>
/// 
/// </summary>
public class BrewingSystem : SingletonPattern<BrewingSystem>
{
    public List<PotionRecipe> recipes;

    /// <summary> The brewing option prefab. </summary>
    public GameObject brewingOptionPrefab;

    /// <summary> The list of all brewing options in the brewing system. </summary>
    private List<GameObject> brewingOptions = new List<GameObject>();

    private int _brewingIndex = 0;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        foreach (PotionRecipe recipe in recipes)
        {
            AddRecipe(recipe);
        }

        brewingOptions[_brewingIndex].SetActive(true);

        gameObject.SetActive(false);


    }

    /// <summary>
    /// Adds a new recipe to the brewing system that we can craft.
    /// </summary>
    /// <param name="recipe">The recipe that has been unlocked.</param>
    public void AddRecipe(PotionRecipe recipe)
    {
        BrewingOption tempOption;

        tempOption = Instantiate(brewingOptionPrefab,
                                    transform.GetChild(0)).GetComponent<BrewingOption>();
            
        tempOption.recipe = recipe;
        tempOption.potionName.text = recipe.GetName().ToString();
        tempOption.description.text = recipe.description.ToString();

        string reqText;

        if (recipe.Requirements.Count == 1)
        {
            reqText = "Requires " + recipe.Requirements[0].Amount + " ingredient " + recipe.GetIngredientInit(recipe.Requirements[0].requirement);
        }
        else
        {
            reqText = "Requires ingredients ";

            for (int i = 0; i < recipe.Requirements.Count; i++)
            {
                RecipeItem requirement = recipe.Requirements[i];

                reqText += recipe.GetIngredientInit(requirement.requirement);

                if (i < recipe.Requirements.Count - 1)
                {
                    reqText += " & ";
                }
            }
        }

        tempOption.requirements.text = reqText;
        tempOption.potionImage.sprite = recipe.potionSprite;

        tempOption.brewButton.onClick.AddListener(() => recipe.Craft(Player.Instance.PInven));

        tempOption.gameObject.SetActive(false);
        brewingOptions.Add(tempOption.gameObject);
    }

    /// <summary>
    /// Moves to the next brew option iwth the right button.
    /// </summary>
    public void NextOption()
    {
        brewingOptions[_brewingIndex].SetActive(false);
        _brewingIndex++;
        if (_brewingIndex >= brewingOptions.Count) _brewingIndex = 0;
        brewingOptions[_brewingIndex].SetActive(true);
    }

    /// <summary>
    /// Moves to the previous brew option with the left arrow.
    /// </summary>
    public void LastOption()
    {
        brewingOptions[_brewingIndex].SetActive(false);
        _brewingIndex--;
        if (_brewingIndex < 0) _brewingIndex = brewingOptions.Count - 1;
        brewingOptions[_brewingIndex].SetActive(true);
    }

}
