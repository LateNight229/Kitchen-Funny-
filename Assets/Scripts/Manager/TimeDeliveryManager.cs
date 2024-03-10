using UnityEngine;

public class TimeDeliveryManager : TimerBase
{
    [SerializeField] private float timeShowVisualCorrect = 0.5f;

    private OrderManager orderManager;
    private VisualUIOrder visualUIOrder;

    private TypeSingleUIDishs typeSingleDish;
    protected override void Start()
    {   
        base.Start();
        orderManager = FindFirstObjectByType<OrderManager>();
        typeSingleDish = GetComponent<TypeSingleUIDishs>();
        visualUIOrder = GetComponent<VisualUIOrder>();
    }
    public override void HandleEndTime()
    {
        HandleFailedDelivery();
    }
    
    private void HandleFailedDelivery()
    {
        var typeOfOrderUI = typeSingleDish.typeSingleDishUI;
        orderManager.HandleOrderFailed(typeOfOrderUI);

        visualUIOrder.HandleVisualUIOrder(false, timeShowVisualCorrect);
    }
}
