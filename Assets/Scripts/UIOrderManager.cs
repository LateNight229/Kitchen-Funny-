using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOrderManager : MonoBehaviour
{
    public List<TypeDishLevel1> dishOrdersUI = new List<TypeDishLevel1>();
      
    public List<GameObject> dishPrefabs = new List<GameObject>();

    public GameObject PanelUiOrderDish;

    public ObjectPool meatBugerPool;
    public ObjectPool bugerPool;
    public ObjectPool soupCarotPotatoPool;

    private void Start()
    {
        
    }

    public void HandleAddDish(TypeDishLevel1 currentDishsRequired)
    {
        if (UpdateDishOrdersUI(currentDishsRequired))
        {
            HandleDishOrdersUI(currentDishsRequired, true);
        }
    }
    public void HandleRemoveDish(TypeDishLevel1 currentDishsRequired)
    {
        HandleDishOrdersUI(currentDishsRequired, false);
    }
    private void HandleDishOrdersUI(TypeDishLevel1 currentDishsRequired, bool value)
    {
        if (value)
        {
            var objPool = HandleDetermineObjPoolByTypeDish(currentDishsRequired);
            SpawnDishUI( objPool, currentDishsRequired);
        }
        else
        {
            var objPool = HandleDetermineObjPoolByTypeDish(currentDishsRequired);
            var numberClear = ClearUIDishOrder(currentDishsRequired);
            if (numberClear >= 0)
            {
                ReturnPrefabForPool(dishPrefabs[numberClear], objPool);
                ClearDishOrderPrefab(numberClear);
            }
            else
            {
                Debug.LogWarning("numberClear < 0 : " + numberClear); 
            }
        }
    }
    private bool UpdateDishOrdersUI(TypeDishLevel1 currentDishsRequired)
    {
        dishOrdersUI.Add(currentDishsRequired);
        return true;
    }
    private ObjectPool HandleDetermineObjPoolByTypeDish(TypeDishLevel1 type)
    {   
        switch (type)
        {
            case TypeDishLevel1.meatBurger:
                return meatBugerPool;
            case TypeDishLevel1.SoupCarotPotato:
                return soupCarotPotatoPool;
            case TypeDishLevel1.burger:
                return bugerPool;
        }
        return null;
    }

    private void SpawnDishUI( ObjectPool objPool, TypeDishLevel1 currentDishsRequired)
    {
        GameObject dishOrderUIPrfab = objPool.GetPrefab();
        if ( dishOrderUIPrfab != null )
        {
            dishOrderUIPrfab.transform.parent = PanelUiOrderDish.transform;
            dishOrderUIPrfab.transform.localScale = Vector3.one;
            dishOrderUIPrfab.transform.localRotation = Quaternion.identity;

            TypeSingleUIDishs timeDeliveryManager = dishOrderUIPrfab.GetComponent<TypeSingleUIDishs>();
            timeDeliveryManager.typeSingleDishUI = currentDishsRequired; 
            dishPrefabs.Add(dishOrderUIPrfab);
        }
    }
    private int ClearUIDishOrder(TypeDishLevel1 currentDishsRequired)
    {
        for(int i = 0; i < dishOrdersUI.Count; i++)
        {
            if (dishOrdersUI[i] == currentDishsRequired )
            {
                dishOrdersUI.RemoveAt(i);
                return i;
            }
        }
        return -1;
    }
    private void ClearDishOrderPrefab(int numberRemove )
    {
        dishPrefabs.RemoveAt(numberRemove);
    }
    private void ReturnPrefabForPool(GameObject prefab, ObjectPool objPool)
    {
        var timeDelivery = prefab.GetComponent<TimeDeliveryManager>();
        if (timeDelivery != null)
        {
            timeDelivery.Timer = 0f;

        }
        else
        {
            Debug.LogWarning("timeDelivery = null");
        }
        objPool.ReturnPrefab(prefab); 
    }
}
