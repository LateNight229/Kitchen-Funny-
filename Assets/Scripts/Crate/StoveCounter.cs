using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StoveCounter : StorageCounter
{
    [SerializeField] private float timeCooking = 10f;
    [SerializeField] private float timeBurn = 10f;
    [SerializeField] private GameObject effectCooking;

    public StateCookedFood currentStateFoodOnPlayer;
    private CookedFood food;
    private StoveUI stoveUI;

    private float fryingTimer;
    private bool isCooking, isStew, isBurn, isPlaySound;
    private Vector3 offSetFoodOnPlate = new Vector3(0f, 0.16f, 0f);


    public override void PauseStew()
    {
        //isStew = true;
    }
    public override void ResumeStew()
    {
       // isStew = false;
    }
    public void PauseBurn()
    {
        isBurn = true;
    }
    public void ResumeBurn()
    {
        isBurn = false;
    }
    protected override void Start()
    {
        base.Start();
        ModifierOffSet();
    }
    void ModifierOffSet()
    {   
        stoveUI = GetComponent<StoveUI>();  
        //PauseStew();
        isStew = true;
        PauseBurn();
        fryingTimer = 0;
        offSetPutKitchenObjOnCounter =new Vector3(0f, 1.5f, 0f);
    }
    public override void Interact(GameObject kitchenObjOnPlayer, GameObject plateOnPlayer)
    {
        if (kitchenObjOnPlayer == null)
        {
            if (isCooking) return;
            HandlePlayerTakeFood(kitchenObjOnPlayer, plateOnPlayer);
        }
        else
        {
            if (kitchenObjOnPlayer.TryGetComponent(out ProcessedFood food))
            {
                if (food.GetProcessedDone())
                {
                    HandleCounterTakeFood(kitchenObjOnPlayer, plateOnPlayer);
                }
            }
        }
    }
    public virtual void HandleCounterTakeFood(GameObject kitchenObjOnPlayer, GameObject plateOnPlayer)
    {
        if (kitchenObjOnPlayer != null && kitchenObjOnCounter == null)
        {
             food = GetCookedFood(kitchenObjOnPlayer);
            currentStateFoodOnPlayer = food.GetState();
            
            switch (currentStateFoodOnPlayer)
            {
                case StateCookedFood.burn:
                    return;
                case StateCookedFood.cooked:
                    fryingTimer = 0;
                    HandleCounterTakeFood(offSetPutKitchenObjOnCounter);
                    ResumeBurn();
                    break;
                case StateCookedFood.idle:
                    fryingTimer = 0;
                 //   Debug.Log("Stove counter was taked food");
                    HandleCounterTakeFood(offSetPutKitchenObjOnCounter);
                    // ResumeStew(); break;
                    isStew = false; break;
            }
            OnEffectCooking(true);
        }
    }
    private void HandlePlayerTakeFood(GameObject kitchenObjOnPlayer, GameObject plateOnPlayer)
    {
        if (kitchenObjOnPlayer == null && this.kitchenObjOnCounter == null)
        {
            Debug.Log("not kitchenObj Available !");
            return;
        }
        else if (kitchenObjOnPlayer == null && this.kitchenObjOnCounter != null)
        {
            currentStateFoodOnPlayer = food.GetState();
            plateOnPlayer = playerController.GetCurrentPlate();
            var determineIntecractiontype = DetermineInteractionType(kitchenObjOnPlayer, plateOnPlayer);
            switch (determineIntecractiontype)
            {
                case InteractionType.NoAction: return;
                case InteractionType.PlayerTakeFoodWhileHoldingPlate:
                    HandlePlayerTakeFoodWhileHodingPlate(plateOnPlayer);
                    break;
                case InteractionType.PlayerTakeFood:
                    HandlePlayerTakeFood(plateOnPlayer); 
                    break;
            }
        }
    }
    protected override InteractionType DetermineInteractionType(GameObject kitchenObjOnPlayer, GameObject plateOnPlayer)
    {
        if (plateOnPlayer == null && currentStateFoodOnPlayer != StateCookedFood.burn) 
            return InteractionType.NoAction;
        else if (currentStateFoodOnPlayer == StateCookedFood.burn && plateOnPlayer == null)
            return InteractionType.PlayerTakeFood; // take kitchenObj was Burned
        else if (plateOnPlayer != null && currentStateFoodOnPlayer != StateCookedFood.burn)
            return InteractionType.PlayerTakeFoodWhileHoldingPlate;
        return InteractionType.NoAction;
    }
    private void Update()
    {
        if (fryingTimer >= timeCooking && isStew == false)
        {   
            //stew done
            HandleWhenCookingDone();
        }
        if (fryingTimer >= timeBurn)
        {   
            // burn done
            if (isBurn == false)
            {
                StopSoundStew();
                food.SetState(StateCookedFood.burn);
                isBurn = true;
                HandleTurnOffDangerIcon();
            }
        }
        if (fryingTimer < timeCooking && isStew == false)
        {   
            //is stewing
            fryingTimer += Time.deltaTime;
            HandleWhenCooking();
        }
        if (fryingTimer < timeBurn)
        {   
            //Start Stew
            if (isBurn) return;
            fryingTimer += Time.deltaTime;
        }
    }
    private void PlaySoundStew()
    {   
        if(isPlaySound) { return; }
        SoundManager.instance.HandlePlaySound("stew",0.1f);
        isPlaySound = true;
    }
    private void StopSoundStew()
    {
        if (!isPlaySound) { return; }
        SoundManager.instance.HandleStopSound("stew");
        isPlaySound = false;
    }
    private CookedFood GetCookedFood(GameObject kitchenObj)
    {
        if (kitchenObj.TryGetComponent(out CookedFood food))
        {
            if (food != null)
            {
                return food;
            }
            else return null;
        }
        else return null;
    }

    protected override void HandlePlayerTakeFoodWhileHodingPlate(GameObject plateOnPlayer)
    {
        if (DeterminePlateTakeKitchenObj(this.kitchenObjOnCounter, plateOnPlayer))
        {
            HandleBoxCollierKitchenObjWhileHoldingPlate(this.kitchenObjOnCounter,handPlayer.position + offSetFoodOnPlate, handPlayer);
            SetkitchenObjOnCounter(null);
            PauseBurn();
            OnEffectCooking(false);
            HandleTurnOffDangerIcon();
            StopSoundStew();
        }
    }
    protected override void HandlePlayerTakeFood(GameObject plateOnPlayer)
    {
        base.HandlePlayerTakeFood(plateOnPlayer);
        OnEffectCooking(false);
    }
    private void OnEffectCooking(bool value)
    {
        if (value)
        {
            effectCooking.SetActive(true);
        }
        else
        {
            effectCooking.SetActive(false); 
        }
    }

    private void HandleWhenCooking()
    {
        isCooking = true;
        PlaySoundStew();
        stoveUI.HandlefryingTimeBar(fryingTimer, timeCooking, active: true);
    }

    private float timeWait = 1.5f + 2f;
    private bool condition; 
    private void HandleWhenCookingDone()
    {
        food.SetState(StateCookedFood.cooked);
        //PauseStew();
        isStew = true; 
        isCooking = false;
        ResumeBurn();
        fryingTimer = 0;

        condition = stoveUI.HandlefryingTimeBar(fryingTimer, timeCooking, active: false);
        
        StartCoroutine(HandleCookingDoneICon(condition));
        StartCoroutine(HandleAfterCooking(timeWait));
    }
    private IEnumerator HandleCookingDoneICon(bool condition)
    {
        yield return new WaitUntil(() => condition);
        if (kitchenObjOnCounter != null) stoveUI.HandleCookingDoneICon();
    }
    private IEnumerator HandleAfterCooking(float timeWait)
    {
        yield return new WaitForSeconds(timeWait);
        if (kitchenObjOnCounter != null) stoveUI.HandleDangerIcon(true);
    }
    private void HandleTurnOffDangerIcon()
    {
        stoveUI.HandleDangerIcon(false);
    }
}
