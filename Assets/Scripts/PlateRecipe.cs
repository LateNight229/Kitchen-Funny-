using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateRecipe : MonoBehaviour
{   
    public List<string> ingredients = new List<string>();
    public List<Transform> images = new List<Transform>();
    public List<GameObject> ingredientObjs = new List<GameObject>();

    private TypeDishLevel1 currentTypeFood;
    private RecipeManager recipe;
    private HandleCompleteDish handleCompleteDish;
    private void Reset()
    {
        this.LoadComponent();
    }
    private void LoadComponent()
    {
        Transform myCanvas = transform.Find("Canvas");
        Transform parent = myCanvas.Find("Panel");
        foreach(Transform obj in parent)
        {
            this.images.Add(obj);
        }
    }
    private void Start()
    {
        recipe = FindObjectOfType<RecipeManager>();
        handleCompleteDish = FindObjectOfType<HandleCompleteDish>();
        HandleTurnOfAllImage();

    }
    private void HandleTurnOfAllImage()
    {
        for(int i = 0; i < images.Count; i++)
        {
            images[i].gameObject.SetActive(false);
        }
        
    }
    public void HandleTakeFood(string ingredientName, Sprite igredientSprite, GameObject ingredientObj)
    {
        Debug.LogWarning("igredientSprite = " + igredientSprite.ToString());
        Debug.LogWarning("ingredientName = " + ingredientName.ToString());
        AddIngredients(ingredientName);
        AddIngredientsObj(ingredientObj);
        ShowIconIngredient(igredientSprite);
        CheckRecipe();
    } 
    public void ShowIconIngredient(Sprite igredientSprite)
    {
        for(int i = 0;i < ingredients.Count;i++)
        {
            if (!images[i].gameObject.activeSelf)
            {
                images[i].gameObject.SetActive(true);
                images[i].GetComponent<PlateSingleUI>().SetupUI(igredientSprite);
                return;
            }
        }
    }
    public void SetDefaultIconIngredient()
    {
        for (int i = 0; i < ingredients.Count; i++)
        {
            images[i].gameObject.SetActive(false);
            images[i].GetComponent<PlateSingleUI>().SetDefaultUI();
        }
       // Debug.Log("SetDefaultIconIngrediented " );
    }
    public void HandleRemoveAllFoodOnPlate()
    {
        for (int i = 0; i < ingredientObjs.Count; i++)
        {
            HandleResetStateFood(ingredientObjs[i]);
            ingredientObjs[i].transform.parent = null;
            ingredientObjs[i].SetActive(false);
           
        }
    }
    private void CheckRecipe()
    {
        currentTypeFood = recipe.FindRecipe(ingredients);
        Debug.Log("currentTypeFood = " + currentTypeFood);
        handleCompleteDish.HandleDetermineDishType(currentTypeFood);
    }
    public TypeDishLevel1 GetCurrentTypeFood()
    {
        return currentTypeFood;
    }
    public void HandleClearAllIngredientsOnPlate()
    {
        HandleRemoveAllFoodOnPlate();
        SetDefaultIconIngredient();
        RemoveAllIngredientsObj();
        RemoveAllIngredients();
    }
    private void HandleResetStateFood(GameObject prefab)
    {
        var cooked = prefab.GetComponent<CookedFood>();
        if (cooked != null)
        {
            cooked.SetState(StateCookedFood.idle);
        }
        var prosseced = prefab.GetComponent<ProcessedFood>();
        if (prosseced != null)
        {
            prosseced.SetState(StateProssecedFood.idle);
        }
    }
    private void AddIngredientsObj(GameObject ingredientObj)
    {
        ingredientObjs.Add(ingredientObj);
    }
    private void AddIngredients(string ingredientName)
    {
        ingredients.Add(ingredientName);
    }
    private void RemoveAllIngredientsObj()
    {
        ingredientObjs.Clear();
    }
    private void RemoveAllIngredients()
    {
        ingredients.Clear();
    }
}
