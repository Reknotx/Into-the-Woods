using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct RecipeItem
{
    public Requirement requirement;
    
    [Range(1, 10)]
    public int Amount;
}

public enum Requirement
{
    IngredientA,
    IngredientB,
    IngredientC,
    IngredientD
}

public enum Result
{
    HealthPotion,
    SuperHealthPotion,
    DoubleDamagePotion,
    FrozenHeartPotion,
    InvisibilityPotion,
    NightWalkerPotion
}

[CreateAssetMenu]
public class PotionRecipe : ScriptableObject
{
    public List<RecipeItem> Requirements;
    public Result result;
    public Sprite potionSprite;
    public string description;


    public bool CanCraft(Inventory PInven)
    {
        foreach (RecipeItem recipeItem in Requirements)
        {
            if (PInven.IngredientCount(recipeItem.requirement) < recipeItem.Amount)
            {
                return false;
            }
        }
        return true;
    }

    public void Craft(Inventory PInven)
    {
        if (CanCraft(PInven))
        {
            foreach (RecipeItem recipeItem in Requirements)
            {
                for (int i = 0; i < recipeItem.Amount; i++)
                {
                    PInven.RemoveIngredient(recipeItem.requirement);
                }
            }

            PInven.AddPotion(MakePotion());
        }
    }


    public string GetName()
    {
        string name = "";
        switch (result)
        {
            case Result.HealthPotion:
                name = "Health Potion";
                break;
            
            case Result.SuperHealthPotion:
                name = "Super Health Potion";
                break;
            
            case Result.DoubleDamagePotion:
                name = "Double Damage Potion";
                break;
            
            case Result.FrozenHeartPotion:
                name = "Frozen Heart Potion";
                break;
            
            case Result.InvisibilityPotion:
                name = "Invisibility Potion";
                break;
            
            case Result.NightWalkerPotion:
                name = "Night Walker Potion";
                break;
        }

        return name;
    }

    public char GetIngredientInit(Requirement potion)
    {
        switch (potion)
        {
            case Requirement.IngredientA:
                return 'A';

            case Requirement.IngredientB:
                return 'B';

            case Requirement.IngredientC:
                return 'C';

            case Requirement.IngredientD:
                return 'D';

            default:
                return 'n';
        }
    }

    public Potion MakePotion()
    {
        Potion potion = null;

        switch (result)
        {
            case Result.HealthPotion:
                potion = new HealthPotion();
                break;

            case Result.SuperHealthPotion:
                potion = new SuperHealthPotion();
                break;

            case Result.DoubleDamagePotion:
                potion = new DoubleDamagePotion();
                break;

            case Result.FrozenHeartPotion:
                potion = new FrozenHeartPotion();
                break;

            case Result.InvisibilityPotion:
                potion = new InvisibilityPotion();
                break;

            case Result.NightWalkerPotion:
                potion = new NightWalkerPotion();
                break;
        }

        return potion;
    }

}
