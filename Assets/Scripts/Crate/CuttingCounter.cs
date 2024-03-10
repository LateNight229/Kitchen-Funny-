using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CuttingCounter : StorageCounter
{
    public Image timeWaitUI;
    public Canvas canvas;
    public GameObject knife;
    public Image boomCutting;

    /*[SerializeField] private*/ public float timeChopping ;

    private ProcessedFood food;
    private float choppingTimer;
    private bool isChopping = false;
    private bool isPaused;

    public void HandleVfxCutting() 
    {
        boomCutting.enabled = true;
        Invoke("OffBoomCutting", 0.1f);
    }
    public void HandleSfxCutting()
    {
        SoundManager.instance.HandlePlaySound("chopingOne", 0.6f);
    }

    private void OffBoomCutting()
    {
        boomCutting.enabled = false;
    }
    public override void PauseStew()
    {
        isPaused = true;
        Debug.Log("isPaused = " + isPaused);
    }
    public override void ResumeStew()
    {
        isPaused = false;
        Debug.Log("isPaused = " + isPaused);
    }

    protected override void Start()
    {   
        base.Start();
        ModifierOffSet();
        PlayerController.EventSpawnBoomCutting += HandleVfxCutting;
        PlayerController.EventSpawnBoomCutting += HandleSfxCutting;
    }
    private void OnDestroy()
    {
        PlayerController.EventSpawnBoomCutting -= HandleVfxCutting;
        PlayerController.EventSpawnBoomCutting -= HandleSfxCutting;
    }
    void ModifierOffSet()
    {
        
        timeWaitUI.fillAmount = 0;
        canvas.enabled = false;
        choppingTimer = 0;
        offSetPutKitchenObjOnCounter = new Vector3 (0, 1.5f, 0);
    }

    public override void Interact(GameObject kitchenObjOnPlayer, GameObject plate)
    {   
        if(plate != null) { return; }
        if(kitchenObjOnPlayer == null )
        {
            if (isChopping) return;
            base.Interact(kitchenObjOnPlayer, plate) ;
        }else 
        {
            if (kitchenObjOnPlayer.TryGetComponent(out ProcessedFood food))
            {
                if (food != null)
                {
                    base.Interact(kitchenObjOnPlayer, plate);
                }
            }
        }
    }
    public override void PerformOperation() { Cutting();}
    private void Cutting()
    {
        //Debug.Log("Join Cutting In Cutting Board!");
        if (kitchenObjOnCounter == null) return;
        food = this.GetProcessedFood();
        if (food.GetProcessedDone()) { return; }
        playerController.animationChopping(true);
        isChopping = true;
    }
    private void Update()
    {
        if (isPaused)
        {
            return;
        }
        if (choppingTimer >= timeChopping / 2f)
        {
            food.SetState(StateProssecedFood.cutting);
        }
        if(choppingTimer >= timeChopping)
        {
            // cutting done!
            food.SetState(StateProssecedFood.cutted);
            choppingTimer = 0;
            isChopping = false; 
            canvas.enabled = false;
            playerController.animationChopping(false);
        }
        if (choppingTimer < timeChopping && isChopping)
        {
            choppingTimer += Time.deltaTime;
            canvas.enabled = true;
            timeWaitUI.fillAmount = choppingTimer / timeChopping;
        }
    }
    private ProcessedFood GetProcessedFood()
    {
       if (kitchenObjOnCounter.TryGetComponent(out ProcessedFood food))
       {
            if(food != null)
            {
                return food;
            }else return null;
       }
       else return null;
    }
    protected override void HandleCounterTakeFood(Vector3 offSet)
    {
        base.HandleCounterTakeFood(offSet);
        knife.SetActive(false);
    }
    protected override void HandlePlayerTakeFood(GameObject plateOnPlayer)
    {
        base.HandlePlayerTakeFood(plateOnPlayer);
        knife.SetActive(true);

    }
}
