using UnityEngine;

public enum StateProssecedFood
{
    idle,
    cutting,
    cutted,
    NoProsseced,
}

public class ProcessedFood : MonoBehaviour
{
    public GameObject Food;
    public GameObject CutFood;
    public GameObject CookedFood;

    [SerializeField] private bool processedDone;
    public StateProssecedFood stateProssecedFood;

    private void Start()
    {
        SetState(stateProssecedFood);
    }
    public void SetState(StateProssecedFood currentState)
    {   
        if(stateProssecedFood == StateProssecedFood.NoProsseced)
        {
            processedDone = true;
            return;
        }
        else
        {
            SetStateFood(currentState);
        }
    }
    void SetStateFood(StateProssecedFood currentState)
    {
        switch (currentState)
        {
            case StateProssecedFood.idle:
                CookedFood.SetActive(false);
                CutFood.SetActive(false);
                Food.SetActive(true);
                processedDone = false;
                break;
            case StateProssecedFood.cutting:
                CookedFood.SetActive(false);
                CutFood.SetActive(true);
                Food.SetActive(false);
                processedDone = false;
                break;
            case StateProssecedFood.cutted:
                CookedFood.SetActive(true);
                CutFood.SetActive(false);
                Food.SetActive(false);
                processedDone = true;
                break;
            case StateProssecedFood.NoProsseced:
                processedDone = true;
                break;
        }
    }
        public bool GetProcessedDone()
    {
        return processedDone;
    }

   
}
