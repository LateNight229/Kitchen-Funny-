using UnityEngine;

public class HandleEventOpenLidTrash : MonoBehaviour
{
    public Animator animator;
    private TrashCounter trashCounter;

    private void Start()
    {
        trashCounter = FindFirstObjectByType<TrashCounter>();
    }

    public virtual void SetValueCanTakeFood()
    {
       // trashCounter.SetValueCanTakeFood(value);
    }
}
