using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
public enum InteractionPlateType
{
    PlayerTakePlate,
    CounterTakePlateWithFood,
    CounterTakePlateNoFood,
    PlayerTakeFoodNoRequireCook,
    PlayerPutFoodOnPlate,
    NoAction
}
public enum InteractionType
{
    PlayerHavePlate,
    PlayerTakeFoodWhileHoldingPlate,
    PlayerTakeFood,
    CounterTakeFood,
    NoAction
}
public class StorageCounter : CounterBase
{
    [SerializeField] protected Vector3 offSetPutKitchenObjOnCounter= new Vector3(0f, 0.75f, 0f); 
    [SerializeField] protected Vector3 offSetPutPlateOnCounter = new Vector3(0f, 0.75f, 0f); 
    [SerializeField] protected Vector3 offSetPutFoodOnPlate = new Vector3(0f, 1f, 0f);
    [SerializeField] protected GameObject kitchenObjOnCounter;
    [SerializeField] protected GameObject plateOnCounter;
    
    protected CounterBase currentCounter;
    protected Sprite kitchenObjSprite;
    protected string kitchenObjName;

    public virtual void SetPlate(GameObject plateValue) {  plateOnCounter = plateValue;}
    public virtual GameObject GetPlate() { return plateOnCounter;}
    
