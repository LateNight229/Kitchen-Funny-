using System;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class PlayerController : MonoBehaviour
{
    public static event Action<bool> UnSelectedCounter;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isMoving;
    [SerializeField] private float normalSpeed = 7f;
    [SerializeField] private float slowSpeed = 0f;
    [SerializeField] private float rotateSpeed = 300f;
    [SerializeField] private Transform InteracPos;

    [Header("Chopping")]
    [SerializeField] private GameObject knife;

    [Header("Holding")]
    [SerializeField] private bool DropFoodInGround;
    [SerializeField] private GameObject kitchenObjHolding;
    [SerializeField] private GameObject plate;

    [Header("Interact")]
    [SerializeField] private float interacDistance = 1f;
    public GameObject Plate { get => plate; }

    [SerializeField] private LayerMask CounterLayerMask;
    private VariableJoystick joystick;
    private Animator animator;
    private CounterBase currentCounter;
    private PlateCounter plateCounter;
    public CounterBase CurrentCounter { get { return currentCounter; } }
    private Vector3 lastDirection;
    private Vector3 currentPosition;
    private Vector3 PositionCutting;
    
    public GameObject GetCurrentPlate() { return plate;   }
    public void SetCurrentPlate(GameObject plateValue)  {   plate = plateValue;   }
    public CounterBase GetCurrentCounter() {    return currentCounter;    }
    public void SetlastCounter(CounterBase newCounter)   { this.currentCounter = newCounter; }

    public void SetCurrentKitchenObj(GameObject newKitchenObj)   { kitchenObjHolding = newKitchenObj; }
    public GameObject GetCurrentKitchenObj()  { return kitchenObjHolding;   }
    private void Awake()
    {
        joystick = FindObjectOfType<VariableJoystick>();
        animator = GetComponentInChildren<Animator>();
        plateCounter =GameObject.FindObjectOfType<PlateCounter>();
    }
    private void Reset()
    {
        knife = GameObject.Find("knife");
        InteracPos = GameObject.Find("InteracPos").transform;
    }
    private void Update()
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
        // <<nhiem vu tiep theo : tao ray xem raycast chieu nhu nao tren scene ? bug: selected 2 counter >>
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
                    SelectedVisual selected = counter.GetComponentInChildren<SelectedVisual>();
                    if (selected != null)
                    {
                        selected.Selected(true);
                        SetlastCounter(counter);
                    }
                    else { Debug.Log("obj not select"); }
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //Debug.Log("Space + " + counter.ToString());
                    counter.Interact(kitchenObjHolding, plate);
                }
                else if (Input.GetKeyDown(KeyCode.LeftAlt))
                {   
                    PositionCutting = transform.position;
                    currentCounter.ResumeStew();
                    counter.PerformOperation();
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
                    CounterBase counter = FindObjectOfType<CounterBase>();
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
        //animator.SetBool("chop", value);
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
