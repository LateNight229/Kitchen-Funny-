using UnityEngine;

public class HandleCompleteDish : MonoBehaviour
{  
    public GameObject carotStewPlate;

    public void HandleDetermineDishType(TypeDishLevel1 type)
    {
        switch (type)
        {
            case TypeDishLevel1.SoupCarotPotato:
                carotStewPlate.SetActive(true);
                break;
            case TypeDishLevel1.burger:
                break;
            case TypeDishLevel1.meatBurger:
                break;
            case TypeDishLevel1.Null:
                carotStewPlate.SetActive(false); 
                break;
        }
    }
}
