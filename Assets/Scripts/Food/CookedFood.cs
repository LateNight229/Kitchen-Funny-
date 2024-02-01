using UnityEngine;

    public enum StateCookedFood
    {
        idle,
        cooked,
        burn,
    }
public class CookedFood: MonoBehaviour
{
    public GameObject idleFood;
    public GameObject stewFood;
    public GameObject burnFood;
    private KitchenObjName kitchenObjName;

    public StateCookedFood currentState;

    private void Awake()
    {
        
    }
    private void Start()
    {  
    }
    public void SetState(StateCookedFood currentState)
    {
        this.currentState = currentState;
        SetStateFood(currentState);
    }
    void SetStateFood(StateCookedFood currentState)
    {
        switch (currentState)
        {
            case StateCookedFood.idle:
                idleFood.SetActive(true);
                burnFood.SetActive(false);
                stewFood.SetActive(false); 
                break;
            case StateCookedFood.cooked:
                idleFood.SetActive(false);
                burnFood.SetActive(false);
                stewFood.SetActive(true); 
                break;
            case StateCookedFood.burn:
                idleFood.SetActive(false);
                burnFood.SetActive(true);
                stewFood.SetActive(false); 
                break;
        }
    }
    public StateCookedFood GetState()
    {
        return currentState;
    }
}