    public virtual void SetkitchenObjOnCounter(GameObject kitchenObj){  this.kitchenObjOnCounter = kitchenObj;}
    public virtual GameObject GetkitchenObjCrateCounter() { return kitchenObjOnCounter; }
    protected virtual InteractionPlateType DetermineInteractionPlateType(GameObject plateOnPlayer, GameObject kitchenObjOnPlayer)
    {
        if (plateOnPlayer == null && this.plateOnCounter != null && kitchenObjOnPlayer == null)
        {
            return InteractionPlateType.PlayerTakePlate;
        }
        else if (kitchenObjOnPlayer != null && plateOnPlayer != null && this.plateOnCounter == null && kitchenObjOnCounter == null)
        {
            return InteractionPlateType.CounterTakePlateWithFood;
        }
        else if (plateOnPlayer != null && this.plateOnCounter == null && kitchenObjOnCounter == null && kitchenObjOnPlayer == null)
        {
            return InteractionPlateType.CounterTakePlateNoFood;
        }else if(plateOnPlayer != null && kitchenObjOnCounter != null && kitchenObjOnCounter.tag == "NotRequireCook")
        {
            return InteractionPlateType.PlayerTakeFoodNoRequireCook;
        }
        else if (this.plateOnCounter != null && kitchenObjOnPlayer != null && plateOnPlayer == null)
        {
            if(kitchenObjOnPlayer.tag == "NotRequireCook")
            {
                return InteractionPlateType.PlayerPutFoodOnPlate;
            }
            return InteractionPlateType.NoAction;
        }
        else
        {
            return InteractionPlateType.NoAction;
        }

    }
    protected virtual InteractionType DetermineInteractionType(GameObject kitchenObjOnPlayer, GameObject plateOnPlayer)
    {
        if (plateOnPlayer != null || this.plateOnCounter != null)
        {
            return InteractionType.PlayerHavePlate;
        }else if(kitchenObjOnPlayer == null && this.kitchenObjOnCounter != null)
        {
            return InteractionType.PlayerTakeFood;
        }else if(kitchenObjOnPlayer != null && this.kitchenObjOnCounter == null)
        {
            return InteractionType.CounterTakeFood;
        }

        return InteractionType.NoAction;

    }
    public virtual void InteractWithPlate(GameObject plateOnPlayer, GameObject kitchenObjOnPlayer)
    {
        InteractionPlateType interactionPlateType = DetermineInteractionPlateType(plateOnPlayer, kitchenObjOnPlayer);
        switch (interactionPlateType)
        {
            case InteractionPlateType.PlayerTakePlate:
                HandlePlayerTakePlate();
                break;

            case InteractionPlateType.CounterTakePlateWithFood:
                HandleCounterTakePlateWithFood(offSetPutPlateOnCounter);
                break;

            case InteractionPlateType.CounterTakePlateNoFood:
                HandleCounterTakePlate(offSetPutPlateOnCounter);
                break;
            case InteractionPlateType.PlayerTakeFoodNoRequireCook:
                HandlePlayerTakeFoodWhileHodingPlate(plateOnPlayer);
                break;
            case InteractionPlateType.PlayerPutFoodOnPlate:
                HandlePlayerPutFoodOnPlate(kitchenObjOnPlayer);
                break;

            case InteractionPlateType.NoAction:
                Debug.Log("No Action !");
                break;
        }
    }
    public override void Interact(GameObject kitchenObjOnPlayer, GameObject plateOnPlayer)
    { 
        var interactionType = DetermineInteractionType( kitchenObjOnPlayer,plateOnPlayer);

        switch (interactionType)
        {
            case InteractionType.PlayerHavePlate:
                {
                    InteractWithPlate(plateOnPlayer, kitchenObjOnPlayer);
                    return;
                }
            case InteractionType.PlayerTakeFood:
                {
                    HandlePlayerTakeFood(plateOnPlayer);
                    break;
                }
            case InteractionType.CounterTakeFood:
                {
                    HandleCounterTakeFood(offSetPutKitchenObjOnCounter);
                    break;
                }
            case InteractionType.NoAction:
                {
                    return;
                }
        }
    }
    protected virtual void HandlePlayerTakeFood(GameObject plateOnPlayer)
    {
        HandleBoxCollierKitchenObj(this.kitchenObjOnCounter);
        SetkitchenObjOnCounter(null);
    }
    protected virtual void HandlePlayerTakeFoodWhileHodingPlate(GameObject plateOnPlayer)
    {
        if (DeterminePlateTakeKitchenObj(this.kitchenObjOnCounter, plateOnPlayer))
        {
            HandleBoxCollierKitchenObjWhileHoldingPlate(this.kitchenObjOnCounter, handPlayer);
            SetkitchenObjOnCounter(null);
        }
    }
    protected virtual void HandlePlayerPutFoodOnPlate(GameObject kitchenObjOnPlayer)
    {
        if (DeterminePlateTakeKitchenObj(kitchenObjOnPlayer, this.plateOnCounter))
        {
            HandleBoxCollierKitchenObjWhileHoldingPlate(kitchenObjOnPlayer, this.transform.position + offSetPutFoodOnPlate, handPlayer);
            playerController.SetCurrentKitchenObj(null);
        }
    }
    protected virtual void HandlePlayerTakePlate()
    {
        if (HandleRemovePlate(this.plateOnCounter))
        {
            Debug.Log("(1): Player take plate ");
            SetPlate(null);
        };
    }
    protected virtual void HandleCounterTakePlateWithFood(Vector3 offSet)
    {
        HandleCounterTakePlate(offSet);
        playerController.SetCurrentKitchenObj(null);
    }
    protected virtual void HandleCounterTakePlate(Vector3 offSet)
    {
        var plate = playerController.GetCurrentPlate();

        plate.transform.parent = transform;
        plate.transform.position = transform.position + offSet;
        plate.transform.rotation = transform.rotation;

        OnBoxCollider(plate, true);
        OnFreezeKitchenObj(plate, false);

        SetPlate(plate);
        playerController.SetCurrentPlate(null);
    }
    protected virtual bool HandleRemovePlate(GameObject plate)
    {
        boxCollider = plate.GetComponent<BoxCollider>();
        boxCollider.enabled = false;

        plate.transform.position = handPlayer.position ;
        plate.transform.rotation = handPlayer.rotation;
        plate.transform.parent = handPlayer.parent;

        OnBoxCollider(plate, false);
        OnFreezeKitchenObj(plate, true);

        playerController.SetCurrentPlate(plate);
        return true;

    }
    protected virtual void HandleCounterTakeFood(Vector3 offSet)
    {
        var kitchenObj = playerController.GetCurrentKitchenObj();

        kitchenObj.transform.parent = transform;
        kitchenObj.transform.position = transform.position + offSet;
        kitchenObj.transform.rotation = transform.rotation;

        OnBoxCollider(kitchenObj, true);
        OnFreezeKitchenObj(kitchenObj, false); 

        SetkitchenObjOnCounter(kitchenObj);
        playerController.SetCurrentKitchenObj(null);
    }
    protected virtual void HandleBoxCollierKitchenObj(GameObject kitchenObj)
    {
        HandleBoxCollierKitchenObjWhileHoldingPlate(kitchenObj,handPlayer);
        kitchenObj.transform.parent = handPlayer.parent;
        playerController.SetCurrentKitchenObj(kitchenObj);
    }
    protected virtual void HandleBoxCollierKitchenObjWhileHoldingPlate(GameObject kitchenObj, Transform PutPosition)
    {
        boxCollider = kitchenObj.GetComponent<BoxCollider>();
        boxCollider.enabled = false;

        kitchenObj.transform.position = PutPosition.position;
        kitchenObj.transform.rotation = PutPosition.rotation;

        OnBoxCollider(kitchenObj, false);
        OnFreezeKitchenObj(kitchenObj, true);

    }
    protected virtual void HandleBoxCollierKitchenObjWhileHoldingPlate(GameObject kitchenObj, Vector3 PutPosition, Transform PutRotation)
    {
        boxCollider = kitchenObj.GetComponent<BoxCollider>();
        boxCollider.enabled = false;

        kitchenObj.transform.position = PutPosition;
        kitchenObj.transform.rotation = PutRotation.rotation;

        OnBoxCollider(kitchenObj, false);
        OnFreezeKitchenObj(kitchenObj, true);

    }
    protected virtual bool DeterminePlateTakeKitchenObj(GameObject kitchenObj, GameObject plate)
    {
        KitchenObjName foodInformation = kitchenObj.GetComponent<KitchenObjName>();

        if (plate != null && foodInformation != null)
        {
            kitchenObjName = foodInformation.Name;
            kitchenObjSprite = foodInformation.ImageKitchenObj;
            kitchenObj.transform.parent = plate.transform;
            if (plate.TryGetComponent(out PlateRecipe plateRecipe))
            {
                plateRecipe.HandleTakeFood(kitchenObjName, kitchenObjSprite, kitchenObj);
            }
            else
            {
                Debug.LogWarning("StoveCounter No Find plateRecipe !");
            }
        }
        else
        {
            Debug.LogWarning("StoveCounter No Find plate !");
        }
        return true;
    }

   
}
