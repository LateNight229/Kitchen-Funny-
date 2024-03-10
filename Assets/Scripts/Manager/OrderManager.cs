using System;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public List<TypeDishLevel1> allDishsRequired = new List<TypeDishLevel1>();

    public List<TypeDishLevel1> currentDishsRequiredList = new List<TypeDishLevel1>();

    public TypeDishLevel1 currentIngredientsFood;    

    [SerializeField] private int MaxCountInLevel = 20;
    [SerializeField] private float timeWaitSpawnOrder = 5f;
    [SerializeField] private bool outOfDishs;
    [SerializeField] private float coinFailedValue = 25f;
    [SerializeField] private float coinCorrectValue = 50f;

    private float orderTimer;
    private PlayerController playerController;
    private UIOrderManager _OrderManagerUI;
    private CoinManager coinManager;
    private TipsManager tipManager;
    private DeliveryCounterUI deliveryCounterUI;

    private void Start()
    {   
        LoadComponents(); 
        ModifierVariable();
        RandomAllOrderFoods();
    }
    void LoadComponents()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        _OrderManagerUI = FindFirstObjectByType<UIOrderManager>();
        coinManager = FindFirstObjectByType<CoinManager>();
        tipManager = FindFirstObjectByType<TipsManager>();
        deliveryCounterUI = FindFirstObjectByType<DeliveryCounterUI>();
    }
    void ModifierVariable()
    {
        orderTimer = timeWaitSpawnOrder - 8;
    }
    private void RandomAllOrderFoods()
    {

        for (int i = 0; i < MaxCountInLevel; i++)
        {
            var typeFood = GetRandomTypeFood();
            if (typeFood == TypeDishLevel1.Null) GetRandomTypeFood();
            else
            {
                allDishsRequired.Add(typeFood);
            }
        }
    }
    private void FixedUpdate()
    {
        if (outOfDishs) return;
        if (orderTimer >= timeWaitSpawnOrder && currentDishsRequiredList.Count < 4)
        {
            HandleSpawnOrderFoods();
            orderTimer = 0;
        }
        if (orderTimer < timeWaitSpawnOrder) orderTimer += Time.deltaTime;
    }
    private void HandleSpawnOrderFoods()
    {
        if(allDishsRequired.Count <= 0) { outOfDishs = true; return; }
        var FoodOrder = allDishsRequired[0];
        currentDishsRequiredList.Add(FoodOrder);
        allDishsRequired.Remove(FoodOrder);
        HandleNoftificationAddDishOrderUI(FoodOrder);
    }
    private TypeDishLevel1 GetRandomTypeFood()
    {
        TypeDishLevel1[] enumTypeFood = (TypeDishLevel1[])Enum.GetValues(typeof(TypeDishLevel1));

        var indexRadom = UnityEngine.Random.Range(0, enumTypeFood.Length);
        return enumTypeFood[indexRadom];
    }

    public void HandleTakeFood(TypeDishLevel1 typeFood)
    {
        currentIngredientsFood = typeFood;
       // Debug.Log("typeFood = " + typeFood.ToString());
        HandleCheckFood(typeFood);
        HandleAfterDeliveryFood();
    }
    private void HandleCheckFood(TypeDishLevel1 typeFood)
    {
        var dishOrderCorrect = currentDishsRequiredList.Contains(typeFood);
        if (dishOrderCorrect)
        {
            HandleOrderCorrect(typeFood);
        }
        else
        {
            HandleOrderFailed();
        }
    }
    private void HandleAfterDeliveryFood()
    {
        playerController.HandleAfterDelivery();
    }
    public void HandleOrderFailed(TypeDishLevel1 typeFood)
    {
        RemoveDishUiOrder(typeFood);
        coinManager.HandleMinusCoin(coinFailedValue);
        tipManager.HandleResetTips();
    }
    private void HandleOrderFailed()
    {
        HandleNoftificationSubmitOrderFailedUI();
        SoundManager.instance.HandlePlaySound("FailedDelivery", 1f);
        coinManager.HandleMinusCoin(coinFailedValue);
        tipManager.HandleResetTips();
    }
    private void HandleOrderCorrect(TypeDishLevel1 typeFood)
    {   
        RemoveDishUiOrder(typeFood);
        var tip = tipManager.TipsValue;
        SoundManager.instance.HandlePlaySound("CorrectDelivery", 1f);
        deliveryCounterUI.HandleShowCorrectText();
        coinManager.HandleAddCoin(coinCorrectValue, tip);
        tipManager.HandleAddTip();

    }
    private void RemoveDishUiOrder(TypeDishLevel1 typeFood)
    {
        currentDishsRequiredList.Remove(typeFood);
        HandleNoftificationRemoveDishOrderUI(typeFood);
    }
    private void HandleNoftificationAddDishOrderUI(TypeDishLevel1 currentDishAdd)
    {
        _OrderManagerUI.HandleAddDish(currentDishAdd);
    }
    private void HandleNoftificationRemoveDishOrderUI(TypeDishLevel1 currentDishAdd)
    {
        _OrderManagerUI.HandleRemoveDish(currentDishAdd);
    }
    private void HandleNoftificationSubmitOrderFailedUI()
    {
        _OrderManagerUI.HandleSubmitDishFailed();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            HandleOrderCorrect(TypeDishLevel1.meatBurger);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            HandleOrderFailed (TypeDishLevel1.meatBurger);
        }
    }
}
