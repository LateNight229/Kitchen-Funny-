using System;
using UnityEngine;

public class PlateCounter : CounterBase
{
    public static event Action HandleEventTakePlate;
    public static event Action HandleEventGetPlate;

    private PlateRecipe plateRecipe;

    public override void Interact(GameObject kitchenObj, GameObject plate)
    {
        HandleRemovePlate(kitchenObj, plate);
    }
    public virtual void HandleRemovePlate(GameObject kitchenObjOnPlayer, GameObject plate)
    {
        if (kitchenObjOnPlayer != null || plate != null) return;
        var currentPlate = pool.GetPrefab();
        if (currentPlate != null)
        {
            if (HandleEventTakePlate != null) HandleEventTakePlate();
            currentPlate.transform.position = handPlayer.position;
            currentPlate.transform.rotation = handPlayer.rotation;
            currentPlate.transform.parent = handPlayer.parent;

            OnFreezeKitchenObj(currentPlate, true);
            OnBoxCollider(currentPlate, false);

            playerController.SetCurrentPlate(currentPlate);
            var currentPlateRecipe = currentPlate.GetComponent<PlateRecipe>();
            if (currentPlateRecipe != null)
            {
                currentPlateRecipe.SetDefaultIconIngredient();
            }
            playerController.SetlastCounter(this);
        }
    }
    public virtual void HandlePlateAfterDelivery(GameObject prefab)
    {
        HandleCleanPlate(prefab);
        pool.ReturnPrefab(prefab);
    }
    protected override void Start()
    {
        base.Start();
    }
    public virtual void HandleCleanPlate(GameObject prefab)
    {
        if (HandleEventGetPlate != null) HandleEventGetPlate();
        var handleCompleteDish = prefab.GetComponentInChildren<HandleCompleteDish>();
        if (handleCompleteDish != null)
        {
            handleCompleteDish.HandleDetermineDishType(TypeDishLevel1.Null);
        }
        var plateRecipe = prefab.GetComponent<PlateRecipe>();
        if (plateRecipe != null)
        {
            plateRecipe.HandleClearAllIngredientsOnPlate();
        }
    }
   
}
