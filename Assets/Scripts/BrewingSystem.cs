using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// Author: Chase O'Connor
/// Date: 2/18/2021
/// <summary>
/// 
/// </summary>
public class BrewingSystem : MonoBehaviour
{
    public List<PotionRecipe> recipes;

    /// <summary> The brewing option prefab. </summary>
    public GameObject brewingOptionPrefab;

    /// <summary> The list of all brewing options in the brewing system. </summary>
    private List<GameObject> brewingOptions = new List<GameObject>();

    private void Start()
    {
        BrewingOption tempOption;

        foreach (PotionRecipe recipe in recipes)
        {
            tempOption = Instantiate(brewingOptionPrefab, transform.GetChild(0)).GetComponent<BrewingOption>();
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

        brewingOptions[0].SetActive(true);
    }
}
