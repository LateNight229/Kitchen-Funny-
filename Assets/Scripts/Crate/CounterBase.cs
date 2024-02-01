using UnityEngine;

public class CounterBase :  MonoBehaviour, IBarTime
{
    protected Transform handPlayer;
    protected BoxCollider boxCollider;
    protected PlayerController playerController;
    protected ObjectPool pool;

    protected virtual void Start()
    {
        pool = GetComponent<ObjectPool>();   
        playerController = FindObjectOfType<PlayerController>();
        handPlayer = GameObject.Find("hand").transform;
    }
    public virtual void Interact(GameObject kitchenObj, GameObject plate)
    {
    }

    public virtual void PerformOperation()
    {

    }
    public virtual void PlayerRemoveKitchenObj(GameObject kitchenObj)
    {
        playerController.SetCurrentKitchenObj(null);
        kitchenObj.transform.parent = null;
        OnFreezeKitchenObj(kitchenObj,false);
        OnBoxCollider(kitchenObj, true);
    }
    protected virtual void OnFreezeKitchenObj(GameObject kitchenObj,bool value)
    {
        Rigidbody rb = kitchenObj.GetComponent<Rigidbody>();
        if (value)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            rb.constraints = ~RigidbodyConstraints.FreezePosition;
        }
    }

    protected virtual void OnBoxCollider(GameObject kitchenObj,bool value)
    {
        BoxCollider box = kitchenObj.GetComponent<BoxCollider>();
        box.enabled = value;
    }

    public virtual void PauseStew()
    {
    }

    public virtual void ResumeStew()
    {
    }
}
