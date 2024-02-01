using UnityEngine;

public class TimeDeliveryManager : TimerBase
{   
    private OrderManager orderManager;

    private TypeSingleUIDishs typeSingleDish;
    protected override void Start()
    {   
        base.Start();
        orderManager = GameObject.FindObjectOfType<OrderManager>();
        typeSingleDish = GetComponent<TypeSingleUIDishs>();
    }
    protected override void HandleEndTime()
    {
        HandleFailedDelivery();
    }
    
    private void HandleFailedDelivery()
    {
        var typeOfOrderUI = typeSingleDish.typeSingleDishUI;
        orderManager.HandleOrderFailed(typeOfOrderUI);
    }
}
