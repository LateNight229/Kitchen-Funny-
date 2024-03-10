using System.Collections;
using UnityEngine;
using DG.Tweening;

public class TrashCounter : StorageCounter
{   
    public Animator animator;

    private PlateRecipe plateRecipe;
    private bool canTakeFood ;
    private Vector3 scaleKitchenObj;

    protected override void Start()
    {
        base.Start();
        plateRecipe = FindFirstObjectByType<PlateRecipe>();
    }
    public virtual void SetTrueEventCanTakeFood()
    {
        canTakeFood = true;
    }
    public virtual void SetFalseEventCanTakeFood()
    {
        canTakeFood = false;
    }
    private void HandleAnimationOpenLid()
    {
        animator.SetTrigger("OpenLid");
    }
    protected override InteractionType DetermineInteractionType(GameObject kitchenObjOnPlayer, GameObject plateOnPlayer)
    {
        if (plateOnPlayer != null || this.plateOnCounter != null)
        {
            return InteractionType.PlayerHavePlate;
        }
        else if (kitchenObjOnPlayer != null && this.kitchenObjOnCounter == null)
        {
            return InteractionType.CounterTakeFood;
        }
        return InteractionType.NoAction;
    }
    public override void Interact(GameObject kitchenObjOnPlayer, GameObject plateOnPlayer)
    {
        var interactionType = DetermineInteractionType(kitchenObjOnPlayer, plateOnPlayer);

        switch (interactionType)
        {
            case InteractionType.PlayerHavePlate:
                {
                    HandleAnimationOpenLid();
                    plateOnCounter = plateOnPlayer;
                    InteractWithPlate(plateOnPlayer, kitchenObjOnPlayer);
                    return;
                }
            case InteractionType.CounterTakeFood:
                {
                    HandleAnimationOpenLid();
                    kitchenObjOnCounter = kitchenObjOnPlayer;
                    HandleCounterTakeFood(offSetPutFoodOnPlate);
                    return;
                }
           
            case InteractionType.NoAction:
                {
                    return;
                }
        }
    }

    public override void InteractWithPlate(GameObject plateOnPlayer, GameObject kitchenObjOnPlayer)
    {
        HandleCounterTakePlateWithFood(offSetPutPlateOnCounter);
    }
    protected override void HandleCounterTakePlateWithFood(Vector3 offSet)
    {
        //plateOnCounter.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.5f).OnComplete(() =>
        //{
        //    plateOnCounter.transform.DOScale(Vector3.one, 0f);
            playerController.HandleAfterDelivery();
        //});
    }
    protected override void HandleCounterTakeFood(Vector3 offSet)
    {
        //kitchenObjOnCounter.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.5f).OnComplete(() =>
        //{
        //    kitchenObjOnCounter.transform.DOScale(1f, 1f);
            HandleResetStateFood(kitchenObjOnCounter);
            kitchenObjOnCounter.transform.parent = null;
            kitchenObjOnCounter.SetActive(false);
            kitchenObjOnCounter = null;
            playerController.SetCurrentKitchenObj(null);
            Debug.Log("OnComplete");
      //  });
       

    }
    void CounterTakeFood()
    {
        kitchenObjOnCounter.transform.DOScale(scaleKitchenObj, 1f);
        HandleResetStateFood(kitchenObjOnCounter);
        kitchenObjOnCounter.transform.parent = null;
        kitchenObjOnCounter.SetActive(false);
        kitchenObjOnCounter = null;
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
}
