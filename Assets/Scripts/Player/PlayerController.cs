using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static event Action<bool> UnSelectedCounter;
    public static event Action EventSpawnBoomCutting;
    public static event Action EventSpawnSoundFx;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isMoving ;
    [SerializeField] private float normalSpeed = 7f, slowSpeed = 0f, rotateSpeed = 300f;
    [SerializeField] private Transform InteracPos;

    [Header("Chopping")]
    [SerializeField] private GameObject knife;

    [Header("Holding")]
    [SerializeField] private bool DropFoodInGround;
    [SerializeField] private GameObject kitchenObjHolding, plate;

    [Header("Interact")]
    [SerializeField] private float interacDistance = 1f;
    public GameObject Plate { get => plate; }

    [SerializeField] private LayerMask CounterLayerMask;
    private DynamicJoystick joystick;
    private Animator animator;
    private CounterBase currentCounter;
    private PlateCounter plateCounter;
    public CounterBase CurrentCounter { get { return currentCounter; } }
    private Vector3 lastDirection;
    private Vector3 currentPosition;
    private Vector3 PositionCutting;
    private SelectedVisual selected;

    public void HandleEventSpawnBoomCutting()
    {
        if(EventSpawnBoomCutting != null)
        {
            EventSpawnBoomCutting();
        }
    }
    public void HandleEventSpawnSoundFx()
    {
        if (EventSpawnSoundFx != null)
        {
            EventSpawnSoundFx();
        }
    }

    public GameObject GetCurrentPlate() { return plate;   }
    public void SetCurrentPlate(GameObject plateValue)  {   plate = plateValue;   }
    public CounterBase GetCurrentCounter() {    return currentCounter;    }
    public void SetlastCounter(CounterBase newCounter)   { this.currentCounter = newCounter; }

    public void SetCurrentKitchenObj(GameObject newKitchenObj)   { kitchenObjHolding = newKitchenObj; }
    public GameObject GetCurrentKitchenObj()  { return kitchenObjHolding;   }
    private void Awake()
    {
        joystick = FindFirstObjectByType<DynamicJoystick>();
        animator = GetComponentInChildren<Animator>();
        plateCounter = FindFirstObjectByType<PlateCounter>();
    }
    protected void Start()
    {
        InputManager.ClickButtonHolding += ButtonHodlingDown;
        InputManager.ClickButtonChopping += ButtonChoppingDown;
    }
    bool clickedHoldingButton;
    bool clickedChoppingButton;
    protected void ButtonHodlingDown()
    {
        clickedHoldingButton = true;
      //  Debug.Log("ButtonHodlingDown = " + clickedHoldingButton);
    }
    protected bool GetButtonHodlingDown()
    {
      //  Debug.Log("GetButtonHodlingDown = ");
        return clickedHoldingButton;
    }
    protected void ButtonChoppingDown()
    {
        clickedChoppingButton = true;
       // Debug.Log("ButtonChoppingDown = " + clickedChoppingButton);
    }
    protected bool GetButtonChoppingDown()
    {
       // Debug.Log("ButtonChoppingDown = ");
        return clickedChoppingButton;
    }
    private void Reset()
    {
        knife = GameObject.Find("knife");
        InteracPos = GameObject.Find("InteracPos").transform;
    }
    protected  void Update()
    {   
        HandleInteraction();
    }
    void FixedUpdate()
    {
        HanldeMovementJoystick();
    }
    void HandleInteraction()
    {

        var direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        if(direction != Vector3.zero)
        {
            lastDirection = direction;
        }

        RaycastHit raycastHit;
        if (Physics.Raycast(InteracPos.position, lastDirection, out raycastHit, interacDistance, CounterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out CounterBase counter))
            {
                if (counter == null )
                {
                    if (UnSelectedCounter != null) UnSelectedCounter(false); return;
                }
                else
                {   
                    if(selected == null)
                    {
                        selected = counter.GetComponentInChildren<SelectedVisual>();
                        if (selected != null)
                        {
                            selected.Selected(true);
                            SetlastCounter(counter);
                        }
                    }
                    else
                    {
                        if (UnSelectedCounter != null) UnSelectedCounter(false);
                        selected = counter.GetComponentInChildren<SelectedVisual>();
                        if (selected != null)
                        {
                            selected.Selected(true);
                            SetlastCounter(counter);
                        }
                    }
                }

                if (Input.GetKeyDown(KeyCode.Space) || GetButtonHodlingDown())
                {
                    SoundManager.instance.HandlePlaySound("taking",volume: 1f);
                    clickedHoldingButton = false;
                    counter.Interact(kitchenObjHolding, plate);
                    PositionCutting = transform.position;
                }
                else if (Input.GetKeyDown(KeyCode.LeftAlt) || GetButtonChoppingDown())
                {
                    clickedChoppingButton = false;
                    PositionCutting = transform.position;
                    currentCounter.ResumeStew();
                    counter.PerformOperation();
                    Debug.Log("ResumeStew");
                }
                if (Vector3.Distance(PositionCutting, transform.position) > 0.1f)
                {
                    currentCounter.PauseStew();
                    Debug.Log("PauseStew");
                }
            }
        }
        else
        {   
            // off visual selected
            if (UnSelectedCounter != null) UnSelectedCounter(false);
            if (Input.GetKeyDown(KeyCode.Space) && DropFoodInGround)
            {
                if (kitchenObjHolding != null)
                {
                    CounterBase counter = FindFirstObjectByType<CounterBase>();
                    currentCounter.PlayerRemoveKitchenObj(kitchenObjHolding);
                }
            }
        }
    }
    void HanldeMovementJoystick()
    {
        currentPosition = transform.position;
        if (joystick != null)
        {
            var direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

            if (direction.magnitude > 0f) //Để tránh xoay khi joystick gần với trung tâm
            {
                isMoving = true;
                Quaternion toRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * rotateSpeed * 10f);
            }
            else
            {
                isMoving= false;
            }
            RaycastHit hit;
            if (Physics.Raycast(InteracPos.position, lastDirection, out hit, 1f, CounterLayerMask))
            {
                moveSpeed = slowSpeed;
            }
            else
            {
                moveSpeed = normalSpeed;
            }
            transform.position += direction.normalized * moveSpeed * Time.deltaTime;
            
            if(direction.magnitude != 0)
            {   
                if( animator != null)
                animationChopping(false);
            }
           
        }

    }
    public  void animationChopping(bool value)
    {   
        animator.SetBool("chop", value);
        knife.SetActive(value);
    }

    public void HandleAfterDelivery()
    {
        if (plate != null)
        {
            plateCounter.HandlePlateAfterDelivery(plate);
            SetCurrentPlate(null);
        }
    }
}
