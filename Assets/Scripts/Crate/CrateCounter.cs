
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class CrateCounter : CounterBase
{
    public override void Interact(GameObject kitchenObj, GameObject plate)
    {
        RemoveKitchenObj(kitchenObj, plate);
    }
    public virtual void RemoveKitchenObj(GameObject kitchenObjOnPlayer, GameObject plate)
    {
        if (kitchenObjOnPlayer != null || plate != null) return; 
        var currentKitchenObj = pool.GetPrefab();
        if(currentKitchenObj != null)
        {
            currentKitchenObj.transform.position = handPlayer.position;
            currentKitchenObj.transform.rotation = handPlayer.rotation;
            currentKitchenObj.transform.parent = handPlayer.parent;

            OnFreezeKitchenObj(currentKitchenObj, true);
            OnBoxCollider(currentKitchenObj, false);

            playerController.SetCurrentKitchenObj(currentKitchenObj);
            playerController.SetlastCounter(this);
        }
    }
}
