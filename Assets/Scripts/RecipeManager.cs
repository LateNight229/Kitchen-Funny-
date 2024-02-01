using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    private Dictionary<TypeDishLevel1, List<string>> recipes = new Dictionary<TypeDishLevel1, List<string>>();
    private HandleCompleteDish setFood;

    private void Awake()
    {
        HandleAddRecipe(TypeDishLevel1.SoupCarotPotato, new List<string> { "carot", "potato" });
       // HandleAddRecipe(TypeDishLevel1.carotStew, new List<string> { "carot" });
      //  HandleAddRecipe(TypeDishLevel1.potatonStew, new List<string> { "potato" });
        HandleAddRecipe(TypeDishLevel1.burger, new List<string> { "burger" });
        HandleAddRecipe(TypeDishLevel1.meatBurger, new List<string> { "burger", "meat" });

    }

    public List<string> GetRecipeIngredients(TypeDishLevel1 type)
    {
        if (recipes.ContainsKey(type))
        {
            return recipes[type];
        }

        Debug.LogWarning("Không tìm thấy công thức nấu ăn: " + type);
        return new List<string>(); 
    }
    private void Start()
    {
        setFood = FindObjectOfType<HandleCompleteDish>();
    }
    public TypeDishLevel1 FindRecipe(List<string> targetIngredients)
    {
        foreach (var recipe in recipes)
        {
            if (ListContainsSameElements(recipe.Value, targetIngredients))
            {
                return recipe.Key;
            }
        }

        return TypeDishLevel1.Null;
    }

    private bool ListContainsSameElements(List<string> listA, List<string> listB)
    {
        return listA.Count == listB.Count && listA.All(listB.Contains);
    }

    private void HandleAddRecipe(TypeDishLevel1 type, List<string> ingredients)
    {
        if (!recipes.ContainsKey(type))
        {
            recipes.Add(type, ingredients);
        }
        else
        {
            Debug.LogWarning("Công thức nấu ăn '" + type + "' đã tồn tại.");
        }
    }
}
