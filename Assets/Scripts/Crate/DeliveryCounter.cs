using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : CounterBase
{
    private OrderManager orderManager;

    protected override void Start()
    {
        base.Start();
        orderManager = GameObject.FindObjectOfType<OrderManager>();
    }

    public override void Interact(GameObject kitchenObj, GameObject plate)
    {
        if (plate == null) return;
        var plateRecipe = plate.GetComponent<PlateRecipe>();
        var food = plateRecipe.GetCurrentTypeFood();
        HandleSendFoodToOrderManager(food);
    }

    private void HandleSendFoodToOrderManager(TypeDishLevel1 typeFood)
    {
        orderManager.HandleTakeFood(typeFood);
    }

}
